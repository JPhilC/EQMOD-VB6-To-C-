using Microsoft.VisualBasic;
using System;
using UpgradeHelpers.Helpers;

namespace Project1
{
	internal static class Alignment
	{

		//Attribute VB_Name = "Alignment"
		public const int MAX_STARS = 1000;
		public const int MAX_COMBINATION = 32767;
		public const int MAX_COMBINATION_COUNT = 50;


		public static bool gThreeStarEnable = false;
		public static int gSelectStar = 0;
		public static int gMaxCombinationCount = 0;
		public static int gLoadAPresetOnUnpark = 0;
		public static int gSaveAPresetOnPark = 0;
		public static int gSaveAPresetOnAppend = 0;

		public static int ProximityRa = 0;
		public static int ProximityDec = 0;

		public static double gRA_GOTO = 0;
		public static double gDEC_GOTO = 0;


		[Serializable]
		public struct AlignmentPoint
		{
			public double OrigTargetRA;
			public double OrigTargetDEC;
			public double TargetRA;
			public double TargetDEC;
			public double EncoderRA;
			public double EncoderDEC;
			public System.DateTime AlignTime;
		}

		public enum AlignmentType
		{
			Onestar = 1,
			ThreeStar = 3,
			multistar = 99
		}

		public static int gAlignmentStars_count = 0;

		public static AlignmentPoint[] AlignmentStars = ArraysHelper.InitializeArray<AlignmentPoint>(MAX_STARS + 1);

		public static eqmodvector.Coord[] ct_Points = ArraysHelper.InitializeArray<eqmodvector.Coord>(MAX_STARS); //Catalog Points
		public static eqmodvector.Coord[] my_Points = ArraysHelper.InitializeArray<eqmodvector.Coord>(MAX_STARS); //My Measured Points
		public static eqmodvector.Coord[] ct_PointsC = ArraysHelper.InitializeArray<eqmodvector.Coord>(MAX_STARS); //Catalog Points (Cartesian)
		public static eqmodvector.Coord[] my_PointsC = ArraysHelper.InitializeArray<eqmodvector.Coord>(MAX_STARS); //My Measured Points (Cartesian)



		internal static void EQ_NPointDelete(int Index)
		{
			int i = 0;

			if (Index != gAlignmentStars_count)
			{
				// first or middle element, move elements one spot
				int tempForEndVar = gAlignmentStars_count - 1;
				for (i = Index; i <= tempForEndVar; i++)
				{
					AlignmentStars[i] = AlignmentStars[i + 1];
				}
			}
			gAlignmentStars_count--;

		}

		internal static void CalcPromximityLimits(int range)
		{
			ProximityRa = Convert.ToInt32(range * EQMath.gTot_RA / 360d);
			ProximityDec = Convert.ToInt32(range * EQMath.gTot_DEC / 360d);
		}

