using Microsoft.VisualBasic;
using System;
using System.Drawing;
using UpgradeHelpers.Helpers;

namespace Project1
{
	internal static class Tracking
	{


		public static int gCustomTrackingOffsetRA = 0;
		public static int gCustomTrackingOffsetDEC = 0;
		public static double gTrackFactorRA = 0;
		public static double gTrackFactorDEC = 0;
		public static Rates g_RAAxisRates = null; // rates available for MoveAxis
		public static Rates g_DECAxisRates = null; // rates available for MoveAxis
		public static TrackingRates g_TrackingRates = null; // Collection of supported drive rates
		public static string gCustomTrackFile = "";
		public static string gCustomTrackName = "";

		public static bool gMoveAxisRASlew = false;
		public static bool gMoveAxisDECSlew = false;
		public static bool gMoveAxisSlewing = false;

		[Serializable]
		public struct TrackRecord_def
		{
			public double time_mjd;
			public double DeltaRa;
			public double DeltaDec;
			public double RaRate;
			public double DecRate;
			public short DecDir;
			public double RAJ2000;
			public double DECJ2000;
			public double RaRateRaw;
			public double DECRateRaw;
			public bool UseRate;
		}

		// main control structure for custom tracking
		[Serializable]
		public struct TrackCtrl_def
		{
			public short FileFormat;
			public bool Precess;
			public bool Waypoint;
			public double AdjustRA;
			public double AdjustDEC;
			public short TrackIdx;
			public bool TrackingChangesEnabled;
			public TrackRecord_def[] TrackSchedule;
		}

		[Serializable]
		public struct RaDecCoords
		{
			public double RA;
			public double DEC;
		}

		static TrackCtrl_def TrackCtrl = new TrackCtrl_def();


		// Start RA motor based on an input rate of arcsec per Second

		internal static void StartRA_by_Rate(double RA_RATE)
		{
			object oLangDll = null;
			object HC = null;

			double j = 0;

			double k = 0;
			double m = 1;
			double i = Math.Abs(RA_RATE);

			if (EQMath.gMount_Ver > 0x301)
			{
				if (i > 1000)
				{
					k = 1;
					m = Common.EQGP(0, 10003);
				}
			}
			else
			{
				if (i > 3000)
				{
					k = 1;
					m = Common.EQGP(0, 10003);
				}
			}

			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message(Convert.ToString(oLangDll.GetLangString(117)) + " " + Conversion.Str(m) + " , " + Conversion.Str(RA_RATE) + " arcsec/sec");

			EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(0); // Stop RA Motor
			if (!(EQMath.eqres != Common.EQ_OK))
			{

				//    Do
				//       eqres = EQ_GetMotorStatus(0)
				//       If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then
				//            GoTo RARateEndhome1
				//       End If
				//    Loop While (eqres And EQ_MOTORBUSY) <> 0


				if (RA_RATE == 0)
				{
					EQMath.gSlewStatus = false;
					EQMath.gRAStatus_slew = false;
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(0);
					EQMath.gRAMoveAxis_Rate = 0;
					return;
				}

				i = RA_RATE;
				j = Math.Abs(i); //Get the absolute value for parameter passing

				if (EQMath.gMount_Ver == 0x301)
				{
					if ((j > 1350) && (j <= 3000))
					{
						if (j < 2175)
						{
							j = 1350;
						}
						else
						{
							j = 3001;
							k = 1;
							m = Common.EQGP(0, 10003);
						}
					}
				}

				EQMath.gRAMoveAxis_Rate = k; //Save Speed Settings

				//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_FileMessage("StartRARate=" + RA_RATE.ToString("N5"));
				//    j = Int((m * 9325.46154 / j) + 0.5) + 30000 'Compute for the rate
				j = Math.Floor((m * gTrackFactorRA / j) + 0.5d) + 30000; //Compute for the rate

				if (i >= 0)
				{
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetCustomTrackRate(0, 1, Convert.ToInt32(j), Convert.ToInt32(k), EQMath.gHemisphere, 0);
				}
				else
				{
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetCustomTrackRate(0, 1, Convert.ToInt32(j), Convert.ToInt32(k), EQMath.gHemisphere, 1);
				}

			}


		}

		// Change RA motor rate based on an input rate of arcsec per Second

		internal static void ChangeRA_by_Rate(double rate)
		{
			object oLangDll = null;
			object HC = null;

			int dir = 0;

			if (rate >= 0)
			{
				dir = 0;
			}
			else
			{
				dir = 1;
			}

			if (rate == 0)
			{
				// rate = 0 so stop motors
				EQMath.gSlewStatus = false;
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(0);
				EQMath.gRAStatus_slew = false;
				EQMath.gRAMoveAxis_Rate = 0;
				return;
			}

			double k = 0; // Assume low speed
			double m = 1; // Speed multiplier = 1

			int init = 0;
			double j = Math.Abs(rate);

			if (EQMath.gMount_Ver > 0x301)
			{
				// if above high speed theshold
				if (j > 1000)
				{
					k = 1; // HIGH SPEED
					m = Common.EQGP(0, 10003); // GET HIGH SPEED MULTIPLIER
				}
			}
			else
			{
				// who knows what Mon is up to here - a special for his mount perhaps?
				if (EQMath.gMount_Ver == 0x301)
				{
					if ((j > 1350) && (j <= 3000))
					{
						if (j < 2175)
						{
							j = 1350;
						}
						else
						{
							j = 3001;
							k = 1;
							m = Common.EQGP(0, 10003);
						}
					}
				}
				// if above high speed theshold
				if (j > 3000)
				{
					k = 1; // HIGH SPEED
					m = Common.EQGP(0, 10003); // GET HIGH SPEED MULTIPLIER
				}
			}

			//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_FileMessage("ChangeRARate=" + rate.ToString("N5"));

			// if there's a switch between high/low speed or if operating at high speed
			// we ned to do additional initialisation
			if (k != 0 || k != EQMath.gRAMoveAxis_Rate)
			{
				init = 1;
			}

			if (init == 1)
			{
				// Stop Motor
				//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_FileMessage("Direction or High/Low speed change");
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(0);
				if (EQMath.eqres != Common.EQ_OK)
				{
					return;
				}

				//        ' wait for motor to stop
				//        Do
				//          eqres = EQ_GetMotorStatus(0)
				//          If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then
				//               GoTo RARateEndhome2
				//          End If
				//        Loop While (eqres And EQ_MOTORBUSY) <> 0
				//force initialisation
			}

			EQMath.gRAMoveAxis_Rate = k;

			//Compute for the rate
			//    j = Int((m * 9325.46154 / j) + 0.5) + 30000
			j = Math.Floor((m * gTrackFactorRA / j) + 0.5d) + 30000;

			EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetCustomTrackRate(0, init, Convert.ToInt32(j), Convert.ToInt32(k), EQMath.gHemisphere, dir);
			//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_FileMessage("EQ_SetCustomTrackRate=0," + init.ToString() + "," + j.ToString() + "," + k.ToString() + "," + EQMath.gHemisphere.ToString() + "," + dir.ToString());
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message(Convert.ToString(oLangDll.GetLangString(117)) + "=" + Conversion.Str(rate) + " arcsec/sec" + "," + EQMath.eqres.ToString());



		}


