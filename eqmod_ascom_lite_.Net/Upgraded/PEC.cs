using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UpgradeHelpers.Helpers;
using UpgradeStubs;

namespace Project1
{
	internal static class PEC
	{

		//---------------------------------------------------------------------
		// Copyright © 2008 EQMOD Development Team
		//
		// Permission is hereby granted to use this Software for any purpose
		// including combining with commercial products, creating derivative
		// works, and redistribution of source or binary code, without
		// limitation or consideration. Any redistributed copies of this
		// Software must include the above Copyright Notice.
		//
		// THIS SOFTWARE IS PROVIDED "AS IS". THE AUTHOR OF THIS CODE MAKES NO
		// WARRANTIES REGARDING THIS SOFTWARE, EXPRESS OR IMPLIED, AS TO ITS
		// SUITABILITY OR FITNESS FOR A PARTICULAR PURPOSE.
		//---------------------------------------------------------------------
		//
		// PEC.bas - Periodic Error Correction functions for EQMOD ASCOM Driver
		//
		//
		// Written:  12-Oct-07   Chris Shillito
		//
		// Edits:
		//
		// When      Who     What
		// --------- ---     --------------------------------------------------
		//---------------------------------------------------------------------
		//
		//
		//  SYNOPSIS:
		//
		//  This is a demonstration of a EQ6/ATLAS/EQG direct stepper motor control access
		//  using the EQCONTRL.DLL driver code.
		//
		//  File EQCONTROL.bas contains all the function prototypes of all subroutines
		//  encoded in the EQCONTRL.dll
		//
		//  The EQ6CONTRL.DLL simplifies execution of the Mount controller board stepper
		//  commands.
		//
		//  The mount circuitry needs to be modified for this program to work.
		//  Circuit details can be found at http://sourceforge.net/projects/eq-mod/
		//

		//  DISCLAIMER:

		//  You can use the information on this site COMPLETELY AT YOUR OWN RISK.
		//  The modification steps and other information on this site is provided
		//  to you "AS IS" and WITHOUT WARRANTY OF ANY KIND, express, statutory,
		//  implied or otherwise, including without limitation any warranty of
		//  merchantability or fitness for any particular or intended purpose.
		//  In no event the author will  be liable for any direct, indirect,
		//  punitive, special, incidental or consequential damages or loss of any
		//  kind whether or not the author  has been advised of the possibility
		//  of such loss.

		//  WARNING:

		//  Circuit modifications implemented on your setup could invalidate
		//  any warranty that you may have with your product. Use this
		//  information at your own risk. The modifications involve direct
		//  access to the stepper motor controls of your mount. Any "mis-control"
		//  or "mis-command"  / "invalid parameter" or "garbage" data sent to the
		//  mount could accidentally activate the stepper motors and allow it to
		//  rotate "freely" damaging any equipment connected to your mount.
		//  It is also possible that any garbage or invalid data sent to the mount
		//  could cause its firmware to generate mis-steps pulse sequences to the
		//  motors causing it to overheat. Make sure that you perform the
		//  modifications and testing while there is no physical "load" or
		//  dangling wires on your mount. Be sure to disconnect the power once
		//  this event happens or if you notice any unusual sound coming from
		//  the motor assembly.
		//


		[Serializable]
		public struct CapRecord
		{
			public double time;
			public double MotorPos;
			public double DeltaPos;
			public double DeltaTime;
			public double rate;
			public double pe;
			public double peSmoothed;
			public double peInc;
		}


		[Serializable]
		public struct PECCapDef
		{
			public System.DateTime StartTime;
			public double Period;
			public double Steps;
			public short idx;
			public string FileName;
			public CapRecord[] CapureData;
			public static PECCapDef CreateInstance()
			{
				PECCapDef result = new PECCapDef();
				result.FileName = String.Empty;
				return result;
			}
		}

		[Serializable]
		public struct PECData
		{
			public double time;
			public double PEPosition;
			public double PECPosition;
			public double RawPosn;
			public double signal;
			public double PErate;
			public double PECrate;
			public short cycle;
		}

		[Serializable]
		public struct PECDefinition
		{
			public PECData[] PECCurve;
			public PECData[] PECCurveTmp;
			public double Period;
			public double Steps;
			public double MaxPe;
			public double MinPe;
			public string FileName;
			public short CurrIdx;
			public static PECDefinition CreateInstance()
			{
				PECDefinition result = new PECDefinition();
				result.FileName = String.Empty;
				return result;
			}
		}

		[Serializable]
		public struct PECFileData
		{
			public double time;
			public double Position;
			public double pe;
			public short cycle;
		}


		private static PECCapDef PECCap = PECCapDef.CreateInstance();
		public static PECDefinition PECDef1 = PECDefinition.CreateInstance();
		public static double gLastPE = 0;
		public static bool gPEC_Enabled = false;
		public static bool gUsePEC = false;
		public static double gPEC_Gain = 0; // current gain setting
		public static int gPEC_Capture_Cycles = 0;
		public static int gPEC_filter_lowpass = 0;
		public static int gPEC_mag = 0;
		public static int gPEC_PhaseAdjust = 0; // current phase adjustment (samples)
		public static int gPEC_TimeStampFiles = 0;
		public static int gPEC_DynamicRateAdjust = 0;
		public static string gPEC_FileDir = "";
		public static int gPEC_trace = 0;
		public static int gPEC_AutoApply = 0;
		public static int gPEC_Debug = 0;

		private static string PEC_File = ""; // path and name of PEC file
		private static double threshold = 0; // minimum correction PEC will make
		private static double phaseshift = 0; // current phase shift (steps)
		private static double gMaxRateAdjust = 0; // Maximum correction PEC is allowed to make
		private static double MaxRate = 0; // Fastset rate allowed
		private static double MinRate = 0; // slowest rate allowed
		private static double SID_RATE_NORTH = 0; // 15.041067        ' arcsecs/sec  (60*60*360) / ((23*60*60)+(56*60)+4)
		private static double SID_RATE_SOUTH = 0; // -15.041067       ' arcsecs/sec

		[Serializable]
		public struct PlaybackTimerStatic
		{
			public short PecResyncCount;
			public double CurrRate; // current rate
			public bool Firsttime; // oneshot flag
			public float newpos;
			public float oldpos;
			public bool timerflag; // timer interlock
			public int ringcounter;
			public double StartRingCounter;
			public double LastRingCounter;
			public double StartTime;
			public double lasttime;
			public double RateSumExpected;
			public double RateSumActual;
			public int TraceIdx;
			public string strPlayback;
			public static PlaybackTimerStatic CreateInstance()
			{
				PlaybackTimerStatic result = new PlaybackTimerStatic();
				result.strPlayback = String.Empty;
				return result;
			}
		}

		[Serializable]
		public struct CaptureTimerStatic
		{
			public short State;
			public bool timerflag; // timer interlock
			public int ringcounter;
			public double StartRingCounter;
			public double LastRingCounter;
			public double StartTime;
			public double lasttime;
			public double pe;
			public float yoffset;
			public float lastx;
			public float lasty;
			public bool PenToggle;
			public short InvertCapture;
			public string strCapture;
			public double MaxStepChange;
			public static CaptureTimerStatic CreateInstance()
			{
				CaptureTimerStatic result = new CaptureTimerStatic();
				result.strCapture = String.Empty;
				return result;
			}
		}

		private static CaptureTimerStatic CaptureTimer = CaptureTimerStatic.CreateInstance();
		private static PlaybackTimerStatic PlaybackTimer = PlaybackTimerStatic.CreateInstance();

		private static int TraceFileNum = 0;

		const int ARCSECS_PER_360DEGREES = 1296000; // 360*60*60

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int GetTickCount();

		internal static void PEC_LoPassScroll_Change()
		{
			object oLangDll = null;
			object PECConfigFrm = null;
			//UPGRADE_TODO: (1067) Member HScroll1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			gPEC_filter_lowpass = Convert.ToInt32(PECConfigFrm.HScroll1.Value);
			if (gPEC_filter_lowpass < 9)
			{
				//UPGRADE_TODO: (1067) Member Label3 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.Label3.Caption = oLangDll.GetLangString(6116);
				gPEC_filter_lowpass = 0;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member Label3 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.Label3.Caption = gPEC_filter_lowpass.ToString();
			}

		}

		internal static void PEC_MagScroll_Change()
		{
			object oLangDll = null;
			object PECConfigFrm = null;
			//UPGRADE_TODO: (1067) Member HScroll2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			gPEC_mag = Convert.ToInt32(PECConfigFrm.HScroll2.Value);
			if (gPEC_mag == 0)
			{
				//UPGRADE_TODO: (1067) Member Label2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.Label2.Caption = oLangDll.GetLangString(6116);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member Label2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.Label2.Caption = gPEC_mag.ToString();
			}
		}

		internal static void PEC_PhaseScroll_Change()
		{
			object PECConfigFrm = null;
			//UPGRADE_TODO: (1067) Member Label45 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PECConfigFrm.Label45.Caption = Math.Floor(360 * (Convert.ToDouble(PECConfigFrm.PhaseScroll.Value) / EQMath.gRAWormPeriod)).ToString() + " deg.";
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			gPEC_PhaseAdjust = Convert.ToInt32(PECConfigFrm.PhaseScroll.Value);
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			phaseshift = Convert.ToDouble(PECConfigFrm.PhaseScroll.Value) * (EQMath.gRAWormSteps / EQMath.gRAWormPeriod);
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(PECConfigFrm.PhaseScroll.Enabled))
			{
				PlaybackTimer.ringcounter = Common.EQGetMotorValues(0);
				PECDef1.CurrIdx = (short) GetIdx(PECDef1);
			}

		}

