using Microsoft.VisualBasic;
using System;
using UpgradeHelpers.Helpers;

namespace Project1
{
	internal static class Limits
	{


		[Serializable]
		public struct LIMIT
		{
			public double Alt;
			public double Az;
			public double ha;
			public double DEC;
		}

		[Serializable]
		public struct TLIMIT_STATUS
		{
			public bool LimitDetected;
			public bool AtLimit;
			public bool Horizon;
			public bool RA;
		}

		public static TLIMIT_STATUS LimitStatus = new TLIMIT_STATUS();
		public static LIMIT[] LimitArray = null; // used for file I/O
		public static LIMIT[] LimitArray2 = ArraysHelper.InitializeArray<LIMIT>(361); // constructed from LimitArray to allow speedy indexing by azimuth.
		public static int gHorizonAlgorithm = 0;
		public static int gLimitSlews = 0;
		public static int gLimitPark = 0;
		public static bool gAutoFlipAllowed = false;
		public static bool gAutoFlipEnabled = false;
		public static bool gSupressHorizonLimits = false;
		private static int AutoFlipState = 0;

		internal static void Limits_Init()
		{
			object HC = null;

			LimitStatus.Horizon = false;
			LimitStatus.RA = false;
			LimitStatus.LimitDetected = false;

			gSupressHorizonLimits = false;

			LimitArray = ArraysHelper.InitializeArray<LIMIT>(1);
			Limits_BuildLimitDef();

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string str = Convert.ToString(HC.oPersist.ReadIniValue("LIMIT_ENABLE"));
			if (str != "")
			{
				//UPGRADE_TODO: (1067) Member ChkEnableLimits is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.ChkEnableLimits.Value = Conversion.Val(str);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member ChkEnableLimits is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.ChkEnableLimits.Value = 1;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			str = Convert.ToString(HC.oPersist.ReadIniValue("LIMIT_FILE"));
			if (str != "")
			{
				// got a file to load
				Limits_ReadFile(str);
			}
			else
			{
				// no file assined - set defaults?
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			str = Convert.ToString(HC.oPersist.ReadIniValue("LIMIT_HORIZON_ALGORITHM"));
			if (str != "")
			{
				gHorizonAlgorithm = Convert.ToInt32(Conversion.Val(str));
			}
			else
			{
				// default to interpolated
				gHorizonAlgorithm = 0;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("LIMIT_HORIZON_ALGORITHM", "0");
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			str = Convert.ToString(HC.oPersist.ReadIniValue("LIMIT_PARK"));
			if (str != "")
			{
				gLimitPark = Convert.ToInt32(Conversion.Val(str));
			}
			else
			{
				// default to interpolated
				gLimitPark = 0;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("LIMIT_PARK", "0");
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			str = Convert.ToString(HC.oPersist.ReadIniValue("LIMIT_SLEWS"));
			if (str != "")
			{
				gLimitSlews = Convert.ToInt32(Conversion.Val(str));
			}
			else
			{
				// default to interpolated
				gLimitSlews = 1;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("LIMIT_SLEWS", "1");
			}

			Common.readAutoFlipData();
			AutoFlipState = 0;

		}
		internal static void Limits_Load()
		{
			object FileDlg = null;
			object HC = null;
			//UPGRADE_TODO: (1067) Member filter is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.filter = "*.txt";
			//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.Show(1);
			//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			Limits_ReadFile(Convert.ToString(FileDlg.FileName));
			//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("LIMIT_FILE", FileDlg.FileName);
		}

		internal static void Limits_ReadFile(string FileName)
		{
			object[, , , , ] aa_hadec = null;
			object HC = null;
			int pos = 0;
			int size = 0;
			int redimcount = 0;
			string temp1 = "";
			string temp2 = "";
			double ha = 0;
			double DEC = 0;

			LimitArray = ArraysHelper.InitializeArray<LIMIT>(1);
			try
			{

				if (FileName != "")
				{
					FileSystem.FileClose(1);
					FileSystem.FileOpen(1, FileName, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
					LimitArray = ArraysHelper.InitializeArray<LIMIT>(101);
					size = 0;
					redimcount = 0;
					while (!FileSystem.EOF(1))
					{
						temp1 = FileSystem.LineInput(1);
						temp2 = temp1.Substring(0, Math.Min(1, temp1.Length));
						if (temp2 != "#" && temp2 != " ")
						{
							pos = (temp1.IndexOf(' ') + 1);
							if (pos != 0)
							{
								temp2 = temp1.Substring(0, Math.Min(pos - 1, temp1.Length));
								temp1 = temp1.Substring(Math.Max(temp1.Length - (Strings.Len(temp1) - pos), 0));
								LimitArray[size].Az = Double.Parse(temp2);
								LimitArray[size].Alt = Double.Parse(temp1);
								object tempAuxVar = aa_hadec[Convert.ToInt32(EQMath.gLatitude * EQMath.DEG_RAD), Convert.ToInt32(LimitArray[size].Alt * EQMath.DEG_RAD), Convert.ToInt32(LimitArray[size].Az * EQMath.DEG_RAD), Convert.ToInt32(ha), Convert.ToInt32(DEC)];
								LimitArray[size].ha = EQMath.Range24(ha * EQMath.RAD_HRS);
								LimitArray[size].DEC = DEC * EQMath.RAD_DEG;
								size++;
								redimcount++;
								if (redimcount > 90)
								{
									redimcount = 0;
									LimitArray = ArraysHelper.RedimPreserve(LimitArray, new int[]{size + 101});
								}
							}
						}
					}
					LimitArray = ArraysHelper.RedimPreserve(LimitArray, new int[]{size + 1});
				}
				Limits_BuildLimitDef();
			}
			catch
			{

				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Error reading limits file");
			}
			finally
			{
				FileSystem.FileClose(1);
			}
		}

		internal static void Limits_Save()
		{
			object FileDlg = null;
			object HC = null;
			int i = 0;
			int size = 0;
			string FileName = "";

			try
			{

				size = LimitArray.GetUpperBound(0);

				//UPGRADE_TODO: (1067) Member filter is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileDlg.filter = "*.txt";
				//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileDlg.Show(1);
				//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileName = Convert.ToString(FileDlg.FileName);
				//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToString(FileDlg.FileName) != "")
				{

					// force a .txt extension
					i = (FileName.IndexOf('.') + 1);
					if (i != 0)
					{
						FileName = FileName.Substring(0, Math.Min(i - 1, FileName.Length));
					}
					FileName = FileName + ".txt";

					FileSystem.FileClose(1);
					FileSystem.FileOpen(1, FileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

					int tempForEndVar = size - 1;
					for (i = 0; i <= tempForEndVar; i++)
					{
						FileSystem.PrintLine(1, Convert.ToInt32(LimitArray[i].Az).ToString() + " " + LimitArray[i].Alt.ToString());
					}
					FileSystem.FileClose(1);
				}
			}
			catch
			{

				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Error writing limits file");
			}

		}
		internal static void Limits_Add(ref LIMIT lim)
		{
			int i = 0;
			int size = 0;
			int j = 0;

			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (endsub)");

			size = LimitArray.GetUpperBound(0);

			lim.Az = Convert.ToInt32(lim.Az);

			i = 0;
			while (i < size)
			{
				if (LimitArray[i].Az > lim.Az)
				{
					goto insert;
				}
				else
				{
					if (LimitArray[i].Az == lim.Az)
					{
						LimitArray[i].Alt = lim.Alt;
						goto endsub;
					}
				}
				i++;
			}
			goto Store;
			insert:
			int tempForEndVar = i + 1;
			for (j = size; j >= tempForEndVar; j--)
			{
				LimitArray[j] = LimitArray[j - 1];
			}
			Store:
			LimitArray[i] = lim;
			LimitArray = ArraysHelper.RedimPreserve(LimitArray, new int[]{size + 2});
			Limits_BuildLimitDef();

			endsub:;
		}
		internal static void Limits_DeleteIdx(int idx)
		{
			int i = 0;
			int size = 0;
			try
			{
				if (idx >= 0)
				{
					size = LimitArray.GetUpperBound(0);
					int tempForEndVar = size - 2;
					for (i = idx; i <= tempForEndVar; i++)
					{
						LimitArray[i].Alt = LimitArray[i + 1].Alt;
						LimitArray[i].Az = LimitArray[i + 1].Az;
					}
					LimitArray = ArraysHelper.RedimPreserve(LimitArray, new int[]{size});
					Limits_BuildLimitDef();
				}
			}
			catch
			{
			}

		}


		internal static void Limits_Execute()
		{
			object oLangDll = null;
			object[] emergency_stop = null;
			object HC = null;

			LimitStatus.Horizon = false;
			LimitStatus.RA = false;
			LimitStatus.LimitDetected = false;

			//UPGRADE_TODO: (1067) Member ChkEnableLimits is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToDouble(HC.ChkEnableLimits.Value) == 1)
			{
				if (EQMath.gEQparkstatus == 0)
				{
					if ((EQMath.gSlewStatus && gLimitSlews == 1) || (!EQMath.gSlewStatus && EQMath.gTrackingStatus > 0))
					{
						LimitStatus = Limits_Detect();
						if (Limits_Detect().LimitDetected)
						{
							LimitStatus.AtLimit = true;
							object[] tempAuxVar = emergency_stop;
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.Add_Message(oLangDll.GetLangString(5017));
							if (gLimitPark != 0)
							{
								// park using currently selected park mode.
								//UPGRADE_TODO: (1067) Member ApplyParkMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.ApplyParkMode();
							}
						}
						else
						{
							LimitStatus.AtLimit = false;
						}
					}
					else
					{
						if (LimitStatus.AtLimit)
						{
							// Currently in the limit state so look for clear.
							LimitStatus = Limits_Detect();
							LimitStatus.AtLimit = Limits_Detect().LimitDetected;
						}
					}
				}
				else
				{
					//If unparking, parking or parked, limits don't apply
					LimitStatus.AtLimit = false;
				}
			}
			else
			{
				//limits not enabled so we can't be at the limit can we!
				LimitStatus.AtLimit = false;
			}
		}

		private static TLIMIT_STATUS Limits_Detect()
		{
			TLIMIT_STATUS result = new TLIMIT_STATUS();
			object oLangDll = null;
			object[] EQ_Beep = null;
			object HC = null;

			double Alt = 0;
			bool LimitDetected = false;

			result.LimitDetected = false;
			result.Horizon = false;
			result.RA = false;

			// Routine to handle RA LIMIT processing
			if ((EQMath.gRA_Limit_East != 0) && (EQMath.gEmulRA < EQMath.gRAEncoder_Zero_pos))
			{
				if (EQMath.gEmulRA < EQMath.gRA_Limit_East)
				{
					if (gAutoFlipEnabled)
					{
						switch(AutoFlipState)
						{
							case 0 : 
								//we've hit the RA limit so initiate autoflip! 
								Goto.gTargetRA = EQMath.gRA; 
								Goto.gTargetDec = EQMath.gDec; 
								//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								HC.Add_Message("CoordSlew: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + " ] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]"); 
								Goto.gSlewCount = Goto.gMaxSlewCount;  //NUM_SLEW_RETRIES               'Set initial iterative slew count 
								object tempAuxVar = EQ_Beep[2]; 
								Goto.radecAsyncSlew(Goto.gGotoRate); 
								AutoFlipState = 1; 
								break;
							default:
								break;
						}
					}
					else
					{
						result.RA = true;
					}
					goto endsub;
				}
				else
				{
					AutoFlipState = 0;
				}
			}

			if ((EQMath.gRA_Limit_West != 0) && (EQMath.gEmulRA > EQMath.gRAEncoder_Zero_pos))
			{
				if (EQMath.gEmulRA > EQMath.gRA_Limit_West)
				{
					if (gAutoFlipEnabled)
					{
						switch(AutoFlipState)
						{
							case 0 : 
								//we've hit the RA limit so initiate autoflip! 
								Goto.gTargetRA = EQMath.gRA; 
								Goto.gTargetDec = EQMath.gDec; 
								//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								HC.Add_Message("CoordSlew: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + " ] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]"); 
								Goto.gSlewCount = Goto.gMaxSlewCount;  //NUM_SLEW_RETRIES               'Set initial iterative slew count 
								object tempAuxVar2 = EQ_Beep[2]; 
								Goto.radecAsyncSlew(Goto.gGotoRate); 
								AutoFlipState = 1; 
								break;
							default:
								break;
						}
					}
					else
					{
						result.RA = true;
					}
					goto endsub;
				}
				else
				{
					AutoFlipState = 0;
				}
			}

			endsub:
			// get altitude limit for current azimuth
			if (!gSupressHorizonLimits)
			{
				if (EQMath.gAlt <= LimitArray2[Convert.ToInt32(EQMath.gAz)].Alt)
				{
					result.Horizon = true;
				}
			}
			else
			{
				result.Horizon = false;
			}


			result.LimitDetected = result.Horizon || result.RA;
			return result;
		}

		internal static double Limits_GetAltLimit(double Az)
		{
			double result = 0;
			int i = 0;
			int size = 0;
			int a = 0;
			int b = 0;
			double dalt = 0;
			double daz = 0;

			// default to absolute horizon

			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (endsub)");

			size = LimitArray.GetUpperBound(0);


			switch(size)
			{
				case 0 : 
					result = 0; 
					 
					break;
				case 1 : 
					result = LimitArray[0].Alt; 
					 
					break;
				default:
					if (size > 0)
					{

						//                If size = 1 Then
						//                    ' only one limit
						//                    Limits_GetAltLimit = LimitArray(0).alt
						//                    GoTo endsub
						//               End If

						a = 0;
						int tempForEndVar = size - 1;
						for (i = 0; i <= tempForEndVar; i++)
						{
							if (LimitArray[i].Az > Az)
							{
								a = i;
								goto found;
							}
						}
						found:
						if (a == 0)
						{
							b = size - 1;
						}
						else
						{
							b = a - 1;
						}

						switch(gHorizonAlgorithm)
						{
							case 0 : 
								// interpolated between two points 
								dalt = LimitArray[a].Alt - LimitArray[b].Alt; 
								if (LimitArray[a].Az > LimitArray[b].Az)
								{
									daz = LimitArray[a].Az - LimitArray[b].Az;
								}
								else
								{
									daz = (360 - LimitArray[b].Az) + LimitArray[a].Az;
								} 
								 
								if (daz == 0)
								{
									// two points with the same azimuth so take the lowest altitude
									if (LimitArray[a].Alt > LimitArray[b].Alt)
									{
										result = LimitArray[a].Alt;
									}
									else
									{
										result = LimitArray[b].Alt;
									}
								}
								else
								{
									if (a == 0)
									{
										if (Az < LimitArray[a].Az)
										{
											result = LimitArray[b].Alt + (dalt / daz * (359 - LimitArray[b].Az + Az));
										}
										else
										{
											result = LimitArray[b].Alt + (dalt / daz * (Az - LimitArray[b].Az));
										}
									}
									else
									{
										result = LimitArray[b].Alt + ((Az - LimitArray[b].Az) * dalt / daz);
									}
								} 
								 
								break;
							case 1 : 
								// higher value of two points 
								if (LimitArray[a].Alt > LimitArray[b].Alt)
								{
									result = LimitArray[a].Alt;
								}
								else
								{
									result = LimitArray[b].Alt;
								} 
								 
								break;
						}

					} 
					break;
			}

			endsub:
			return result;
		}

		internal static void Limits_Clear()
		{
			object HC = null;
			LimitArray = ArraysHelper.InitializeArray<LIMIT>(1);
			Limits_BuildLimitDef();
			// remove reference to limits file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("LIMIT_FILE", "");

		}

		internal static void Limits_edit()
		{
			object LimitEditForm = null;
			//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			LimitEditForm.Show(0);
		}

		internal static void Limits_BuildLimitDef()
		{
			object[, , , , ] aa_hadec = null;
			int idx = 0;
			double ha = 0;
			double DEC = 0;

			// Because of the amount of maths involved to determine current limits we
			// maintain two arrays. LimitArray is a 'sparse' array used to file storage.
			// From this we construct LimitArray2 which holds limits for every degree of azimuth.
			// Limit and display code can therefore quickly access limits by using the current
			// azimuth as an index into LimitArray(2)

			for (idx = 0; idx <= 359; idx++)
			{
				LimitArray2[idx].Alt = Limits_GetAltLimit(Convert.ToDouble(idx));
				LimitArray2[idx].Az = idx;
				object tempAuxVar = aa_hadec[Convert.ToInt32(EQMath.gLatitude * EQMath.DEG_RAD), Convert.ToInt32(LimitArray2[idx].Alt * EQMath.DEG_RAD), Convert.ToInt32(LimitArray2[idx].Az * EQMath.DEG_RAD), Convert.ToInt32(ha), Convert.ToInt32(DEC)];
				LimitArray2[idx].ha = EQMath.Range24(ha * EQMath.RAD_HRS);
				LimitArray2[idx].DEC = DEC * EQMath.RAD_DEG;
			}
		}


		internal static double Limits_TimeToHorizon()
		{
			double result = 0;
			int i = 0;
			double ha = 0;
			double tmp = 0;
			// Establish the time the scaope will take, at sidereal rate, to reach the horizon

			// -1 indicates never reaches horizon
			result = -1;

			try
			{

				if (EQMath.gHemisphere == 0)
				{
					// only consider western horizon (stars just don't set in the east!)
					for (i = 180; i <= 359; i++)
					{
						// search for the point where the horizon declination is greater or equal to our scope declination
						ha = LimitArray2[i].ha;
						tmp = LimitArray2[i].DEC;


						if (tmp >= EQMath.gDec)
						{
							// calulate difference between horizon hour angle and scope hour angle

							tmp = ha - EQMath.Range24(EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD) - EQMath.gRA);
							if (tmp < 0)
							{
								tmp = 24 + tmp;
							}
							result = tmp;

							throw new Exception();
						}
					}

				}
				else
				{

					// only consider western horizon (stars just don't set in the east!)
					for (i = 180; i <= 359; i++)
					{
						// search for the point where the horizon declination is greater or equal to our scope declination
						ha = LimitArray2[i].ha;
						tmp = LimitArray2[i].DEC;


						if (tmp >= EQMath.gDec)
						{
							// calulate difference between horizon hour angle and scope hour angle

							tmp = ha - EQMath.Range24(EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD) - EQMath.gRA);
							if (tmp < 0)
							{
								tmp = 24 + tmp;
							}
							result = tmp;

							throw new Exception();
						}
					}


				}
			}
			catch
			{
			}




			return result;
		}

		internal static double Limits_TimeToMeridian()
		{
			double result = 0;
			double Steps = 0;
			double rate = 0;

			// Establish the time the scope will take, at sidereal rate, to reach the Meridian limit

			// -1 indicates never reaches horizon
			result = -1;

			try
			{

				if (EQMath.gHemisphere == 0)
				{
					if (EQMath.gRA_Limit_West != 0)
					{
						if (EQMath.gEmulRA < EQMath.gRA_Limit_West)
						{
							Steps = EQMath.gRA_Limit_West - EQMath.gEmulRA;
							// sidereal rate as steps hour
							rate = 3600 * EQMath.gTot_RA / 86164.0905d;
							result = Steps / rate;
						}
					}
				}
				else
				{
					if (EQMath.gRA_Limit_East != 0)
					{
						if (EQMath.gEmulRA > EQMath.gRA_Limit_East)
						{
							Steps = EQMath.gEmulRA - EQMath.gRA_Limit_East;
							// sidereal rate as steps hour
							rate = 3600 * EQMath.gTot_RA / 86164.0905d;
							result = Steps / rate;
						}
					}
				}
			}
			catch
			{
			}


			return result;
		}

		internal static void SetRaLimitDefaults()
		{

			// make up some defaults
			double tmp = 90.88d * EQMath.gTot_step / 360d;
			EQMath.gRA_Limit_East = EQMath.gRAEncoder_Zero_pos - Convert.ToInt32(tmp); // homepos - 90.88degrees of step
			EQMath.gRA_Limit_West = EQMath.gRAEncoder_Zero_pos + Convert.ToInt32(tmp); // homepos + 90.88degrees of step

		}

		internal static void writeRAlimit()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("RA_LIMIT_EAST", EQMath.gRA_Limit_East.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("RA_LIMIT_WEST", EQMath.gRA_Limit_West.ToString());

		}

		internal static void readRALimit()
		{
			object HC = null;


			SetRaLimitDefaults();

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("RA_LIMIT_EAST"));
			if (tmptxt != "")
			{
				EQMath.gRA_Limit_East = Conversion.Val(tmptxt);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("RA_LIMIT_EAST", EQMath.gRA_Limit_East.ToString());
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("RA_LIMIT_WEST"));
			if (tmptxt != "")
			{
				EQMath.gRA_Limit_West = Conversion.Val(tmptxt);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("RA_LIMIT_WEST", EQMath.gRA_Limit_West.ToString());
			}

			if (EQMath.gRA_Limit_West == EQMath.gRA_Limit_East && EQMath.gRA_Limit_West != 0)
			{
				SetRaLimitDefaults();
				writeRAlimit();
			}
		}
		internal static bool OutOfBounds(double pos)
		{
			bool result = false;
			object HC = null;


			//UPGRADE_TODO: (1067) Member ChkEnableLimits is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToDouble(HC.ChkEnableLimits.Value) == 0)
			{
				// no limits
				return true;
			}

			// Routine to handle RA LIMIT processing
			if ((EQMath.gRA_Limit_East != 0) && (pos < EQMath.gRAEncoder_Zero_pos))
			{
				if (pos < EQMath.gRA_Limit_East)
				{
					return true;
				}
			}

			if ((EQMath.gRA_Limit_West != 0) && (pos > EQMath.gRAEncoder_Zero_pos))
			{
				if (pos > EQMath.gRA_Limit_West)
				{
					result = true;
				}
			}

			return result;
		}

		internal static bool RALimitsActive()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member ChkEnableLimits is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToDouble(HC.ChkEnableLimits.Value) == 0)
			{
				return false;
			}
			else
			{
				if (EQMath.gRA_Limit_West == 0 || EQMath.gRA_Limit_West == 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
	}
}