		// Start DEC motor based on an input rate of arcsec per Second

		internal static void StartDEC_by_Rate(double DEC_RATE)
		{
			object oLangDll = null;
			object HC = null;

			double j = 0;

			double k = 0;
			double m = 1;
			double i = Math.Abs(DEC_RATE);

			if (EQMath.gMount_Ver > 0x301)
			{
				if (i > 1000)
				{
					k = 1;
					m = Common.EQGP(1, 10003);
				}
			}
			else
			{
				if (i > 3000)
				{
					k = 1;
					m = Common.EQGP(1, 10003);
				}
			}


			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message(Convert.ToString(oLangDll.GetLangString(118)) + " " + Conversion.Str(m) + " , " + Conversion.Str(DEC_RATE) + " arcsec/sec");

			EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1); // Stop RA Motor
			if (!(EQMath.eqres != Common.EQ_OK))
			{

				//    Do
				//       eqres = EQ_GetMotorStatus(1)
				//       If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then
				//            GoTo DECRateEndhome1
				//       End If
				//    Loop While (eqres And EQ_MOTORBUSY) <> 0

				if (DEC_RATE == 0)
				{
					EQMath.gSlewStatus = false;
					EQMath.gRAStatus_slew = false;
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
					EQMath.gDECMoveAxis_Rate = 0;
					return;
				}

				i = DEC_RATE;
				j = Math.Abs(i); //Get the absolute value for parameter passing


				if (EQMath.gMount_Ver == 0x301)
				{
					if ((j > 1350) && (j <= 3000))
					{
						if (j < 2175)
						{
							j = 1350;
						}
						else
						{
							j = 3001;
							k = 1;
							m = Common.EQGP(1, 10003);
						}
					}
				}


				EQMath.gDECMoveAxis_Rate = k; //Save Speed Settings

				//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_FileMessage("StartDecRate=" + DEC_RATE.ToString("N5"));
				//    j = Int((m * 9325.46154 / j) + 0.5) + 30000 'Compute for the rate
				j = Math.Floor((m * gTrackFactorDEC / j) + 0.5d) + 30000; //Compute for the rate

				if (i >= 0)
				{
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetCustomTrackRate(1, 1, Convert.ToInt32(j), Convert.ToInt32(k), EQMath.gHemisphere, 0);
				}
				else
				{
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetCustomTrackRate(1, 1, Convert.ToInt32(j), Convert.ToInt32(k), EQMath.gHemisphere, 1);
				}

			}


		}



		// Change DEC motor rate based on an input rate of arcsec per Second

		internal static void ChangeDEC_by_Rate(double rate)
		{
			object oLangDll = null;
			object HC = null;

			int dir = 0;

			if (rate >= 0)
			{
				dir = 0;
			}
			else
			{
				dir = 1;
			}

			if (rate == 0)
			{
				// rate = 0 so stop motors
				EQMath.gSlewStatus = false;
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
				//        gRAStatus_slew = False
				EQMath.gDECMoveAxis_Rate = 0;
				return;
			}

			double k = 0; // Assume low speed
			double m = 1; // Speed multiplier = 1
			int init = 0;
			double j = Math.Abs(rate);

			if (EQMath.gMount_Ver > 0x301)
			{
				// if above high speed theshold
				if (j > 1000)
				{
					k = 1; // HIGH SPEED
					m = Common.EQGP(1, 10003); // GET HIGH SPEED MULTIPLIER
				}
			}
			else
			{
				// who knows what Mon is up to here - a special for his mount perhaps?
				if (EQMath.gMount_Ver == 0x301)
				{
					if ((j > 1350) && (j <= 3000))
					{
						if (j < 2175)
						{
							j = 1350;
						}
						else
						{
							j = 3001;
							k = 1;
							m = Common.EQGP(1, 10003);
						}
					}
				}
				// if above high speed theshold
				if (j > 3000)
				{
					k = 1; // HIGH SPEED
					m = Common.EQGP(1, 10003); // GET HIGH SPEED MULTIPLIER
				}
			}

			//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_FileMessage("ChangeDECRate=" + rate.ToString("N5"));

			// if there's a switch between high/low speed or if operating at high speed
			// we need to do additional initialisation
			if (k != 0 || k != EQMath.gDECMoveAxis_Rate)
			{
				init = 1;
			}

			if (init == 1)
			{
				// Stop Motor
				//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_FileMessage("Direction or High/Low speed change");
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
				if (EQMath.eqres != Common.EQ_OK)
				{
					return;
				}

				//        ' wait for motor to stop
				//        Do
				//          eqres = EQ_GetMotorStatus(1)
				//          If (eqres = EQ_NOTINITIALIZED) Or (eqres = EQ_COMNOTOPEN) Or (eqres = EQ_COMTIMEOUT) Then
				//               GoTo DECRateEndhome2
				//          End If
				//        Loop While (eqres And EQ_MOTORBUSY) <> 0
				//force initialisation
			}


			EQMath.gDECMoveAxis_Rate = k;

			//Compute for the rate
			j = Math.Floor((m * gTrackFactorDEC / j) + 0.5d) + 30000;
			//    j = Int((m * 9325.46154 / j) + 0.5) + 30000

			EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetCustomTrackRate(1, init, Convert.ToInt32(j), Convert.ToInt32(k), EQMath.gHemisphere, dir);
			//UPGRADE_TODO: (1067) Member Add_FileMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_FileMessage("EQ_SetCustomTrackRate=1," + init.ToString() + "," + j.ToString() + "," + k.ToString() + "," + EQMath.gHemisphere.ToString() + "," + dir.ToString());
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message(Convert.ToString(oLangDll.GetLangString(118)) + "=" + Conversion.Str(rate) + " arcsec/sec" + "," + EQMath.eqres.ToString());



		}