		internal static bool EQ_NPointAppend(double RightAscension, double Declination, double pLongitude, int pHemisphere)
		{
			bool result = false;
			object oLangDll = null;
			object HC = null;
			object StarEditform = null;

			double tRa = 0;
			double tPier = 0;

			double DeltaRa = 0;
			double DeltaDec = 0;

			int i = 0;
			int Count = 0;
			bool flipped = false;

			result = true;

			int curalign = gAlignmentStars_count + 1;

			// build alignment record
			int ERa = Common.EQGetMotorValues(0);
			int EDec = Common.EQGetMotorValues(1);
			double vRA = RightAscension;
			double vDEC = Declination;

			// look at current position and detemrine if flipped
			double RA_Hours = EQMath.Get_EncoderHours(EQMath.gRAEncoder_Zero_pos, Convert.ToDouble(ERa), EQMath.gTot_RA, EQMath.gHemisphere);
			if (RA_Hours > 12)
			{
				// Yes we're currently flipped!
				flipped = true;
			}
			else
			{
				flipped = false;
			}

			double tha = EQMath.RangeHA(vRA - EQMath.EQnow_lst(pLongitude * EQMath.DEG_RAD));
			if (tha < 0)
			{
				if (flipped)
				{
					if (EQMath.gHemisphere == 0)
					{
						tPier = 0;
					}
					else
					{
						tPier = 1;
					}
					tRa = vRA;
				}
				else
				{
					if (EQMath.gHemisphere == 0)
					{
						tPier = 1;
					}
					else
					{
						tPier = 0;
					}
					tRa = EQMath.Range24(vRA - 12);
				}
			}
			else
			{
				if (flipped)
				{
					if (EQMath.gHemisphere == 0)
					{
						tPier = 1;
					}
					else
					{
						tPier = 0;
					}
					tRa = EQMath.Range24(vRA - 12);
				}
				else
				{
					if (EQMath.gHemisphere == 0)
					{
						tPier = 0;
					}
					else
					{
						tPier = 1;
					}
					tRa = vRA;
				}
			}

			//Compute for Sync RA/DEC Encoder Values
			AlignmentStars[curalign].OrigTargetDEC = Declination;
			AlignmentStars[curalign].OrigTargetRA = RightAscension;
			AlignmentStars[curalign].TargetRA = EQMath.Get_RAEncoderfromRA(tRa, 0, pLongitude, EQMath.gRAEncoder_Zero_pos, EQMath.gTot_RA, pHemisphere);
			AlignmentStars[curalign].TargetDEC = EQMath.Get_DECEncoderfromDEC(vDEC, tPier, EQMath.gDECEncoder_Zero_pos, EQMath.gTot_DEC, pHemisphere);
			AlignmentStars[curalign].EncoderRA = ERa;
			AlignmentStars[curalign].EncoderDEC = EDec;
			AlignmentStars[curalign].AlignTime = DateTime.Now;

			DeltaRa = AlignmentStars[curalign].TargetRA - AlignmentStars[curalign].EncoderRA; //'  Difference between theoretical position and encode position.
			DeltaDec = AlignmentStars[curalign].TargetDEC - AlignmentStars[curalign].EncoderDEC; //'  Difference between theoretical position and encode position.


			//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.EncoderTimer.Enabled = true;

			if ((Math.Abs(DeltaRa) < EQMath.gEQ_MAXSYNC) && (Math.Abs(DeltaDec) < EQMath.gEQ_MAXSYNC) || Common.gDisableSyncLimit)
			{

				// Use this data also for next sync until a three star is achieved
				EQMath.gRA1Star = DeltaRa;
				EQMath.gDEC1Star = DeltaDec;

				if (curalign < 3)
				{
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message(Conversion.Str(curalign) + " " + Convert.ToString(oLangDll.GetLangString(6009)));
					gAlignmentStars_count++;
				}
				else
				{
					if (curalign == 3)
					{
						gAlignmentStars_count = 3;
						SendtoMatrix();
					}
					else
					{
						// add new point
						Count = 1;
						// copy points to temp array
						int tempForEndVar = curalign - 1;
						for (i = 1; i <= tempForEndVar; i++)
						{
							DeltaRa = Math.Abs(AlignmentStars[i].EncoderRA - ERa);
							DeltaDec = Math.Abs(AlignmentStars[i].EncoderDEC - EDec);
							if (DeltaRa > ProximityRa || DeltaDec > ProximityDec)
							{
								// point is far enough away from the new point - so keep it
								AlignmentStars[Count] = AlignmentStars[i];
								Count++;
							}
							else
							{
								//                        HC.Add_Message ("Old Point too close " & CStr(deltaRA) & " " & CStr(deltadec) & " " & CStr(ProximityDec))
							}
						}

						AlignmentStars[Count] = AlignmentStars[curalign];
						curalign = Count;
						gAlignmentStars_count = curalign;

						SendtoMatrix();

						//UPGRADE_TODO: (1067) Member RefreshDisplay is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						StarEditform.RefreshDisplay = true;

					}
				}
			}
			else
			{
				// sync is too large!
				result = false;
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(6004));
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Target  RA=" + EQMath.FmtSexa(EQMath.gRA, false));
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Sync    RA=" + EQMath.FmtSexa(RightAscension, false));
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Target DEC=" + EQMath.FmtSexa(EQMath.gDec, true));
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Sync   DEC=" + EQMath.FmtSexa(Declination, true));
			}