		internal static void PECMode_click()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Ini = Convert.ToString(HC.oPersist.GetIniPath) + "\\EQMOD.ini";
			string key = "[pec]";

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("DYNAMIC_RATE_ADJUST", gPEC_DynamicRateAdjust.ToString(), key, Ini);

		}


		internal static void PEC_Initialise()
		{
			object HC = null;
			object oLangDll = null;
			object PECConfigFrm = null;
			PlaybackTimer.Firsttime = true;
			//UPGRADE_TODO: (1067) Member CmdPecSave is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.CmdPecSave.Enabled = false;
			//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PECConfigFrm.GainScroll.Enabled = false;
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PECConfigFrm.PhaseScroll.Enabled = false;

			PECDef1.PECCurve = ArraysHelper.InitializeArray<PEC.PECData>(Convert.ToInt32(EQMath.gRAWormPeriod) + 1);
			PECDef1.PECCurveTmp = ArraysHelper.InitializeArray<PECData>(Convert.ToInt32(EQMath.gRAWormPeriod) + 1);
			PECDef1.Period = EQMath.gRAWormPeriod;
			PECDef1.Steps = EQMath.gRAWormSteps;

			SID_RATE_NORTH = EQMath.SID_RATE;
			SID_RATE_SOUTH = -1 * EQMath.SID_RATE; // gSiderealRate
			PEC_ReadParams();

			if (EQMath.gHemisphere != 0)
			{
				MaxRate = SID_RATE_SOUTH + gMaxRateAdjust;
				MinRate = SID_RATE_SOUTH - gMaxRateAdjust;
			}
			else
			{
				MaxRate = SID_RATE_NORTH + gMaxRateAdjust;
				MinRate = SID_RATE_NORTH - gMaxRateAdjust;
			}

			if (EQMath.gTot_RA != 0)
			{
				if (EQMath.gRAWormPeriod > 0)
				{
					//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					PECConfigFrm.PhaseScroll.max = EQMath.gRAWormPeriod;
					if (!Import(ref PECDef1))
					{
						KillPec();
					}
				}
			}

			//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PEC_DrawAxis((PictureBox) HC.plot);
			//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PEC_DrawAxis((PictureBox) HC.PlotCap);

			PEC_UpdateControls();

			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PlaybackTimer.strPlayback = Convert.ToString(oLangDll.GetLangString(6117));

		}

		internal static void PEC_Timestamp()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Ini = Convert.ToString(HC.oPersist.GetIniPath) + "\\EQMOD.ini";
			string key = "[pec]";
			double pos = Common.EQGetMotorValues(0);
			string temp = DateTimeHelper.ToString(DateTime.Now);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("SYNCPOS", pos.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("SYNCTIME", temp, key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("STAR_DEC", EQMath.gDec.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("STAR_RA", EQMath.gRA.ToString(), key, Ini);
		}

		internal static void PEC_StartTracking()
		{
			object HC = null;
			object oLangDll = null;

			//UPGRADE_TODO: (1067) Member PECTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PECTimer.Enabled = false;

			gPEC_Enabled = true;
			//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(188));
			PlaybackTimer.CurrRate = 0;
			if (EQMath.gTrackingStatus == 0)
			{
				EQMath.gTrackingStatus = 1;
			}
			PEC_PlotCurve(PECDef1);
			PlaybackTimer.Firsttime = true;
			PlaybackTimer.timerflag = false;
			PlaybackTimer.TraceIdx = 0;
			//UPGRADE_TODO: (1067) Member PECTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PECTimer.Interval = 1000;
			//UPGRADE_TODO: (1067) Member PECTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PECTimer.Enabled = true;

			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{
				TraceFileNum = FileSystem.FreeFile();
				FileSystem.FileClose(TraceFileNum);
				if (gPEC_trace == 1)
				{
					FileSystem.FileClose(TraceFileNum);
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					FileSystem.FileOpen(TraceFileNum, (Convert.ToDouble(HC.oPersist.GetIniPath()) + Double.Parse("\\pectrace_")).ToString() + gPEC_DynamicRateAdjust.ToString() + ".txt", OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
					//        Print TraceFileNum, "Idx WormIndex Motor StepsMoved NextRate ElapsedTime CurrentRate RateAchived RateSumExpected RateSumActual RateError OverallRate"
					FileSystem.PrintLine(TraceFileNum, "Idx WormIdx Motor StepsMoved NextRate OverallRate elapesedtime dt TimerInterval RateError MeasuredRate");
				}
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}

		internal static void PEC_StopTracking()
		{
			object oLangDll = null;
			gPEC_Enabled = false;
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PlaybackTimer.strPlayback = Convert.ToString(oLangDll.GetLangString(6117));
			EQMath.gRA_LastRate = 0;
			FileSystem.FileClose(TraceFileNum);
		}

		internal static void PEC_Unload()
		{
			PEC_WriteParams();
			FileSystem.FileClose(TraceFileNum);
		}

		internal static void PEC_GainScroll_Change()
		{
			object PECConfigFrm = null;
			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{
				//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				gPEC_Gain = Convert.ToDouble(PECConfigFrm.GainScroll.Value) / 10d;
				//UPGRADE_TODO: (1067) Member Label43 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.Label43.Caption = "x" + gPEC_Gain.ToString();
				//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(PECConfigFrm.GainScroll.Enabled))
				{
					if (CalcRates(PECDef1))
					{
						KillPec();
					}
					PEC_WriteParams();
				}
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}
		}
		internal static void PEC_Clear()
		{
			PEC_File = "";
			PEC_WriteParams();
			KillPec();

		}

		internal static void PEC_OnUse()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(HC.CheckPEC.Value))
			{
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(1).Visible = true;
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(0).Visible = false;
				//UPGRADE_TODO: (1067) Member CommandPecPlay is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CommandPecPlay.Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp109;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(1).Visible = false;
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(0).Visible = true;
				//UPGRADE_TODO: (1067) Member CommandPecPlay is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CommandPecPlay.Picture = App.Resources.Resources_EQMOD_ASCOM5.bmp108;
				PEC_StopTracking();
			}

			if (EQMath.gTrackingStatus == 1)
			{
				Tracking.EQStartSidereal();
			}
		}
		internal static bool PEC_SetGain(string sGain)
		{
			object PECConfigFrm = null;
			double dGain = Conversion.Val(sGain);
			//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (dGain >= Convert.ToDouble(PECConfigFrm.GainScroll.min) && dGain <= Convert.ToDouble(PECConfigFrm.GainScroll.max))
			{
				//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.GainScroll.Value = dGain;
				return true;
			}
			else
			{
				return false;
			}
		}

		internal static bool PEC_SetPhase(string sPhase)
		{
			object PECConfigFrm = null;
			double dPhase = Conversion.Val(sPhase);
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (dPhase >= Convert.ToDouble(PECConfigFrm.PhaseScroll.min) && dPhase <= Convert.ToDouble(PECConfigFrm.PhaseScroll.max))
			{
				//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.PhaseScroll.Value = dPhase;
				return true;
			}
			else
			{
				return false;
			}
		}

		internal static void PEC_Load()
		{
			object FileDlg = null;
			//UPGRADE_TODO: (1067) Member filter is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.filter = "*.txt*";
			//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.Show(1);
			//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToString(FileDlg.FileName) != "")
			{
				//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PEC_LoadFile(Convert.ToString(FileDlg.FileName));
			}
		}

		internal static bool PEC_LoadFile(string FileName)
		{
			object HC = null;
			PEC_File = FileName;

			PECDef1.FileName = FileName;
			if (Import(ref PECDef1))
			{
				PEC_WriteParams();
				//UPGRADE_TODO: (1067) Member Change_Display is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Change_Display(3);
				return true;
			}
			else
			{
				KillPec();
				return false;
			}

		}

		internal static void PEC_Save()
		{
			object FileDlg = null;
			object PECConfigFrm = null;
			int i = 0;
			//UPGRADE_TODO: (1067) Member filter is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.filter = "*.txt*";
			//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			FileDlg.Show(1);
			//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToString(FileDlg.FileName) != "")
			{
				//UPGRADE_TODO: (1067) Member FileName is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PEC_File = Convert.ToString(FileDlg.FileName);
				// force a .txt extension
				i = (PEC_File.IndexOf('.') + 1);
				if (i != 0)
				{
					PEC_File = PEC_File.Substring(0, Math.Min(i - 1, PEC_File.Length));
				}
				PEC_File = PEC_File + ".txt";
				PECDef1.FileName = PEC_File;
				//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				Export(PECDef1, Convert.ToInt32(PECConfigFrm.PhaseScroll.Value));
				//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.PhaseScroll.Value = 0;
				PEC_WriteParams();
				if (Import(ref PECDef1))
				{
					PEC_PlotCurve(PECDef1);
				}
				else
				{
					KillPec();
				}
			}
		}

		internal static bool PEC_SaveFile(string FileName, ref PECDefinition PECDef)
		{
			PECDef.FileName = FileName;
			return Export(PECDef, 0);
		}

		internal static void PEC_Timer()
		{
			object HC = null;
			object oLangDll = null;
			double rate = 0;
			double TimeSlip = 0;
			int StepsMoved = 0;
			double RateError = 0;
			double OverallRate = 0;
			double MeasuredRate = 0;

			double elapsedtime = 0;
			double curr = 0; //current time
			double dt = 0; //delta time
			double TimerInterval = 0;

			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{

				if (!PlaybackTimer.timerflag)
				{
					PlaybackTimer.timerflag = true;

					if (PlaybackTimer.Firsttime)
					{
						PlaybackTimer.lasttime = Environment.TickCount;
						PlaybackTimer.StartTime = PlaybackTimer.lasttime;
						PlaybackTimer.ringcounter = Common.EQGetMotorValues(0);
						PlaybackTimer.LastRingCounter = PlaybackTimer.ringcounter;
						PlaybackTimer.StartRingCounter = PlaybackTimer.ringcounter;

						// force immediate rate update
						PECDef1.CurrIdx = (short) GetIdx(PECDef1);
						rate = PECDef1.PECCurve[PECDef1.CurrIdx].PECrate;
						if (gPEC_Enabled && EQMath.gTrackingStatus == 1)
						{
							PEC_MoveAxis(0, rate);
						}

						//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.plot.DrawMode = 7;
						//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						PlaybackTimer.newpos = (float) (PECDef1.CurrIdx * Convert.ToDouble(HC.plot.ScaleWidth) / PECDef1.Period);
						//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.plot.Line(PlaybackTimer.newpos, 0, PlaybackTimer.newpos, HC.plot.ScaleHeight, Color.Red);
						PlaybackTimer.oldpos = PlaybackTimer.newpos;


						PlaybackTimer.RateSumActual = 0;
						PlaybackTimer.RateSumExpected = rate;
						PlaybackTimer.CurrRate = rate;

						PlaybackTimer.Firsttime = false;
					}
					else
					{
						curr = Environment.TickCount; // read current system time
						//determine the diff between times
						elapsedtime = Math.Abs(curr - PlaybackTimer.StartTime) / 1000d;

						dt = Math.Abs(curr - PlaybackTimer.lasttime) / 1000d; //determine the diff between times
						PlaybackTimer.lasttime = curr;

						//            If gTrackingStatus <> 0 Then
						//only maintain pe trace updates if we're tracking

						// only apply rate changes if we're tracking at sidreal and PEC is on
						if (gPEC_Enabled && EQMath.gTrackingStatus == 1)
						{

							PlaybackTimer.ringcounter = Common.EQGetMotorValues(0);
							StepsMoved = Convert.ToInt32(PlaybackTimer.ringcounter - PlaybackTimer.LastRingCounter);

							PECDef1.CurrIdx = (short) (PECDef1.CurrIdx + 1);
							if (PECDef1.CurrIdx >= PECDef1.Period)
							{
								PECDef1.CurrIdx = 0;
							}

							PlaybackTimer.PecResyncCount = (short) (PlaybackTimer.PecResyncCount + 1);
							if (PlaybackTimer.PecResyncCount >= PECDef1.Period)
							{
								PECDef1.CurrIdx = (short) GetIdx(PECDef1);
								TimerInterval = 1000;
								PlaybackTimer.StartTime = Environment.TickCount;
								PlaybackTimer.StartRingCounter = PlaybackTimer.ringcounter;
								PlaybackTimer.RateSumExpected = 0;
								PlaybackTimer.RateSumActual = 0;
								RateError = 0;
								PEC_PlotCurve(PECDef1);
							}
							else
							{
								TimeSlip = elapsedtime - PlaybackTimer.PecResyncCount;
								MeasuredRate = (StepsMoved / dt) * (1296000 / EQMath.gTot_RA);
								PlaybackTimer.RateSumActual += MeasuredRate;
								RateError = PlaybackTimer.RateSumExpected - PlaybackTimer.RateSumActual;

								if (TimeSlip > 0)
								{
									TimerInterval = 1000 - (TimeSlip * 1000);
									if (TimerInterval < 100)
									{
										TimerInterval = 100;
									}
								}
								else
								{
									TimerInterval = 1000;
								}
							}
							//UPGRADE_TODO: (1067) Member PECTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.PECTimer.Interval = TimerInterval;

							PlaybackTimer.LastRingCounter = PlaybackTimer.ringcounter;


							// Get next rate to apply.
							rate = PECDef1.PECCurve[PECDef1.CurrIdx].PECrate;
							PlaybackTimer.RateSumExpected += rate;
							if (rate != PlaybackTimer.CurrRate)
							{
								// apply the min/max limits - just in case there's
								// an error in the rate calculations this prevents'
								// the mount from ever slewing wildly!
								if (rate > MaxRate)
								{
									rate = MaxRate;
								}
								else
								{
									if (rate < MinRate)
									{
										rate = MinRate;
									}
								}

								if (EQMath.gHemisphere == 0)
								{
									//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									PlaybackTimer.strPlayback = Convert.ToString(oLangDll.GetLangString(6118)) + " " + (rate - EQMath.SID_RATE).ToString("N3");
								}
								else
								{
									//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									PlaybackTimer.strPlayback = Convert.ToString(oLangDll.GetLangString(6118)) + " " + (-rate - EQMath.SID_RATE).ToString("N3");
								}

								switch(gPEC_DynamicRateAdjust)
								{
									case 1 : 
										PEC_MoveAxis(0, rate + RateError); 
										break;
									case 0 : 
										PEC_MoveAxis(0, rate); 
										break;
								}
								PlaybackTimer.CurrRate = rate;
							}
						}
						else
						{
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							PlaybackTimer.strPlayback = Convert.ToString(oLangDll.GetLangString(6117));
							PlaybackTimer.ringcounter = Convert.ToInt32(EQMath.gEmulRA);
							PECDef1.CurrIdx = (short) GetIdx(PECDef1);
						}

						//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.plot.DrawMode = 7;
						//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						PlaybackTimer.newpos = (float) (PECDef1.CurrIdx * Convert.ToDouble(HC.plot.ScaleWidth) / PECDef1.Period);
						if (Convert.ToInt32(PlaybackTimer.oldpos) != Convert.ToInt32(PlaybackTimer.newpos))
						{
							//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.plot.Line(PlaybackTimer.oldpos, 0, PlaybackTimer.oldpos, HC.plot.ScaleHeight, Color.Red);
							//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.plot.Line(PlaybackTimer.newpos, 0, PlaybackTimer.newpos, HC.plot.ScaleHeight, Color.Red);
							PlaybackTimer.oldpos = PlaybackTimer.newpos;
						}

						//           End If

						if (gPEC_trace == 1)
						{
							OverallRate = ((PlaybackTimer.ringcounter - PlaybackTimer.StartRingCounter) * 1296000 / EQMath.gTot_RA) / elapsedtime;
							FileSystem.PrintLine(TraceFileNum, PlaybackTimer.TraceIdx.ToString() + " " + PECDef1.CurrIdx.ToString() + " " + PlaybackTimer.ringcounter.ToString() + " " + StepsMoved.ToString() + " " + PlaybackTimer.CurrRate.ToString() + " " + OverallRate.ToString() + " " + elapsedtime.ToString() + " " + dt.ToString() + " " + Convert.ToInt32(TimerInterval).ToString() + " " + RateError.ToString() + " " + MeasuredRate.ToString());
						}

					}

					PlaybackTimer.timerflag = false;
				}
				else
				{
					FileSystem.PrintLine(TraceFileNum, "TimerOverflow");
				}
				PlaybackTimer.TraceIdx++;
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}
		internal static void PEC_CaptureTimer()
		{
			object HC = null;
			object oLangDll = null;
			object PECConfigFrm = null;
			double TimeSlip = 0;
			double elapsedtime = 0;
			double curr = 0; //current time
			double dt = 0; //delta time
			double motor = 0;
			double TimerInterval = 0;
			float X = 0;
			float Y = 0;


			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{

				if (!CaptureTimer.timerflag)
				{
					CaptureTimer.timerflag = true;


					switch(CaptureTimer.State)
					{
						case 0 : 
							// initialise 
							//UPGRADE_TODO: (1067) Member ComboPecCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
							gPEC_Capture_Cycles = Convert.ToInt32(PECConfigFrm.ComboPecCap.ItemData(PECConfigFrm.ComboPecCap.ListIndex)); 
							PECCap.CapureData = ArraysHelper.InitializeArray<CapRecord>(Convert.ToInt32(EQMath.gRAWormPeriod * gPEC_Capture_Cycles) + 1); 
							PECCap.Period = EQMath.gRAWormPeriod; 
							PECCap.Steps = EQMath.gRAWormSteps; 
							PECCap.idx = 0; 
							 
							CaptureTimer.lasttime = Environment.TickCount; 
							CaptureTimer.StartTime = CaptureTimer.lasttime; 
							CaptureTimer.ringcounter = Common.EQGetMotorValues(0); 
							CaptureTimer.LastRingCounter = CaptureTimer.ringcounter; 
							CaptureTimer.StartRingCounter = CaptureTimer.ringcounter; 
							CaptureTimer.pe = 0; 
							CaptureTimer.lastx = 0; 
							//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
							CaptureTimer.lasty = (float) (Convert.ToDouble(HC.PlotCap.ScaleHeight) / 2d); 
							CaptureTimer.yoffset = 0; 
							// what is the max epected change in motor position 
							// well in a second we could guide 15 arcsecs and track move by 15 arcsec 
							// so call it 40 arc secs difference is possible 
							// 1 arc sec = CDbl(gTot_RA) / 1296000 steps 
							CaptureTimer.MaxStepChange = 40 * EQMath.gTot_RA / 1296000d; 
							//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
							HC.PlotCap.Cls(); 
							//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
							HC.PlotCap.DrawMode = 13; 
							CaptureTimer.State = 1; 
							 
							break;
						case 1 : 
							// capture 
							if (EQMath.gTrackingStatus == 1)
							{
								motor = Common.EQGetMotorValues(0);
								curr = Environment.TickCount; // read current system time
								//determine the diff between times
								elapsedtime = Math.Abs(curr - CaptureTimer.StartTime) / 1000d;
								dt = Math.Abs(curr - CaptureTimer.lasttime) / 1000d; //determine the diff between times

								if (Math.Abs(motor - CaptureTimer.LastRingCounter) < (CaptureTimer.MaxStepChange * dt))
								{
									// If motor <= 16777215 Then

									//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									CaptureTimer.strCapture = Convert.ToString(oLangDll.GetLangString(6119)) + " " + PECCap.idx.ToString() + "/" + PECCap.CapureData.GetUpperBound(0).ToString();

									CaptureTimer.lasttime = curr;
									PECCap.CapureData[PECCap.idx].MotorPos = motor;
									PECCap.CapureData[PECCap.idx].DeltaPos = PECCap.CapureData[PECCap.idx].MotorPos - CaptureTimer.LastRingCounter;
									PECCap.CapureData[PECCap.idx].DeltaTime = dt;
									CaptureTimer.LastRingCounter = PECCap.CapureData[PECCap.idx].MotorPos;
									PECCap.CapureData[PECCap.idx].time = elapsedtime;
									PECCap.CapureData[PECCap.idx].rate = (PECCap.CapureData[PECCap.idx].DeltaPos / PECCap.CapureData[PECCap.idx].DeltaTime) * (1296000 / EQMath.gTot_RA);

									if (CaptureTimer.InvertCapture != 0)
									{
										PECCap.CapureData[PECCap.idx].peInc = PECCap.CapureData[PECCap.idx].rate - EQMath.gSiderealRate;
									}
									else
									{
										PECCap.CapureData[PECCap.idx].peInc = EQMath.gSiderealRate - PECCap.CapureData[PECCap.idx].rate;
									}


									CaptureTimer.pe += PECCap.CapureData[PECCap.idx].peInc;
									PECCap.CapureData[PECCap.idx].pe = CaptureTimer.pe;
									X = (float) (PECCap.idx % Convert.ToInt32(PECCap.Period));
									if (X == 0)
									{
										//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										CaptureTimer.yoffset = (float) (CaptureTimer.pe * Convert.ToDouble(HC.PlotCap.ScaleHeight) / 180d);
										//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										CaptureTimer.lasty = (float) (Convert.ToDouble(HC.PlotCap.ScaleHeight) / 2d);
										CaptureTimer.lastx = 0;
										if (CaptureTimer.PenToggle)
										{
											//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
											HC.PlotCap.ForeColor = ColorTranslator.ToOle(Color.Lime);
											CaptureTimer.PenToggle = false;
										}
										else
										{
											//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
											HC.PlotCap.ForeColor = ColorTranslator.ToOle(Color.Red);
											CaptureTimer.PenToggle = true;
										}
									}
									//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									X = (float) (X * Convert.ToDouble(HC.PlotCap.ScaleWidth) / PECCap.Period);
									if (Convert.ToInt32(X) != Convert.ToInt32(CaptureTimer.lastx))
									{
										//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										Y = (float) (Convert.ToDouble(HC.PlotCap.ScaleHeight) / 2d - (CaptureTimer.pe * Convert.ToDouble(HC.PlotCap.ScaleHeight) / 180d) + CaptureTimer.yoffset);
										//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										HC.PlotCap.Line(X + 1, Convert.ToDouble(HC.PlotCap.ScaleHeight) / 2d, X + 1, Y); //, vbRed
										//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										HC.PlotCap.Line(X, 0, X, HC.PlotCap.ScaleHeight, Color.Black);
										//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										HC.PlotCap.Line(CaptureTimer.lastx, CaptureTimer.lasty, X, Y); //, vbMagenta
										CaptureTimer.lastx = X;
										CaptureTimer.lasty = Y;
									}
									//UPGRADE_TODO: (1067) Member PlotCap is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									PEC_DrawAxis((PictureBox) HC.PlotCap);

									PECCap.idx = (short) (PECCap.idx + 1);
									if (PECCap.idx < PECCap.CapureData.GetUpperBound(0))
									{
										TimeSlip = elapsedtime - PECCap.idx;
										if (TimeSlip > 0)
										{
											TimerInterval = 1000 - (TimeSlip * 1000);
											if (TimerInterval < 100)
											{
												TimerInterval = 100;
											}
										}
										else
										{
											TimerInterval = 1000;
										}
										//UPGRADE_TODO: (1067) Member PECCapTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										HC.PECCapTimer.Interval = TimerInterval;
									}
									else
									{
										// capture complete
										CaptureTimer.State = 2;
										//UPGRADE_TODO: (1067) Member PECCapTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										HC.PECCapTimer.Enabled = false;
										//UPGRADE_TODO: (1067) Member CheckCapPec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
										HC.CheckCapPec.Value = 0;
										CaptureTimer.strCapture = "";
									}
								}
								else
								{
									// error reading motor position!
								}
							}
							else
							{
								// kill capture if not tracking
								//UPGRADE_TODO: (1067) Member CheckCapPec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.CheckCapPec.Value = 0;
							} 
							 
							break;
						case 2 : 
							// capture complete 
							//UPGRADE_TODO: (1067) Member PECCapTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
							HC.PECCapTimer.Enabled = false; 
							CaptureTimer.strCapture = ""; 
							 
							break;
						default:
							//UPGRADE_TODO: (1067) Member CheckCapPec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
							HC.CheckCapPec.Value = 0; 
							 
							break;
					}

					CaptureTimer.timerflag = false;
				}
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}

		internal static void PEC_UpdateControls()
		{
			object PECConfigFrm = null;
			//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (gPEC_Gain * 10 > Convert.ToDouble(PECConfigFrm.GainScroll.max))
			{
				gPEC_Gain = 1;
			}
			//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PECConfigFrm.GainScroll.Value = gPEC_Gain * 10;
			PEC_GainScroll_Change();
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PECConfigFrm.PhaseScroll.Value = gPEC_PhaseAdjust;
			PEC_PhaseScroll_Change();
		}

		internal static void PEC_ReadParams()
		{
			object HC = null;
			object PECConfigFrm = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Ini = Convert.ToString(HC.oPersist.GetIniPath) + "\\EQMOD.ini";
			string key = "[pec]";

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("WORKING_DIR", key, Ini));
			if (tmptxt != "")
			{
				gPEC_FileDir = tmptxt;
			}
			else
			{
				// no value exists - create a default
				gPEC_FileDir = Interaction.Environ("ProgramFiles") + "\\EQMOD\\PEC\\";
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("WORKING_DIR", gPEC_FileDir, key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("INVERT_CAPTURE", key, Ini));
			if (tmptxt != "")
			{
				CaptureTimer.InvertCapture = (short) Convert.ToInt32(Double.Parse(tmptxt));
			}
			else
			{
				// no value exists - create a default
				CaptureTimer.InvertCapture = 0;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("INVERT_CAPTURE", CaptureTimer.InvertCapture.ToString(), key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("TIMESTAMP_FILES", key, Ini));
			if (tmptxt != "")
			{
				gPEC_TimeStampFiles = Convert.ToInt32(Double.Parse(tmptxt));
			}
			else
			{
				// no value exists - create a default
				gPEC_TimeStampFiles = 0;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("TIMESTAMP_FILES", gPEC_TimeStampFiles.ToString(), key, Ini);
			}


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("FILTER_LOPASS", key, Ini));
			if (tmptxt != "")
			{
				gPEC_filter_lowpass = Convert.ToInt32(Double.Parse(tmptxt));
				if (gPEC_filter_lowpass < 10)
				{
					gPEC_filter_lowpass = 10;
				}
			}
			else
			{
				// no value exists - create a default
				gPEC_filter_lowpass = 30;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("FILTER_LOPASS", gPEC_filter_lowpass.ToString(), key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("FILTER_MAG", key, Ini));
			if (tmptxt != "")
			{
				gPEC_mag = Convert.ToInt32(Double.Parse(tmptxt));
			}
			else
			{
				// no value exists - create a default
				gPEC_mag = 10;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("FILTER_MAG", gPEC_mag.ToString(), key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("CAPTURE_CYCLES", key, Ini));
			if (tmptxt != "")
			{
				gPEC_Capture_Cycles = Convert.ToInt32(Double.Parse(tmptxt));
			}
			else
			{
				// no value exists - create a default
				gPEC_Capture_Cycles = 5;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("CAPTURE_CYCLES", gPEC_Capture_Cycles.ToString(), key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("AUTO_APPLY", key, Ini));
			if (tmptxt != "")
			{
				if (Convert.ToInt32(Double.Parse(tmptxt)) == 1)
				{
					gPEC_AutoApply = 1;
				}
				else
				{
					gPEC_AutoApply = 0;
				}
			}
			else
			{
				// no value exists - create a default
				gPEC_AutoApply = 1;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("AUTO_APPLY", gPEC_AutoApply.ToString(), key, Ini);
			}



			//    tmptxt = HC.oPersist.ReadIniValueEx("FILTER_HIPASS", key, Ini)
			//    If tmptxt <> "" Then
			//        filter_hipass = CInt(tmptxt)
			//    Else
			//       ' no value exists - create a default
			//        filter_hipass = 1000#
			//        Call HC.oPersist.WriteIniValueEx("FILTER_HIPASS", CStr(filter_hipass), key, Ini)
			//    End If

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("THRESHOLD", key, Ini));
			if (tmptxt != "")
			{
				threshold = Double.Parse(tmptxt);
			}
			else
			{
				// no value exists - create a default
				threshold = 0d;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("THRESHOLD", threshold.ToString(), key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("GAIN", key, Ini));
			if (tmptxt != "")
			{
				gPEC_Gain = Double.Parse(tmptxt);
			}
			else
			{
				// no value exists - create a default
				gPEC_Gain = 1d;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("GAIN", gPEC_Gain.ToString(), key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PEC_File = Convert.ToString(HC.oPersist.ReadIniValueEx("PEC_FILE", key, Ini));
			PECDef1.FileName = PEC_File;
			if (PECDef1.FileName == "")
			{
				//       PEC_File = "pec.txt"
				// no value exists - create a default
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("PEC_FILE", "", key, Ini);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("PHASE_SHIFT", key, Ini));
			if (tmptxt != "")
			{
				gPEC_PhaseAdjust = Convert.ToInt32(Double.Parse(tmptxt));
			}
			else
			{
				// no value exists - create a default
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("PHASE_SHIFT", "0", key, Ini);
				gPEC_PhaseAdjust = 0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("MAX_RATEADJUST", key, Ini));
			if (tmptxt != "")
			{
				gMaxRateAdjust = Double.Parse(tmptxt);
				if (gMaxRateAdjust < 3)
				{
					// fix to increse previous default of 1
					gMaxRateAdjust = 3;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValueEx("MAX_RATEDAJUST", "3", key, Ini);
				}
			}
			else
			{
				// no value exists - create a default
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("MAX_RATEDAJUST", "3", key, Ini);
				gMaxRateAdjust = 3;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("DYNAMIC_RATE_ADJUST", key, Ini));
			if (tmptxt != "")
			{
				gPEC_DynamicRateAdjust = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("DYNAMIC_RATE_ADJUST", "0", key, Ini);
				gPEC_DynamicRateAdjust = 0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("DEBUG", key, Ini));
			if (tmptxt == "")
			{
				gPEC_Debug = 0;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("DEBUG", "0", key, Ini);
			}
			else
			{
				gPEC_Debug = Convert.ToInt32(Conversion.Val(tmptxt));
			}

			switch(gPEC_Debug)
			{
				case 1 : 
					//UPGRADE_TODO: (1067) Member CheckTracePec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					PECConfigFrm.CheckTracePec.Visible = true; 
					//UPGRADE_TODO: (1067) Member PECMethodCombo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					PECConfigFrm.PECMethodCombo.Visible = true; 
					break;
				default:
					//UPGRADE_TODO: (1067) Member CheckTracePec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					PECConfigFrm.CheckTracePec.Visible = false; 
					//UPGRADE_TODO: (1067) Member PECMethodCombo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					PECConfigFrm.PECMethodCombo.Visible = false; 
					break;
			}

		}
		internal static void PEC_WriteParams()
		{
			object PECConfigFrm = null;
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Ini = Convert.ToString(HC.oPersist.GetIniPath) + "\\EQMOD.ini";
			string key = "[pec]";
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("THRESHOLD", threshold.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("GAIN", gPEC_Gain.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("PEC_FILE", PECDef1.FileName, key, Ini);
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("PHASE_SHIFT", Convert.ToString(PECConfigFrm.PhaseScroll.Value), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("CAPTURE_CYCLES", gPEC_Capture_Cycles.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("FILTER_LOPASS", gPEC_filter_lowpass.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("FILTER_MAG", gPEC_mag.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("TIMESTAMP_FILES", gPEC_TimeStampFiles.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("AUTO_APPLY", gPEC_AutoApply.ToString(), key, Ini);
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("WORKING_DIR", gPEC_FileDir, key, Ini);

		}

		internal static double NormalisePosition(double Position, double wormsteps)
		{
			// Normalisation is intended for raw stepper position which are centered
			// around H80000. Once normalised the range will be 0-50132.

			if (Position > wormsteps)
			{
				//        While position < gRAEncoder_Zero_pos
				//            position = position + gTot_RA
				//        Wend
				//        position = position - gRAEncoder_Zero_pos
				return Convert.ToInt32(Position) % (Convert.ToInt32(wormsteps));
			}
			else
			{
				// don't take into account the H80000 ofset if the position
				// is already normalised!
				return Position;
			}

		}
		private static bool Import(ref PECDefinition PECDef)
		{
			bool result = false;
			object HC = null;
			object oLangDll = null;
			object PECConfigFrm = null;
			string temp1 = "";
			string temp2 = "";
			int lineno = 0;
			int idx = 0;
			int pos = 0;
			PECData CurveMin = new PECData();
			PECData CurveMax = new PECData();
			double MotorPos = 0;
			int CycleCount = 0;
			bool error = false;
			double drift = 0;
			double wp = 0;
			int NF1 = 0;

			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (ImportError)");

			NF1 = FileSystem.FreeFile();
			error = false;

			if (PECDef.FileName == "")
			{
				error = true;
				goto errCheck;
			}

			FileSystem.FileClose(NF1);
			FileSystem.FileOpen(NF1, PECDef.FileName, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);

			lineno = 0;
			idx = 0;
			while (!FileSystem.EOF(NF1))
			{
				temp1 = FileSystem.LineInput(NF1);
				if (lineno > 0)
				{
					if (temp1.StartsWith("!"))
					{
						// parse parameters
						pos = (temp1.IndexOf('=') + 1);
						if (pos != 0)
						{
							temp2 = temp1.Substring(0, Math.Min(pos - 1, temp1.Length));
							if (temp2 == "!WormPeriod")
							{
								temp1 = temp1.Substring(Math.Max(temp1.Length - (Strings.Len(temp1) - pos), 0));
								wp = Math.Floor(Double.Parse(temp1) + 0.5d);
								PECDef.Period = wp;
								PECDef.PECCurve = ArraysHelper.InitializeArray<PEC.PECData>(Convert.ToInt32(wp) + 1);
								PECDef.PECCurveTmp = ArraysHelper.InitializeArray<PECData>(Convert.ToInt32(wp) + 1);
								double tempForEndVar = (wp - 1);
								for (idx = 0; idx <= tempForEndVar; idx++)
								{
									PECDef.PECCurve[idx].signal = 0;
								}
								idx = 0;
								// apply a default if steps per worm isn't in the pec file
								PECDef.Steps = EQMath.gRAWormSteps;
							}
							else
							{
								if (temp2 == "!StepsPerWorm")
								{
									temp1 = temp1.Substring(Math.Max(temp1.Length - (Strings.Len(temp1) - pos), 0));
									PECDef.Steps = Math.Floor(Double.Parse(temp1) + 0.5d);
								}
							}
						}
					}
					else
					{
						if (!temp1.StartsWith("#"))
						{
							// replace tabs with spaces
							temp1 = StringsHelper.Replace(temp1, "\t", " ", 1, -1, CompareMethod.Binary);
							pos = (temp1.IndexOf(' ') + 1);
							if (pos != 0)
							{
								temp2 = temp1.Substring(0, Math.Min(pos - 1, temp1.Length));
								temp1 = temp1.Substring(Math.Max(temp1.Length - (Strings.Len(temp1) - pos), 0));
								PECDef.PECCurve[idx].time = Double.Parse(temp2);

								pos = (temp1.IndexOf(' ') + 1);
								if (pos != 0)
								{
									temp2 = temp1.Substring(0, Math.Min(pos - 1, temp1.Length));
									temp1 = temp1.Substring(Math.Max(temp1.Length - (Strings.Len(temp1) - pos), 0));
									if (CycleCount == 0)
									{
										// store the motor positions for the first cycle
										MotorPos = Double.Parse(temp2);
										PECDef.PECCurve[idx].RawPosn = MotorPos;
										PECDef.PECCurve[idx].PEPosition = NormalisePosition(Math.Floor(MotorPos), PECDef.Steps);
									}
									PECDef.PECCurve[idx].signal = (PECDef.PECCurve[idx].signal + Double.Parse(temp1));
									PECDef.PECCurve[idx].cycle = (short) (CycleCount + 1);
								}
								idx++;
								if (idx == wp)
								{
									CycleCount++;
									idx = 0;
								}
							}
						}
					}
				}
				lineno++;
			}


			FileSystem.FileClose(NF1);
			if (error)
			{
				goto errCheck;
			}

			if (CycleCount >= 1)
			{
				// average the signal
				double tempForEndVar2 = (PECDef.Period - 1);
				for (idx = 0; idx <= tempForEndVar2; idx++)
				{
					PECDef.PECCurve[idx].signal /= ((double) PECDef.PECCurve[idx].cycle);
				}

				// remove any net cycle offset from the PEC curve
				drift = (PECDef.PECCurve[Convert.ToInt32(PECDef.Period - 1)].signal - PECDef.PECCurve[0].signal) / (PECDef.Period + 1);
				CurveMin.signal = 100;
				CurveMax.signal = -100;
				double tempForEndVar3 = (PECDef.Period - 1);
				for (idx = 0; idx <= tempForEndVar3; idx++)
				{
					PECDef.PECCurve[idx].signal -= idx * drift;
					if (PECDef.PECCurve[idx].signal > CurveMax.signal)
					{
						CurveMax = PECDef.PECCurve[idx];
					}
					if (PECDef.PECCurve[idx].signal < CurveMin.signal)
					{
						CurveMin = PECDef.PECCurve[idx];
					}
				}

				PECDef.CurrIdx = 0;

				PECDef.MaxPe = CurveMax.signal;
				PECDef.MinPe = CurveMin.signal;
				PEC_PlotCurve(PECDef);

				// caluculate correction rates to be used.
				error = CalcRates(PECDef);

			}
			else
			{
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message("PECImport: Insufficient PEC samples!");
				error = true;
			}

			goto errCheck;

			ImportError:
			FileSystem.FileClose(NF1);
			error = true;
			//UPGRADE_WARNING: (2081) Err.Number has a new behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2081
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message("PECImport: ErrNo." + Information.Err().Number.ToString());
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message(Information.Err().Description);
			Information.Err().Clear();

			errCheck:
			if (error)
			{
				result = false;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member PECTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.PECTimer.Enabled = true;
				//UPGRADE_TODO: (1067) Member CmdPecSave is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdPecSave.Enabled = true;
				//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.GainScroll.Enabled = true;
				//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.PhaseScroll.Enabled = true;
				//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckPEC.Enabled = true;
				//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckPEC.Value = 1;
				//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CmdTrack(1).Enabled = true;
				result = true;
				// set the PEC frame caption to show file name
				pos = PEC_File.LastIndexOf("\\") + 1;
				temp1 = PEC_File.Substring(Math.Max(PEC_File.Length - (Strings.Len(PEC_File) - pos), 0));
				//UPGRADE_TODO: (1067) Member Frame9 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Frame9.Caption = Convert.ToString(oLangDll.GetLangString(19)) + " " + temp1;
			}

			return result;
		}

		private static void KillPec()
		{
			object HC = null;
			object oLangDll = null;
			object PECConfigFrm = null;
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message("PEC: Disabled");
			//UPGRADE_TODO: (1067) Member Frame9 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Frame9.Caption = oLangDll.GetLangString(19);
			//UPGRADE_TODO: (1067) Member CmdTrack is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.CmdTrack(1).Visible = false;
			//UPGRADE_TODO: (1067) Member PECTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PECTimer.Enabled = false;
			//UPGRADE_TODO: (1067) Member CmdPecSave is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.CmdPecSave.Enabled = false;
			//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PECConfigFrm.GainScroll.Enabled = false;
			//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PECConfigFrm.PhaseScroll.Enabled = false;
			//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.CheckPEC.Enabled = false;
			//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.CheckPEC.Value = 0;
			gPEC_Enabled = false;
			//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.plot.Cls();
		}

		private static bool Export(PECDefinition PECDef, int phaseshift)
		{
			bool result = false;
			object HC = null;

			int idx = 0;
			int pos = 0;
			int NF1 = 0;

			try
			{
				result = true;
				NF1 = FileSystem.FreeFile();

				FileSystem.FileClose(NF1);
				FileSystem.FileOpen(NF1, PECDef.FileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
				//        Print NF1, Date$ & " " & Time$
				//UPGRADE_TODO: (1067) Member MainLabel is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileSystem.PrintLine(NF1, "# " + Convert.ToString(HC.MainLabel.Caption));
				FileSystem.PrintLine(NF1, "!WormPeriod=" + PECDef.Period.ToString());
				FileSystem.PrintLine(NF1, "!StepsPerWorm=" + PECDef.Steps.ToString());
				FileSystem.PrintLine(NF1, "# time - motor - smoothed PE");
				int tempForEndVar = PECDef.PECCurve.GetUpperBound(0) - 1;
				for (idx = 0; idx <= tempForEndVar; idx++)
				{
					// apply local phase shift
					pos = Convert.ToInt32((idx + phaseshift) % Convert.ToInt32(PECDef.Period));
					FileSystem.PrintLine(NF1, Strings.FormatNumber(idx, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PECDef.PECCurve[idx].PEPosition, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PECDef.PECCurve[pos].signal, 4, TriState.UseDefault, TriState.UseDefault, TriState.False));
				}
			}
			catch
			{
				result = false;
			}
			finally
			{
				FileSystem.FileClose(NF1);
			}
			return result;
		}

		internal static bool PEC_Write_Table(double Index, double Position, double signal)
		{
			object HC = null;
			int i = 0;
			if (Index == 0)
			{
				// first element being written - clear out existing data
				PECDef1.PECCurveTmp = ArraysHelper.InitializeArray<PECData>(Convert.ToInt32(PECDef1.Period) + 1);
			}

			if (Index >= PECDef1.PECCurveTmp.GetUpperBound(0))
			{
				return false;
			}

			if (Position > PECDef1.Steps)
			{
				return false;
			}

			// data is only written to the temporary curve
			PECDef1.PECCurveTmp[Convert.ToInt32(Index)].time = Index;
			PECDef1.PECCurveTmp[Convert.ToInt32(Index)].signal = signal;
			PECDef1.PECCurveTmp[Convert.ToInt32(Index)].PEPosition = Position;
			PECDef1.PECCurveTmp[Convert.ToInt32(Index)].RawPosn = Position;

			if (Index == PECDef1.PECCurveTmp.GetUpperBound(0) - 1)
			{
				// when the last index is written we save the file.
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PEC_File = Convert.ToString(HC.oPersist.GetIniPath) + "\\PEC.txt";
				PECDef1.FileName = PEC_File;
				// copy temp store to real curve
				double tempForEndVar = PECDef1.Period - 1;
				for (i = 0; i <= tempForEndVar; i++)
				{
					PECDef1.PECCurve[i] = PECDef1.PECCurveTmp[i];
				}
				// write curve to file
				Export(PECDef1, 0);
				// save PEC file name
				PEC_WriteParams();
				// load from file
				if (Import(ref PECDef1))
				{
					// update display
					PEC_PlotCurve(PECDef1);
				}
				else
				{
					KillPec();
				}
			}
			return true;

		}

		private static void PEC_DrawAxis(PictureBox plot)
		{
			int mid = Convert.ToInt32(plot.ClientRectangle.Height * 15 / 2f);
			using (Graphics g = plot.CreateGraphics())
			{

				g.DrawLine(new Pen(Color.FromArgb(255, 128, 0)), 0, mid, Convert.ToInt32(plot.ClientRectangle.Width * 15), mid);
			}
			using (Graphics g = plot.CreateGraphics())
			{

				g.DrawLine(new Pen(Color.FromArgb(255, 128, 0)), 0, (mid) - 4, 0, mid + 2);
			}
			using (Graphics g = plot.CreateGraphics())
			{

				g.DrawLine(new Pen(Color.FromArgb(255, 128, 0)), Convert.ToInt32(plot.ClientRectangle.Width * 15 * 0.25d), mid - 2, Convert.ToInt32(plot.ClientRectangle.Width * 15 * 0.25d), mid + 2);
			}
			using (Graphics g = plot.CreateGraphics())
			{

				g.DrawLine(new Pen(Color.FromArgb(255, 128, 0)), Convert.ToInt32(plot.ClientRectangle.Width * 15 * 0.5d), mid - 2, Convert.ToInt32(plot.ClientRectangle.Width * 15 * 0.5d), mid + 2);
			}
			using (Graphics g = plot.CreateGraphics())
			{

				g.DrawLine(new Pen(Color.FromArgb(255, 128, 0)), Convert.ToInt32(plot.ClientRectangle.Width * 15 * 0.75d), (mid) - 2, Convert.ToInt32(plot.ClientRectangle.Width * 15 * 0.75d), mid + 2);
			}
			using (Graphics g = plot.CreateGraphics())
			{

				g.DrawLine(new Pen(Color.FromArgb(255, 128, 0)), Convert.ToInt32(plot.ClientRectangle.Width * 15 - 1), mid - 2, Convert.ToInt32(plot.ClientRectangle.Width * 15 - 1), mid + 2);
			}
		}

		private static void PEC_PlotCurve(PECDefinition PECDef)
		{
			object HC = null;
			int idx = 0;
			double oldval = 0;
			double newval = 0;

			double range = PECDef.MaxPe - PECDef.MinPe;
			//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.plot.Cls();
			//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			PEC_DrawAxis((PictureBox) HC.plot);
			//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			int mid = Convert.ToInt32(Convert.ToDouble(HC.plot.ScaleHeight) / 2d);

			//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			double hscale = Convert.ToDouble(HC.plot.ScaleWidth) / PECDef.Period;
			if (range > 0)
			{
				//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				oldval = mid - PECDef.PECCurve[0].signal * 0.8d * Convert.ToDouble(HC.plot.ScaleHeight) / range;
				double tempForEndVar = (PECDef.Period - 1);
				for (idx = 1; idx <= tempForEndVar; idx++)
				{
					//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					newval = mid - PECDef.PECCurve[idx].signal * 0.8d * Convert.ToDouble(HC.plot.ScaleHeight) / range;
					//UPGRADE_TODO: (1067) Member plot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.plot.Line(idx * hscale, newval, (idx - 1) * hscale, oldval, Color.Magenta);
					oldval = newval;
				}
			}
		}

		private static bool CalcRates(PECDefinition PECDef)
		{
			bool result = false;
			object HC = null;
			double idx = 0;
			double ratesum = 0;
			double lastrate = 0;
			double rate = 0;
			double truerate = 0;
			double remainder = 0;
			double newpos = 0;
			double StepsMoved = 0;
			int i = 0;
			int NF1 = 0;

			int debugmark = 0;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string debugfile = (Convert.ToDouble(HC.oPersist.GetIniPath()) + Double.Parse("\\pec_rates_")).ToString() + PECDef.Period.ToString() + ".txt";
			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (endsub)");
			NF1 = FileSystem.FreeFile();

			FileSystem.FileClose(NF1);
			FileSystem.FileOpen(NF1, debugfile, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (handle_error)");

			// calculate the rate change between each PE curve sample
			ratesum = 0;
			double tempForEndVar = (PECDef.Period - 1);
			for (idx = 1; idx <= tempForEndVar; idx++)
			{
				PECDef.PECCurve[Convert.ToInt32(idx)].PErate = PECDef.PECCurve[Convert.ToInt32(idx - 1)].signal - PECDef.PECCurve[Convert.ToInt32(idx)].signal;
				ratesum += PECDef.PECCurve[Convert.ToInt32(idx)].PErate;
			}
			// first rate = average of 2nd and last to remove any discontinuities
			PECDef.PECCurve[0].PErate = (PECDef.PECCurve[Convert.ToInt32(PECDef.Period - 1)].PErate + PECDef.PECCurve[1].PErate) / 2d;
			ratesum += PECDef.PECCurve[0].PErate;

			// Apply the current threshhold and gain settings
			// The threshold setting allows us to reduce the number of rate corrections sent
			// to the mount.
			// The gain is just a user 'fiddle' factor ;-)
			debugmark = 1;
			ratesum = 0;
			lastrate = PECDef.PECCurve[Convert.ToInt32(PECDef.Period - 1)].PErate;
			double tempForEndVar2 = (PECDef.Period - 1);
			for (idx = 0; idx <= tempForEndVar2; idx++)
			{
				rate = PECDef.PECCurve[Convert.ToInt32(idx)].PErate;
				if (Math.Abs(lastrate - rate) > threshold)
				{
					lastrate = PECDef.PECCurve[Convert.ToInt32(idx)].PErate;
				}
				else
				{
					rate = lastrate;
				}
				rate *= gPEC_Gain;
				PECDef.PECCurve[Convert.ToInt32(idx)].PECrate = rate;
				ratesum += rate;
			}

			// The sum of all rate changes over a single cycle should be 0 i.e. sidereal rate
			// If this isn't the case then adjust them accordingly to ensure there is no net drift
			debugmark = 2;
			double tempForEndVar3 = (PECDef.Period - 1);
			for (idx = 0; idx <= tempForEndVar3; idx++)
			{
				PECDef.PECCurve[Convert.ToInt32(idx)].PECrate -= (ratesum / PECDef.Period);
			}

			// unfortunately the way the mount accepts 'quantised' rate change messages means that we can't have
			// just any rate we want. So work out what the rate will the mount will actually track at
			// determine any error and attemp to correct for it in the next sample.
			// Using this approach we should be able to acheve at worst sidereal - 0.024 arcsecs/sec
			debugmark = 3;
			remainder = 0;
			for (i = 1; i <= 3; i++)
			{
				ratesum = 0;
				double tempForEndVar5 = (PECDef.Period - 1);
				for (idx = 0; idx <= tempForEndVar5; idx++)
				{
					if (EQMath.gHemisphere == 0)
					{
						rate = SID_RATE_NORTH + PECDef.PECCurve[Convert.ToInt32(idx)].PECrate + remainder;
						//            truerate = (9325.46154 / (Int((9325.46154 / RATE) + 0.5)))
						truerate = (Tracking.gTrackFactorRA / (Math.Floor((Tracking.gTrackFactorRA / rate) + 0.5d)));
						// work out the error for next time
						remainder = rate - truerate;
						PECDef.PECCurve[Convert.ToInt32(idx)].PECrate = truerate - SID_RATE_NORTH;
						ratesum += PECDef.PECCurve[Convert.ToInt32(idx)].PECrate;
					}
					else
					{
						rate = SID_RATE_SOUTH + PECDef.PECCurve[Convert.ToInt32(idx)].PECrate + remainder;
						//            truerate = (9325.46154 / (Int((9325.46154 / RATE) - 0.5)))
						truerate = (Tracking.gTrackFactorRA / (Math.Floor((Tracking.gTrackFactorRA / rate) - 0.5d)));
						remainder = rate - truerate;
						PECDef.PECCurve[Convert.ToInt32(idx)].PECrate = truerate - SID_RATE_SOUTH;
						ratesum += PECDef.PECCurve[Convert.ToInt32(idx)].PECrate;
					}
				}
			}

			double tempForEndVar6 = (PECDef.Period - 1);
			for (idx = 0; idx <= tempForEndVar6; idx++)
			{
				if (EQMath.gHemisphere == 0)
				{
					PECDef.PECCurve[Convert.ToInt32(idx)].PECrate += SID_RATE_NORTH;
				}
				else
				{
					PECDef.PECCurve[Convert.ToInt32(idx)].PECrate += SID_RATE_SOUTH;
				}
			}


			// now we know what rates the mount will be tracking at we can
			// calculate the expected motor positions at each sample
			debugmark = 4;
			PECDef.PECCurve[0].PECPosition = PECDef.PECCurve[0].PEPosition;
			lastrate = 0;
			double tempForEndVar7 = (PECDef.Period - 1);
			for (idx = 1; idx <= tempForEndVar7; idx++)
			{
				// motorpos = lastmotorpos + elapsedtime * gTot_RA /  (ARCSECS_PER_360DEGREES / lastRate)
				rate = PECDef.PECCurve[Convert.ToInt32(idx - 1)].PECrate;
				if (rate == 0)
				{
					rate = lastrate;
				}
				else
				{
					lastrate = rate;
				}

				StepsMoved = EQMath.gTot_RA / (ARCSECS_PER_360DEGREES / rate);
				newpos = PECDef.PECCurve[Convert.ToInt32(idx - 1)].PECPosition + StepsMoved;
				PECDef.PECCurve[Convert.ToInt32(idx)].PECPosition = Convert.ToInt32(newpos) % (Convert.ToInt32(PECDef.Steps));
			}

			// A lot has gone on here so write out a debug file
			// for anaysis if things don't work as the should.
			debugmark = 5;
			FileSystem.PrintLine(NF1, "Index PE PosRawPE PosPE PosPEC RatePE RatePEC");
			double tempForEndVar8 = (PECDef.Period - 1);
			for (idx = 0; idx <= tempForEndVar8; idx++)
			{
				FileSystem.PrintLine(NF1, idx.ToString() + " " + 
				                     PECDef.PECCurve[Convert.ToInt32(idx)].signal.ToString("N4") + " " + 
				                     PECDef.PECCurve[Convert.ToInt32(idx)].RawPosn.ToString("N0") + " " + 
				                     PECDef.PECCurve[Convert.ToInt32(idx)].PEPosition.ToString("N0") + " " + 
				                     PECDef.PECCurve[Convert.ToInt32(idx)].PECPosition.ToString("N0") + " " + 
				                     PECDef.PECCurve[Convert.ToInt32(idx)].PErate.ToString("N4") + " " + 
				                     PECDef.PECCurve[Convert.ToInt32(idx)].PECrate.ToString("N4"));
			}
			FileSystem.PrintLine(NF1, "RateSum=" + ratesum.ToString("N4"));
			goto endsub;

			handle_error:
			//UPGRADE_WARNING: (2081) Err.Number has a new behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2081
			FileSystem.PrintLine(NF1, "ERROR NUMBER=" + Information.Err().Number.ToString());
			FileSystem.PrintLine(NF1, "ERROR DESCRIPTION=" + Information.Err().Description);
			FileSystem.PrintLine(NF1, "CodeTrace=" + debugmark.ToString());
			FileSystem.PrintLine(NF1, "idx=" + idx.ToString());
			FileSystem.PrintLine(NF1, "PECDef.PECCurve LBound=" + PECDef1.PECCurve.GetLowerBound(0).ToString());
			FileSystem.PrintLine(NF1, "PECDef.PECCurve UBound=" + PECDef1.PECCurve.GetUpperBound(0).ToString());
			FileSystem.PrintLine(NF1, "Period=" + PECDef.Period.ToString());
			Information.Err().Clear();
			result = true;
			endsub:
			FileSystem.FileClose(NF1);

			return result;
		}

		private static int GetIdx(PECDefinition PECDef)
		{
			int result = 0;
			double MotorPos = 0;
			double curvepos = 0;
			double i = 0;
			// Determine an appropriate index into the PEC table that gives the
			// best match for the current motor position. The best match is the
			// position that is one step in advance of the motor.
			// Generally this will just result in an incermenting of the index so
			// a starting position is suplied to speed up the number of comparisons
			// required.

			int idx = 0;

			// if this PEC definition is in use
			if (PECDef.Period != 0)
			{

				idx = PECDef.CurrIdx;

				if (gPEC_Enabled)
				{
					// PEC tacking - sync with PEC calculated motor positions.
					curvepos = PECDef.PECCurve[idx].PECPosition;
				}
				else
				{
					// sidereal track so use uncorrected positions
					if (PECDef.Period != 0)
					{
						curvepos = PECDef.PECCurve[idx].PEPosition;
					}
					else
					{
						idx = 0;
						goto endfunc;
					}
				}

				i = 0;
				if (EQMath.gHemisphere == 0)
				{
					// Northern hemisphere
					// For northern hemisphere curves the motor positions increase
					// with increasing index
					MotorPos = NormalisePosition(PlaybackTimer.ringcounter + phaseshift, PECDef.Steps);
					if (MotorPos > curvepos)
					{
						while (MotorPos > curvepos && i < PECDef.Period)
						{
							// search forwards till we find a curve position that is
							// greater than the motor position
							idx = Convert.ToInt32((idx + 1) % Convert.ToInt32(PECDef.Period));
							curvepos = PECDef.PECCurve[idx].PECPosition;
							i++;
						}
					}
					else
					{
						while (MotorPos < curvepos && i < PECDef.Period)
						{
							// search backwards till we find a curve position that is
							// less than the motor position
							idx--;
							if (idx < 0)
							{
								idx = (Convert.ToInt32(PECDef.Period - 1));
							}
							curvepos = PECDef.PECCurve[idx].PECPosition;
							i++;
						}
						// now increment to next curve position
						// its best to have the curve in advance of the motor!
						idx = Convert.ToInt32((idx + 1) % Convert.ToInt32(PECDef.Period));
					}
				}
				else
				{
					// Southern hemisphere
					// For southern hemisphere curves the motor positions decrease
					// with increasing index
					MotorPos = NormalisePosition(PlaybackTimer.ringcounter - phaseshift, PECDef.Steps);
					if (MotorPos > curvepos)
					{
						while (MotorPos > curvepos && i < PECDef.Period)
						{
							// search backwards till we find a curve position that is
							// smaller than the motor position
							idx--;
							if (idx < 0)
							{
								idx = (Convert.ToInt32(PECDef.Period - 1));
							}
							curvepos = PECDef.PECCurve[idx].PECPosition;
							i++;
						}
						// now increment to next curve position
						// its best to have the curve in advance of the motor!
						idx = Convert.ToInt32((idx + 1) % Convert.ToInt32(PECDef.Period));
					}
					else
					{
						// search forwards till we find a curve position that is
						// greater than the motor position
						while (MotorPos < curvepos && i < PECDef.Period)
						{
							idx = Convert.ToInt32((idx + 1) % Convert.ToInt32(PECDef.Period));
							curvepos = PECDef.PECCurve[idx].PECPosition;
							i++;
						}
					}
				}
			}
			endfunc:
			result = idx;
			PlaybackTimer.PecResyncCount = 0;
			return result;
		}

		internal static void PEC_MoveAxis(double axis, double rate)
		{
			object HC = null;
			object oLangDll = null;

			//    If rate <> 0 Then HC.TrackingFrame.Caption = oLangDll.GetLangString(121) & " " & oLangDll.GetLangString(188)
			if (axis == 0)
			{
				if ((rate == 0) && (EQMath.gDeclinationRate == 0))
				{
					//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(178));
				}
				if (EQMath.gEQRAPulseDuration == 0)
				{
					if ((EQMath.gRightAscensionRate * rate) <= 0 || EQMath.gTrackingStatus != 1)
					{
						Tracking.StartRA_by_Rate(rate);
					}
					else
					{
						Tracking.ChangeRA_by_Rate(rate);
					}
				}
				EQMath.gRightAscensionRate = rate;
				EQMath.gTrackingStatus = 1;
				EQMath.gRA_LastRate = rate;
			}
		}

		internal static void PEC_StartCapture()
		{
			object HC = null;
			if (EQMath.gTrackingStatus == 1)
			{
				//        Call KillPec
				CaptureTimer.State = 0;
				//UPGRADE_TODO: (1067) Member PECCapTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.PECCapTimer.Enabled = true;
			}
			else
			{
				// can't capture if not tacking!
				//UPGRADE_TODO: (1067) Member CheckCapPec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckCapPec.Value = 0;
			}
		}

		internal static void PEC_StopCapture()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member PECCapTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PECCapTimer.Enabled = false;
			if (CaptureTimer.State == 2)
			{
				// capture has completed
				SaveCaptureData();
			}
			else
			{
				// capture has been aborted
			}
			CaptureTimer.State = 0;
		}

		private static void SaveCaptureData()
		{
			object HC = null;
			object PECConfigFrm = null;

			int idx = 0;
			int PECIdx = 0;
			double pe = 0;
			string FileName = "";
			int NF1 = 0;

			try
			{

				// create a capture file for debug
				PECFileData[] PEC_Data = ArraysHelper.InitializeArray<PECFileData>(Convert.ToInt32(PECCap.Period) + 1);

				// clear pe
				int tempForEndVar = PEC_Data.GetUpperBound(0) - 1;
				for (idx = 0; idx <= tempForEndVar; idx++)
				{
					PEC_Data[idx].pe = 0;
					PEC_Data[idx].cycle = 0;
				}


				// linearly regress to remove drifts and apply fft smoothing
				PEC_RegressAndSmooth();

				if (gPEC_TimeStampFiles == 1)
				{
					FileName = gPEC_FileDir + "pecapture_" + GetTimeStamp() + "_EQMOD.txt";
				}
				else
				{
					FileName = gPEC_FileDir + "pecapture_EQMOD.txt";
				}

				NF1 = FileSystem.FreeFile();
				FileSystem.FileClose(NF1);
				FileSystem.FileOpen(NF1, FileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

				// output a perecorder format type file of the raw data
				//UPGRADE_TODO: (1067) Member MainLabel is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileSystem.PrintLine(NF1, "# " + Convert.ToString(HC.MainLabel.Caption));
				FileSystem.PrintLine(NF1, "# AUTO-PEC");
				FileSystem.PrintLine(NF1, "# RA  = " + EQMath.gRA.ToString());
				FileSystem.PrintLine(NF1, "# DEC = " + EQMath.gDec.ToString());
				if (Common.gAscomCompatibility.AllowPulseGuide)
				{
					//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					FileSystem.PrintLine(NF1, "# PulseGuide, Rate=" + (Convert.ToDouble(HC.HScrollRARate.Value) * 0.1d).ToString());
				}
				else
				{
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					FileSystem.PrintLine(NF1, "# ST-4 Guide, Rate=" + Convert.ToString(HC.RAGuideRateList.Text));
				}
				FileSystem.PrintLine(NF1, "!WormPeriod=" + PECCap.Period.ToString());
				FileSystem.PrintLine(NF1, "!StepsPerWorm=" + PECCap.Steps.ToString());
				FileSystem.PrintLine(NF1, "#Time MotorPosition PE");

				// Average signals to get a single cycle error signal
				int tempForEndVar2 = PECCap.idx - 1;
				for (idx = 0; idx <= tempForEndVar2; idx++)
				{ //UBound(PECCap.CapureData) - 1
					PECIdx = Convert.ToInt32(idx % Convert.ToInt32(PECCap.Period));
					PEC_Data[PECIdx].Position = NormalisePosition(PECCap.CapureData[idx].MotorPos, PECCap.Steps);
					// ignore first and last 120 samples as fft filter may exagerate their data
					if (idx > 120 && idx < PECCap.idx - 120)
					{
						//              PEC_signal(PECIdx) = PEC_signal(PECIdx) + .peSmoothed / (gPEC_Capture_Cycles)
						pe = PEC_Data[PECIdx].pe * PEC_Data[PECIdx].cycle + PECCap.CapureData[idx].peSmoothed;
						PEC_Data[PECIdx].cycle = (short) (PEC_Data[PECIdx].cycle + 1);
						PEC_Data[PECIdx].pe = pe / ((double) PEC_Data[PECIdx].cycle);
					}
					FileSystem.PrintLine(NF1, Strings.FormatNumber(PECCap.CapureData[idx].time, 3, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + PECCap.CapureData[idx].MotorPos.ToString() + " " + Strings.FormatNumber(PECCap.CapureData[idx].pe, 4, TriState.UseDefault, TriState.UseDefault, TriState.False));
				}
				FileSystem.FileClose(NF1);


				// generate pec file
				if (gPEC_TimeStampFiles == 1)
				{
					FileName = gPEC_FileDir + "pec_" + GetTimeStamp() + ".txt";
				}
				else
				{
					FileName = gPEC_FileDir + "pec.txt";
				}
				NF1 = FileSystem.FreeFile();
				FileSystem.FileClose(NF1);
				FileSystem.FileOpen(NF1, FileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
				//UPGRADE_TODO: (1067) Member MainLabel is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileSystem.PrintLine(NF1, "# " + Convert.ToString(HC.MainLabel.Caption));
				FileSystem.PrintLine(NF1, "!WormPeriod=" + PECCap.Period.ToString());
				FileSystem.PrintLine(NF1, "!StepsPerWorm=" + PECCap.Steps.ToString());
				FileSystem.PrintLine(NF1, "# time - motor - smoothed PE");
				int tempForEndVar3 = PEC_Data.GetUpperBound(0) - 1;
				for (idx = 0; idx <= tempForEndVar3; idx++)
				{
					pe = PEC_Data[idx].pe;
					FileSystem.PrintLine(NF1, Strings.FormatNumber(idx, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PEC_Data[idx].Position, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(pe, 4, TriState.UseDefault, TriState.UseDefault, TriState.False));
				}
				FileSystem.FileClose(NF1);

				// load pec
				if (gPEC_AutoApply == 1)
				{
					PEC_LoadFile(FileName);
					// set pec gain to x1
					//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					PECConfigFrm.GainScroll.Value = 10;
					// set phase shift to 0
					//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					PECConfigFrm.PhaseScroll.Value = 0;
				}
			}
			catch
			{

				FileSystem.FileClose(NF1);
			}
		}


		private static void PEC_RegressAndSmooth()
		{
			object[] FFT_Free = null;
			object HC = null;
			double[] FFT_GetSample = null;
			object[] FFT_ForwardFFTComplex = null;
			object[, ] FFT_Initialise = null;
			object[, ] FFT_SetSample = null;
			object[] FFT_InverseFFTComplex = null;
			object[, , ] FFT_ApplyFilter = null;
			object[] FFT_NormaliseMag = null;

			double xy_sum = 0;
			double xx_sum = 0;
			double x_sum = 0;
			double y_sum = 0;
			double tmp = 0;
			int i = 0;
			double RawDataSize = 0;
			double slope = 0;
			double intercept = 0;
			int NF1 = 0;

			try
			{

				xy_sum = 0;
				xx_sum = 0;
				y_sum = 0;
				x_sum = 0;
				RawDataSize = PECCap.CapureData.GetUpperBound(0);
				int tempForEndVar = PECCap.idx - 1;
				for (i = 0; i <= tempForEndVar; i++)
				{
					xy_sum += (i * PECCap.CapureData[i].pe);
					x_sum += i;
					y_sum += PECCap.CapureData[i].pe;
					xx_sum += (i * i);
				}

				// Calculate slope of linear regression line
				slope = ((RawDataSize * xy_sum) - (x_sum * y_sum)) / ((RawDataSize * xx_sum) - (x_sum * x_sum));

				// Calculate intercept of linear regression line
				intercept = (y_sum - (slope * x_sum)) / RawDataSize;

				// initalise fft
				object tempAuxVar = FFT_Initialise[4096, 1];

				// remove slope from data and store in fft time domain
				double tempForEndVar2 = RawDataSize - 1;
				for (i = 0; i <= tempForEndVar2; i++)
				{
					tmp = PECCap.CapureData[i].pe - (slope * i) - intercept;
					//        PECCap.CapureData(i).pe = tmp
					// add to time domain
					object tempAuxVar2 = FFT_SetSample[i, Convert.ToInt32(tmp)];
				}

				// generate frequency domain
				object[] tempAuxVar3 = FFT_ForwardFFTComplex;
				object[] tempAuxVar4 = FFT_NormaliseMag;

				// filter out anything with a relative magnitude of 10% or less, and anything with a period < 33 sec or period > 1.5*worm period
				//    Call FFT_ApplyFilter(0.03, 1 / (2 * PECCap.Period), 10)
				object tempAuxVar5 = FFT_ApplyFilter[Convert.ToInt32(1 / Convert.ToDouble(gPEC_filter_lowpass)), Convert.ToInt32(1 / (1.5d * PECCap.Period)), Convert.ToInt32(Convert.ToDouble(gPEC_mag))];

				// generate new time domain
				object[] tempAuxVar6 = FFT_InverseFFTComplex;

				if (gPEC_Debug == 1)
				{
					// create a capture file for debug
					PECCap.FileName = gPEC_FileDir + "PECCapture_" + GetTimeStamp() + ".txt";
					NF1 = FileSystem.FreeFile();
					FileSystem.FileClose(NF1);
					FileSystem.FileOpen(NF1, PECCap.FileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
					//UPGRADE_TODO: (1067) Member MainLabel is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					FileSystem.PrintLine(NF1, "# " + Convert.ToString(HC.MainLabel.Caption));
					FileSystem.PrintLine(NF1, "!WormPeriod=" + PECCap.Period.ToString());
					FileSystem.PrintLine(NF1, "!StepsPerWorm=" + PECCap.Steps.ToString());
					FileSystem.PrintLine(NF1, "# RA  = " + EQMath.gRA.ToString());
					FileSystem.PrintLine(NF1, "# DEC = " + EQMath.gDec.ToString());
					if (Common.gAscomCompatibility.AllowPulseGuide)
					{
						//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						FileSystem.PrintLine(NF1, "# PulseGuide, Rate=" + (Convert.ToDouble(HC.HScrollRARate.Value) * 0.1d).ToString());
					}
					else
					{
						//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						FileSystem.PrintLine(NF1, "# ST-4 Guide, Rate=" + Convert.ToString(HC.RAGuideRateList.Text));
					}
					FileSystem.PrintLine(NF1, "#Idx Time DeltaTime MotorPos DeltaPos Rate DeltaPE RawPE SmothedPE");
				}

				// store as smoothed capture data.
				double tempForEndVar3 = RawDataSize - 1;
				for (i = 0; i <= tempForEndVar3; i++)
				{
					PECCap.CapureData[i].peSmoothed = FFT_GetSample[i];
					if (gPEC_Debug == 1)
					{
						FileSystem.PrintLine(NF1, Strings.FormatNumber(i, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PECCap.CapureData[i].time, 3, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PECCap.CapureData[i].DeltaTime, 3, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + PECCap.CapureData[i].MotorPos.ToString() + " " + PECCap.CapureData[i].DeltaPos.ToString() + " " + Strings.FormatNumber(PECCap.CapureData[i].rate, 4, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PECCap.CapureData[i].peInc, 4, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PECCap.CapureData[i].pe, 4, TriState.UseDefault, TriState.UseDefault, TriState.False) + " " + Strings.FormatNumber(PECCap.CapureData[i].peSmoothed, 4, TriState.UseDefault, TriState.UseDefault, TriState.False));
					}
				}

				FileSystem.FileClose(NF1);

				object[] tempAuxVar7 = FFT_Free;
			}
			catch
			{
			}


		}

		private static string GetTimeStamp()
		{

			string result = "";
			result = DateTime.Today.ToString("MM-dd-yyyy") + DateTime.Now.ToString("HH:mm:ss");
			result = StringsHelper.Replace(result, ":", "", 1, -1, CompareMethod.Binary);
			result = StringsHelper.Replace(result, "\\", "", 1, -1, CompareMethod.Binary);
			result = StringsHelper.Replace(result, "/", "", 1, -1, CompareMethod.Binary);
			result = StringsHelper.Replace(result, " ", "", 1, -1, CompareMethod.Binary);
			return StringsHelper.Replace(result, "-", "", 1, -1, CompareMethod.Binary);

		}

		internal static void PEC_DispalyUpdate(PictureBox plot)
		{
			object oLangDll = null;
			//UPGRADE_ISSUE: (2064) PictureBox method plot.Cls was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
			plot.Cls();
			plot.Font = plot.Font.Change(size:8);
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_ISSUE: (2064) PictureBox method plot.Print was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
			plot.Print(Convert.ToString(oLangDll.GetLangString(191)) + " = " + gPEC_Gain.ToString());
			plot.Font = plot.Font.Change(size:12);
			//UPGRADE_ISSUE: (2064) PictureBox method plot.Print was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
			plot.Print(PlaybackTimer.strPlayback);
			//UPGRADE_ISSUE: (2064) PictureBox method plot.Print was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
			plot.Print(CaptureTimer.strCapture);

		}
	}
}