		internal static void EQMoveAxis(double axis, double rate)
		{
			object HC = null;
			object oLangDll = null;

			double current_rate = 0;

			if (rate != 0)
			{
				//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(189));
			}

			double j = rate * 3600; // Convert to Arcseconds

			if (axis == 0)
			{

				if (rate == 0 && (EQMath.gDeclinationRate == 0))
				{
					//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(178));
				}


				if (EQMath.gHemisphere == 1)
				{
					j = -1 * j;
					current_rate = EQMath.gRightAscensionRate * -1;
				}
				else
				{
					current_rate = EQMath.gRightAscensionRate;
				}

				// check for change of direction
				if ((current_rate * j) <= 0)
				{
					StartRA_by_Rate(j);
				}
				else
				{
					ChangeRA_by_Rate(j);
				}

				EQMath.gRightAscensionRate = j;

				if (rate == 0)
				{
					gMoveAxisRASlew = false;
				}
				else
				{
					EQMath.gTrackingStatus = 4;
					gMoveAxisRASlew = true;
				}

			}

			if (axis == 1)
			{

				if (rate == 0 && (EQMath.gRightAscensionRate == 0))
				{
					//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(178));
				}

				// Mon seems to have included the code below for the Move South/Move North requirements of satelite tracking
				// However ASCOM requires that a positive rate always moces the axis clockwise so this code is well iffy!
				//        j = j * -1
				//
				//        If gHemisphere = 0 Then
				//            If (gDec_DegNoAdjust > 90) And (gDec_DegNoAdjust <= 270) Then j = j * -1
				//        Else
				//            If (gDec_DegNoAdjust <= 90) Or (gDec_DegNoAdjust > 270) Then j = j * -1
				//        End If

				// check for change of direction
				if ((EQMath.gDeclinationRate * j) <= 0)
				{
					StartDEC_by_Rate(j);
				}
				else
				{
					ChangeDEC_by_Rate(j);
				}

				EQMath.gDeclinationRate = j;
				if (rate == 0)
				{
					gMoveAxisDECSlew = false;
				}
				else
				{
					EQMath.gTrackingStatus = 4;
					gMoveAxisDECSlew = true;
				}

			}

			gMoveAxisSlewing = false;
			if (gMoveAxisDECSlew || gMoveAxisRASlew)
			{
				gMoveAxisSlewing = true;
			}
		}

		internal static void CustomMoveAxis(double axis, double rate, bool init, string RateName)
		{
			object HC = null;
			object oLangDll = null;


			if (rate != 0)
			{
				//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + RateName;
			}

			double j = rate;

			if (axis == 0)
			{
				if (rate == 0 && (EQMath.gDeclinationRate == 0))
				{
					//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(178));
				}
				if (init)
				{
					StartRA_by_Rate(j);
				}
				else
				{
					if (j != EQMath.gRightAscensionRate)
					{
						ChangeRA_by_Rate(j);
					}
				}
				EQMath.gRightAscensionRate = j;
				EQMath.gTrackingStatus = 4;
			}

			if (axis == 1)
			{
				if (rate == 0 && (EQMath.gRightAscensionRate == 0))
				{
					//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(178));
				}
				if (init)
				{
					StartDEC_by_Rate(j);
				}
				else
				{
					if (j != EQMath.gDeclinationRate)
					{
						ChangeDEC_by_Rate(j);
					}
				}
				EQMath.gDeclinationRate = j;
				EQMath.gTrackingStatus = 4;
			}

		}


		internal static void Start_CustomTracking2()
		{
			object oLangDll = null;
			object[] EQ_Beep = null;
			object HC = null;
			if (EQMath.gEQparkstatus != 0)
			{
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(5013));
				return;
			}