			if (gSaveAPresetOnAppend == 1)
			{
				// don't write emtpy list!
				if (gAlignmentStars_count > 0)
				{
					//idx = GetPresetIdx
					string tempRefParam = "";
					SaveAlignmentStars(GetPresetIdx(), ref tempRefParam);
				}
			}

			return result;
		}

		internal static void SendtoMatrix()
		{

			int i = 0;

			int tempForEndVar = gAlignmentStars_count;
			for (i = 1; i <= tempForEndVar; i++)
			{
				ct_Points[i - 1].x = AlignmentStars[i].TargetRA;	// Calculated encoder position
				ct_Points[i - 1].Y = AlignmentStars[i].TargetDEC;
				ct_Points[i - 1].z = 1;
				ct_PointsC[i - 1] = eqmodvector.EQ_sp2Cs(ct_Points[i - 1]);
				my_Points[i - 1].x = AlignmentStars[i].EncoderRA;	// Actual encoder position
				my_Points[i - 1].Y = AlignmentStars[i].EncoderDEC;
				my_Points[i - 1].z = 1;
				my_PointsC[i - 1] = eqmodvector.EQ_sp2Cs(my_Points[i - 1]);
			}

			//Activate Matrix here
			ActivateMatrix();

		}

		internal static void ActivateMatrix()
		{
			object HC = null;

			int i = 0;

			// assume false - will set true later if 3 stars active
			gThreeStarEnable = false;

			//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.EncoderTimer.Enabled = false;
			//UPGRADE_TODO: (1067) Member PolarEnable is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToDouble(HC.PolarEnable.Value) == 1)
			{
				if (gAlignmentStars_count >= 3)
				{
					i = eqmodvector.EQ_AssembleMatrix_Taki(0, 0, ct_PointsC[0], ct_PointsC[1], ct_PointsC[2], my_PointsC[0], my_PointsC[1], my_PointsC[2]);
					i = eqmodvector.EQ_AssembleMatrix_Affine(0, 0, my_PointsC[0], my_PointsC[1], my_PointsC[2], ct_PointsC[0], ct_PointsC[1], ct_PointsC[2]);
					gThreeStarEnable = true;
				}
			}
			else
			{
				if (gAlignmentStars_count >= 3)
				{
					i = eqmodvector.EQ_AssembleMatrix_Taki(0, 0, ct_PointsC[0], ct_PointsC[1], ct_PointsC[2], my_PointsC[0], my_PointsC[1], my_PointsC[2]);
					i = eqmodvector.EQ_AssembleMatrix_Affine(0, 0, my_PointsC[0], my_PointsC[1], my_PointsC[2], ct_PointsC[0], ct_PointsC[1], ct_PointsC[2]);
					gThreeStarEnable = true;
				}
			}
			//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.EncoderTimer.Enabled = true;

		}



		//''''''''''''''''''''''''
		// Alignment preset stuff
		//''''''''''''''''''''''''

