using Microsoft.VisualBasic;
using System;
using UpgradeHelpers.Helpers;

namespace Project1
{
	internal static class Goto
	{

		//Attribute VB_Name = "Goto"
		//Option Explicit

		[Serializable]
		public struct GOTO_PARAMS
		{
			public double RA_currentencoder;
			public short RA_Direction;
			public double RA_targetencoder;
			public short RA_SlewActive;
			public double DEC_currentencoder;
			public short DEC_Direction;
			public double DEC_targetencoder;
			public short DEC_SlewActive;
			public short rate;
			public short SuperSafeMode;
		}

		public static GOTO_PARAMS gGotoParams = new GOTO_PARAMS();
		public static int gGotoRate = 0;
		public static int gDisbleFlipGotoReset = 0;
		public static bool gCWUP = false;
		public static int gMaxSlewCount = 0;
		public static int gSlewCount = 0;
		public static int gFRSlewCount = 0;
		public static int gGotoResolution = 0;
		public static double gTargetRA = 0;
		public static double gTargetDec = 0;
		public static double gRAGotoRes = 0; // Iterative Slew minimum difference in arcsecs
		public static double gDECGotoRes = 0; // Iterative Slew minimum difference in arcsecs
		public static int gRA_Compensate = 0; // Least RA discrepancy Compensation
		public static double gRAMeridianWest = 0;
		public static double gRAMeridianEast = 0;