			EQMath.gRA_LastRate = 0;
			if (PEC.gPEC_Enabled)
			{
				PEC.PEC_StopTracking();
			}
			object tempAuxVar = EQ_Beep[13];
			Start_CustomTracking();

		}


		internal static void Start_CustomTracking()
		{
			object HC = null;
			object oLangDll = null;
			object[] emergency_stop = null;
			double i = 0;
			double j = 0;

			try
			{

				if (gCustomTrackFile == "")
				{

					TrackCtrl.TrackingChangesEnabled = false;


					//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					i = Convert.ToDouble(HC.raCustom);
					//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					j = Convert.ToDouble(HC.decCustom);
					if (EQMath.gHemisphere == 1)
					{
						i = -1 * i;
					}

					if ((Math.Abs(i) > 12000) || (Math.Abs(j) > 12000))
					{
						throw new Exception();
					}

					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message(Convert.ToString(oLangDll.GetLangString(5040)) + StringsHelper.Format(Conversion.Str(i), "000.00") + " DEC:" + StringsHelper.Format(Conversion.Str(j), "000.00") + " arcsec/sec");

					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					CustomMoveAxis(0, i, true, Convert.ToString(oLangDll.GetLangString(189)));
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					CustomMoveAxis(1, j, true, Convert.ToString(oLangDll.GetLangString(189)));
				}
				else
				{
					// custom track file is assigned
					int tempRefParam = 1;
					TrackCtrl.TrackIdx = (short) GetTrackFileIdx(ref tempRefParam, true);
					if (TrackCtrl.TrackIdx != -1)
					{
						if (TrackCtrl.Waypoint)
						{
							GetTrackTarget(ref i, ref j);
							TrackCtrl.AdjustRA = EQMath.gRA - i;
							TrackCtrl.AdjustDEC = EQMath.gDec - j;
						}
						else
						{
							TrackCtrl.AdjustRA = 0;
							TrackCtrl.AdjustDEC = 0;
						}
						i = TrackCtrl.TrackSchedule[TrackCtrl.TrackIdx].RaRate;
						j = TrackCtrl.TrackSchedule[TrackCtrl.TrackIdx].DecRate;
						//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.decCustom.Text = j.ToString("N5");
						if (EQMath.gHemisphere == 1)
						{
							//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.raCustom.Text = (-1 * i).ToString("N5");
						}
						else
						{
							//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.raCustom.Text = i.ToString("N5");
						}
						CustomMoveAxis(0, i, true, gCustomTrackName);
						CustomMoveAxis(1, j, true, gCustomTrackName);
					}
					else
					{
					}
					TrackCtrl.TrackingChangesEnabled = true;
					//UPGRADE_TODO: (1067) Member CustomTrackTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.CustomTrackTimer.Enabled = true;
				}
			}
			catch
			{

				//   HC.Add_Message (oLangDll.GetLangString(5039))
				object[] tempAuxVar = emergency_stop;
			}
		}

		internal static void Restore_CustomTracking()
		{
			object HC = null;
			object oLangDll = null;
			double rate = 0;
			double RA = 0;
			double DEC = 0;

			if (EQMath.gTrackingStatus == 4)
			{
				if (gCustomTrackFile == "")
				{
					TrackCtrl.TrackingChangesEnabled = false;
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					CustomMoveAxis(0, EQMath.gRightAscensionRate, true, Convert.ToString(oLangDll.GetLangString(189)));
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					CustomMoveAxis(1, EQMath.gDeclinationRate, true, Convert.ToString(oLangDll.GetLangString(189)));
				}
				else
				{
					int tempRefParam = 1;
					TrackCtrl.TrackIdx = (short) GetTrackFileIdx(ref tempRefParam, false);
					if (TrackCtrl.TrackIdx != -1)
					{

						if (TrackCtrl.Waypoint)
						{
							GetTrackTarget(ref RA, ref DEC);
							TrackCtrl.AdjustRA = EQMath.gRA - RA;
							TrackCtrl.AdjustDEC = EQMath.gDec - DEC;
						}
						else
						{
							TrackCtrl.AdjustRA = 0;
							TrackCtrl.AdjustDEC = 0;
						}

						rate = TrackCtrl.TrackSchedule[TrackCtrl.TrackIdx].RaRate;
						if (EQMath.gHemisphere == 1)
						{
							//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.raCustom.Text = (-1 * rate).ToString("N5");
						}
						else
						{
							//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.raCustom.Text = rate.ToString("N5");
						}
						CustomMoveAxis(0, rate, true, gCustomTrackName);

						rate = TrackCtrl.TrackSchedule[TrackCtrl.TrackIdx].DecRate;
						//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.decCustom.Text = rate.ToString("N5");
						CustomMoveAxis(1, rate, true, gCustomTrackName);
					}
					else
					{
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						CustomMoveAxis(0, EQMath.gRightAscensionRate, true, Convert.ToString(oLangDll.GetLangString(189)));
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						CustomMoveAxis(1, EQMath.gDeclinationRate, true, Convert.ToString(oLangDll.GetLangString(189)));
						//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.raCustom.Text = EQMath.gRightAscensionRate.ToString("N5");
						//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.decCustom.Text = EQMath.gDeclinationRate.ToString("N5");
					}
					TrackCtrl.TrackingChangesEnabled = true;
				}
			}

		}

		internal static void EQStartSidereal2()
		{
			object oLangDll = null;
			object[] EQ_Beep = null;
			object HC = null;
			if (EQMath.gEQparkstatus != 0)
			{
				// no tracking if parked!
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(5013));
			}
			else
			{
				EQStartSidereal();
				object tempAuxVar = EQ_Beep[10];
			}
		}



		internal static void EQStartSidereal()
		{
			object HC = null;
			object oLangDll = null;
			EQMath.gRA_LastRate = 0;

			if (EQMath.gEQparkstatus != 0)
			{
				// no tracking if parked!
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(5013));
			}
			else
			{
				// Stop DEC motor
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
				EQMath.gDeclinationRate = 0;

				// start RA motor at sidereal
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_StartRATrack(0, EQMath.gHemisphere, EQMath.gHemisphere);
				EQMath.gRAMoveAxis_Rate = 0;
				EQMath.gTrackingStatus = 1;
				EQMath.gRightAscensionRate = EQMath.SID_RATE;

				//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToDouble(HC.CheckPEC.Value) == 1)
				{
					// track using PEC
					PEC.PEC_StartTracking();
				}
				else
				{
					// Set Caption
					//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(122));
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message(oLangDll.GetLangString(5014));
				}
			}

		}

		internal static void StopTrackingUpdates()
		{
			switch(EQMath.gTrackingStatus)
			{
				case 1 : 
					PEC.PEC_StopTracking(); 
					break;
				case 2 : case 3 : 
					break;
				case 4 : 
					TrackCtrl.TrackingChangesEnabled = false; 
					break;
				default:
					break;
			}
		}


		internal static void RestartTracking()
		{
			object[] Start_Lunar = null;
			object[] Start_Solar = null;

			EQMath.gRAMoveAxis_Rate = 0;

			switch(EQMath.gTrackingStatus)
			{
				case 1 : 
					EQStartSidereal(); 
					break;
				case 2 : 
					object tempAuxVar = Start_Lunar[1]; 
					break;
				case 3 : 
					object tempAuxVar2 = Start_Solar[1]; 
					break;
				case 4 : 
					Restore_CustomTracking(); 
					break;
				default:
					// not tracking 
					//            eqres = EQ_MotorStop(0) 
					//            eqres = EQ_MotorStop(1) 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(2); 
					break;
			}

		}

		internal static void writeCustomRa()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_RA", HC.raCustom.Text);
			//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_DEC", HC.decCustom.Text);
			//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_TRACKFILE", HC.LabelTrackFile.ToolTipText);
		}

		internal static void readCustomRa()
		{
			object HC = null;
			object oLangDll = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_RA"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.raCustom.Text = tmptxt;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.raCustom.Text = (15.041067d).ToString();
				writeCustomRa();
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_DEC"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.decCustom.Text = tmptxt;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.decCustom.Text = "0";
				writeCustomRa();
			}

			// reload custom track file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_TRACKFILE"));
			if (tmptxt != "")
			{
				if (Track_LoadFile(tmptxt))
				{
					gCustomTrackFile = tmptxt;
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.Caption = Common.StripPath(gCustomTrackFile);
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.ToolTipText = gCustomTrackFile;
					//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.CmdTrack(4).ToolTipText = gCustomTrackName;
					// check data is current
					int tempRefParam = 1;
					TrackCtrl.TrackIdx = (short) GetTrackFileIdx(ref tempRefParam, true);
				}
				else
				{
					gCustomTrackFile = "";
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.Caption = "";
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.ToolTipText = "";
					//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.CmdTrack(4).ToolTipText = oLangDll.GetLangString(189);
					writeCustomRa();
				}
			}
			else
			{
				gCustomTrackFile = "";
				//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.LabelTrackFile.Caption = "";
				//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.LabelTrackFile.ToolTipText = "";
			}

			if (gCustomTrackFile != "")
			{
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(4).Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp111;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(4).Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp110;
			}


		}
		internal static void readSiderealRate()
		{
			object HC = null;
			string tmptxt = "";
			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (readerr)");

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("SIDEREAL_RATE"));
			if (tmptxt != "")
			{
				EQMath.gSiderealRate = Double.Parse(tmptxt);
			}
			else
			{
				readerr:
				EQMath.gSiderealRate = 15.041067d;
				writeSiderealRate();
			}

		}

		internal static void writeSiderealRate()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("SIDEREAL_RATE", EQMath.gSiderealRate.ToString());
		}

		internal static void LoadTrackingRates()
		{
			object FileDlg = null;
			object HC = null;
			object oLangDll = null;
			//UPGRADE_TODO: (1067) Member filter is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.filter = "*.txt*";
			if (gCustomTrackFile != "")
			{
				//UPGRADE_TODO: (1067) Member lastdir is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileDlg.lastdir = Common.GetPath(gCustomTrackFile);
				//UPGRADE_TODO: (1067) Member notfirst is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileDlg.notfirst = true;
			}
			//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.Show(1);
			//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToString(FileDlg.FileName) != "")
			{
				//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Track_LoadFile(Convert.ToString(FileDlg.FileName)))
				{
					//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					gCustomTrackFile = Convert.ToString(FileDlg.FileName);
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member filename2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.Caption = FileDlg.filename2;
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.ToolTipText = FileDlg.FileName;
					if (gCustomTrackName == "")
					{
						//UPGRADE_TODO: (1067) Member filename2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						gCustomTrackName = Convert.ToString(FileDlg.filename2);
					}
					//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.CmdTrack(4).ToolTipText = gCustomTrackName;
					// check data is current
					int tempRefParam = 1;
					TrackCtrl.TrackIdx = (short) GetTrackFileIdx(ref tempRefParam, true);
				}
				else
				{
					gCustomTrackFile = "";
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.Caption = "";
					//UPGRADE_TODO: (1067) Member LabelTrackFile is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.LabelTrackFile.ToolTipText = "";
					//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.CmdTrack(4).ToolTipText = oLangDll.GetLangString(189);
				}
			}

			if (gCustomTrackFile != "")
			{
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(4).Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp111;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(4).Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp110;
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(4).ToolTipText = oLangDll.GetLangString(189);
			}
			writeCustomRa();

		}

		internal static bool Track_LoadFile(string FileName)
		{
			bool result = false;
			object[, , , ] cal_mjd = null;
			object HC = null;
			string temp1 = "";
			string[] temp2 = null;
			int lineno = 0;
			int idx = 0;
			int NF1 = 0;
			int NF2 = 0;
			double month = 0;
			double year = 0;
			double day = 0;
			double hour = 0;
			double minute = 0;
			double second = 0;
			double RA = 0;
			double DEC = 0;
			double Lastra = 0;
			double Lastdec = 0;
			double mjd = 0;
			double Lastmjd = 0;
			double DecEncoder = 0;
			double LastDecEncoder = 0;
			double LastRaRate = 0;
			double LastDecRate = 0;
			double RaRate = 0;
			double DecRate = 0;
			double deltat = 0;
			int Format = 0;
			TrackRecord_def TrackNew = new TrackRecord_def();
			string[] params_Renamed = null;
			UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME typTime = new UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME();
			double mjdNow = 0;

			try
			{

				result = false;

				if (FileName == "")
				{
					throw new Exception();
				}


				NF2 = FileSystem.FreeFile();
				FileSystem.FileClose(NF2);
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				temp1 = Convert.ToString(HC.oPersist.GetIniPath) + "\\CustomRateDebug.txt";
				FileSystem.FileOpen(NF2, temp1, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
				FileSystem.PrintLine(NF2, "MJD RaDelta RaRate DecDelta DecRate DecDir");

				NF1 = FileSystem.FreeFile();
				FileSystem.FileClose(NF1);
				FileSystem.FileOpen(NF1, FileName, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);

				lineno = 0;
				idx = 0;
				Lastra = 0;
				Lastdec = 0;
				Lastmjd = 0;
				TrackCtrl.FileFormat = 0;
				TrackCtrl.Precess = true;
				TrackCtrl.Waypoint = false;
				TrackCtrl.TrackSchedule = ArraysHelper.InitializeArray<TrackRecord_def>(2);

				gCustomTrackName = Common.StripPath(FileName);

				while (!FileSystem.EOF(NF1))
				{
					temp1 = FileSystem.LineInput(NF1);
					if (temp1 != "")
					{

						switch(temp1.Substring(0, Math.Min(1, temp1.Length)))
						{
							case "#" : 
								// comment 
								 
								break;
							case "!" : 
								// parameter 
								params_Renamed = (string[]) temp1.Split('='); 
								switch(params_Renamed[0])
								{
									case "!Format" : case "!Format " : 
										switch(params_Renamed[1])
										{
											case " MPC" : case "MPC" : 
												TrackCtrl.FileFormat = 1; 
												break;
											case "MPC2" : 
												TrackCtrl.FileFormat = 2; 
												break;
											case "JPL" : 
												TrackCtrl.FileFormat = 3; 
												break;
											case "JPL2" : 
												TrackCtrl.FileFormat = 4; 
												break;
											default:
												TrackCtrl.FileFormat = 0; 
												break;
										} 
										break;
									case "!Name" : 
										gCustomTrackName = params_Renamed[1]; 
										break;
									case "!Precess" : 
										switch(params_Renamed[1])
										{
											case "1" : 
												TrackCtrl.Precess = true; 
												break;
											case "0" : 
												TrackCtrl.Precess = false; 
												break;
											default:
												TrackCtrl.Precess = false; 
												break;
										} 
										 
										break;
									case "!Waypoints" : 
										switch(params_Renamed[1])
										{
											case "1" : 
												TrackCtrl.Waypoint = true; 
												break;
											case "0" : 
												TrackCtrl.Waypoint = false; 
												break;
											default:
												TrackCtrl.Waypoint = false; 
												break;
										} 
										 
										break;
									case "!End" : 
										goto ParseEnd; 
										 
										break;
								} 
								 
								break;
							default:
								switch(TrackCtrl.FileFormat)
								{
									case 0 : 
										 
										break;
									case 1 : case 2 : 
										//mpc 
										 
										// strip out multiple spaces 

										while((temp1.IndexOf("  ") >= 0))
										{
											temp1 = StringsHelper.Replace(temp1, "  ", " ", 1, -1, CompareMethod.Binary);
										}; 
										 
										temp2 = (string[]) temp1.Split(' '); 
										 
										year = Conversion.Val(temp2[0]); 
										month = Conversion.Val(temp2[1]); 
										day = Conversion.Val(temp2[2]); 
										hour = Conversion.Val(temp2[3].Substring(0, Math.Min(2, temp2[3].Length))); 
										minute = Conversion.Val(temp2[3].Substring(2, Math.Min(2, temp2[3].Length - 2))); 
										second = Conversion.Val(temp2[3].Substring(4, Math.Min(2, temp2[3].Length - 4))); 
										//                            day = day + (hour * 3600 + minute * 60 + second) / 86400 
										object tempAuxVar = cal_mjd[Convert.ToInt32(month), Convert.ToInt32(day), Convert.ToInt32(year), Convert.ToInt32(mjd)]; 
										// convert to "julian seconds" 
										mjd = mjd * 86400 + (hour * 3600 + minute * 60 + second); 
										 
										// calculates current julian date in seconds 
										UpgradeSolution1Support.PInvoke.SafeNative.kernel32.GetSystemTime(ref typTime); 
										day = Convert.ToDouble(typTime.wDay); 
										object tempAuxVar2 = cal_mjd[typTime.wMonth, Convert.ToInt32(day), typTime.wYear, Convert.ToInt32(mjdNow)]; 
										mjdNow = mjdNow * 86400 + (Convert.ToDouble(typTime.wHour) * 3600 + Convert.ToDouble(typTime.wMinute) * 60 + Convert.ToDouble(typTime.wSecond)); 
										 
										if (mjd < mjdNow)
										{
											// data is earlier then now so keep line number reset to 0
											lineno = 0;
										} 
										 
										// get RA in seconds (of time) 
										RA = Conversion.Val(temp2[4]) * 3600 + Conversion.Val(temp2[5]) * 60 + Conversion.Val(temp2[6]); 
										 
										// get DEC in seconds (angle) 
										DEC = Conversion.Val(temp2[7]); 
										if (DEC < 0)
										{
											DEC = DEC * 3600 - Conversion.Val(temp2[8]) * 60 - Conversion.Val(temp2[9]);
										}
										else
										{
											DEC = DEC * 3600 + Conversion.Val(temp2[8]) * 60 + Conversion.Val(temp2[9]);
										} 
										 
										DecEncoder = EncoderFromDec(DEC / 3600d, RA / 3600d); 
										 
										RaRate = Conversion.Val(temp2[15]); 
										DecRate = Conversion.Val(temp2[16]); 
										 
										if (lineno > 0)
										{
											//                                    DeltaT = (mjd - Lastmjd) * 86400
											deltat = (mjd - Lastmjd);
											// calc change in RA (seconds of time)
											TrackNew.DeltaRa = RA - Lastra;
											// calc change in DEC (seconds of angle)
											TrackNew.DeltaDec = DEC - Lastdec;
											// Establish DEC direction
											if (DecEncoder > LastDecEncoder)
											{
												TrackNew.DecDir = 0;
											}
											else
											{
												TrackNew.DecDir = 1;
											}

											TrackNew.time_mjd = Lastmjd;
											TrackNew.RAJ2000 = Lastra;
											TrackNew.DECJ2000 = Lastdec;
											if (TrackCtrl.FileFormat == 2)
											{
												// high precision - calculated
												// Convert from seconds to arcseconds
												TrackNew.RaRate = TrackNew.DeltaRa * 15 / deltat;
												TrackNew.DecRate = TrackNew.DeltaDec / deltat;
											}
											else
											{
												// lower precision - read rates direct from file
												TrackNew.RaRate = LastRaRate;
												TrackNew.DecRate = LastDecRate;
											}
											TrackNew.RaRateRaw = TrackNew.RaRate;
											TrackNew.DECRateRaw = TrackNew.DecRate;

											// increase in tracking rate will decrease RA
											// so need to subtract
											TrackNew.RaRate = EQMath.SID_RATE - TrackNew.RaRate;
											if (EQMath.gHemisphere == 1)
											{
												// for some reason dll doesn't seem to sort out
												// southern hemisphere movement so we must make it negative
												TrackNew.RaRate *= -1;
											}

											TrackNew.DecRate = Math.Abs(TrackNew.DecRate);
											if (TrackNew.DecDir == 1)
											{
												TrackNew.DecRate = -1 * TrackNew.DecRate;
											}

											// add new record
											TrackCtrl.TrackSchedule = ArraysHelper.RedimPreserve(TrackCtrl.TrackSchedule, new int[]{lineno + 1});
											TrackCtrl.TrackSchedule[lineno] = TrackNew;
											FileSystem.PrintLine(NF2, TrackNew.time_mjd.ToString() + " " + TrackNew.DeltaRa.ToString("N5") + " " + TrackNew.RaRate.ToString("N5") + " " + TrackNew.DeltaDec.ToString("N5") + " " + TrackNew.DecRate.ToString("N5") + " " + TrackNew.DecDir.ToString("N0"));
										} 
										lineno++; 
										Lastra = RA; 
										Lastdec = DEC; 
										Lastmjd = mjd; 
										LastDecEncoder = DecEncoder; 
										LastRaRate = RaRate; 
										LastDecRate = DecRate; 
										 
										if (mjd > (mjdNow + 86400))
										{
											//we've loaded 24 hours of data - should be enough
											//if it isn't then user can always reload when current set runs out
											goto ParseEnd;
										} 
										 
										break;
									case 3 : 
										//JPL 
										 
										temp1 = StringsHelper.Replace(temp1, "     ", " ? ", 1, -1, CompareMethod.Binary); 
										// strip out multiple spaces 

										while((temp1.IndexOf("  ") >= 0))
										{
											temp1 = StringsHelper.Replace(temp1, "  ", " ", 1, -1, CompareMethod.Binary);
										}; 
										temp2 = (string[]) temp1.Trim().Split(' '); 
										 
										year = Conversion.Val(temp2[0].Substring(0, Math.Min(4, temp2[0].Length))); 
										switch(temp2[0].Substring(5, Math.Min(3, temp2[0].Length - 5)))
										{
											case "Jan" : 
												month = 1; 
												break;
											case "Feb" : 
												month = 2; 
												break;
											case "Mar" : 
												month = 3; 
												break;
											case "Apr" : 
												month = 4; 
												break;
											case "May" : 
												month = 5; 
												break;
											case "Jun" : 
												month = 6; 
												break;
											case "Jul" : 
												month = 7; 
												break;
											case "Aug" : 
												month = 8; 
												break;
											case "Sep" : 
												month = 9; 
												break;
											case "Oct" : 
												month = 10; 
												break;
											case "Nov" : 
												month = 11; 
												break;
											case "Dec" : 
												month = 12; 
												break;
										} 
										 
										day = Conversion.Val(temp2[0].Substring(Math.Max(temp2[0].Length - 2, 0))); 
										switch(Strings.Len(temp2[1]))
										{
											case 5 : 
												hour = Conversion.Val(temp2[1].Substring(0, Math.Min(2, temp2[1].Length))); 
												minute = Conversion.Val(temp2[1].Substring(3, Math.Min(2, temp2[1].Length - 3))); 
												second = 0; 
												break;
											case 8 : 
												hour = Conversion.Val(temp2[1].Substring(0, Math.Min(2, temp2[1].Length))); 
												minute = Conversion.Val(temp2[1].Substring(3, Math.Min(2, temp2[1].Length - 3))); 
												second = Conversion.Val(temp2[1].Substring(Math.Max(temp2[1].Length - 2, 0))); 
												break;
											case 12 : 
												hour = Conversion.Val(temp2[1].Substring(0, Math.Min(2, temp2[1].Length))); 
												minute = Conversion.Val(temp2[1].Substring(3, Math.Min(2, temp2[1].Length - 3))); 
												second = Conversion.Val(temp2[1].Substring(Math.Max(temp2[1].Length - 6, 0))); 
												break;
											default:
												throw new Exception(); 
												break;
										} 
										 
										//                            day = day + (hour * 3600 + minute * 60 + second) / 86400 
										object tempAuxVar3 = cal_mjd[Convert.ToInt32(month), Convert.ToInt32(day), Convert.ToInt32(year), Convert.ToInt32(mjd)]; 
										// convert to "julian seconds" 
										mjd = mjd * 86400 + (hour * 3600 + minute * 60 + second); 
										 
										// calculates current julian date in seconds 
										UpgradeSolution1Support.PInvoke.SafeNative.kernel32.GetSystemTime(ref typTime); 
										day = Convert.ToDouble(typTime.wDay); 
										object tempAuxVar4 = cal_mjd[typTime.wMonth, Convert.ToInt32(day), typTime.wYear, Convert.ToInt32(mjdNow)]; 
										mjdNow = mjdNow * 86400 + (Convert.ToDouble(typTime.wHour) * 3600 + Convert.ToDouble(typTime.wMinute) * 60 + Convert.ToDouble(typTime.wSecond)); 
										 
										if (mjd < mjdNow)
										{
											// data is earlier then now so keep line number reset to 0
											lineno = 0;
										} 

										 
										RA = Conversion.Val(temp2[3]) * 3600 + Conversion.Val(temp2[4]) * 60 + Conversion.Val(temp2[5]); 
										DEC = Conversion.Val(temp2[6]); 
										 
										if (DEC < 0)
										{
											DEC = DEC * 3600 - Conversion.Val(temp2[7]) * 60 - Conversion.Val(temp2[8]);
										}
										else
										{
											DEC = DEC * 3600 + Conversion.Val(temp2[7]) * 60 + Conversion.Val(temp2[8]);
										} 
										 
										DecEncoder = EncoderFromDec(DEC / 3600d, RA / 3600d); 
										 
										//                            ' d(RA*cos(Dec))/dt  arcsec/hour 
										//                            RaRate = val(temp2(9)) / Cos(RA * DEG_RAD / 3600) 
										//                            RaRate = RaRate / 3600 
										//                            ' d(DEC)/dt arcsec/hour 
										//                            DecRate = val(temp2(10)) / 3600 
										 
										if (lineno > 0)
										{
											//                                    DeltaT = (mjd - Lastmjd) * 86400
											deltat = (mjd - Lastmjd);
											TrackNew.DeltaRa = RA - Lastra;
											TrackNew.DeltaDec = DEC - Lastdec;
											if (DecEncoder > LastDecEncoder)
											{
												TrackNew.DecDir = 0;
											}
											else
											{
												TrackNew.DecDir = 1;
											}

											TrackNew.time_mjd = Lastmjd;
											TrackNew.RAJ2000 = Lastra;
											TrackNew.DECJ2000 = Lastdec;
											if (TrackCtrl.FileFormat == 3)
											{
												TrackNew.RaRate = TrackNew.DeltaRa * 15 / deltat;
												TrackNew.DecRate = TrackNew.DeltaDec / deltat;
											}
											else
											{
												//                                        .RaRate = LastRaRate
												//                                        .DecRate = LastDecRate
											}
											TrackNew.RaRateRaw = TrackNew.RaRate;
											TrackNew.DECRateRaw = TrackNew.DecRate;

											// increase in tracking rate will decrease RA
											// so need to subtract
											TrackNew.RaRate = EQMath.SID_RATE - TrackNew.RaRate;
											if (EQMath.gHemisphere == 1)
											{
												// for some reason dll doesn't seem to sort out
												// southern hemisphere movement so we must make it negative
												TrackNew.RaRate *= -1;
											}

											TrackNew.DecRate = Math.Abs(TrackNew.DecRate);
											if (TrackNew.DecDir == 1)
											{
												TrackNew.DecRate = -1 * TrackNew.DecRate;
											}

											// add new record
											TrackCtrl.TrackSchedule = ArraysHelper.RedimPreserve(TrackCtrl.TrackSchedule, new int[]{lineno + 1});
											TrackCtrl.TrackSchedule[lineno] = TrackNew;
											FileSystem.PrintLine(NF2, TrackNew.time_mjd.ToString() + " " + TrackNew.DeltaRa.ToString("N5") + " " + TrackNew.RaRate.ToString("N5") + " " + TrackNew.DeltaDec.ToString("N5") + " " + TrackNew.DecRate.ToString("N5") + " " + TrackNew.DecDir.ToString("N0"));
										} 
										lineno++; 
										Lastra = RA; 
										Lastdec = DEC; 
										Lastmjd = mjd; 
										LastDecEncoder = DecEncoder; 
										LastRaRate = RaRate; 
										LastDecRate = DecRate; 
										 
										if (mjd > (mjdNow + 86400))
										{
											//we've loaded 24 hours of data - should be enough
											//if it isn't then user can always reload when current set runs out
											goto ParseEnd;
										} 
										break;
								} 
								break;
						}
					}
				}
				ParseEnd:
				if (lineno >= 2)
				{
					result = true;
				}
				else
				{
					gCustomTrackName = "";
					if (Format == 0)
					{
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("Tracking File Error: Missing Header");
					}
					else
					{
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("Tracking File Error: Insufficient Data");
					}
				}
				FileSystem.FileClose(NF1);
				FileSystem.FileClose(NF2);
			}
			catch
			{

				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("Tracking File Error");
				FileSystem.FileClose(NF1);
				FileSystem.FileClose(NF2);
				gCustomTrackName = "";
			}
			return result;
		}

		internal static void TrackTimer()
		{
			object HC = null;
			double rate = 0;
			int idx = 0;
			double RA = 0;
			double DEC = 0;

			if (EQMath.gTrackingStatus == 4)
			{
				if (TrackCtrl.TrackingChangesEnabled)
				{
					if (TrackCtrl.TrackIdx != -1)
					{
						int tempRefParam = TrackCtrl.TrackIdx;
						idx = GetTrackFileIdx(ref tempRefParam, false);
						TrackCtrl.TrackIdx = (short) tempRefParam;
						if (idx != -1)
						{
							if (idx != TrackCtrl.TrackIdx)
							{
								TrackCtrl.TrackIdx = (short) idx;
								rate = TrackCtrl.TrackSchedule[TrackCtrl.TrackIdx].RaRate;
								if (EQMath.gHemisphere == 1)
								{
									//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.raCustom.Text = (-1 * rate).ToString("N5");
								}
								else
								{
									//UPGRADE_TODO: (1067) Member raCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.raCustom.Text = rate.ToString("N5");
								}
								if (rate != EQMath.gRightAscensionRate)
								{
									CustomMoveAxis(0, rate, false, gCustomTrackName);
								}
								rate = TrackCtrl.TrackSchedule[TrackCtrl.TrackIdx].DecRate;
								if (rate != EQMath.gDeclinationRate)
								{
									CustomMoveAxis(1, rate, false, gCustomTrackName);
								}
								//UPGRADE_TODO: (1067) Member decCustom is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.decCustom.Text = EQMath.gDeclinationRate.ToString("N5");
								if (TrackCtrl.Waypoint)
								{
									// perform waypoint correction
									if (GetTrackTarget(ref RA, ref DEC))
									{
										goto_TrackTarget(RA + TrackCtrl.AdjustRA, DEC + TrackCtrl.AdjustDEC, true);
									}
								}
							}
						}
						else
						{

						}
					}
				}
			}

		}

		internal static int GetTrackFileIdx(ref int StartIdx, bool Alert)
		{
			int result = 0;
			object[, , , ] cal_mjd = null;
			object HC = null;
			int i = 0;
			UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME typTime = new UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME();
			double mjd = 0;
			double day = 0;
			try
			{

				UpgradeSolution1Support.PInvoke.SafeNative.kernel32.GetSystemTime(ref typTime);

				result = -1;
				day = Convert.ToDouble(typTime.wDay);
				//    day = CDbl(typTime.wDay) + (CDbl(typTime.wHour) * 3600 + CDbl(typTime.wMinute) * 60 + CDbl(typTime.wSecond)) / 86400
				object tempAuxVar = cal_mjd[typTime.wMonth, Convert.ToInt32(day), typTime.wYear, Convert.ToInt32(mjd)];
				// calc elasped 'julian' seconds
				mjd = mjd * 86400 + (Convert.ToDouble(typTime.wHour) * 3600 + Convert.ToDouble(typTime.wMinute) * 60 + Convert.ToDouble(typTime.wSecond));

				if (StartIdx == 0)
				{
					StartIdx = 1;
				}

				// search forwards through data
				int tempForEndVar = TrackCtrl.TrackSchedule.GetUpperBound(0);
				for (i = StartIdx; i <= tempForEndVar; i++)
				{
					if (TrackCtrl.TrackSchedule[i].time_mjd > mjd)
					{
						return i - 1;
					}
				}

				// data set is out of date - try reloading new data set from file
				Track_LoadFile(gCustomTrackFile);
				// check through all of data
				int tempForEndVar2 = TrackCtrl.TrackSchedule.GetUpperBound(0);
				for (i = 1; i <= tempForEndVar2; i++)
				{
					if (TrackCtrl.TrackSchedule[i].time_mjd > mjd)
					{
						return i - 1;
					}
				}

				// file has no useful data - use last rate we know about
				result = i - 1;
				// turn icon red
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(4).Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp112;
				// send out warning message
				if (Alert)
				{
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message("Tracking file is out of date!" + Environment.NewLine + "Using last known rate.");
				}
			}
			catch
			{

				result = -1;
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(4).Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp112;
			}
			return result;
		}

		private static int GetPosnIdx(ref int StartIdx, bool Alert)
		{
			int result = 0;
			object[, , , ] cal_mjd = null;
			object HC = null;
			int i = 0;
			UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME typTime = new UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME();
			double mjd = 0;
			double day = 0;

			try
			{

				UpgradeSolution1Support.PInvoke.SafeNative.kernel32.GetSystemTime(ref typTime);

				result = -1;
				day = Convert.ToDouble(typTime.wDay);
				//    day = CDbl(typTime.wDay) + (CDbl(typTime.wHour) * 3600 + CDbl(typTime.wMinute) * 60 + CDbl(typTime.wSecond)) / 86400
				object tempAuxVar = cal_mjd[typTime.wMonth, Convert.ToInt32(day), typTime.wYear, Convert.ToInt32(mjd)];
				// calc elasped 'julian' seconds
				mjd = mjd * 86400 + (Convert.ToDouble(typTime.wHour) * 3600 + Convert.ToDouble(typTime.wMinute) * 60 + Convert.ToDouble(typTime.wSecond));

				if (StartIdx == 0)
				{
					StartIdx = 1;
				}

				int tempForEndVar = TrackCtrl.TrackSchedule.GetUpperBound(0);
				for (i = StartIdx; i <= tempForEndVar; i++)
				{
					if (TrackCtrl.TrackSchedule[i].time_mjd > mjd)
					{
						return i - 1;
					}
				}

				// data set is out of date - try reloading new data set from file
				Track_LoadFile(gCustomTrackFile);
				// check through all of data
				int tempForEndVar2 = TrackCtrl.TrackSchedule.GetUpperBound(0);
				for (i = 1; i <= tempForEndVar2; i++)
				{
					if (TrackCtrl.TrackSchedule[i].time_mjd > mjd)
					{
						return i - 1;
					}
				}

				if (Alert)
				{
					//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.CmdTrack(4).Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp112;
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message("Tracking data is out of date!");
				}
			}
			catch
			{

				if (Alert)
				{
				}
				result = -1;
			}

			return result;
		}

		private static double EncoderFromDec(double DEC, double RA)
		{
			double tPier = 0;

			if (EQMath.RangeHA(RA - EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD)) < 0)
			{
				if (EQMath.gHemisphere == 0)
				{
					tPier = 1;
				}
				else
				{
					tPier = 0;
				}
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
			return EQMath.Get_DECEncoderfromDEC(DEC, tPier, EQMath.gDECEncoder_Zero_pos, EQMath.gTot_DEC, EQMath.gHemisphere);

		}

		internal static object goto_TrackTarget(double RA, double DEC, bool mute)
		{
			object oLangDll = null;
			object[] EQ_Beep = null;
			object HC = null;
			UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME typTime = new UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME();

			try
			{

				if (EQMath.gEQparkstatus == 0)
				{
					// slew
					Goto.gTargetRA = RA;
					Goto.gTargetDec = DEC;
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message("Goto: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + " ] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]");
					Goto.gSlewCount = Goto.gMaxSlewCount; //NUM_SLEW_RETRIES               'Set initial iterative slew count
					Goto.radecAsyncSlew(Goto.gGotoRate);
					if (!mute)
					{
						object tempAuxVar = EQ_Beep[20];
					}
				}
				else
				{
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message(oLangDll.GetLangString(5000));
				}
			}
			catch
			{
			}



			return null;
		}

		internal static bool GetTrackTarget(ref double RA, ref double DEC)
		{
			double J2000 = 0;
			object[, , , ] Precess = null;
			object[, , , ] cal_mjd = null;
			object[] now_mjd = null;
			double epochnow = 0;
			UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME typTime = new UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME();
			double mjd = 0;
			double day = 0;
			double deltat = 0;

			int tempRefParam = 0;
			int idx = GetPosnIdx(ref tempRefParam, true);
			if (idx >= 0)
			{

				// get RA,DEC (in seconds)
				RA = TrackCtrl.TrackSchedule[idx].RAJ2000;
				DEC = TrackCtrl.TrackSchedule[idx].DECJ2000;

				// calculates current julian date in seconds
				UpgradeSolution1Support.PInvoke.SafeNative.kernel32.GetSystemTime(ref typTime);
				day = Convert.ToDouble(typTime.wDay);
				object tempAuxVar = cal_mjd[typTime.wMonth, Convert.ToInt32(day), typTime.wYear, Convert.ToInt32(mjd)];
				mjd = mjd * 86400 + (Convert.ToDouble(typTime.wHour) * 3600 + Convert.ToDouble(typTime.wMinute) * 60 + Convert.ToDouble(typTime.wSecond));

				// establish how many seconds have elapsed since record date/time
				deltat = mjd - TrackCtrl.TrackSchedule[idx].time_mjd;

				// compensate for movement
				RA += TrackCtrl.TrackSchedule[idx].RaRateRaw * deltat / 15d;
				DEC += TrackCtrl.TrackSchedule[idx].DECRateRaw * deltat;

				// convert back into hours
				RA /= 3600d;
				DEC /= 3600d;

				// adjust to JNOW
				if (TrackCtrl.Precess)
				{
					epochnow = 2000 + (Convert.ToDouble(now_mjd) - J2000) / 365.25d;
					object tempAuxVar2 = Precess[Convert.ToInt32(RA), Convert.ToInt32(DEC), 2000, Convert.ToInt32(epochnow)];
				}

				return true;
			}
			else
			{
				return false;
			}

		}
	}
}