		internal static void SaveAlignmentStars(int preset, ref string presetName)
		{
			object HC = null;
			int Index = 0;
			string DataStr = "";
			string tmp = "";

			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Alignini = Convert.ToString(HC.oPersist.GetIniPath) + "\\ALIGN.ini";

			string key = "[alignment_preset" + preset.ToString() + "]";

			if (presetName == "")
			{
				// get existing name
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				presetName = Convert.ToString(HC.oPersist.ReadIniValueEx("NAME", key, Alignini));
			}

			// delete existing section
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.DeleteSection(key, Alignini);

			// write new data
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("STAR_COUNT", gAlignmentStars_count.ToString(), key, Alignini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("NAME", presetName, key, Alignini);

			int tempForEndVar = gAlignmentStars_count;
			for (Index = 1; Index <= tempForEndVar; Index++)
			{
				tmp = "Star" + Index.ToString();
				DataStr = DateTimeHelper.ToString(AlignmentStars[Index].AlignTime) + ";" + AlignmentStars[Index].OrigTargetRA.ToString() + ";" + AlignmentStars[Index].OrigTargetDEC.ToString() + ";" + AlignmentStars[Index].TargetRA.ToString() + ";" + AlignmentStars[Index].TargetDEC.ToString() + ";" + AlignmentStars[Index].EncoderRA.ToString() + ";" + AlignmentStars[Index].EncoderDEC.ToString() + ";";
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx(tmp, DataStr, key, Alignini);
			}
		}

		internal static bool LoadAlignmentPreset(int preset)
		{
			object HC = null;
			string tmptxt2 = "";
			string VarStr = "";
			int pos = 0;
			int Index = 0;
			int ValidCount = 0;
			int MaxCount = 0;
			AlignmentPoint NewData = new AlignmentPoint();

			bool ret = false;

			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Alignini = Convert.ToString(HC.oPersist.GetIniPath) + "\\ALIGN.ini";

			string key = "[alignment_preset" + preset.ToString() + "]";

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("STAR_COUNT", key, Alignini));
			if (tmptxt != "")
			{
				MaxCount = Convert.ToInt32(Conversion.Val(tmptxt));
				if (MaxCount > MAX_STARS)
				{
					MaxCount = MAX_STARS;
				}
			}
			else
			{
				MaxCount = 0;
			}

			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (DecodeError)");
			if (MaxCount != 0)
			{
				ValidCount = 0;
				int tempForEndVar = MaxCount;
				for (Index = 1; Index <= tempForEndVar; Index++)
				{
					VarStr = "Star" + Index.ToString();
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx(VarStr, key, Alignini));
					if (tmptxt != "")
					{
						pos = (tmptxt.IndexOf(';') + 1);
						if (pos == 0)
						{
							goto DecodeError;
						}
						tmptxt2 = tmptxt.Substring(0, Math.Min(pos - 1, tmptxt.Length));
						tmptxt = tmptxt.Substring(Math.Max(tmptxt.Length - (Strings.Len(tmptxt) - pos), 0));
						NewData.AlignTime = DateTime.Parse(tmptxt2);

						pos = (tmptxt.IndexOf(';') + 1);
						if (pos == 0)
						{
							goto DecodeError;
						}
						tmptxt2 = tmptxt.Substring(0, Math.Min(pos - 1, tmptxt.Length));
						tmptxt = tmptxt.Substring(Math.Max(tmptxt.Length - (Strings.Len(tmptxt) - pos), 0));
						NewData.OrigTargetRA = Double.Parse(tmptxt2);

						pos = (tmptxt.IndexOf(';') + 1);
						if (pos == 0)
						{
							goto DecodeError;
						}
						tmptxt2 = tmptxt.Substring(0, Math.Min(pos - 1, tmptxt.Length));
						tmptxt = tmptxt.Substring(Math.Max(tmptxt.Length - (Strings.Len(tmptxt) - pos), 0));
						NewData.OrigTargetDEC = Double.Parse(tmptxt2);

						pos = (tmptxt.IndexOf(';') + 1);
						if (pos == 0)
						{
							goto DecodeError;
						}
						tmptxt2 = tmptxt.Substring(0, Math.Min(pos - 1, tmptxt.Length));
						tmptxt = tmptxt.Substring(Math.Max(tmptxt.Length - (Strings.Len(tmptxt) - pos), 0));
						NewData.TargetRA = Double.Parse(tmptxt2);

						pos = (tmptxt.IndexOf(';') + 1);
						if (pos == 0)
						{
							goto DecodeError;
						}
						tmptxt2 = tmptxt.Substring(0, Math.Min(pos - 1, tmptxt.Length));
						tmptxt = tmptxt.Substring(Math.Max(tmptxt.Length - (Strings.Len(tmptxt) - pos), 0));
						NewData.TargetDEC = Double.Parse(tmptxt2);

						pos = (tmptxt.IndexOf(';') + 1);
						if (pos == 0)
						{
							goto DecodeError;
						}
						tmptxt2 = tmptxt.Substring(0, Math.Min(pos - 1, tmptxt.Length));
						tmptxt = tmptxt.Substring(Math.Max(tmptxt.Length - (Strings.Len(tmptxt) - pos), 0));
						NewData.EncoderRA = Double.Parse(tmptxt2);

						pos = (tmptxt.IndexOf(';') + 1);
						if (pos == 0)
						{
							goto DecodeError;
						}
						tmptxt2 = tmptxt.Substring(0, Math.Min(pos - 1, tmptxt.Length));
						NewData.EncoderDEC = Double.Parse(tmptxt2);

						// all data read ok - copy to alignment stars
						AlignmentStars[Index] = NewData;
						ValidCount++;
					}
					else
					{
						goto DecodeError;
					}
				}

				DecodeError:
				//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
				try
				{
					gAlignmentStars_count = ValidCount;

					// send to matrix will initialise the catalog and measured points arrays
					SendtoMatrix();

					ret = true;
				}
				catch (Exception exc)
				{
					NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
				}
			}