		//Routine to Slew the mount to target location
		internal static void radecAsyncSlew(int GotoRate)
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.EncoderTimer.Enabled = false;
			CalcEncoderTargets();
			gGotoParams.rate = (short) GotoRate;
			if (gCWUP)
			{
				Limits.gSupressHorizonLimits = true;
				// a counterweights up slew has been requested
				if (!Limits.RALimitsActive())
				{
					// Limits are off so play safe and slew RA and DEC independently
					if (EQMath.gRA_Hours > 12)
					{
						// we're currently in a counterweights up position
						if (gGotoParams.RA_currentencoder > EQMath.RAEncoder_Home_pos)
						{
							// single axis slew to nearest limit position
							// followed by dual axis slew to target limit
							// followed by single axis slew to target ra
							gGotoParams.SuperSafeMode = 3;
							StartSlew(gRAMeridianWest, gGotoParams.DEC_currentencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
						else
						{
							// single axis slew to nearest limit position
							// followed by dual axis slew to target limit
							// followed by single axis slew to target ra
							gGotoParams.SuperSafeMode = 3;
							StartSlew(gRAMeridianEast, gGotoParams.DEC_currentencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
					}
					else
					{
						// we're currently in a counterweights down position
						if (gGotoParams.RA_targetencoder > EQMath.RAEncoder_Home_pos)
						{
							// dual axis slew to limit position followed by ra only slew to target
							gGotoParams.SuperSafeMode = 1;
							StartSlew(gRAMeridianWest, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
						else
						{
							// dual axis slew to limit position followed by ra only slew to target
							gGotoParams.SuperSafeMode = 1;
							StartSlew(gRAMeridianEast, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
					}
				}
				else
				{
					// Limits are active so allow simulatenous RA/DEC movement
					gGotoParams.SuperSafeMode = 0;
					StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
				}
			}
			else
			{
				// we're currently in a counterweights up position
				if (!Limits.RALimitsActive())
				{
					// Limits are off
					if (gGotoParams.RA_currentencoder > gRAMeridianWest)
					{
						//Slew in RA to limit position - then complete move as dual axis slew
						gGotoParams.SuperSafeMode = 1;
						Limits.gSupressHorizonLimits = true;
						StartSlew(gRAMeridianWest, gGotoParams.DEC_currentencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
					}
					else
					{
						if (gGotoParams.RA_currentencoder < gRAMeridianEast)
						{
							//Slew in RA to limit position - then complete move as dual axis slew
							gGotoParams.SuperSafeMode = 1;
							Limits.gSupressHorizonLimits = true;
							StartSlew(gRAMeridianEast, gGotoParams.DEC_currentencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
						else
						{
							// standard slew - simulatanous RA and DEc movement
							gGotoParams.SuperSafeMode = 0;
							StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
					}
				}
				else
				{
					// Limits are enabled
					if (gGotoParams.RA_currentencoder > EQMath.gRA_Limit_West)
					{
						//Slew in RA to limit position - then complete move as dual axis slew
						gGotoParams.SuperSafeMode = 1;
						Limits.gSupressHorizonLimits = true;
						StartSlew(EQMath.gRA_Limit_West, gGotoParams.DEC_currentencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
					}
					else
					{
						if (gGotoParams.RA_currentencoder < EQMath.gRA_Limit_East)
						{
							//Slew in RA to limit position - then complete move as dual axis slew
							gGotoParams.SuperSafeMode = 1;
							Limits.gSupressHorizonLimits = true;
							StartSlew(EQMath.gRA_Limit_East, gGotoParams.DEC_currentencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
						else
						{
							// standard slew - simulatanous RA and DEc movement
							gGotoParams.SuperSafeMode = 0;
							StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
						}
					}
				}
			}
			//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.EncoderTimer.Enabled = true;

		}

		internal static void CalcEncoderTargets()
		{
			object HC = null;
			double targetRAEncoder = 0;
			double targetDECEncoder = 0;
			double currentRAEncoder = 0;
			double currentDECEncoder = 0;
			eqmodvector.Coordt tmpcoord = new eqmodvector.Coordt();
			double tRa = 0;
			double tha = 0;
			double tPier = 0;

			try
			{

				EQMath.gSlewStatus = false;

				//stop the motors
				PEC.PEC_StopTracking();
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(2);
				//    eqres = EQ_MotorStop(0)
				//    eqres = EQ_MotorStop(1)

				//    'Wait for motor stop , Need to add timeout routines here
				//    Do
				//        eqres = EQ_GetMotorStatus(0)
				//        If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then GoTo SL01
				//    Loop While (eqres And EQ_MOTORBUSY) <> 0
				//
				//SL01:
				//    Do
				//        eqres = EQ_GetMotorStatus(1)
				//        If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then GoTo SL02
				//    Loop While (eqres And EQ_MOTORBUSY) <> 0
				//
				//SL02:

				// read current
				currentRAEncoder = Common.EQGetMotorValues(0);
				currentDECEncoder = Common.EQGetMotorValues(1);

				tha = EQMath.RangeHA(gTargetRA - EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD));
				if (tha < 0)
				{
					if (gCWUP)
					{
						if (EQMath.gHemisphere == 0)
						{
							tPier = 0;
						}
						else
						{
							tPier = 1;
						}
						tRa = gTargetRA;
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
						tRa = EQMath.Range24(gTargetRA - 12);
					}
				}
				else
				{
					if (gCWUP)
					{
						if (EQMath.gHemisphere == 0)
						{
							tPier = 1;
						}
						else
						{
							tPier = 0;
						}
						tRa = EQMath.Range24(gTargetRA - 12);
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
						tRa = gTargetRA;
					}
				}

				//Compute for Target RA/DEC Encoder
				targetRAEncoder = EQMath.Get_RAEncoderfromRA(tRa, 0, EQMath.gLongitude, EQMath.gRAEncoder_Zero_pos, EQMath.gTot_RA, EQMath.gHemisphere);
				targetDECEncoder = EQMath.Get_DECEncoderfromDEC(gTargetDec, tPier, EQMath.gDECEncoder_Zero_pos, EQMath.gTot_DEC, EQMath.gHemisphere);

				if (gCWUP)
				{
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message("Goto: CW-UP slew requested");
					// if RA limits are active
					//UPGRADE_TODO: (1067) Member ChkEnableLimits is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToDouble(HC.ChkEnableLimits.value) == 1 && EQMath.gRA_Limit_East != 0 && EQMath.gRA_Limit_West != 0)
					{
						// check that the target position is within limits
						if (EQMath.gHemisphere == 0)
						{
							if (targetRAEncoder < EQMath.gRA_Limit_East || targetRAEncoder > EQMath.gRA_Limit_West)
							{
								// target position is outside limits
								gCWUP = false;
							}
						}
						else
						{
							if (targetRAEncoder > EQMath.gRA_Limit_East || targetRAEncoder < EQMath.gRA_Limit_West)
							{
								// target position is outside limits
								gCWUP = false;
							}
						}

						// if target position is outside limits
						if (!gCWUP)
						{
							//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.Add_Message("Goto: RA Limits prevent CW-UP slew");
							//then abandon Counter Weights up Slew and recalculate for a standard slew.
							if (tha < 0)
							{
								if (EQMath.gHemisphere == 0)
								{
									tPier = 1;
								}
								else
								{
									tPier = 0;
								}
								tRa = EQMath.Range24(gTargetRA - 12);
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
								tRa = gTargetRA;
							}
							targetRAEncoder = EQMath.Get_RAEncoderfromRA(tRa, 0, EQMath.gLongitude, EQMath.gRAEncoder_Zero_pos, EQMath.gTot_RA, EQMath.gHemisphere);
							targetDECEncoder = EQMath.Get_DECEncoderfromDEC(gTargetDec, tPier, EQMath.gDECEncoder_Zero_pos, EQMath.gTot_DEC, EQMath.gHemisphere);
						}
					}
				}

				if (!Alignment.gThreeStarEnable)
				{
					Alignment.gSelectStar = 0;
					currentRAEncoder = EQMath.Delta_RA_Map(currentRAEncoder);
					currentDECEncoder = EQMath.Delta_DEC_Map(currentDECEncoder);
				}
				else
				{
					// Transform target using model
					switch(Common.gAlignmentMode)
					{
						case 2 : 
							// n-star+nearest 
							tmpcoord = EQMath.DeltaSyncReverse_Matrix_Map(targetRAEncoder - EQMath.gRASync01, targetDECEncoder - EQMath.gDECSync01); 
							break;
						case 1 : 
							// n-star 
							tmpcoord = EQMath.Delta_Matrix_Map(targetRAEncoder - EQMath.gRASync01, targetDECEncoder - EQMath.gDECSync01); 
							break;
						default:
							// nearest 
							tmpcoord = EQMath.Delta_Matrix_Map(targetRAEncoder - EQMath.gRASync01, targetDECEncoder - EQMath.gDECSync01); 
							 
							if (tmpcoord.f == 0)
							{
								tmpcoord = EQMath.DeltaSyncReverse_Matrix_Map(targetRAEncoder - EQMath.gRASync01, targetDECEncoder - EQMath.gDECSync01);
							} 
							break;
					}
					targetRAEncoder = tmpcoord.x;
					targetDECEncoder = tmpcoord.Y;
				}

				//Execute the actual slew
				gGotoParams.RA_targetencoder = targetRAEncoder;
				gGotoParams.RA_currentencoder = currentRAEncoder;
				gGotoParams.DEC_targetencoder = targetDECEncoder;
				gGotoParams.DEC_currentencoder = currentDECEncoder;
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Goto: " + EQMath.FmtSexa(gTargetRA, false) + " " + EQMath.FmtSexa(gTargetDec, true));
				//    HC.Add_Message "Goto: RaEnc=" & CStr(currentRAEncoder) & " Target=" & CStr(targetRAEncoder)
				//    HC.Add_Message "Goto: DecEnc=" & CStr(currentDECEncoder) & " Target=" & CStr(targetDECEncoder)
			}
			catch
			{
			}


		}

		internal static void CalcEncoderGotoTargets(double tRa, double tDec, ref double RaEnc, ref double DecEnc)
		{

			eqmodvector.Coordt tmpcoord = new eqmodvector.Coordt();
			double tPier = 0;

			double tha = EQMath.RangeHA(tRa - EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD));
			if (tha < 0)
			{
				if (EQMath.gHemisphere == 0)
				{
					tPier = 1;
				}
				else
				{
					tPier = 0;
				}
				tRa = EQMath.Range24(tRa - 12);
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
			}

			//Compute for Target RA/DEC Encoder
			RaEnc = EQMath.Get_RAEncoderfromRA(tRa, 0, EQMath.gLongitude, EQMath.gRAEncoder_Zero_pos, EQMath.gTot_RA, EQMath.gHemisphere);
			DecEnc = EQMath.Get_DECEncoderfromDEC(tDec, tPier, EQMath.gDECEncoder_Zero_pos, EQMath.gTot_DEC, EQMath.gHemisphere);

			if (Alignment.gThreeStarEnable)
			{
				// Transform target using model
				switch(Common.gAlignmentMode)
				{
					case 2 : 
						// n-star+nearest 
						tmpcoord = EQMath.DeltaSyncReverse_Matrix_Map(RaEnc - EQMath.gRASync01, DecEnc - EQMath.gDECSync01); 
						break;
					case 1 : 
						// n-star 
						tmpcoord = EQMath.Delta_Matrix_Map(RaEnc - EQMath.gRASync01, DecEnc - EQMath.gDECSync01); 
						break;
					default:
						// nearest 
						tmpcoord = EQMath.Delta_Matrix_Map(RaEnc - EQMath.gRASync01, DecEnc - EQMath.gDECSync01); 
						 
						if (tmpcoord.f == 0)
						{
							tmpcoord = EQMath.DeltaSyncReverse_Matrix_Map(RaEnc - EQMath.gRASync01, DecEnc - EQMath.gDECSync01);
						} 
						break;
				}
				RaEnc = tmpcoord.x;
				DecEnc = tmpcoord.Y;
			}

		}



		internal static void StartSlew(double targetRAEncoder, double targetDECEncoder, double currentRAEncoder, double currentDECEncoder)
		{
			int DeltaRAStep = 0;
			int DeltaDECStep = 0;

			try
			{

				// calculate relative amount to move
				DeltaRAStep = Convert.ToInt32(Math.Abs(targetRAEncoder - currentRAEncoder));
				DeltaDECStep = Convert.ToInt32(Math.Abs(targetDECEncoder - currentDECEncoder));

				if (DeltaRAStep != 0)
				{
					// Compensate for the smallest discrepancy after the final slew
					if (EQMath.gTrackingStatus > 0)
					{
						if (targetRAEncoder > currentRAEncoder)
						{
							if (EQMath.gHemisphere == 0)
							{
								DeltaRAStep += gRA_Compensate;
							}
							else
							{
								DeltaRAStep -= gRA_Compensate;
							}
						}
						else
						{
							if (EQMath.gHemisphere == 0)
							{
								DeltaRAStep -= gRA_Compensate;
							}
							else
							{
								DeltaRAStep += gRA_Compensate;
							}
						}
						if (DeltaRAStep < 0)
						{
							DeltaRAStep = 0;
						}
					}

					if (targetRAEncoder > currentRAEncoder)
					{
						gGotoParams.RA_Direction = 0;
						switch(gGotoParams.rate)
						{
							case 0 : 
								// let mount decide on slew rate 
								gGotoParams.RA_SlewActive = 0; 
								EQMath.eqres = Common.EQStartMoveMotor(0, 0, 0, DeltaRAStep, Convert.ToInt32(EQMath.GetSlowdown(DeltaRAStep))); 
								break;
							default:
								gGotoParams.RA_SlewActive = 1; 
								EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_Slew(0, 0, 0, Convert.ToInt32(gGotoParams.rate)); 
								break;
						}
					}
					else
					{
						gGotoParams.RA_Direction = 1;
						switch(gGotoParams.rate)
						{
							case 0 : 
								gGotoParams.RA_SlewActive = 0; 
								EQMath.eqres = Common.EQStartMoveMotor(0, 0, 1, DeltaRAStep, Convert.ToInt32(EQMath.GetSlowdown(DeltaRAStep))); 
								break;
							default:
								gGotoParams.RA_SlewActive = 1; 
								EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_Slew(0, 0, 1, Convert.ToInt32(gGotoParams.rate)); 
								break;
						}
					}
				}

				if (DeltaDECStep != 0)
				{
					if (targetDECEncoder > currentDECEncoder)
					{
						gGotoParams.DEC_Direction = 0;
						switch(gGotoParams.rate)
						{
							case 0 : 
								// let mount decide on slew rate 
								gGotoParams.DEC_SlewActive = 0; 
								EQMath.eqres = Common.EQStartMoveMotor(1, 0, 0, DeltaDECStep, Convert.ToInt32(EQMath.GetSlowdown(DeltaDECStep))); 
								break;
							default:
								gGotoParams.DEC_SlewActive = 1; 
								EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_Slew(1, 0, 0, Convert.ToInt32(gGotoParams.rate)); 
								break;
						}
					}
					else
					{
						gGotoParams.DEC_Direction = 1;
						switch(gGotoParams.rate)
						{
							case 0 : 
								// let mount decide on slew rate 
								gGotoParams.DEC_SlewActive = 0; 
								EQMath.eqres = Common.EQStartMoveMotor(1, 0, 1, DeltaDECStep, Convert.ToInt32(EQMath.GetSlowdown(DeltaDECStep))); 
								break;
							default:
								gGotoParams.DEC_SlewActive = 1; 
								EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_Slew(1, 0, 1, Convert.ToInt32(gGotoParams.rate)); 
								break;
						}
					}
				}

				// Activate Asynchronous Slew Monitoring Routine
				EQMath.gRAStatus = Common.EQ_MOTORBUSY;
				EQMath.gDECStatus = Common.EQ_MOTORBUSY;
				EQMath.gRAStatus_slew = false;
			}
			catch
			{
			}


			EQMath.gSlewStatus = true;

		}

		// called from the encoder timer to supervise active gotos
		internal static void ManageGoto()
		{
			object oLangDll = null;
			object[] EQ_Beep = null;
			object HC = null;
			double ra_diff = 0;
			double dec_diff = 0;

			//'''''''''''''''''''''''''''''''''''''''''''''
			// Fixed rate slew
			//'''''''''''''''''''''''''''''''''''''''''''''
			if (gGotoParams.RA_SlewActive == 1 || gGotoParams.DEC_SlewActive == 1)
			{
				// Handle as fixed rate slew
				if (gGotoParams.RA_SlewActive != 0)
				{
					if (gGotoParams.RA_Direction == 0)
					{
						if (EQMath.gRA_Encoder >= gGotoParams.RA_targetencoder)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(0);
							//                    Do
							//                        eqres = EQ_GetMotorStatus(0)
							//                        If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then GoTo MG1
							//                    Loop While (eqres And EQ_MOTORBUSY) <> 0
							//MG1:
							gGotoParams.RA_SlewActive = 0;
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_StartRATrack(0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
					}
					else
					{
						if (EQMath.gRA_Encoder <= gGotoParams.RA_targetencoder)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(0);
							//                    Do
							//                        eqres = EQ_GetMotorStatus(0)
							//                        If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then GoTo MG2
							//                    Loop While (eqres And EQ_MOTORBUSY) <> 0
							//MG2:
							gGotoParams.RA_SlewActive = 0;
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_StartRATrack(0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
					}
				}

				if (gGotoParams.DEC_SlewActive != 0)
				{
					if (gGotoParams.DEC_Direction == 0)
					{
						if (EQMath.gDec_Encoder >= gGotoParams.DEC_targetencoder)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
							//                    Do
							//                        eqres = EQ_GetMotorStatus(1)
							//                       If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then GoTo MG3
							//                    Loop While (eqres And EQ_MOTORBUSY) <> 0
							//MG3:
							gGotoParams.DEC_SlewActive = 0;
						}
					}
					else
					{
						if (EQMath.gDec_Encoder <= gGotoParams.DEC_targetencoder)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
							//                    Do
							//                        eqres = EQ_GetMotorStatus(1)
							//                        If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then GoTo MG4
							//                    Loop While (eqres And EQ_MOTORBUSY) <> 0
							//MG4:
							gGotoParams.DEC_SlewActive = 0;
						}
					}
				}


				if (gGotoParams.RA_SlewActive == 0 && gGotoParams.DEC_SlewActive == 0)
				{

					switch(gGotoParams.SuperSafeMode)
					{
						case 0 : 
							// rough fixed rate slew complete 
							CalcEncoderTargets(); 
							ra_diff = Math.Abs(gGotoParams.RA_targetencoder - EQMath.gRA_Encoder); 
							dec_diff = Math.Abs(gGotoParams.DEC_targetencoder - EQMath.gDec_Encoder); 
							//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
							HC.Add_Message("Goto: FRSlew complete ra_diff=" + ra_diff.ToString() + " dec_diff=" + dec_diff.ToString()); 
							if ((ra_diff < EQMath.gTot_RA / 360d) && (dec_diff < EQMath.gTot_DEC / 540d))
							{
								// initiate a standard itterative goto if within a 3/4 of a degree.
								gGotoParams.rate = 0;
								StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
							}
							else
							{
								// Do another rough slew.
								//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Add_Message("Goto: FRSlew");
								gFRSlewCount++;
								if (gFRSlewCount >= 5)
								{
									//if we can't get close after 5 attempts then abandon the FR slew
									//and use the full speed iterative slew
									gFRSlewCount = 0;
									gGotoParams.rate = 0;
								}
								StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
							} 
							 
							break;
						case 1 : 
							// move to RA target 
							CalcEncoderTargets(); 
							gGotoParams.SuperSafeMode = 0; 
							StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder); 
							 
							//                Case 2 
							//                    ' we're at a limit about to go to target 
							//                    Call CalcEncoderTargets 
							//                    gGotoParams.SuperSafeMode = 0 
							//                    Call StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder) 
							 
							break;
						case 3 : 
							// were at a limit position 
							if (gGotoParams.RA_targetencoder > EQMath.RAEncoder_Home_pos)
							{
								// dual axis slew to limit position nearest to target
								gGotoParams.SuperSafeMode = 1;
								if (!Limits.RALimitsActive())
								{
									StartSlew(gRAMeridianWest, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
								}
								else
								{
									StartSlew(EQMath.gRA_Limit_West, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
								}
							}
							else
							{
								// dual axis slew to limit position nearest to target
								gGotoParams.SuperSafeMode = 1;
								if (!Limits.RALimitsActive())
								{
									StartSlew(gRAMeridianEast, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
								}
								else
								{
									StartSlew(EQMath.gRA_Limit_East, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
								}
							} 
							 
							break;
					}
				}
				return;

			}

			//'''''''''''''''''''''''''''''''''''''''''''''
			// Iterative slew - variable rate
			//'''''''''''''''''''''''''''''''''''''''''''''
			if ((Convert.ToInt32(EQMath.gRAStatus) & Convert.ToInt32(Common.EQ_MOTORBUSY)) == 0)
			{
				//At This point RA motor has completed the slew
				EQMath.gRAStatus_slew = true;
				if ((Convert.ToInt32(EQMath.gDECStatus) & Convert.ToInt32(Common.EQ_MOTORBUSY)) != 0)
				{
					// The DEC motor is still moving so start sidereal tracking to hold position in RA
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_StartRATrack(0, EQMath.gHemisphere, EQMath.gHemisphere);
				}
			}

			if ((Convert.ToInt32(EQMath.gDECStatus) & Convert.ToInt32(Common.EQ_MOTORBUSY)) == 0 && EQMath.gRAStatus_slew)
			{
				//DEC and RA motors have finished slewing at this point
				//We need to check if a new slew is needed to reduce the any difference
				//Caused by the Movement of the earth during the slew process

				switch(gGotoParams.SuperSafeMode)
				{
					case 0 : 
						// decrement the slew retry count 
						gSlewCount--; 
						 
						// calculate the difference (arcsec)  between target and current coords 
						ra_diff = 3600 * Math.Abs(EQMath.gRA - gTargetRA); 
						dec_diff = 3600 * Math.Abs(EQMath.gDec - gTargetDec); 
						 
						if ((gSlewCount > 0) && (EQMath.gTrackingStatus > 0))
						{ // Retry only if tracking is enabled
							// aim to get within the goto resolution (default = 10 steps)
							if (gGotoResolution > 0 && ra_diff <= gRAGotoRes && dec_diff <= gDECGotoRes)
							{
								goto slewcomplete;
							}
							else
							{
								//Re Execute a new RA-Only slew here
								CalcEncoderTargets();
								gGotoParams.rate = 0;
								StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder);
							}
						}
						else
						{
							goto slewcomplete;
						} 
						 
						break;
					case 1 : 
						// move to target 
						gGotoParams.SuperSafeMode = 0; 
						CalcEncoderTargets(); 
						gGotoParams.rate = 0; 
						//kick of an iterative slew to get us accurately to target RA 
						StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gGotoParams.RA_currentencoder, gGotoParams.DEC_currentencoder); 
						 
						//            Case 2 
						//                ' At a limit about to slew to target 
						//                Call CalcEncoderTargets 
						//                gGotoParams.SuperSafeMode = 0 
						//               Call StartSlew(gGotoParams.RA_targetencoder, gGotoParams.DEC_targetencoder, gEmulRA, gEmulDEC) 
						 
						break;
					case 3 : 
						// we are at a limit position 
						if (gGotoParams.RA_targetencoder > EQMath.RAEncoder_Home_pos)
						{
							// dual axis slew to limit position nearest to target
							gGotoParams.SuperSafeMode = 1;
							if (!Limits.RALimitsActive())
							{
								StartSlew(gRAMeridianWest, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
							}
							else
							{
								StartSlew(EQMath.gRA_Limit_West, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
							}
						}
						else
						{
							// dual axis slew to limit position nearest to target
							gGotoParams.SuperSafeMode = 1;
							if (!Limits.RALimitsActive())
							{
								StartSlew(gRAMeridianEast, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
							}
							else
							{
								StartSlew(EQMath.gRA_Limit_West, gGotoParams.DEC_targetencoder, EQMath.gEmulRA, EQMath.gEmulDEC);
							}
						} 

						 
						break;
				}
			}
			return;

			slewcomplete:
			EQMath.gSlewStatus = false;
			EQMath.gRAStatus_slew = false;
			Limits.gSupressHorizonLimits = false;

			// slew may have terminated early if parked
			if (EQMath.gEQparkstatus != 1)
			{
				// we've reached the desired target coords - resume tracking.
				switch(EQMath.gTrackingStatus)
				{
					case 0 : case 1 : 
						Tracking.EQStartSidereal(); 
						break;
					case 2 : case 3 : case 4 : 
						Tracking.RestartTracking(); 
						break;
				}

				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(Convert.ToString(oLangDll.GetLangString(5018)) + " " + EQMath.FmtSexa(EQMath.gRA, false) + " " + EQMath.FmtSexa(EQMath.gDec, true));
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Goto: SlewItereations=" + (gMaxSlewCount - gSlewCount).ToString());
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Goto: " + "RaDiff=" + StringsHelper.Format(Conversion.Str(ra_diff), "000.00") + " DecDiff=" + StringsHelper.Format(Conversion.Str(dec_diff), "000.00"));

				// goto complete
				object tempAuxVar = EQ_Beep[6];
			}

			if (gDisbleFlipGotoReset == 0)
			{
				//UPGRADE_TODO: (1067) Member ChkForceFlip is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.ChkForceFlip.value = 0;
			}

		}

		internal static void writeGotoRate()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("GOTO_RATE", gGotoRate.ToString());
		}
		internal static void readGotoRate()
		{
			object gParkParams = null;
			object HC = null;

			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("GOTO_RATE"));
				if (tmptxt != "")
				{
					gGotoRate = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					gGotoRate = 0;
					Tracking.writeCustomRa();
				}
				//    gGotoRate = 0
				if (gGotoRate == 0)
				{
					//UPGRADE_TODO: (1067) Member HScrollSlewLimit is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.HScrollSlewLimit.value = HC.HScrollSlewLimit.min;
				}
				else
				{
					//UPGRADE_TODO: (1067) Member HScrollSlewLimit is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.HScrollSlewLimit.value = gGotoRate;
				}
				//UPGRADE_TODO: (1067) Member rate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				gParkParams.rate = gGotoRate;
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}

		internal static void readFlipGoto()
		{
			object HC = null;

			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("DISABLE_FLIPGOTO_RESET"));
				if (tmptxt != "")
				{
					gDisbleFlipGotoReset = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("DISABLE_FLIPGOTO_RESET", "0");
					gDisbleFlipGotoReset = 0;
				}
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}
	}
}