			return ret;
		}

		internal static void SavePresetIdx(int idx)
		{
			object HC = null;

			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Alignini = Convert.ToString(HC.oPersist.GetIniPath) + "\\ALIGN.ini";
			string tmptxt = idx.ToString();
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("active_preset", tmptxt, "[default]", Alignini);

		}

		internal static int GetPresetIdx()
		{
			int result = 0;
			object HC = null;

			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Alignini = Convert.ToString(HC.oPersist.GetIniPath) + "\\ALIGN.ini";
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("active_preset", "[default]", Alignini));

			if (tmptxt == "")
			{
				// ini file entry doesn't exist so create one
				tmptxt = "0";
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("active_preset", tmptxt, "[default]", Alignini);
			}

			result = Convert.ToInt32(Conversion.Val(tmptxt));

			if (result > 10)
			{
				result = 0;
			}

			return result;
		}

		internal static void ReadParkOptions()
		{
			object HC = null;

			string keyStr = "[default]";
			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Alignini = Convert.ToString(HC.oPersist.GetIniPath) + "\\ALIGN.ini";
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("LOAD_APRESET_ON_UNPARK", keyStr, Alignini));
			if (tmptxt == "")
			{
				// create a preset place holder
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("LOAD_APRESET_ON_UNPARK", "0", keyStr, Alignini);
				gLoadAPresetOnUnpark = 0;
			}
			else
			{
				gLoadAPresetOnUnpark = Convert.ToInt32(Conversion.Val(tmptxt));
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("SAVE_APRESET_ON_UNPARK", keyStr, Alignini));
			if (tmptxt == "")
			{
				// create a preset place holder
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("SAVE_APRESET_ON_UNPARK", "0", keyStr, Alignini);
				gSaveAPresetOnPark = 0;
			}
			else
			{
				gSaveAPresetOnPark = Convert.ToInt32(Conversion.Val(tmptxt));
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("SAVE_APRESET_ON_APPEND", keyStr, Alignini));
			if (tmptxt == "")
			{
				// create a preset place holder
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("SAVE_APRESET_ON_APPEND", "0", keyStr, Alignini);
				gSaveAPresetOnAppend = 0;
			}
			else
			{
				gSaveAPresetOnAppend = Convert.ToInt32(Conversion.Val(tmptxt));
			}
		}
		internal static void WriteParkOptions()
		{
			object HC = null;

			string keyStr = "[default]";
			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Alignini = Convert.ToString(HC.oPersist.GetIniPath) + "\\ALIGN.ini";
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("LOAD_APRESET_ON_UNPARK", gLoadAPresetOnUnpark.ToString(), keyStr, Alignini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("SAVE_APRESET_ON_UNPARK", gSaveAPresetOnPark.ToString(), keyStr, Alignini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("SAVE_APRESET_ON_APPEND", gSaveAPresetOnAppend.ToString(), keyStr, Alignini);
		}

		internal static void AligmentStarsPark()
		{
			int idx = 0;
			ReadParkOptions();
			if (gSaveAPresetOnPark == 1)
			{
				// don't write emtpy list!
				if (gAlignmentStars_count > 0)
				{
					idx = GetPresetIdx();
					string tempRefParam = "";
					SaveAlignmentStars(idx, ref tempRefParam);
				}
			}
		}
		internal static void AlignmentStarsUnpark()
		{
			int idx = 0;
			ReadParkOptions();
			// if load on unpark selected
			if (gLoadAPresetOnUnpark == 1)
			{
				// read curent preset index from ini file
				idx = GetPresetIdx();
				// load the preset data
				LoadAlignmentPreset(idx);
			}
		}
	}
}