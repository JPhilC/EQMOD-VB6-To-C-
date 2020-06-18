using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UpgradeHelpers.Helpers;

namespace Project1
{
	public class Telescope
	: UpgradeStubs.ITelescope
	{



		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int GetTickCount();


		//UPGRADE_ISSUE: (2068) DriverHelper.Serial object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		private DriverHelper.Serial m_Serial = null;
		//UPGRADE_ISSUE: (2068) DriverHelper.Util object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		private DriverHelper.Util m_Util = null;
		//UPGRADE_ISSUE: (2068) DriverHelper.Profile object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		private DriverHelper.Profile m_Profile = null;
		private int m_iSettleTime = 0;
		private double m_RaRateAdjust = 0;
		private double m_DecRateAdjust = 0;
		//UPGRADE_ISSUE: (2068) GuideDirections object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		private UpgradeStubs.GuideDirections LastGuideNS = null;



		public Telescope()
		{
			object oLangDll = null;
			object DriverHelper = null;
			object[] ReadProcessPriority = null;
			object[] readUserParkPos = null;
			object HC = null;
			object[] ReadSiteValues = null;

			if (Common.ClientCount < 2)
			{

				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_End();

				m_Serial = new DriverHelper.Serial();
				m_Util = new DriverHelper.Util();
				m_Profile = new DriverHelper.Profile();
				//UPGRADE_TODO: (1067) Member DeviceType is not defined in type Profile. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				m_Profile.DeviceType = "Telescope"; // We're a Telescope driver
				m_iSettleTime = 0; // Default 0 slew settle time
				//UPGRADE_TODO: (1067) Member Register is not defined in type Profile. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				m_Profile.Register(Definitions.ASCOM_id, Definitions.ASCOM_DESC); // Self-register


				// initialise a rates colection for each axis but don't assign any rates yet
				// this will be done on connection
				Tracking.g_RAAxisRates = new Rates();
				Tracking.g_DECAxisRates = new Rates();

				EQMath.gHemisphere = 1;
				Goto.gTargetRA = Common.EQ_INVALIDCOORDINATE;
				Goto.gTargetDec = Common.EQ_INVALIDCOORDINATE;
				EQMath.gEQTimeDelta = 0;

				Alignment.gLoadAPresetOnUnpark = 0;
				Alignment.gSaveAPresetOnPark = 0;

				EQMath.gTot_step = EQMath.gDefault_step;
				EQMath.gEQ_MAXSYNC = EQMath.EQ_MAXSYNC_Const;

				Common.GetDllVer();

				//Initialize position values - They are actually done also on
				//Driver connect
				Common.readAscomCompatibiity();
				Tracking.readSiderealRate();
				object[] tempAuxVar = ReadSiteValues;
				ReadSyncMap();
				ReadAlignMap();
				object[] tempAuxVar2 = readUserParkPos;
				object[] tempAuxVar3 = ReadProcessPriority;


				EQMath.gEQparkstatus = 0;
				EQMath.gTrackingStatus = 0; // Initially not tracking
				EQMath.gDeclinationRate = 0;
				m_DecRateAdjust = 0;
				EQMath.gRightAscensionRate = 0;
				m_RaRateAdjust = 0;

				Limits.readRALimit();
				Limits.Limits_Init();

				// At least with an initial value

				EQMath.gDECEncoder_Home_pos = EQMath.DECEncoder_Home_pos;


				EQMath.gRAStatus = Common.EQ_MOTORBUSY; // RA Motor Busy Status
				EQMath.gDECStatus = Common.EQ_MOTORBUSY; // DEC Motor Busy Status
				EQMath.gSlewStatus = false; // Not Slewing status
				EQMath.gRAStatus_slew = false; // Slew to track condidition
				Goto.gSlewCount = 0; // Goto Iterative Counter
				Goto.gFRSlewCount = 0; // Goto Iterative Counter


				// Initialize these values for polling emulation

				EQMath.gEmulRA = 0;
				EQMath.gEmulDEC = 0;
				EQMath.gEmulOneShot = false;
				EQMath.gEmulNudge = false;


				ReadComPortSettings();


				EQMath.gTot_RA = EQMath.gTot_step; // Set RA Total Encoder Step Count
				EQMath.gTot_DEC = EQMath.gTot_step; // Set DEC total Encoder Step Count
				Goto.gRAGotoRes = 12960000 / EQMath.gTot_RA; // 12960000 = secs in 360 degress * 10
				Goto.gDECGotoRes = 12960000 / EQMath.gTot_DEC; // 12960000 = secs in 360 degress * 10
				//       gGotoResolution = 10                        ' steps

				EQMath.gRAWormSteps = 50133;
				EQMath.gDECWormSteps = 50133;
				EQMath.gRAWormPeriod = 480;
				EQMath.gSOP = 0; //Set at unknown pier setting


				//UPGRADE_TODO: (1067) Member HCMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HCMessage.Text = ""; //Set Message Center to BLANK
				//        HC.HCTextAlign.Text = ""

				//UPGRADE_TODO: (1067) Member DisplayTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.DisplayTimer.Enabled = true;
			}

			Common.ClientCount++;
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message(Convert.ToString(oLangDll.GetLangString(5134)) + (Common.ClientCount - 1).ToString());

		}

		~Telescope()
		{
			object oLangDll = null;
			object Align = null;
			object AscomTrace = null;
			object ColorPick = null;
			object CustomMountDlg = null;
			object FileDlg = null;
			object GotoDialog = null;
			object[] writeParkStatus = null;
			object DefineParkForm = null;
			object GPSSetup = null;
			object HC = null;
			object JStickConfigForm = null;
			object LimitEditForm = null;
			object PECConfigFrm = null;
			object polarfrm = null;
			object ProgressFrm = null;
			object Setupfrm = null;
			object Slewpad = null;
			object SoundsFrm = null;
			object StarEditform = null;

			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{

				if (Common.ClientCount == 2)
				{

					//UPGRADE_TODO: (1067) Member DisplayTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.DisplayTimer.Enabled = false;

					if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() == 1)
					{ // We update only if the mount is online
						// clients really should have disconnected via ASCOM prior to shutdown
						// but if just in case they don't save the park status
						object tempAuxVar = writeParkStatus[EQMath.gEQparkstatus];
					}

					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_End();

					Common.writeratebarstateHC();

					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(HC);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(Align);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(AscomTrace);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(ColorPick);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(CustomMountDlg);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(DefineParkForm);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(FileDlg);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(GotoDialog);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(GPSSetup);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(JStickConfigForm);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(LimitEditForm);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(polarfrm);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(ProgressFrm);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(Setupfrm);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(Slewpad);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(SoundsFrm);
					// Unload StarSim
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(StarEditform);
					//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
					//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
					UpgradeStubs.VB.getGlobal().Unload(PECConfigFrm);
					Common.ClientCount = 1;
				}
				else
				{
					Common.ClientCount--;
					if (Common.ClientCount > 0)
					{
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message(Convert.ToString(oLangDll.GetLangString(5134)) + (Common.ClientCount - 1).ToString());
					}
				}
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}

		//------------------ V2 Compliance Properties

		//UPGRADE_ISSUE: (2068) AlignmentModes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) AlignmentModes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public UpgradeStubs.AlignmentModes AlignmentMode
		{
			get
			{
				//UPGRADE_ISSUE: (2068) AlignmentModes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				UpgradeStubs.AlignmentModes algGermanPolar = null;

				return algGermanPolar;

			}
		}


		public bool AtHome
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET AtHome :F");
				}
				return result;
			}
		}


		public bool AtPark
		{
			get
			{
				bool result = false;
				object AscomTrace = null;

				switch(EQMath.gEQparkstatus)
				{
					case 0 : 
						// if unparked 
						result = false; 
						break;
					case 1 : 
						// if parked 
						result = true; 
						break;
					case 2 : 
						// ASCOM has no means of detecting a parking state 
						// However some folks will be closing their roofs based upon the stare of AtPark 
						// So we must respond with a false! 
						result = false; 
						break;
				}
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET AtPark :" + result.ToString());
				}

				return result;
			}
		}


		public bool CanSetDeclinationRate
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSetDeclination :T");
				}
				return true;
			}
		}


		public bool CanSetGuideRates
		{
			get
			{
				object AscomTrace = null;
				//    If gAscomCompatibility.AllowPulseGuide Then
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSetGuideRates :T");
				}
				return true;
				//    Else
				//        If AscomTrace.AscomTraceEnabled Then AscomTrace.Add_log 3, "GET CanSetGuideRates :F"
				//        CanSetGuideRates = False
				//    End If
			}
		}


		public bool CanSetPierSide
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSetPierSide :F");
				}
				return false;
			}
		}


		public bool CanSetRightAscensionRate
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSetRARate :F");
				}
				return true;
			}
		}


		public bool CanSlewAltAz
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSlewAltAz :F");
				}
				return false;
			}
		}


		public bool CanSlewAltAzAsync
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSlewAltAzAsync :F");
				}
				return false;
			}
		}


		public bool CanSyncAltAz
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSyncAltAz :F");
				}
				return false;
			}
		}


		public string DriverVersion
		{
			get
			{
				string result = "";
				object AscomTrace = null;
				result = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileMajorPart.ToString() + "." + FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileMinorPart.ToString();
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET DriverVersion :" + result);
				}
				return result;
			}
		}


		//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public UpgradeStubs.EquatorialCoordinateType EquatorialSystem
		{
			get
			{
				UpgradeStubs.EquatorialCoordinateType result = null;
				object AscomTrace = null;
				//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				UpgradeStubs.EquatorialCoordinateType equB1950 = null;
				//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				UpgradeStubs.EquatorialCoordinateType equJ2000 = null;
				//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				UpgradeStubs.EquatorialCoordinateType equJ2050 = null;
				//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				UpgradeStubs.EquatorialCoordinateType equLocalTopocentric = null;
				//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				UpgradeStubs.EquatorialCoordinateType equOther = null;
				switch(Common.gAscomCompatibility.Epoch)
				{
					case 0 : 
						result = equOther; 
						break;
					case 1 : 
						result = equLocalTopocentric; 
						break;
					case 2 : 
						result = equJ2000; 
						break;
					case 3 : 
						result = equJ2050; 
						break;
					case 4 : 
						result = equB1950; 
						break;
					default:
						result = equOther; 
						break;
				}
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
					//UPGRADE_WARNING: (1068) EquatorialSystem of type EquatorialCoordinateType is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET EquatorialCoordinateType :" + result);
				}
				return result;
			}
		}



		public double GuideRateDeclination
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				if (Common.gAscomCompatibility.AllowPulseGuide)
				{
					// movement rate offset in degress/sec
					//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type HC. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					result = (Convert.ToDouble(HC.HScrollDecRate.Value) * 0.1d * EQMath.SID_RATE) / 3600d;
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(4, "GET GuideRateDEC :" + result.ToString());
					}
				}
				else
				{
					//RaiseError SCODE_NOT_IMPLEMENTED, ERR_SOURCE, "Property Get GuideRateDeclination" & MSG_NOT_IMPLEMENTED
					//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type HC. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					switch(Convert.ToInt32(HC.DECGuideRateList.ListIndex))
					{
						case 1 : 
							result = (0.5d * EQMath.SID_RATE) / 3600d; 
							break;
						case 2 : 
							result = (0.75d * EQMath.SID_RATE) / 3600d; 
							break;
						case 3 : 
							result = (EQMath.SID_RATE) / 3600d; 
							break;
						case 4 : 
							RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Get GuideRateDeclination" + ErrorConstants.MSG_NOT_IMPLEMENTED); 
							break;
						default:
							result = (0.25d * EQMath.SID_RATE) / 3600d; 
							break;
					}
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(4, "GET GuideRateDEC :" + result.ToString());
					}
				}

				return result;
			}
			set
			{
				object AscomTrace = null;
				object HC = null;
				if (Common.gAscomCompatibility.AllowPulseGuide)
				{
					// convert from degrees per sec to a factor of sidereal
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(4, "LET GuideRateDEC(" + value.ToString() + ")");
					}
					value = value * 3600 / (0.1d * EQMath.SID_RATE);
					//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (value < Convert.ToDouble(HC.HScrollDecRate.min))
					{
						//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						value = Convert.ToDouble(HC.HScrollDecRate.min);
					}
					else
					{
						//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (value > Convert.ToDouble(HC.HScrollDecRate.max))
						{
							//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							value = Convert.ToDouble(HC.HScrollDecRate.max);
						}
					}
					//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.HScrollDecRate.Value = Convert.ToInt32(value);
				}
				else
				{
					//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToDouble(HC.DECGuideRateList.ListIndex) == 2)
					{
						//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
						{
							//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							AscomTrace.Add_log(4, "LET GuideRateDEC(" + value.ToString() + ") :NOT_SUPPORTED");
						}
						RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Let GuideRateDeclination" + ErrorConstants.MSG_NOT_IMPLEMENTED);
					}
					else
					{
						value = value * 3600 / EQMath.SID_RATE;
						if (value > 0.75d)
						{
							//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.DECGuideRateList.ListIndex = 3;
						}
						else
						{
							if (value > 0.5d)
							{
								//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.DECGuideRateList.ListIndex = 2;
							}
							else
							{
								if (value > 0.25d)
								{
									//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.DECGuideRateList.ListIndex = 1;
								}
								else
								{
									//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.DECGuideRateList.ListIndex = 0;
								}
							}
						}
						//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
						{
							//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							AscomTrace.Add_log(4, "LET GuideRateDEC(" + value.ToString() + ")");
						}
					}
				}

			}
		}



		public double GuideRateRightAscension
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				if (Common.gAscomCompatibility.AllowPulseGuide)
				{
					// movement rate offset in degrees/sec
					//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type HC. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					result = (Convert.ToDouble(HC.HScrollRARate.Value) * 0.1d * EQMath.SID_RATE) / 3600d;
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(4, "GET GuideRateRA :" + result.ToString());
					}
				}
				else
				{
					// RaiseError SCODE_NOT_IMPLEMENTED, ERR_SOURCE, "Property Get GuideRateRightAscension" & MSG_NOT_IMPLEMENTED
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type HC. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					switch(Convert.ToInt32(HC.RAGuideRateList.ListIndex))
					{
						case 1 : 
							result = (0.5d * EQMath.SID_RATE) / 3600d; 
							break;
						case 2 : 
							result = (0.75d * EQMath.SID_RATE) / 3600d; 
							break;
						case 3 : 
							result = (EQMath.SID_RATE) / 3600d; 
							break;
						case 4 : 
							RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Get GuideRateRightAscension" + ErrorConstants.MSG_NOT_IMPLEMENTED); 
							break;
						default:
							result = (0.25d * EQMath.SID_RATE) / 3600d; 
							break;
					}
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(4, "GET GuideRateRA :" + result.ToString());
					}
				}

				return result;
			}
			set
			{
				object AscomTrace = null;
				object HC = null;
				// We can't support properly beacuse the ASCOM spec does not distinquish between ST4 and Pulseguiding
				// and states that this property relates to both - crazy!
				if (Common.gAscomCompatibility.AllowPulseGuide)
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(4, "LET GuideRateRA(" + value.ToString() + ")");
					}
					value = value * 3600 / (0.1d * EQMath.SID_RATE);
					//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (value < Convert.ToDouble(HC.HScrollRARate.min))
					{
						//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						value = Convert.ToDouble(HC.HScrollRARate.min);
					}
					else
					{
						//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (value > Convert.ToDouble(HC.HScrollRARate.max))
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							value = Convert.ToDouble(HC.HScrollRARate.max);
						}
					}
					//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.HScrollRARate.Value = Convert.ToInt32(value);
				}
				else
				{
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToDouble(HC.RAGuideRateList.ListIndex) == 4)
					{
						//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
						{
							//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							AscomTrace.Add_log(4, "LET GuideRateRA(" + value.ToString() + ") :NOT_SUPPORTED");
						}
						RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Let GuideRateRightAscension" + ErrorConstants.MSG_NOT_IMPLEMENTED);
					}
					else
					{
						value = value * 3600 / EQMath.SID_RATE;
						if (value > 0.75d)
						{
							//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.RAGuideRateList.ListIndex = 3;
						}
						else
						{
							if (value > 0.5d)
							{
								//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.RAGuideRateList.ListIndex = 2;
							}
							else
							{
								if (value > 0.25d)
								{
									//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.RAGuideRateList.ListIndex = 1;
								}
								else
								{
									//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.RAGuideRateList.ListIndex = 0;
								}
							}
						}
						//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
						{
							//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							AscomTrace.Add_log(4, "LET GuideRateRA(" + value.ToString() + ")");
						}
					}
				}
			}
		}


		public int InterfaceVersion
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET InterfaceVersion=2");
				}
				return 2;
			}
		}



		//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public UpgradeStubs.PierSide SideOfPier
		{
			get
			{
				UpgradeStubs.PierSide result = null;
				object AscomTrace = null;
				object pierUnknown = null;


				switch(Common.gAscomCompatibility.SideOfPier)
				{
					case 0 : 
						// Pointing Side of Pier 
						// Not the side of pier at all - but that's what ASCOM in their widsom chose to call it - duh! 
						NotUpgradedHelper.NotifyNotUpgradedElement("The following assignment/return was commented because it has incompatible types"); 
						//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068 
						//result = (UpgradeStubs.PierSide) EQMath.SOP_Pointing(EQMath.gDec_DegNoAdjust); 
						 
						break;
					case 1 : 
						// Physical Side of Pier 
						// this is what folks expect side of pier to be - but it won't work in ASCOM land. 
						NotUpgradedHelper.NotifyNotUpgradedElement("The following assignment/return was commented because it has incompatible types"); 
						//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068 
						//result = (UpgradeStubs.PierSide) EQMath.SOP_Physical(EQMath.gRA_Hours); 
						 
						break;
					case 2 : 
						// Don't know, don't care 
						// sometimes the safest options given the confusion ASCOM have managed to create around SOP 
						//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068 
						result = (UpgradeStubs.PierSide) pierUnknown; 
						 
						break;
					case 3 : 
						// V1.24g mode - not ASCOM but folks seem to like it! 
						NotUpgradedHelper.NotifyNotUpgradedElement("The following assignment/return was commented because it has incompatible types"); 
						//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068 
						//result = (UpgradeStubs.PierSide) EQMath.SOP_DEC(EQMath.gDec_DegNoAdjust); 

						 
						break;
				}

				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
					//UPGRADE_WARNING: (1068) SideOfPier of type PierSide is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET SideOfPier :" + result);
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
					//UPGRADE_WARNING: (1068) newval of type PierSide is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "LET SideOfPier(" + value + ") :NOT_SUPPORTED");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Let SideOfPier" + ErrorConstants.MSG_NOT_IMPLEMENTED);
			}
		}



		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public UpgradeStubs.DriveRates TrackingRate
		{
			get
			{
				UpgradeStubs.DriveRates result = null;
				object AscomTrace = null;
				object driveSidereal = null;
				//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				result = (UpgradeStubs.DriveRates) driveSidereal;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
					//UPGRADE_WARNING: (1068) TrackingRate of type DriveRates is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "GET TrackingRate :" + result);
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				UpgradeStubs.DriveRates driveSidereal = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
					//UPGRADE_WARNING: (1068) newval of type DriveRates is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "LET TrackingRate :" + value + ")");
				}
				if (value != driveSidereal)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "Property Let TrackingRate" + ErrorConstants.MSG_PROP_RANGE_ERROR);
				}
			}
		}



		public object TrackingRates
		{
			get
			{
				object result = null;
				object AscomTrace = null;
				result = Tracking.g_TrackingRates;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "GET Tracking Rates");
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "LET TrackingRates :NOT_SUPPORTED");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Let TrackingRates" + ErrorConstants.MSG_NOT_IMPLEMENTED);
			}
		}



		//-------------------------- V1 Properties Starts here -----------------


		public bool DoesRefraction
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET DoesRfraction :F");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Get DoesRefraction" + ErrorConstants.MSG_NOT_IMPLEMENTED);
				return result;
			}
			set
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "SET DoesRfraction :NOT SUPPORTED");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property Get DoesRefraction" + ErrorConstants.MSG_NOT_IMPLEMENTED);
			}
		}


		public double altitude
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.gAlt;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(2, "GET altitude :" + EQMath.gAlt.ToString());
				}
				return result;
			}
		}


		public double ApertureDiameter
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET ApertureDiameter :NOT_SUPPORTED");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property ApertureDiameter" + ErrorConstants.MSG_NOT_IMPLEMENTED);
				return 0;
			}
		}


		public double ApertureArea
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET ApertureArea :NOT_SUPPORTED");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property ApertureDiameter" + ErrorConstants.MSG_NOT_IMPLEMENTED);
				return 0;
			}
		}


		public double Azimuth
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.gAz;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(2, "GET Azimuth :" + EQMath.gAz.ToString());
				}
				return result;
			}
		}


		public bool CanFindHome
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanFindHome :F");
				}
				return result;
			}
		}


		public bool CanPark
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = true;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanPark :T");
				}
				return result;
			}
		}


		public bool CanSetPark
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = true;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSetPark :T");
				}
				return result;
			}
		}


		public bool CanSetTracking
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = true;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSetTracking :T");
				}
				return result;
			}
		}


		public bool CanSlew
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = true;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSlew :T");
				}
				return result;
			}
		}


		public bool CanSlewAsync
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = true;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSlewAsync :T");
				}
				return result;
			}
		}


		public bool CanSync
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = true;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanSync :T");
				}
				return result;
			}
		}


		public bool CanUnpark
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = true;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "GET CanUnpark :T");
				}
				return result;
			}
		}

		public bool CanPulseGuide
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				if (Common.gAscomCompatibility.AllowPulseGuide)
				{
					result = true;
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(3, "GET CanPulseGuide :T");
					}
				}
				else
				{
					result = false;
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(3, "GET CanPulseGuide :F");
					}
				}
				return result;
			}
		}



		public bool Connected
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() != 1)
				{
					result = false;
				}
				else
				{
					if (HC.EncoderReadErrCount >= 5)
					{
						// comms has bit problems so report as disconnected
						result = false;
					}
					else
					{
						result = true;
					}
				}
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(1, "GET Connected :" + result.ToString());
				}
				return result;
			}
			set
			{
				object[] writeParkStatus = null;
				object HC = null;
				object[] readParkModes = null;
				object oLangDll = null;
				object[] readparkStatus = null;
				double SPSD = 0;
				object[] readUnpark = null;
				object AscomTrace = null;
				object[] SetParkCaption = null;

				int icast = 0;
				double MaxRate = 0;
				string strtmp = "";

				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(1, "LET Connected(" + value.ToString() + ")");
				}
				if (value)
				{

					EQMath.gpl_interval = 50;

					Common.readRASyncCheckVal(); // RA Sync Auto
					Common.readPulseguidepwidth(); // Read Pulseguide interval

					//        Call ReadSiteValues
					//        Call ReadSyncMap
					//        Call ReadAlignMap

					if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() == 0)
					{
						// if mount hasn't been connected yet then do stuff!

						// prefix the com port to ensure com10+ works
						strtmp = "\\\\.\\" + EQMath.gPort;
						Common.gInitResult = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_Init(ref strtmp, EQMath.gBaud, EQMath.gTimeout, EQMath.gRetry);

						EQMath.gMount_Ver = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountVersion();
						EQMath.gMount_Features = EQCONTRL.EQ_GetMountFeatures();
						Common.readExtendedMountFunctions();
						Mount.readCustomMount();
						Common.readDriftVal(); // Read the Drift offset value

						EQMath.gRA_LastRate = 0;
						EQMath.gCurrent_time = 0;
						EQMath.gLast_time = 0;
						EQMath.gEmulRA_Init = 0;

						Alignment.gAlignmentStars_count = 0;

						EQMath.gEQRAPulseDuration = 0;
						EQMath.gEQDECPulseDuration = 0;
						EQMath.gEQPulsetimerflag = true;
						Alignment.gThreeStarEnable = false;
						Alignment.gSelectStar = 0;
						Alignment.gRA_GOTO = 0;
						Alignment.gDEC_GOTO = 0;

						EQMath.gRAMoveAxis_Rate = 0;
						EQMath.gDECMoveAxis_Rate = 0;

						//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Show();

						Common.ShowExtendedMountFunctions();

						// Make sure we have the right board
						EQMath.eqres = Mount.CheckMount(Common.gInitResult);

						if (Common.gInitResult == Common.EQ_OK && UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() == 1)
						{
							Limits.readRALimit();
							Mount.readCustomMount();
							EQMath.eqres = Mount.EQGetTotal360microstep(0);
							if (EQMath.eqres < 0x1000000)
							{
								EQMath.gTot_RA = EQMath.eqres;
								Goto.gRAGotoRes = Goto.gGotoResolution * 1296000 / EQMath.gTot_RA; // 1296000 = seconds per 360 degrees
								EQMath.gRAWormSteps = Common.EQGP(0, 10006);
								//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Add_Message(EQMath.gRAWormSteps.ToString() + " RAWormSteps read");
								if (Mount.gCustomMount == 0)
								{
									switch(Convert.ToInt32(EQMath.gRAWormSteps))
									{
										case 0 : 
											if (EQMath.gTot_RA == 5184000)
											{
												//AZEQ5GT detected, worm steps need fixing!
												EQMath.gRAWormSteps = 38400;
												//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
												HC.Add_Message("AZEQ5GT:RAWormSteps=38400");
											}
											else
											{
												// prevent divide by 0 later
												EQMath.gRAWormSteps = 1;
											} 
											 
											break;
										case 61866 : 
											if (EQMath.gTot_RA == 11136000)
											{
												//EQ8 detected, worm steps need fixing!
												EQMath.gRAWormSteps = 25600;
												//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
												HC.Add_Message("EQ8:RAWormSteps=25600");
											} 
											break;
										case 51200 : 
											// AZEQ6GT 
											break;
										case 50133 : 
											//EQ6Pro 
											break;
										case 66844 : 
											//HEQ5 
											break;
										case 35200 : 
											//EQ3 
											break;
										case 31288 : 
											// EQ4/EQ5 
											break;
										default:
											//                                ' mount isn't returning known values - read from ini instead 
											//                                Call readWormSteps 
											break;
									}
								}
								EQMath.gRAWormPeriod = Math.Floor((SPSD * EQMath.gRAWormSteps / EQMath.gTot_RA) + 0.5d);
							}

							EQMath.eqres = Mount.EQGetTotal360microstep(1);
							if (EQMath.eqres < 0x1000000)
							{
								EQMath.gTot_DEC = EQMath.eqres;
								Goto.gDECGotoRes = Goto.gGotoResolution * 1296000 / EQMath.gTot_DEC; // 1296000 = seconds per 360 degrees
								EQMath.gDECWormSteps = Common.EQGP(1, 10006);
								//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Add_Message(EQMath.gDECWormSteps.ToString() + " DECWormSteps read");
								if (Mount.gCustomMount == 0)
								{
									switch(Convert.ToInt32(EQMath.gDECWormSteps))
									{
										case 0 : 
											if (EQMath.gTot_DEC == 5184000)
											{
												//AZEQ5GT detected, worm steps need fixing!
												EQMath.gDECWormSteps = 38400;
												//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
												HC.Add_Message("AZEQ5GT:DECWormSteps=38400");
											}
											else
											{
												// prevent divide by 0 later
												EQMath.gDECWormSteps = 1;
											} 
											break;
										case 61866 : 
											if (EQMath.gTot_DEC == 11136000)
											{
												//EQ8 detected, worm steps need fixing!
												EQMath.gDECWormSteps = 25600;
												//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
												HC.Add_Message("EQ8:DECWormSteps=25600");
											} 
											break;
										case 51200 : 
											// AZEQ6GT 
											break;
										case 50133 : 
											//EQ6Pro 
											break;
										case 66844 : 
											//HEQ5 
											break;
										case 35200 : 
											//EQ3 
											break;
										case 31288 : 
											// EQ4/EQ5 
											break;
										default:
											//                                ' mount isn't returning known values - read from ini instead 
											//                                Call readWormSteps 
											break;
									}
								}
							}

							Tracking.gTrackFactorRA = Convert.ToDouble(Common.EQGP(0, 10004)) * EQMath.SID_RATE;
							Tracking.gTrackFactorDEC = Convert.ToDouble(Common.EQGP(0, 10005)) * EQMath.SID_RATE;

							//                i = CDbl(EQGP(0, 10002)) / (CDbl(EQGP(0, 10001)) / 86164)
							//                gTrackFactorRA = i * SID_RATE
							//                i = CDbl(EQGP(1, 10002)) / (CDbl(EQGP(1, 10001)) / 86164)
							//                gTrackFactorDEC = i * SID_RATE

							//Make sure motors are not running
							//                eqres = EQ_MotorStop(0)     ' Stop RA Motor
							//                eqres = EQ_MotorStop(1)     ' Stop DEC Motor
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(2); // Stop RA & DEC Motor

							//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(178));

							//Get state of at least one of the motors

							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMotorStatus(0);

							// If its an error then Initialize it

							if (EQMath.eqres == Common.EQ_NOTINITIALIZED)
							{
								icast = Convert.ToInt32(EQMath.gDECEncoder_Home_pos); // Typecast
								EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_InitMotors(Convert.ToInt32(EQMath.RAEncoder_Home_pos), icast);
							}


							// set up rates collection
							MaxRate = EQMath.SID_RATE * 800 / 3600d;
							Tracking.g_RAAxisRates.Add(MaxRate, 0d);
							Tracking.g_DECAxisRates.Add(MaxRate, 0d);


							//Make sure we get the latest data from the registry


							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.Add_Message(Convert.ToString(oLangDll.GetLangString(5132)) + " " + EQMath.gPort + ":" + Conversion.Str(EQMath.gBaud));
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.Add_Message(Convert.ToString(oLangDll.GetLangString(5133)) + " " + EQMath.printhex(UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountVersion()) + " DLL Version:" + EQMath.printhex(UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_DriverVersion()));
							//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.Add_Message("Using " + EQMath.gRAWormSteps.ToString() + "RAWormSteps");
							//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.EncoderTimer.Enabled = true;
							//UPGRADE_TODO: (1067) Member EncoderTimerFlag is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.EncoderTimerFlag = true;
							EQMath.gEQPulsetimerflag = true;
							//UPGRADE_TODO: (1067) Member Pulseguide_Timer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.Pulseguide_Timer.Enabled = false; //Enabled only during pulseguide session

							object[] tempAuxVar = readParkModes;
							Common.readAlignProximity();

							EQMath.gEQparkstatus = Convert.ToInt32(readparkStatus);

							if (EQMath.gEQparkstatus == 1)
							{
								// currently parked
								//UPGRADE_TODO: (1067) Member Frame15 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Frame15.Caption = Convert.ToString(oLangDll.GetLangString(146)) + " " + Convert.ToString(oLangDll.GetLangString(177));
								// Read Park position
								object[] tempAuxVar2 = readUnpark;
								// Preset the Encoder values to Park position
								EQMath.eqres = Common.EQSetMotorValues(0, EQMath.gRAEncoderUNPark);
								EQMath.eqres = Common.EQSetMotorValues(1, EQMath.gDECEncoderUNPark);
							}
							else
							{
								//UPGRADE_TODO: (1067) Member Frame15 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Frame15.Caption = Convert.ToString(oLangDll.GetLangString(146)) + " " + Convert.ToString(oLangDll.GetLangString(179));
							}
							object[] tempAuxVar3 = SetParkCaption;

							Common.readportrate(); // Read Autoguider port settings from registry and send to mount
							PEC.PEC_Initialise(); // only initialise PEc when we've defaults for worm

						}
						else
						{
							//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.EncoderTimer.Enabled = false;
							//UPGRADE_TODO: (1067) Member EncoderTimerFlag is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.EncoderTimerFlag = false;
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_End();
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.Add_Message(Convert.ToString(oLangDll.GetLangString(5135)) + " " + EQMath.gPort + ":" + Conversion.Str(EQMath.gBaud));

						}
					}
				}
				else
				{
					if (Common.ClientCount <= 2)
					{

						if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() == 1)
						{ // We update only if the mount is online
							object tempAuxVar4 = writeParkStatus[EQMath.gEQparkstatus];
						}

						//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.EncoderTimer.Enabled = false;
						//UPGRADE_TODO: (1067) Member EncoderTimerFlag is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.EncoderTimerFlag = false;

						//Save alignment and Sync data if scope is parked
						//Otherwise an re-alignment / re-Sync process has to be made on restart

						//       If gEQparkstatus = 1 Then
						//           Call WriteSyncMap
						//           Call WriteAlignMap
						//       End If

						EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_End();


					}

				}

			}
		}


		public double Declination
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.gDec;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(2, "GET Declination :" + EQMath.gDec.ToString());
				}
				return result;
			}
		}



		public double DeclinationRate
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.gDeclinationRate;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "GET DeclinationRate :" + EQMath.gDeclinationRate.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				object HC = null;
				object oLangDll = null;

				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "SET DeclinationRate(" + value.ToString() + ")");
				}
				m_DecRateAdjust = value;
				// don't action this if we're parked!
				if (EQMath.gEQparkstatus == 0)
				{
					if (value == 0 && m_RaRateAdjust == 0)
					{
						Tracking.EQStartSidereal2();
					}
					else
					{
						// if we're already tracking then apply the new rate.
						if (EQMath.gTrackingStatus != 0)
						{
							if ((EQMath.gDeclinationRate * value) <= 0)
							{
								Tracking.StartDEC_by_Rate(value);
							}
							else
							{
								Tracking.ChangeDEC_by_Rate(value);
							}

							EQMath.gTrackingStatus = 4;
							// Custom tracking!
							//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(189));
						}
					}
					EQMath.gDeclinationRate = value;
				}
				else
				{
					RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "Method DeclinationRate() " + ErrorConstants.MSG_SCOPE_PARKED);
				}
			}
		}


		public string Description
		{
			get
			{
				string result = "";
				object AscomTrace = null;
				result = "EQMOD ASCOM Driver";
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET Destription :" + result);
				}
				return result;
			}
		}


		public string DriverInfo
		{
			get
			{
				string result = "";
				object AscomTrace = null;
				//
				// Use the Project/Properties sheet, Make tab, to set these
				// items. That way they will show in the Version tab of the
				// Explorer property sheet, and the exact same data will
				// show in Telescope.DriverInfo.
				//
				result = "EQASCOM " + Common.gVersion + Environment.NewLine;
				if (Application.CompanyName != "")
				{
					result = result + Environment.NewLine + Application.CompanyName;
				}
				if (FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LegalCopyright != "")
				{
					result = result + Environment.NewLine + FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LegalCopyright;
				}
				if (FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Comments != "")
				{
					result = result + Environment.NewLine + FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Comments;
				}

				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET DriverInfo :" + result);
				}

				return result;
			}
		}


		public double FocalLength
		{
			get
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET FocalLength :NOT_SUPPORTED");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property FocalLength" + ErrorConstants.MSG_NOT_IMPLEMENTED);
				return 0;
			}
		}


		public string name
		{
			get
			{
				string result = "";
				object AscomTrace = null;
				result = Definitions.ASCOM_DESC; // 1-word name
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET Name :" + result);
				}
				return result;
			}
		}


		public double RightAscension
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = Common.GetEmulRA_EQ();
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(2, "GET RightAscension :" + result.ToString());
				}
				return result;
			}
		}



		public double RightAscensionRate
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				if (EQMath.gHemisphere == 0)
				{
					result = EQMath.gRightAscensionRate - EQMath.SID_RATE;
				}
				else
				{
					result = EQMath.gRightAscensionRate + EQMath.SID_RATE;
				}
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "GET RightAscensionRate :" + result.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				object HC = null;
				object oLangDll = null;

				//newval is in arcseconds , convert to degrees
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "SET RightAscensionRate(" + value.ToString() + ")");
				}

				// don't action this if we're parked!
				m_RaRateAdjust = value;
				if (EQMath.gEQparkstatus == 0)
				{
					if (value == 0 && m_DecRateAdjust == 0)
					{
						Tracking.EQStartSidereal2();
					}
					else
					{
						if (EQMath.gHemisphere == 0)
						{
							value = EQMath.SID_RATE + value; // Treat newval as an offset
						}
						else
						{
							value -= EQMath.SID_RATE; // Treat newval as an offset
						}
						// if we're already tracking then apply the new rate.
						if (EQMath.gTrackingStatus != 0)
						{
							if ((EQMath.gRightAscensionRate * value) <= 0)
							{
								Tracking.StartRA_by_Rate(value);
							}
							else
							{
								Tracking.ChangeRA_by_Rate(value);
							}
							EQMath.gTrackingStatus = 4;
							// Custom tracking!
							//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(189));
						}
					}
					EQMath.gRightAscensionRate = value;
				}
				else
				{
					m_RaRateAdjust = 0;
					RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "Method RightAscensionRate() " + ErrorConstants.MSG_SCOPE_PARKED);
				}
			}
		}


		public double SiderealTime
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD);
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET SiderealTime :" + result.ToString());
				}
				return result;
			}
		}



		public double SiteElevation
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.gElevation;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET SiteElevation :" + result.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				object[] UpdateSiteControls = null;
				if (Common.gAscomCompatibility.AllowSiteWrites)
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(0, "SET SiteElevation :" + value.ToString());
					}
					if ((value < -300) || (value > 10000))
					{
						RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, ErrorConstants.MSG_VAL_OUTOFRANGE);
					}
					else
					{
						EQMath.gElevation = value;
						object[] tempAuxVar = UpdateSiteControls;
					}
				}
				else
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(0, "SET SiteElevation :NOT_SUPPORTED");
					}
					RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property SiteElevation" + ErrorConstants.MSG_NOT_IMPLEMENTED);
				}
			}
		}



		public double SiteLatitude
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.gLatitude;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET SiteLatitude :" + result.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				object[] UpdateSiteControls = null;
				if (Common.gAscomCompatibility.AllowSiteWrites)
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(0, "SET SiteLatitude :" + value.ToString());
					}
					if ((value < -90) || (value > 90))
					{
						RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, ErrorConstants.MSG_VAL_OUTOFRANGE);
					}
					else
					{
						EQMath.gLatitude = value;
						object[] tempAuxVar = UpdateSiteControls;
					}
				}
				else
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(0, "SET SiteLatitude :NOT_SUPPORTED");
					}
					RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property SiteLatitude" + ErrorConstants.MSG_NOT_IMPLEMENTED);
				}
			}
		}



		public double SiteLongitude
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				result = EQMath.gLongitude;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET SiteLongitude :" + result.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				object[] UpdateSiteControls = null;
				if (Common.gAscomCompatibility.AllowSiteWrites)
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(0, "SET SiteLongitude :" + value.ToString());
					}
					if ((value < -180) || (value > 180))
					{
						RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, ErrorConstants.MSG_VAL_OUTOFRANGE);
					}
					else
					{
						EQMath.gLongitude = value;
					}
					object[] tempAuxVar = UpdateSiteControls;
				}
				else
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(0, "SET SiteLongitude :NOT_SUPPORTED");
					}
					RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property SiteLongitude" + ErrorConstants.MSG_NOT_IMPLEMENTED);
				}
			}
		}


		public bool Slewing
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				switch(EQMath.gEQparkstatus)
				{
					case 0 : 
						// unparked 
						result = EQMath.gSlewStatus; 
						if (!result)
						{
							result = Tracking.gMoveAxisSlewing;
						} 
						break;
					case 1 : 
						// parked 
						result = false; 
						break;
					case 2 : 
						// parking 
						result = true; 
						break;
				}
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(6, "GET Slewing :" + result.ToString());
				}
				return result;
			}
		}



		public int SlewSettleTime
		{
			get
			{
				int result = 0;
				object AscomTrace = null;
				result = m_iSettleTime;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(6, "GET SlewSettleTime :" + result.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(6, "LET SlewSettleTime(" + value.ToString() + ")");
				}
				if ((value < 0) || (value > 100))
				{
					RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, ErrorConstants.MSG_VAL_OUTOFRANGE);
				}
				m_iSettleTime = value;
			}
		}



		public double TargetDeclination
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				if (Goto.gTargetDec == Common.EQ_INVALIDCOORDINATE)
				{
					RaiseError(ErrorConstants.SCODE_VALUE_NOT_SET, ErrorConstants.ERR_SOURCE, "Property TargetDeclination " + ErrorConstants.MSG_PROP_NOT_SET);
				}
				result = Goto.gTargetDec;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(6, "GET TargetDeclination :" + result.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(6, "Let TargetDeclination(" + value.ToString() + ")");
				}
				if (value > 90 || value < -90)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "Property TargetDeclination " + ErrorConstants.MSG_VAL_OUTOFRANGE);
				}
				else
				{
					Goto.gTargetDec = value;
				}
			}
		}



		public double TargetRightAscension
		{
			get
			{
				double result = 0;
				object AscomTrace = null;
				if (Goto.gTargetRA == Common.EQ_INVALIDCOORDINATE)
				{
					RaiseError(ErrorConstants.SCODE_VALUE_NOT_SET, ErrorConstants.ERR_SOURCE, "Property TargetRightAscension " + ErrorConstants.MSG_PROP_NOT_SET);
				}
				result = Goto.gTargetRA;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(6, "GET TargetRightAscension :" + result.ToString());
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(6, "Let TargetRightAscension(" + value.ToString() + ")");
				}
				if (value > 24 || value < 0)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "Property TargetRightAscension " + ErrorConstants.MSG_VAL_OUTOFRANGE);
				}
				else
				{
					Goto.gTargetRA = value;
				}
			}
		}



		public bool Tracking
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				result = EQMath.gTrackingStatus != 0;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "GET Tracking :" + result.ToString());
				}
				return result;
			}
			set
			{
				object[] EQ_Beep = null;
				object AscomTrace = null;
				object HC = null;
				object oLangDll = null;
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(5, "LET Tracking(" + value.ToString() + ")");
				}
				if (EQMath.gEQparkstatus == 0 || (EQMath.gEQparkstatus == 1 && !value))
				{
					if (value)
					{

						if (m_RaRateAdjust == 0 && m_DecRateAdjust == 0)
						{
							// track at sidereal
							Tracking.EQStartSidereal2();
							EQMath.gEmulOneShot = true; // Get One shot cap
						}
						else
						{
							// track at custom rate
							EQMath.gRA_LastRate = 0;
							EQMath.gDeclinationRate = m_DecRateAdjust;
							EQMath.gRightAscensionRate = EQMath.SID_RATE + m_RaRateAdjust;
							if (PEC.gPEC_Enabled)
							{
								PEC.PEC_StopTracking();
							}
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							Tracking.CustomMoveAxis(0, EQMath.gRightAscensionRate, true, Convert.ToString(oLangDll.GetLangString(189)));
							//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							Tracking.CustomMoveAxis(1, EQMath.gDeclinationRate, true, Convert.ToString(oLangDll.GetLangString(189)));
						}

					}
					else
					{

						//            eqres = EQ_MotorStop(0)
						//            eqres = EQ_MotorStop(1)
						EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(2);
						object tempAuxVar = EQ_Beep[7];
						EQMath.gTrackingStatus = 0;
						// not sure that we should be clearing the rate offests ASCOM Spec is no help
						EQMath.gDeclinationRate = 0;
						EQMath.gRightAscensionRate = 0;

						//UPGRADE_TODO: (1067) Member TrackingFrame is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.TrackingFrame.Caption = Convert.ToString(oLangDll.GetLangString(121)) + " " + Convert.ToString(oLangDll.GetLangString(178));

					}
				}
				else
				{
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message(oLangDll.GetLangString(5013));
					RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "Tracking change " + ErrorConstants.MSG_SCOPE_PARKED);
				}

			}
		}



		public System.DateTime UTCDate
		{
			get
			{
				System.DateTime result = DateTime.FromOADate(0);
				object AscomTrace = null;
				object utc_offs = null;
				//UPGRADE_WARNING: (1068) utc_offs() of type Variant is being forced to double. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
				result = DateTime.FromOADate(DateTime.Now.ToOADate() + (Convert.ToDouble(((Array) utc_offs).GetValue()) / 86400d));
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "GET Date :" + DateTimeHelper.ToString(result));
				}
				return result;
			}
			set
			{
				object AscomTrace = null;
				//Impossible to set own PC time
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(0, "SET Date :NOT_SUPPORTED");
				}
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Property UTCDate" + ErrorConstants.MSG_NOT_IMPLEMENTED);
			}
		}

		public int ramotor
		{
			get
			{
				//RAMotor = EQGetMotorValues(0)
				return Convert.ToInt32(Common.GetEmulRA());
			}
		}


		public int DECMotor
		{
			get
			{
				//DECMotor = EQGetMotorValues(1)
				return Convert.ToInt32(EQMath.gEmulDEC);
			}
		}


		public int raEncoder
		{
			get
			{
				return Common.EQGetMotorValues(3);
			}
		}


		public int DecEncoder
		{
			get
			{
				return Common.EQGetMotorValues(4);
			}
		}



		public int SyncRAMotor
		{
			get
			{
				int result = 0;
				EQMath.gEmulRA = Common.EQGetMotorValues(0);
				result = Convert.ToInt32(EQMath.gEmulRA);
				EQMath.gLast_time = EQMath.EQnow_lst_norange();
				EQMath.gCurrent_time = EQMath.gLast_time;
				EQMath.gEmulRA_Init = EQMath.gEmulRA;
				return result;
			}
		}


		public int SyncDECMotor
		{
			get
			{
				EQMath.gEmulDEC = Common.EQGetMotorValues(1);
				return Convert.ToInt32(EQMath.gEmulDEC);
			}
		}

		public int RAWormPeriod
		{
			get
			{
				return Convert.ToInt32(EQMath.gRAWormPeriod);
			}
		}

		public double RAWormPeriodFloat
		{
			get
			{
				double SPSD = 0;
				return SPSD * EQMath.gRAWormSteps / EQMath.gTot_RA;
			}
		}

		public double PecGain
		{
			get
			{
				return PEC.gPEC_Gain;
			}
			set
			{
				object PECConfigFrm = null;
				//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				PECConfigFrm.GainScroll.Value = value * 10;
			}
		}


		public bool IsPulseGuiding
		{
			get
			{
				bool result = false;
				object AscomTrace = null;
				if (Common.gAscomCompatibility.AllowPulseGuide)
				{
					result = (EQMath.gEQRAPulseDuration + EQMath.gEQDECPulseDuration) != 0;
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(4, "Get IsPulseGuiding :" + result.ToString());
					}
				}
				else
				{
					RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Read Property IsPulseGuidng :NOT SUPPORTED");
				}
				return result;
			}
		}


		public double DecNoAdjust
		{
			get
			{
				return EQMath.gDec_DegNoAdjust;
			}
		}


		public double PulseGuideRateRa
		{
			get
			{
				return Common.gPulseguideRateRa;
			}
		}


		public double PulseGuideRateDec
		{
			get
			{
				return Common.gPulseguideRateDec;
			}
		}


		//UPGRADE_ISSUE: (2068) AlignmentModes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (get AlignmentMode) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.AlignmentModes AlignmentMode()
		//{
			//UpgradeStubs.AlignmentModes result = null;
			//result = AlignmentMode;
			//return result;
		//}
		//UPGRADE_ISSUE: (2068) AlignmentModes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068

		//UPGRADE_NOTE: (7001) The following declaration (get altitude) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double altitude()
		//{
			//double result = 0;
			//result = altitude;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get ApertureArea) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double ApertureArea()
		//{
			//double result = 0;
			//result = ApertureArea;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get ApertureDiameter) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double ApertureDiameter()
		//{
			//double result = 0;
			//result = ApertureDiameter;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get AtHome) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool AtHome()
		//{
			//bool result = false;
			//result = AtHome;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get AtPark) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool AtPark()
		//{
			//bool result = false;
			//result = AtPark;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get Azimuth) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double Azimuth()
		//{
			//double result = 0;
			//result = Azimuth;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanFindHome) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanFindHome()
		//{
			//bool result = false;
			//result = CanFindHome;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanPark) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanPark()
		//{
			//bool result = false;
			//result = CanPark;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanPulseGuide) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanPulseGuide()
		//{
			//bool result = false;
			//result = CanPulseGuide;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSetDeclinationRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSetDeclinationRate()
		//{
			//bool result = false;
			//result = CanSetDeclinationRate;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSetGuideRates) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSetGuideRates()
		//{
			//bool result = false;
			//result = CanSetGuideRates;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSetPark) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSetPark()
		//{
			//bool result = false;
			//result = CanSetPark;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSetPierSide) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSetPierSide()
		//{
			//bool result = false;
			//result = CanSetPierSide;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSetRightAscensionRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSetRightAscensionRate()
		//{
			//bool result = false;
			//result = CanSetRightAscensionRate;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSetTracking) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSetTracking()
		//{
			//bool result = false;
			//result = CanSetTracking;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSlew) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSlew()
		//{
			//bool result = false;
			//result = CanSlew;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSlewAltAz) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSlewAltAz()
		//{
			//bool result = false;
			//result = CanSlewAltAz;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSlewAltAzAsync) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSlewAltAzAsync()
		//{
			//bool result = false;
			//result = CanSlewAltAzAsync;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSlewAsync) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSlewAsync()
		//{
			//bool result = false;
			//result = CanSlewAsync;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSync) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSync()
		//{
			//bool result = false;
			//result = CanSync;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanSyncAltAz) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanSyncAltAz()
		//{
			//bool result = false;
			//result = CanSyncAltAz;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get CanUnpark) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool CanUnpark()
		//{
			//bool result = false;
			//result = CanUnpark;
			//return result;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get Connected) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool Connected()
		//{
			//bool result = false;
			//result = Connected;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let Connected) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void Connected(bool value)
		//{
			//Connected = value;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get Declination) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double Declination()
		//{
			//double result = 0;
			//result = Declination;
			//return result;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get DeclinationRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double DeclinationRate()
		//{
			//double result = 0;
			//result = DeclinationRate;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let DeclinationRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void DeclinationRate(double value)
		//{
			//DeclinationRate = value;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get Description) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private string Description()
		//{
			//string result = "";
			//result = Description;
			//return result;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get DoesRefraction) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool DoesRefraction()
		//{
			//bool result = false;
			//result = DoesRefraction;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let DoesRefraction) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void DoesRefraction(bool value)
		//{
			//DoesRefraction = value;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get DriverInfo) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private string DriverInfo()
		//{
			//string result = "";
			//result = DriverInfo;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get DriverVersion) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private string DriverVersion()
		//{
			//string result = "";
			//result = DriverVersion;
			//return result;
		//}

		//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (get EquatorialSystem) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.EquatorialCoordinateType EquatorialSystem()
		//{
			//UpgradeStubs.EquatorialCoordinateType result = null;
			//result = EquatorialSystem;
			//return result;
		//}
		//UPGRADE_ISSUE: (2068) EquatorialCoordinateType object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068

		//UPGRADE_NOTE: (7001) The following declaration (get FocalLength) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double FocalLength()
		//{
			//double result = 0;
			//result = FocalLength;
			//return result;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get GuideRateDeclination) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double GuideRateDeclination()
		//{
			//double result = 0;
			//result = GuideRateDeclination;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let GuideRateDeclination) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void GuideRateDeclination(double value)
		//{
			//GuideRateDeclination = value;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get GuideRateRightAscension) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double GuideRateRightAscension()
		//{
			//double result = 0;
			//result = GuideRateRightAscension;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let GuideRateRightAscension) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void GuideRateRightAscension(double value)
		//{
			//GuideRateRightAscension = value;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get InterfaceVersion) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private int InterfaceVersion()
		//{
			//int result = 0;
			//result = InterfaceVersion;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get IsPulseGuiding) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool IsPulseGuiding()
		//{
			//bool result = false;
			//result = IsPulseGuiding;
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get name) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private string name()
		//{
			//string result = "";
			//return "I_" + result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get RightAscension) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double RightAscension()
		//{
			//double result = 0;
			//result = RightAscension;
			//return result;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get RightAscensionRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double RightAscensionRate()
		//{
			//double result = 0;
			//result = RightAscensionRate;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let RightAscensionRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void RightAscensionRate(double value)
		//{
			//RightAscensionRate = value;
		//}


		//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (get SideOfPier) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.PierSide SideOfPier()
		//{
			//UpgradeStubs.PierSide result = null;
			//result = SideOfPier;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let SideOfPier) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void SideOfPier(UpgradeStubs.PierSide value)
		//{
			////UPGRADE_WARNING: (1068) SideOfPier of type PierSide is being forced to Scalar. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
			//SideOfPier = value;
		//}
		//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068

		//UPGRADE_NOTE: (7001) The following declaration (get SiderealTime) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double SiderealTime()
		//{
			//double result = 0;
			//result = SiderealTime;
			//return result;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get SiteElevation) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double SiteElevation()
		//{
			//double result = 0;
			//result = SiteElevation;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let SiteElevation) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void SiteElevation(double value)
		//{
			//SiteElevation = value;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get SiteLatitude) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double SiteLatitude()
		//{
			//double result = 0;
			//result = SiteLatitude;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let SiteLatitude) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void SiteLatitude(double value)
		//{
			//SiteLatitude = value;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get SiteLongitude) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double SiteLongitude()
		//{
			//double result = 0;
			//result = SiteLongitude;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let SiteLongitude) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void SiteLongitude(double value)
		//{
			//SiteLongitude = value;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (get Slewing) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool Slewing()
		//{
			//bool result = false;
			//result = Slewing;
			//return result;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get SlewSettleTime) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private int SlewSettleTime()
		//{
			//int result = 0;
			//result = SlewSettleTime;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let SlewSettleTime) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void SlewSettleTime(int value)
		//{
			//SlewSettleTime = value;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get TargetDeclination) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double TargetDeclination()
		//{
			//double result = 0;
			//result = TargetDeclination;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let TargetDeclination) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void TargetDeclination(double value)
		//{
			//TargetDeclination = value;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get TargetRightAscension) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double TargetRightAscension()
		//{
			//double result = 0;
			//result = TargetRightAscension;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let TargetRightAscension) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void TargetRightAscension(double value)
		//{
			//TargetRightAscension = value;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get Tracking) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool Tracking()
		//{
			//bool result = false;
			//result = Tracking;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let Tracking) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void Tracking(bool value)
		//{
			//Tracking = value;
		//}


		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (get TrackingRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.DriveRates TrackingRate()
		//{
			//UpgradeStubs.DriveRates result = null;
			//result = TrackingRate;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let TrackingRate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void TrackingRate(UpgradeStubs.DriveRates value)
		//{
			////UPGRADE_WARNING: (1068) TrackingRate of type DriveRates is being forced to Scalar. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
			//TrackingRate = value;
		//}
		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068

		//UPGRADE_ISSUE: (2068) ITrackingRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (get TrackingRates) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.ITrackingRates TrackingRates()
		//{
			////
			// Note that this more or less "casts" our internal TrackingRates
			// object's interface to ITrackingRates.
			////
			////UPGRADE_ISSUE: (2068) ITrackingRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
			//UpgradeStubs.ITrackingRates result = null;
			//return (UpgradeStubs.ITrackingRates) result;
		//}
		//UPGRADE_ISSUE: (2068) ITrackingRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068


		//UPGRADE_NOTE: (7001) The following declaration (get UTCDate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private System.DateTime UTCDate()
		//{
			//System.DateTime result = DateTime.FromOADate(0);
			//result = UTCDate;
			//return result;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let UTCDate) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void UTCDate(System.DateTime value)
		//{
			//UTCDate = value;
		//}

		//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public object AxisRates(UpgradeStubs.TelescopeAxes axis)
		{
			object result = null;
			object AscomTrace = null;
			object axisPrimary = null;
			object axisSecondary = null;

			if (axis == axisPrimary)
			{
				result = Tracking.g_RAAxisRates;
			}
			else if (axis == axisSecondary)
			{ 
				result = Tracking.g_DECAxisRates;
			}
			else
			{
				result = new Rates();
			}

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				//UPGRADE_WARNING: (1068) axis of type TelescopeAxes is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "AxisRates(" + axis + ")");
			}
			return result;
		}

		//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public bool CanMoveAxis(UpgradeStubs.TelescopeAxes axis)
		{
			object AscomTrace = null;
			object axisPrimary = null;
			object axisSecondary = null;

			if (Common.gAscomCompatibility.Strict)
			{
				//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
				{
					//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
					//UPGRADE_WARNING: (1068) axis of type TelescopeAxes is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
					//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					AscomTrace.Add_log(3, "CanMoveAxis(" + axis + ") :True");
				}
				return false;
			}
			else
			{
				if (axis == axisPrimary || axis == axisSecondary)
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
						//UPGRADE_WARNING: (1068) axis of type TelescopeAxes is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(3, "CanMoveAxis(" + axis + ") :True");
					}
					return true;

				}
				else
				{
					//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
					{
						//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
						//UPGRADE_WARNING: (1068) axis of type TelescopeAxes is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
						//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						AscomTrace.Add_log(3, "CanMoveAxis(" + axis + ") :False");
					}
					return false;
				}
			}

		}

		public int DestinationSideOfPier(double destRa, double destDec)
		{
			int result = 0;
			object AscomTrace = null;
			object[, ] range = null;
			int pierEast = 0;
			int pierUnknown = 0;
			int pierWest = 0;
			double ha = 0;
			double RaEnc = 0;
			double DecEnc = 0;
			double Dec_DegNoAdjust = 0;

			if (ValidateRADEC(destRa, destDec))
			{
				switch(Common.gAscomCompatibility.SideOfPier)
				{
					case 0 : 
						// pointing 
						// estsablish the encoder values for the target destination 
						Goto.CalcEncoderGotoTargets(destRa, destDec, ref RaEnc, ref DecEnc); 
						// convert dec encoder postion to degrees of axis rotation 
						Dec_DegNoAdjust = EQMath.Get_EncoderDegrees(EQMath.gDECEncoder_Zero_pos, DecEnc, EQMath.gTot_DEC, EQMath.gHemisphere); 
						// work out side of pier 
						result = (int) EQMath.SOP_Pointing(Dec_DegNoAdjust); 

						 
						break;
					case 1 : 
						break;
					case 3 : 
						// physical 
						ha = EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD) - destRa; 
						object tempAuxVar = range[Convert.ToInt32(ha), 24L]; 
						if (ha < 12d)
						{
							if (Common.gAscomCompatibility.SwapPhysicalSideOfPier)
							{
								result = pierWest;
							}
							else
							{
								result = pierEast;
							}
						}
						else
						{
							if (Common.gAscomCompatibility.SwapPhysicalSideOfPier)
							{
								result = pierEast;
							}
							else
							{
								result = pierWest;
							}
						} 

						 
						break;
					case 2 : 
						// just don't know or care! 
						result = pierUnknown; 
						 
						break;
					//UPGRADE_NOTE: (7001) The following case (switch) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
					//case 3 : 
						// 
						//break;
				}
			}
			else
			{
				RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "DestinationSideOfPier() " + ErrorConstants.MSG_VAL_OUTOFRANGE);
			}

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(0, "DestinationSideOfPier(" + destRa.ToString() + "," + destDec.ToString() + ")=" + result.ToString());
			}
			return result;
		}

		//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public void MoveAxis(UpgradeStubs.TelescopeAxes axis, double rate)
		{
			object AscomTrace = null;
			object axisPrimary = null;
			object axisSecondary = null;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				//UPGRADE_WARNING: (1068) axis of type TelescopeAxes is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "COMMAND MoveAxis(" + axis + "," + rate.ToString() + ")");
			}

			if (Common.gAscomCompatibility.Strict)
			{
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method MoveAxis() " + ErrorConstants.MSG_NOT_IMPLEMENTED);
				return;
			}

			if (EQMath.gEQparkstatus != 0)
			{
				// no move axis if parked or parking!
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "Method MoveAxis() " + ErrorConstants.MSG_SCOPE_PARKED);
				return;
			}

			if (axis == axisPrimary)
			{
				if (Common.RateIsInRange(rate, Tracking.g_RAAxisRates))
				{
					if (!EQMath.gSlewStatus)
					{
						Tracking.EQMoveAxis(0, rate);
					}
				}
				else
				{
					RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "Method MoveAxis() " + ErrorConstants.MSG_VAL_OUTOFRANGE);
				}
			}
			else if (axis == axisSecondary)
			{ 
				if (Common.RateIsInRange(rate, Tracking.g_DECAxisRates))
				{
					if (!EQMath.gSlewStatus)
					{
						Tracking.EQMoveAxis(1, rate);
					}
				}
				else
				{
					RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "Method MoveAxis() " + ErrorConstants.MSG_VAL_OUTOFRANGE);
				}
			}
			else
			{
				RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "Method MoveAxis() " + ErrorConstants.MSG_VAL_OUTOFRANGE);
			}

		}

		public void SlewToAltAz(double Azimuth, double altitude)
		{
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "SlewToAltAz(" + Azimuth.ToString() + "," + altitude.ToString() + ")");
			}
			RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method SlewToAltAz()" + ErrorConstants.MSG_NOT_IMPLEMENTED);
		}

		public void SlewToAltAzAsync(double Azimuth, double altitude)
		{
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "SlewToAltAzAsync(" + Azimuth.ToString() + "," + altitude.ToString() + ")");
			}
			RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method SlewToAltAzAsync()" + ErrorConstants.MSG_NOT_IMPLEMENTED);
		}
		public void SyncToAltAz(double Azimuth, double altitude)
		{
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(7, "SyncToAltAz(" + Azimuth.ToString() + "," + altitude.ToString() + ")");
			}
			RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method SyncToAltAz()" + ErrorConstants.MSG_NOT_IMPLEMENTED);
		}

		//----------------------------------------------
		//-------Some Extra COM interfaces -------------
		//----------------------------------------------

		//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public void PecMoveAxis(UpgradeStubs.TelescopeAxes axis, double rate)
		{
			object axisPrimary = null;
			object axisSecondary = null;
			// special sub for PEC control - normal moveaxis results in background polling of motor positions
			if (!EQMath.gSlewStatus)
			{
				if (axis == axisPrimary)
				{
					PEC.PEC_MoveAxis(0, rate * 3600);
				}
				if (axis == axisSecondary)
				{
					PEC.PEC_MoveAxis(1, rate * 3600);
				}
			}
		}

		public int MoveMotor(int motor_id, int hemisphere, int direction, int Steps, int stepslowdown)
		{
			return Common.EQStartMoveMotor(motor_id, hemisphere, direction, Steps, stepslowdown);
		}



		// ------------------------- Methods Portion of the Code -------------


		public void AbortSlew()
		{
			object AscomTrace = null;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "COMMAND AbortSlew");
			}
			if (EQMath.gEQparkstatus != 0)
			{
				// no move axis if parked or parking!
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "AbortSlew() " + ErrorConstants.MSG_SCOPE_PARKED);
				return;
			}

			if (EQMath.gSlewStatus)
			{
				EQMath.gSlewStatus = false;
				// stop the slew if already slewing
				//        eqres = EQ_MotorStop(0)
				//        eqres = EQ_MotorStop(1)
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(2);
				EQMath.gRAStatus_slew = false;

				// restart tracking
				Tracking.RestartTracking();

			}

		}

		public void CommandBlind(string command, bool Raw = false)
		{
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(0, "COMMAND CommandBlind(" + command + " :NOT_SUPPORTED");
			}
			RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method xxx()" + ErrorConstants.MSG_NOT_IMPLEMENTED);

		}

		public bool CommandBool(string command, bool Raw = false)
		{
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(0, "COMMAND CommandBool(" + command + " :NOT_SUPPORTED");
			}
			RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method CommandBlind()" + ErrorConstants.MSG_NOT_IMPLEMENTED);

			return false;
		}

		public string CommandString(string command, bool Raw = false)
		{
			string result = "";
			object AscomTrace = null;
			object[] ApplyUnParkMode2 = null;
			object HC = null;
			object[] ApplyParkMode2 = null;
			object PECConfigFrm = null;

			int pos = 0;
			string tmpstr = "";
			double arg1 = 0;
			double arg2 = 0;
			double arg3 = 0;
			int tmp1 = 0;

			string[] comStr = null;
			bool ParseErr = false;
			int res = 0;
			byte[] txbuf = null;
			byte[] rxbuf = new byte[20];
			int i = 0;
			int p1 = 0;
			int p2 = 0;

			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (HandleError)");

			if (command.StartsWith(">"))
			{
				if (Strings.Len(command) >= 3)
				{
					// its a mount protocol thing
					txbuf = new byte[20];
					command = command.Substring(0, Math.Min(20, command.Length));
					int tempForEndVar = Strings.Len(command) - 1;
					for (i = 0; i <= tempForEndVar; i++)
					{
						txbuf[i] = (byte) Strings.Asc(command.Substring(i, Math.Min(1, command.Length - i))[0]);
					}
					txbuf[0] = 58; //":"
					GCHandle gh = GCHandle.Alloc(txbuf[0], GCHandleType.Pinned);
					IntPtr tmpPtr = gh.AddrOfPinnedObject();
					p1 = tmpPtr.ToInt32();
					gh.Free();
					GCHandle gh2 = GCHandle.Alloc(rxbuf[0], GCHandleType.Pinned);
					IntPtr tmpPtr2 = gh2.AddrOfPinnedObject();
					p2 = tmpPtr2.ToInt32();
					gh2.Free();
					res = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_QueryMount(p1, p2, 20);
					result = "";
					for (i = 0; i <= 19; i++)
					{
						if (rxbuf[i] != 0)
						{
							result = result + Strings.Chr(rxbuf[i]).ToString();
						}
					}
					result = result.Trim();
				}
				return result;
			}


			ParseErr = false;
			if (command.Substring(Math.Max(command.Length - 1, 0)) != "#")
			{
				ParseErr = true;
			}
			else
			{
				command = command.Substring(0, Math.Min(Strings.Len(command) - 1, command.Length));
				comStr = (string[]) command.Split(',');

				switch(comStr[0])
				{
					case ":PECGAIN" : 
						switch(comStr.GetUpperBound(0))
						{
							case 1 : 
								//Update the PEC Gain value 
								if (PEC.PEC_SetGain(comStr[1]))
								{
									result = "1#";
								}
								else
								{
									result = "0#";
								} 
								break;
							case 0 : 
								//UPGRADE_TODO: (1067) Member GainScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								result = Strings.FormatNumber(PECConfigFrm.GainScroll.Value, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#"; 
								break;
							default:
								ParseErr = true; 
								break;
						} 
						 
						break;
					case ":PECPHASE" : 
						switch(comStr.GetUpperBound(0))
						{
							case 1 : 
								if (PEC.PEC_SetPhase(comStr[1]))
								{
									result = "1#";
								}
								else
								{
									result = "0#";
								} 
								break;
							case 0 : 
								//UPGRADE_TODO: (1067) Member PhaseScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								result = Strings.FormatNumber(PECConfigFrm.PhaseScroll.Value, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#"; 
								break;
							default:
								ParseErr = true; 
								break;
						} 
						 
						break;
					case ":PECFILE" : 
						result = PEC.PECDef1.FileName + "#"; 
						 
						break;
					case ":PECLOAD" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							// Load a PEC file
							if (PEC.PEC_LoadFile(comStr[1]))
							{
								result = "1#";
							}
							else
							{
								result = "0#";
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":PECSAVE" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							// Save a PEC file
							if (PEC.PEC_SaveFile(comStr[1], ref PEC.PECDef1))
							{
								result = "1#";
							}
							else
							{
								result = "0#";
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":PECENA" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							switch(comStr[1])
							{
								case "1" : 
									//enable PEC 
									//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.CheckPEC.Value = 1; 
									PEC.PEC_StartTracking(); 
									break;
								case "0" : 
									//disable PEC 
									//UPGRADE_TODO: (1067) Member CheckPEC is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.CheckPEC.Value = 0; 
									PEC.PEC_StopTracking(); 
									break;
								default:
									ParseErr = true; 
									break;
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":PECSTA" : 
						// read pec state 
						if (PEC.gPEC_Enabled)
						{
							result = "1#";
						}
						else
						{
							result = "0#";
						} 
						 
						break;
					case ":PECWTC" : 
						// read worm tooth count 
						if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() != 1)
						{
							result = Strings.FormatNumber(-1, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						}
						else
						{
							pos = Convert.ToInt32(EQMath.gTot_RA / EQMath.gRAWormSteps);
							result = Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						} 
						 
						break;
					case ":PECIDX" : 
						// read worm position 
						if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() != 1)
						{
							result = Strings.FormatNumber(-1, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						}
						else
						{
							pos = Convert.ToInt32(PEC.NormalisePosition(SyncRAMotor, EQMath.gRAWormSteps));
							result = Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						} 
						 
						break;
					case ":PECINFO" : 
						// read worm 
						if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() != 1)
						{
							result = Strings.FormatNumber(-1, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "," + Strings.FormatNumber(-1, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						}
						else
						{
							pos = Convert.ToInt32(Math.Floor(EQMath.gRAWormSteps - 1));
							result = Strings.FormatNumber(Math.Floor(EQMath.gRAWormPeriod), 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "," + Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						} 
						 
						break;
					case ":PECSET" : 
						// Write a PEC table entry 
						if (comStr.GetUpperBound(0) == 3)
						{
							arg1 = Convert.ToInt32(Double.Parse(comStr[1]));
							arg2 = Convert.ToInt32(Double.Parse(comStr[2]));
							arg3 = Double.Parse(comStr[3]) / 1000d;
							if (PEC.PEC_Write_Table(arg1, arg2, arg3))
							{
								result = "1#";
							}
							else
							{
								result = "0#";
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":PECGET" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							// Read a PEC table entry
							arg1 = Convert.ToInt32(Double.Parse(comStr[1]));
							if (arg1 >= 0 && arg1 <= PEC.PECDef1.PECCurve.GetUpperBound(0))
							{
								pos = Convert.ToInt32(PEC.PECDef1.PECCurve[Convert.ToInt32(arg1)].signal * 1000);
								tmpstr = Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False);
								pos = Convert.ToInt32(PEC.PECDef1.PECCurve[Convert.ToInt32(arg1)].PEPosition);
								result = "1," + Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "," + tmpstr + "#";
							}
							else
							{
								result = "#0";
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":PARK" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							tmp1 = Convert.ToInt32(Conversion.Val(comStr[1]));
							if (tmp1 >= 0 && tmp1 <= 7)
							{
								if (tmp1 == 0)
								{
									//UPGRADE_TODO: (1067) Member ApplyParkMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.ApplyParkMode();
								}
								else
								{
									object tempAuxVar = ApplyParkMode2[tmp1 - 1];
								}
							}
							if (EQMath.gEQparkstatus != 0)
							{
								// mount is parked or is parking
								result = "1#";
							}
							else
							{
								// mount is still unparked
								result = "0#";
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":UNPARK" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							tmp1 = Convert.ToInt32(Conversion.Val(comStr[1]));
							if (tmp1 >= 0 && tmp1 <= 6)
							{
								if (tmp1 == 0)
								{
									//UPGRADE_TODO: (1067) Member ApplyUnParkMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
									HC.ApplyUnParkMode();
								}
								else
								{
									object tempAuxVar2 = ApplyUnParkMode2[tmp1 - 1];
								}
							}
							if (EQMath.gEQparkstatus == 0)
							{
								result = "1#";
							}
							else
							{
								result = "0#";
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":RA_ENC" : 
						if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() != 1)
						{
							result = Strings.FormatNumber(-1, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						}
						else
						{
							pos = SyncRAMotor;
							result = Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						} 
						 
						break;
					case ":DEC_ENC" : 
						if (UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMountStatus() != 1)
						{
							result = Strings.FormatNumber(-1, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						}
						else
						{
							pos = SyncDECMotor;
							result = Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#";
						} 
						 
						break;
					case ":RA_AUX" : 
						pos = Common.EQGetMotorValues(3); 
						result = Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#"; 
						 
						break;
					case ":DEC_AUX" : 
						pos = Common.EQGetMotorValues(4); 
						result = Strings.FormatNumber(pos, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#"; 
						 
						break;
					case ":ST4_RARATE" : 
						switch(comStr.GetUpperBound(0))
						{
							case 0 : 
								// read current RA_Rate 
								//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								result = Convert.ToString(HC.RAGuideRateList.Text).Substring(Math.Max(Convert.ToString(HC.RAGuideRateList.Text).Length - 4, 0)); 
								break;
							case 1 : 
								// write new RA rate 
								switch(comStr[1])
								{
									case "1.00" : case "0.75" : case "0.50" : case "0.25" : 
										Common.writeportrateRa("x" + comStr[1]); 
										Common.readportrate(); 
										// success 
										result = "1#"; 
										break;
									default:
										// failure 
										result = "0#"; 
										break;
								} 
								break;
							default:
								ParseErr = true; 
								break;
						} 
						 
						break;
					case ":ST4_DECRATE" : 
						switch(comStr.GetUpperBound(0))
						{
							case 0 : 
								// read current DEC_Rate 
								//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								result = Convert.ToString(HC.DECGuideRateList.Text).Substring(Math.Max(Convert.ToString(HC.DECGuideRateList.Text).Length - 4, 0)); 
								break;
							case 1 : 
								// write new dec rate 
								switch(comStr[1])
								{
									case "1.00" : case "0.75" : case "0.50" : case "0.25" : 
										Common.writeportrateDec("x" + comStr[1]); 
										Common.readportrate(); 
										// success 
										result = "1#"; 
										break;
									default:
										// failure 
										result = "0#"; 
										break;
								} 
								break;
							default:
								ParseErr = true; 
								break;
						} 
						 
						break;
					case ":PG_RARATE" : 
						switch(comStr.GetUpperBound(0))
						{
							case 0 : 
								// read current RA_Rate 
								//UPGRADE_TODO: (1067) Member Label14 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								result = Convert.ToString(HC.Label14.Caption).Substring(Math.Max(Convert.ToString(HC.Label14.Caption).Length - 3, 0)); 
								break;
							case 1 : 
								// write new RA rate 
								// assume success 
								result = "1#"; 
								switch(comStr[1])
								{
									case "0.9" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 9; 
										break;
									case "0.8" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 8; 
										break;
									case "0.7" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 7; 
										break;
									case "0.6" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 6; 
										break;
									case "0.5" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 5; 
										break;
									case "0.4" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 4; 
										break;
									case "0.3" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 3; 
										break;
									case "0.2" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 2; 
										break;
									case "0.1" : 
										//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollRARate.Value = 1; 
										break;
									default:
										// failure 
										result = "0#"; 
										break;
								} 
								break;
							default:
								ParseErr = true; 
								break;
						} 
						 
						break;
					case ":PG_DECRATE" : 
						switch(comStr.GetUpperBound(0))
						{
							case 0 : 
								// read current DEC_Rate 
								//UPGRADE_TODO: (1067) Member Label15 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								result = Convert.ToString(HC.Label15.Caption).Substring(Math.Max(Convert.ToString(HC.Label15.Caption).Length - 3, 0)); 
								break;
							case 1 : 
								// write new RA rate 
								// assume success 
								result = "1#"; 
								switch(comStr[1])
								{
									case "0.9" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 9; 
										break;
									case "0.8" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 8; 
										break;
									case "0.7" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 7; 
										break;
									case "0.6" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 6; 
										break;
									case "0.5" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 5; 
										break;
									case "0.4" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 4; 
										break;
									case "0.3" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 3; 
										break;
									case "0.2" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 2; 
										break;
									case "0.1" : 
										//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
										HC.HScrollDecRate.Value = 1; 
										break;
									default:
										// failure 
										result = "0#"; 
										break;
								} 
								break;
							default:
								ParseErr = true; 
								break;
						} 
						 
						break;
					case ":FORM_RESET" : 
						Common.ResetFormPosition(); 
						 
						break;
					case ":PGENA_RA" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							switch(comStr[1])
							{
								case "1" : 
									//enable RA Pulseguideing 
									//UPGRADE_TODO: (1067) Member rapulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.rapulse_enchk.Value = 1; 
									break;
								case "0" : 
									//disable RA Pulseguideing 
									//UPGRADE_TODO: (1067) Member rapulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.rapulse_enchk.Value = 0; 
									break;
								default:
									ParseErr = true; 
									break;
							}
						}
						else
						{
							//UPGRADE_TODO: (1067) Member rapulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							if (Convert.ToDouble(HC.rapulse_enchk.Value) == 0)
							{
								result = "0#";
							}
							else
							{
								result = "1#";
							}
						} 
						 
						break;
					case ":PGENA_DEC" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							switch(comStr[1])
							{
								case "1" : 
									//enable DEC Pulseguideing 
									//UPGRADE_TODO: (1067) Member decpulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.decpulse_enchk.Value = 1; 
									result = "1#"; 
									break;
								case "0" : 
									//disable DEC Pulseguideing 
									//UPGRADE_TODO: (1067) Member decpulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.decpulse_enchk.Value = 0; 
									result = "1#"; 
									break;
								default:
									ParseErr = true; 
									break;
							}
						}
						else
						{
							//UPGRADE_TODO: (1067) Member decpulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							if (Convert.ToDouble(HC.decpulse_enchk.Value) == 0)
							{
								result = "0#";
							}
							else
							{
								result = "1#";
							}
						} 


						 
						break;
					case ":DRIFTCOMP" : 
						switch(comStr.GetUpperBound(0))
						{
							case 1 : 
								Common.gDriftComp = Convert.ToInt32(Conversion.Val(comStr[1])); 
								//UPGRADE_TODO: (1067) Member DriftScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
								HC.DriftScroll.Value = Common.gDriftComp; 
								result = "1#"; 
								break;
							case 0 : 
								result = Strings.FormatNumber(Common.gDriftComp, 0, TriState.UseDefault, TriState.UseDefault, TriState.False) + "#"; 
								break;
							default:
								ParseErr = true; 
								break;
						} 
						 
						break;
					case ":DLLVER" : 
						pos = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_DriverVersion(); 
						tmpstr = (Convert.ToInt32((pos & 0xF00000) / 1048576d) & 0xF).ToString("X") + (Convert.ToInt32((pos & 0xF0000) / 65536d) & 0xF).ToString("X"); 
						tmpstr = tmpstr + "." + (Convert.ToInt32((pos & 0xF000) / 4096d) & 0xF).ToString("X") + (Convert.ToInt32((pos & 0xF00) / 256d) & 0xF).ToString("X"); 
						tmpstr = tmpstr + "." + (Convert.ToInt32((pos & 0xF0) / 16d) & 0xF).ToString("X") + (pos & 0xF).ToString("X"); 
						result = tmpstr; 
						 
						break;
					case ":MOUNTVER" : 
						tmpstr = (Convert.ToInt32((Convert.ToInt32(EQMath.gMount_Ver) & 0xF0) / 16d) & 0xF).ToString("X") + (Convert.ToInt32(EQMath.gMount_Ver) & 0xF).ToString("X"); 
						tmpstr = tmpstr + "." + (Convert.ToInt32((Convert.ToInt32(EQMath.gMount_Ver) & 0xF000) / 4096d) & 0xF).ToString("X") + (Convert.ToInt32((Convert.ToInt32(EQMath.gMount_Ver) & 0xF00) / 256d) & 0xF).ToString("X"); 
						tmpstr = tmpstr + "." + (Convert.ToInt32((Convert.ToInt32(EQMath.gMount_Ver) & 0xF00000) / 1048576d) & 0xF).ToString("X") + (Convert.ToInt32((Convert.ToInt32(EQMath.gMount_Ver) & 0xF0000) / 65536d) & 0xF).ToString("X"); 
						result = tmpstr; 
						 
						break;
					case ":DRIVERVER" : 
						result = Common.gVersion; 
						 
						break;
					case ":ALIGN_MODE" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							switch(comStr[1])
							{
								case "0" : 
									//Set dialog mode 
									//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.ListSyncMode.ListIndex = 0; 
									result = "1#"; 
									break;
								case "1" : 
									//Set append mode 
									//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.ListSyncMode.ListIndex = 1; 
									result = "1#"; 
									break;
								default:
									ParseErr = true; 
									break;
							}
						}
						else
						{
							//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							if (Convert.ToDouble(HC.ListSyncMode.ListIndex) == 0)
							{
								result = "0#";
							}
							else
							{
								result = "1#";
							}
						} 
						 
						break;
					case ":ALIGN_CLEAR_SYNC" : 
						Common.resetsync(); 
						result = "1#"; 
						 
						break;
					case ":ALIGN_CLEAR_POINTS" : 
						//UPGRADE_TODO: (1067) Member ResetAlign is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
						HC.ResetAlign(); 
						result = "1#"; 
						 
						break;
					case ":ALIGN_SYNC_LIMIT" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							switch(comStr[1])
							{
								case "0" : 
									Common.gDisableSyncLimit = true; 
									result = "1#"; 
									break;
								case "1" : 
									Common.gDisableSyncLimit = false; 
									result = "1#"; 
									break;
								default:
									ParseErr = true; 
									break;
							}
						}
						else
						{
							if (Common.gDisableSyncLimit)
							{
								result = "0#";
							}
							else
							{
								result = "1#";
							}
						} 

						 
						break;
					case ":FLIP_GOTO" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							switch(comStr[1])
							{
								case "0" : 
									//UPGRADE_TODO: (1067) Member ChkForceFlip is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.ChkForceFlip.Value = 0; 
									result = "1#"; 
									break;
								case "1" : 
									//UPGRADE_TODO: (1067) Member ChkForceFlip is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
									HC.ChkForceFlip.Value = 1; 
									result = "1#"; 
									break;
								default:
									ParseErr = true; 
									break;
							}
						}
						else
						{
							//UPGRADE_TODO: (1067) Member ChkForceFlip is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							if (Convert.ToDouble(HC.ChkForceFlip.Value) == 1)
							{
								result = "1#";
							}
							else
							{
								result = "0#";
							}
						} 
						 
						break;
					case ":SNAP1" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							if (comStr[1] == "1")
							{
								result = "1#";
								//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Check1(2).Value = 1;
							}
							else
							{
								result = "0#";
								//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Check1(2).Value = 0;
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					case ":SNAP2" : 
						if (comStr.GetUpperBound(0) == 1)
						{
							if (comStr[1] == "1")
							{
								result = "1#";
								//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Check1(3).Value = 1;
							}
							else
							{
								result = "0#";
								//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
								HC.Check1(3).Value = 0;
							}
						}
						else
						{
							ParseErr = true;
						} 
						 
						break;
					default:
						// unknown command! 
						ParseErr = true; 
						 
						break;
				}
			}

			if (ParseErr)
			{
				HandleError:
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method CommandString():" + command + " " + ErrorConstants.MSG_NOT_IMPLEMENTED);
			}

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(0, "COMMAND CommandString()" + command + " :" + result);
			}

			return result;
		}

		public void FindHome()
		{
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(7, "COMMAND FindHome :NOT_SUPPORTED");
			}
			RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "Method FindHome()" + ErrorConstants.MSG_NOT_IMPLEMENTED);
		}

		public void Park()
		{
			object AscomTrace = null;
			object[] ParktoUserDefine = null;
			double endtime = 0;
			double nowtime = 0;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(7, "COMMAND Park");
			}
			if (EQMath.gEQparkstatus == 0)
			{
				// initiate park
				object[] tempAuxVar = ParktoUserDefine;

				// ASCOM, in their wisdom (or lack of it), require that park blocks the client until completion.
				// This is rather poor and we have chosen to ignor that part of the spec believing that
				// non blocking asynchronous methods are a much better solution. However some clients may
				// require a blocking function so we've provided an option to allow this.
				if (Common.gAscomCompatibility.BlockPark)
				{

					//----------------------------
					// Blocking
					//----------------------------
					// 3 minute timeout should be more than long enough to park from any position
					nowtime = EQMath.EQnow_lst_norange();
					endtime = nowtime + 180;
					while (EQMath.gEQparkstatus != 1)
					{
						Application.DoEvents();
						nowtime = EQMath.EQnow_lst_norange();
						if (nowtime >= endtime)
						{
							// timeout - lets ingnore the crazy ASCOM syncronos park and do it right i.e. asynchronously
							return;
						}
					}

				}
			}
		}

		//UPGRADE_ISSUE: (2068) GuideDirections object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public void PulseGuide(UpgradeStubs.GuideDirections direction, int Duration)
		{
			object AscomTrace = null;
			object guideEast = null;
			object guideNorth = null;
			object[, , ] Plot_PG = null;
			object guideSouth = null;
			object guideWest = null;
			object HC = null;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_ISSUE: (2068) GuideDirections object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				//UPGRADE_WARNING: (1068) direction of type GuideDirections is being forced to string. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(4, "COMMAND PulseGuide(" + direction + "," + Duration.ToString() + ")");
			}

			if (!Common.gAscomCompatibility.AllowPulseGuide)
			{
				RaiseError(ErrorConstants.SCODE_NOT_IMPLEMENTED, ErrorConstants.ERR_SOURCE, "PulseGuide() :NOT SUPPORTED");
				return;
			}

			if (EQMath.gSlewStatus)
			{
				// no guiding whilst slewing - makes no sense and means the slew will terminate!
				return;
			}

			switch(EQMath.gEQparkstatus)
			{
				case 0 : 
					// unparked 
					break;
				case 1 : 
					// parked 
					// no guidng if parked 
					if (Common.gAscomCompatibility.AllowPulseGuideExceptions)
					{
						RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "Method PulseGuide() " + ErrorConstants.MSG_SCOPE_PARKED);
					} 
					return;
				case 2 : 
					// parking 
					// no guidng if parking 
					return;
			}

			//Pulse guide implemtation for EQMOD
			//This uses the duration parameter and an asynchronous timer
			//that will decrement the duration count for every time tick
			//It then disables the guiderate upon expiration of the counter

			//UPGRADE_TODO: (1067) Member Pulseguide_Timer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Pulseguide_Timer.Enabled = false;

			if ((direction) == guideNorth || (direction) == guideSouth)
			{ //'DEC+,DEC-
				//UPGRADE_TODO: (1067) Member decpulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToDouble(HC.decpulse_enchk.Value) == 1)
				{
					// apply gain to duration
					//UPGRADE_TODO: (1067) Member decfixed_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToDouble(HC.decfixed_enchk.Value) == 0)
					{
						// apply gain
						//UPGRADE_TODO: (1067) Member HScrollDECWidth is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						EQMath.gEQDECPulseDuration = Convert.ToInt32(Duration * (Convert.ToDouble(HC.HScrollDECWidth.Value) / 100d));
					}
					else
					{
						// use fixed period
						//UPGRADE_TODO: (1067) Member HScrollDecOride is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						EQMath.gEQDECPulseDuration = Convert.ToInt32(Convert.ToDouble(HC.HScrollDecOride.Value) * 100);
					}

					if (direction != LastGuideNS)
					{
						EQMath.gEQDECPulseDuration += Common.gBacklashDec;
					}

					if (EQMath.gEQDECPulseDuration < (EQMath.gpl_interval / 2d))
					{
						// pulse duration is too small to action - better to ignor it than over correct
						Duration = 0;
						EQMath.gEQDECPulseDuration = 0;
					}
				}
				else
				{
					EQMath.gEQDECPulseDuration = 0;
					Duration = 0;
					// nothing to do!
					goto endsub;
				}

			}
			else if ((direction) == guideEast || (direction) == guideWest)
			{  //'RA+,RA-
				//UPGRADE_TODO: (1067) Member rapulse_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (Convert.ToDouble(HC.rapulse_enchk.Value) == 1)
				{
					//UPGRADE_TODO: (1067) Member rafixed_enchk is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToDouble(HC.rafixed_enchk.Value) == 0)
					{
						// apply gain to duration
						//UPGRADE_TODO: (1067) Member HScrollRAWidth is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						EQMath.gEQRAPulseDuration = Convert.ToInt32(Duration * (Convert.ToDouble(HC.HScrollRAWidth.Value) / 100d));
					}
					else
					{
						//use fixed period
						//UPGRADE_TODO: (1067) Member HScrollRAOride is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						EQMath.gEQRAPulseDuration = Convert.ToInt32(Convert.ToDouble(HC.HScrollRAOride.Value) * 100);
					}

					if (EQMath.gEQRAPulseDuration < (EQMath.gpl_interval / 2d))
					{
						// pulse duration is too small - better to ignor it than over correct
						Duration = 0;
						EQMath.gEQRAPulseDuration = 0;
					}
				}
				else
				{
					EQMath.gEQRAPulseDuration = 0;
					Duration = 0;
					goto endsub;
				}

			}
			else
			{
				// invalid direction
				if (Common.gAscomCompatibility.AllowPulseGuideExceptions)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "Pulse Guide: GuideDirections" + ErrorConstants.MSG_VAL_OUTOFRANGE);
				}
			}

			if (EQMath.gTrackingStatus == 4)
			{
				if (Duration == 0)
				{
					if ((direction) == guideNorth)
					{ //DEC+
						EQMath.gEQDECPulseDuration = 0;
						Tracking.ChangeDEC_by_Rate(EQMath.gDeclinationRate);
						object tempAuxVar = Plot_PG[1, 0, 0];

					}
					else if ((direction) == guideSouth)
					{  //DEC-
						EQMath.gEQDECPulseDuration = 0;
						Tracking.ChangeDEC_by_Rate(EQMath.gDeclinationRate);
						object tempAuxVar2 = Plot_PG[1, 1, 0];

					}
					else if ((direction) == guideEast)
					{  //RA+
						if (EQMath.gRA_LastRate == 0)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, 0, 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							Tracking.ChangeRA_by_Rate(EQMath.gRightAscensionRate);
						}
						EQMath.gEQRAPulseDuration = 0;
						object tempAuxVar3 = Plot_PG[0, 0, 0];

					}
					else if ((direction) == guideWest)
					{  //RA-
						if (EQMath.gRA_LastRate == 0)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, 0, 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							Tracking.ChangeRA_by_Rate(EQMath.gRightAscensionRate);
						}
						object tempAuxVar4 = Plot_PG[0, 1, 0];
						EQMath.gEQRAPulseDuration = 0;
					}
				}
				else
				{
					if ((direction) == guideNorth)
					{ //DEC+
						//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Tracking.ChangeDEC_by_Rate(EQMath.gDeclinationRate + (EQMath.gDeclinationRate * Convert.ToDouble(HC.HScrollDecRate.Value) * 0.1d));
						object tempAuxVar5 = Plot_PG[1, 0, EQMath.gEQDECPulseDuration];

					}
					else if ((direction) == guideSouth)
					{  //DEC-
						//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Tracking.ChangeDEC_by_Rate(EQMath.gDeclinationRate - (EQMath.gDeclinationRate * Convert.ToDouble(HC.HScrollDecRate.Value) * 0.1d));
						//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(1, EQMath.gTrackingStatus - 1, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollDecRate.Value))), 0, 0, 0);
						object tempAuxVar6 = Plot_PG[1, 1, EQMath.gEQDECPulseDuration];

					}
					else if ((direction) == guideEast)
					{  //RA+
						if (EQMath.gRA_LastRate == 0)
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollRARate.Value))), 1, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							Tracking.ChangeRA_by_Rate(EQMath.gRightAscensionRate - (EQMath.gRightAscensionRate * Convert.ToDouble(HC.HScrollRARate.Value) * 0.1d));
						}
						object tempAuxVar7 = Plot_PG[0, 0, EQMath.gEQRAPulseDuration];

					}
					else if ((direction) == guideWest)
					{  //RA-
						if (EQMath.gRA_LastRate == 0)
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollRARate.Value))), 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							Tracking.ChangeRA_by_Rate(EQMath.gRightAscensionRate + (EQMath.gRightAscensionRate * Convert.ToDouble(HC.HScrollRARate.Value) * 0.1d));
						}
						object tempAuxVar8 = Plot_PG[0, 1, EQMath.gEQRAPulseDuration];
					}
				}
			}
			else
			{
				// Process if Equatorial Tracking
				if (Duration == 0)
				{

					if ((direction) == guideNorth)
					{ //DEC+
						EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
						if (EQMath.eqres == 1)
						{
							return;
						}
						//                    Do
						//                      eqres = EQ_GetMotorStatus(1)
						//                      If eqres = 1 Then GoTo PError
						//                    Loop While (eqres And &H10) <> 0
						EQMath.gEQDECPulseDuration = 0;
						Common.gPulseguideRateDec = 0;
						object tempAuxVar9 = Plot_PG[1, 0, 0];

					}
					else if ((direction) == guideSouth)
					{  //DEC-
						EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_MotorStop(1);
						if (EQMath.eqres == 1)
						{
							return;
						}
						//                   Do
						//                     eqres = EQ_GetMotorStatus(1)
						//                     If eqres = 1 Then GoTo PError
						//                   Loop While (eqres And &H10) <> 0
						EQMath.gEQDECPulseDuration = 0;
						Common.gPulseguideRateDec = 0;
						object tempAuxVar10 = Plot_PG[1, 1, 0];

					}
					else if ((direction) == guideEast)
					{  //RA+
						if (EQMath.gTrackingStatus == 0)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, 0, 0, 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, 0, 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						EQMath.gEQRAPulseDuration = 0;
						Common.gPulseguideRateRa = 0;
						object tempAuxVar11 = Plot_PG[0, 0, 0];

					}
					else if ((direction) == guideWest)
					{  //RA-
						if (EQMath.gTrackingStatus == 0)
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, 0, 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, 0, 0, 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						EQMath.gEQRAPulseDuration = 0;
						Common.gPulseguideRateRa = 0;
						object tempAuxVar12 = Plot_PG[0, 1, 0];

					}

				}
				else
				{

					if ((direction) == guideNorth)
					{ //DEC+
						//                    eqres = EQ_MotorStop(1)
						//                    If eqres = 1 Then GoTo PError
						//                    Do
						//                      eqres = EQ_GetMotorStatus(1)
						//                      If eqres = 1 Then GoTo PError
						//                    Loop While (eqres And &H10) <> 0
						if (EQMath.gTrackingStatus == 0)
						{
							//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(1, 0, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollDecRate.Value))), 1, 0, 0);
						}
						else
						{
							//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(1, EQMath.gTrackingStatus - 1, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollDecRate.Value))), 1, 0, 0);
						}

						//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Common.gPulseguideRateDec = Convert.ToDouble(HC.HScrollDecRate.Value) * 0.1d;

						object tempAuxVar13 = Plot_PG[1, 0, EQMath.gEQDECPulseDuration];
						//UPGRADE_ISSUE: (2068) GuideDirections object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
						//UPGRADE_WARNING: (1068) guideNorth of type Variant is being forced to GuideDirections. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
						//UPGRADE_WARNING: (1068) LastGuideNS of type GuideDirections is being forced to Scalar. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
						LastGuideNS = (UpgradeStubs.GuideDirections) guideNorth;

					}
					else if ((direction) == guideSouth)
					{  //DEC-
						//                    eqres = EQ_MotorStop(1)
						//                    If eqres = 1 Then GoTo PError
						//                    Do
						//                      eqres = EQ_GetMotorStatus(1)
						//                      If eqres = 1 Then GoTo PError
						//                    Loop While (eqres And &H10) <> 0
						if (EQMath.gTrackingStatus == 0)
						{
							//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(1, 0, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollDecRate.Value))), 0, 0, 0);
						}
						else
						{
							//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(1, EQMath.gTrackingStatus - 1, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollDecRate.Value))), 0, 0, 0);
						}

						//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Common.gPulseguideRateDec = Convert.ToDouble(HC.HScrollDecRate.Value) * (-0.1d);

						object tempAuxVar14 = Plot_PG[1, 1, EQMath.gEQDECPulseDuration];
						//UPGRADE_ISSUE: (2068) GuideDirections object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
						//UPGRADE_WARNING: (1068) guideSouth of type Variant is being forced to GuideDirections. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
						//UPGRADE_WARNING: (1068) LastGuideNS of type GuideDirections is being forced to Scalar. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1068
						LastGuideNS = (UpgradeStubs.GuideDirections) guideSouth;

					}
					else if ((direction) == guideEast)
					{  //RA+
						if (EQMath.gTrackingStatus == 0)
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, 0, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollRARate.Value))), 1, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollRARate.Value))), 1, EQMath.gHemisphere, EQMath.gHemisphere);
						}

						//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Common.gPulseguideRateRa = Convert.ToDouble(HC.HScrollRARate.Value) * (-0.1d);

						object tempAuxVar15 = Plot_PG[0, 0, EQMath.gEQRAPulseDuration];

					}
					else if ((direction) == guideWest)
					{  //RA-
						if (EQMath.gTrackingStatus == 0)
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, 0, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollRARate.Value))), 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						else
						{
							//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SendGuideRate(0, EQMath.gTrackingStatus - 1, Convert.ToInt32(Conversion.Val(Convert.ToString(HC.HScrollRARate.Value))), 0, EQMath.gHemisphere, EQMath.gHemisphere);
						}
						//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Common.gPulseguideRateRa = Convert.ToDouble(HC.HScrollRARate.Value) * 0.1d;
						object tempAuxVar16 = Plot_PG[0, 1, EQMath.gEQRAPulseDuration];
					}
				}
			}

			endsub:
			if (EQMath.gEQDECPulseDuration > 0)
			{
				EQMath.gEQDECPulseStart = Environment.TickCount;
				EQMath.gEQDECPulseEnd = Convert.ToInt32(EQMath.gEQDECPulseDuration + EQMath.gEQDECPulseStart - (EQMath.gpl_interval / 2d));
				if (EQMath.gEQDECPulseEnd < EQMath.gEQDECPulseStart)
				{
					EQMath.gEQDECPulseEnd = EQMath.gEQDECPulseStart;
				}
			}
			if (EQMath.gEQRAPulseDuration > 0)
			{
				EQMath.gEQRAPulseStart = Environment.TickCount;
				EQMath.gEQRAPulseEnd = Convert.ToInt32(EQMath.gEQRAPulseDuration + EQMath.gEQRAPulseStart - (EQMath.gpl_interval / 2d));
				if (EQMath.gEQRAPulseEnd < EQMath.gEQRAPulseStart)
				{
					EQMath.gEQRAPulseEnd = EQMath.gEQRAPulseStart;
				}
			}
			//UPGRADE_TODO: (1067) Member Pulseguide_Timer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Pulseguide_Timer.Enabled = true;



		}

		public void SetPark()
		{
			object oLangDll = null;
			object HC = null;
			object[] DefinePark = null;
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(7, "COMMAND SetPark");
			}
			//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Add_Message(oLangDll.GetLangString(5004));
			object tempAuxVar = DefinePark[-1];
		}

		public void SetupDialog()
		{
			object Setupfrm = null;
			//UPGRADE_ISSUE: (2064) VB method VB.Global was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
			//UPGRADE_ISSUE: (1040) Unload function is not supported. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1040
			UpgradeStubs.VB.getGlobal().Unload(Setupfrm);
			//UPGRADE_TODO: (1067) Member Show is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			Setupfrm.Show(1);
		}

		public void SlewToCoordinates(double RightAscension, double Declination)
		{
			object oLangDll = null;
			object AscomTrace = null;
			object HC = null;
			object Align = null;
			object[] EQ_Beep = null;
			double nowtime = 0;
			double endtime = 0;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "COMMAND SlewToCoordinates(" + RightAscension.ToString() + "," + Declination.ToString() + ")");
			}
			if (EQMath.gEQparkstatus == 0)
			{
				if (!Common.gAscomCompatibility.SlewWithTrackingOff && EQMath.gTrackingStatus == 0)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_OPERATION_EXCEPTION, ErrorConstants.ERR_SOURCE, "SlewToCoordinates() " + ErrorConstants.MSG_RADEC_SLEW_ERROR);
				}
				else
				{
					if (ValidateRADEC(RightAscension, Declination))
					{
						//UPGRADE_TODO: (1067) Member Visible is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (Convert.ToBoolean(Align.Visible))
						{
							//UPGRADE_TODO: (1067) Member FillAlignmentStar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							Align.FillAlignmentStar(RightAscension, Declination);
						}
						Goto.gTargetRA = RightAscension;
						Goto.gTargetDec = Declination;
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("SyncCSlew: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + " ] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]");
						Goto.gSlewCount = Goto.gMaxSlewCount; //NUM_SLEW_RETRIES               'Set initial iterative slew count
						Goto.radecAsyncSlew(Goto.gGotoRate);
						object tempAuxVar = EQ_Beep[20];

						//----------------------------
						// Blocking
						//----------------------------
						// 3 minute timeout should be more than long enough to slew to any position
						nowtime = EQMath.EQnow_lst_norange();
						endtime = nowtime + 180;
						while (EQMath.gSlewStatus)
						{
							Application.DoEvents();
							nowtime = EQMath.EQnow_lst_norange();
							if (nowtime >= endtime)
							{
								return;
							}
						}
					}
					else
					{
						RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "SlewToCoordinates() " + ErrorConstants.MSG_VAL_OUTOFRANGE);
					}

				}
			}
			else
			{
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(5000));
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "SlewToCoordinates() " + ErrorConstants.MSG_SCOPE_PARKED);
			}
		}

		public void SlewToCoordinatesAsync(double RightAscension, double Declination)
		{
			object oLangDll = null;
			object AscomTrace = null;
			object HC = null;
			object Align = null;
			object[] EQ_Beep = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "COMMAND SlewToCoordinatesAsync(" + RightAscension.ToString() + "," + Declination.ToString() + ")");
			}
			if (EQMath.gEQparkstatus == 0)
			{

				if (!Common.gAscomCompatibility.SlewWithTrackingOff && EQMath.gTrackingStatus == 0)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_OPERATION_EXCEPTION, ErrorConstants.ERR_SOURCE, "SlewToCoordinatesAsync() " + ErrorConstants.MSG_RADEC_SLEW_ERROR);
				}
				else
				{
					if (ValidateRADEC(RightAscension, Declination))
					{
						//UPGRADE_TODO: (1067) Member Visible is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						if (Convert.ToBoolean(Align.Visible))
						{
							//UPGRADE_TODO: (1067) Member FillAlignmentStar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
							Align.FillAlignmentStar(RightAscension, Declination);
						}
						Goto.gTargetRA = RightAscension;
						Goto.gTargetDec = Declination;
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("CoordSlew: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + " ] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]");
						Goto.gSlewCount = Goto.gMaxSlewCount; //NUM_SLEW_RETRIES               'Set initial iterative slew count
						Goto.radecAsyncSlew(Goto.gGotoRate);
						object tempAuxVar = EQ_Beep[20];
					}
					else
					{
						RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "SlewToCoordinates() " + ErrorConstants.MSG_VAL_OUTOFRANGE);
					}
				}
			}
			else
			{
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(5000));
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "SlewToCoordinatesAsync() " + ErrorConstants.MSG_SCOPE_PARKED);
			}
		}

		public void SlewToTarget()
		{
			object oLangDll = null;
			object AscomTrace = null;
			object HC = null;
			object Align = null;
			object[] EQ_Beep = null;
			double nowtime = 0;
			double endtime = 0;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "COMMAND SlewToTarget");
			}
			if (EQMath.gEQparkstatus == 0)
			{
				if (!Common.gAscomCompatibility.SlewWithTrackingOff && EQMath.gTrackingStatus == 0)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_OPERATION_EXCEPTION, ErrorConstants.ERR_SOURCE, "SlewToTarget() " + ErrorConstants.MSG_RADEC_SLEW_ERROR);
				}
				else
				{
					//UPGRADE_TODO: (1067) Member Visible is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(Align.Visible))
					{
						//UPGRADE_TODO: (1067) Member FillAlignmentStar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Align.FillAlignmentStar(Goto.gTargetRA, Goto.gTargetDec);
					}
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message("SyncSlew: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + "] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]");
					Goto.gSlewCount = Goto.gMaxSlewCount; //NUM_SLEW_RETRIES               'Set initial iterative slew count
					Goto.radecAsyncSlew(Goto.gGotoRate);
					object tempAuxVar = EQ_Beep[20];

					//----------------------------
					// Blocking
					//----------------------------
					// 3 minute timeout should be more than long enough to slew to any position
					nowtime = EQMath.EQnow_lst_norange();
					endtime = nowtime + 180;
					while (EQMath.gSlewStatus)
					{
						Application.DoEvents();
						nowtime = EQMath.EQnow_lst_norange();
						if (nowtime >= endtime)
						{
							return;
						}
					}

				}
			}
			else
			{
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(5000));
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "SlewToTarget() " + ErrorConstants.MSG_SCOPE_PARKED);
			}

		}

		public void SlewToTargetAsync()
		{
			object oLangDll = null;
			object AscomTrace = null;
			object HC = null;
			object Align = null;
			object[] EQ_Beep = null;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(6, "COMMAND SlewToTargetAsync");
			}
			if (EQMath.gEQparkstatus == 0)
			{
				if (!Common.gAscomCompatibility.SlewWithTrackingOff && EQMath.gTrackingStatus == 0)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_OPERATION_EXCEPTION, ErrorConstants.ERR_SOURCE, "SlewToTargetAsync() " + ErrorConstants.MSG_RADEC_SLEW_ERROR);
				}
				else
				{
					//UPGRADE_TODO: (1067) Member Visible is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (Convert.ToBoolean(Align.Visible))
					{
						//UPGRADE_TODO: (1067) Member FillAlignmentStar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						Align.FillAlignmentStar(Goto.gTargetRA, Goto.gTargetDec);
					}
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message("AsyncSlew: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + "] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]");
					Goto.gSlewCount = Goto.gMaxSlewCount; //NUM_SLEW_RETRIES               'Set initial iterative slew count
					Goto.radecAsyncSlew(Goto.gGotoRate);
					object tempAuxVar = EQ_Beep[20];
				}
			}
			else
			{
				//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Add_Message(oLangDll.GetLangString(5000));
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "SlewToTargetAsync() " + ErrorConstants.MSG_SCOPE_PARKED);
			}

		}

		public void SyncToCoordinates(double RightAscension, double Declination)
		{
			object oLangDll = null;
			object HC = null;
			object[] EQ_Beep = null;
			object AscomTrace = null;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(7, "COMMAND SyncToCoordinates(" + RightAscension.ToString() + "," + Declination.ToString() + ")");
			}
			if (EQMath.gEQparkstatus == 0)
			{
				if (EQMath.gTrackingStatus == 0)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_OPERATION_EXCEPTION, ErrorConstants.ERR_SOURCE, "SyncToCoordinates() " + ErrorConstants.MSG_RADEC_SYNC_ERROR);
				}
				else
				{
					if (ValidateRADEC(RightAscension, Declination))
					{
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("SynCoor: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(RightAscension, false) + "] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Declination, true) + " ]");
						if (Common.SyncToRADEC(RightAscension, Declination, EQMath.gLongitude, EQMath.gHemisphere))
						{
							object tempAuxVar = EQ_Beep[4];
						}
						else
						{
							// commented out on advice from Chris Rowland
							// RaiseError SCODE_INVALID_OPERATION_EXCEPTION, ERR_SOURCE, "SyncToTarget() " & MSG_RADEC_SYNC_REJECT
						}
					}
					else
					{
						RaiseError(ErrorConstants.SCODE_INVALID_VALUE, ErrorConstants.ERR_SOURCE, "SlewToCoordinates() " + ErrorConstants.MSG_VAL_OUTOFRANGE);
					}
				}
			}
			else
			{
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "SyncToCoordinates() " + ErrorConstants.MSG_SCOPE_PARKED);
			}

		}

		public void SyncToTarget()
		{
			object oLangDll = null;
			object HC = null;
			object[] EQ_Beep = null;
			object AscomTrace = null;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(7, "COMMAND SyncToTarget");
			}
			if (EQMath.gEQparkstatus == 0)
			{
				if (EQMath.gTrackingStatus == 0)
				{
					RaiseError(ErrorConstants.SCODE_INVALID_OPERATION_EXCEPTION, ErrorConstants.ERR_SOURCE, "SyncToTarget() " + ErrorConstants.MSG_RADEC_SYNC_ERROR);
				}
				else
				{
					//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Add_Message("SyncTaget: " + Convert.ToString(oLangDll.GetLangString(105)) + "[ " + EQMath.FmtSexa(Goto.gTargetRA, false) + "] " + Convert.ToString(oLangDll.GetLangString(106)) + "[ " + EQMath.FmtSexa(Goto.gTargetDec, true) + " ]");
					if (Common.SyncToRADEC(Goto.gTargetRA, Goto.gTargetDec, EQMath.gLongitude, EQMath.gHemisphere))
					{
						object tempAuxVar = EQ_Beep[4];
					}
					else
					{
						// commented out on advice from Chris Rowland
						// RaiseError SCODE_INVALID_OPERATION_EXCEPTION, ERR_SOURCE, "SyncToTarget() " & MSG_RADEC_SYNC_REJECT
					}
				}
			}
			else
			{
				RaiseError(ErrorConstants.SCODE_INVALID_WHILST_PARKED, ErrorConstants.ERR_SOURCE, "SyncToTarget() " + ErrorConstants.MSG_SCOPE_PARKED);
			}


		}

		public void UnPark()
		{
			object AscomTrace = null;
			object[] Unparkscope = null;
			double nowtime = 0;
			double endtime = 0;

			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(7, "COMMAND Unpark");
			}
			object[] tempAuxVar = Unparkscope;

			if (Common.gAscomCompatibility.BlockPark)
			{
				//----------------------------
				// Blocking
				//----------------------------
				// 3 minute timeout should be more than long enough to park from any position
				nowtime = EQMath.EQnow_lst_norange();
				endtime = nowtime + 180;
				while (EQMath.gEQparkstatus != 0)
				{
					Application.DoEvents();
					nowtime = EQMath.EQnow_lst_norange();
					if (nowtime >= endtime)
					{
						// timeout - lets ingnore the crazy ASCOM syncronos park and do it right i.e. asynchronously
						return;
					}
				}
			}

		}

		public void StopClientCount()
		{
			// A client can use this to ensure when it closes EQASCOM will close too irrespective of other attached clients.
			Common.ClientCount = 2;
		}

		public void SetClientCount(double Count)
		{
			Common.ClientCount = Convert.ToInt32(Count);
		}
		public void IncClientCount()
		{
			Common.ClientCount++;
		}

		public void ReadComPortSettings()
		{
			object HC = null;



			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("Port"));
			if (tmptxt != "")
			{
				EQMath.gPort = tmptxt;
			}
			else
			{
				EQMath.gPort = "COM1";
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("Baud"));
			if (tmptxt != "")
			{
				EQMath.gBaud = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				EQMath.gBaud = 9600;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("Timeout"));
			if (tmptxt != "")
			{
				EQMath.gTimeout = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				EQMath.gTimeout = 1000;
			}


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("Retry"));
			if (tmptxt != "")
			{
				EQMath.gRetry = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				EQMath.gRetry = 1;
			}

		}

		public void ReadSyncMap()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("RSYNC01"));
			if (tmptxt != "")
			{
				EQMath.gRASync01 = Conversion.Val(tmptxt);
			}
			else
			{
				EQMath.gRASync01 = 0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("DSYNC01"));
			if (tmptxt != "")
			{
				EQMath.gDECSync01 = Conversion.Val(tmptxt);
			}
			else
			{
				EQMath.gDECSync01 = 0;
			}

			//UPGRADE_TODO: (1067) Member DxSalbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.DxSalbl.Caption = StringsHelper.Format(Conversion.Str(EQMath.gRASync01), "000000000");
			//UPGRADE_TODO: (1067) Member DxSblbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.DxSblbl.Caption = StringsHelper.Format(Conversion.Str(EQMath.gDECSync01), "000000000");

		}
		public void ReadAlignMap()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("RALIGN01"));
			if (tmptxt != "")
			{
				EQMath.gRA1Star = Conversion.Val(tmptxt);
			}
			else
			{
				EQMath.gRA1Star = 0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("DALIGN01"));
			if (tmptxt != "")
			{
				EQMath.gDEC1Star = Conversion.Val(tmptxt);
			}
			else
			{
				EQMath.gDEC1Star = 0;
			}

		}
		private bool ValidateRADEC(double RA, double DEC)
		{
			bool result = false;
			if (RA >= 0 && RA <= 24)
			{
				if (DEC >= -90 && DEC <= 90)
				{
					result = true;
				}
			}
			return result;
		}

		private void RaiseError(int ErrNumber, string ErrSource, string ErrDescription)
		{
			object AscomTrace = null;
			//UPGRADE_TODO: (1067) Member AscomTraceEnabled is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToBoolean(AscomTrace.AscomTraceEnabled))
			{
				//UPGRADE_TODO: (1067) Member Add_log is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				AscomTrace.Add_log(0, "ERROR RAISED: errno=" + ErrNumber.ToString("X") + " " + ErrDescription);
			}
			if (Common.gAscomCompatibility.AllowExceptions)
			{
				throw new System.Exception(ErrNumber.ToString() + ", " + ErrSource + ", " + ErrDescription);
			}

		}


		// ============================
		// Implementation of ITelescope
		// ============================

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_AbortSlew) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_AbortSlew()
		//{
			//AbortSlew();
		//}

		//UPGRADE_ISSUE: (2068) IAxisRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_AxisRates) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.IAxisRates ITelescope_AxisRates(UpgradeStubs.TelescopeAxes axis)
		//{
			////
			// Note that this more or less "casts" our internal AxisRates
			// object's interface to AxisRates.
			////
			////UPGRADE_ISSUE: (2068) IAxisRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
			//return (UpgradeStubs.IAxisRates) AxisRates(axis);
		//}

		//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_CanMoveAxis) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool ITelescope_CanMoveAxis(UpgradeStubs.TelescopeAxes axis)
		//{
			//return CanMoveAxis(axis);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_CommandBlind) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_CommandBlind(string command, bool Raw = false)
		//{
			//    CommandBlind Command, Raw
			//CommandBlind(command);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_CommandBool) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private bool ITelescope_CommandBool(string command, bool Raw = false)
		//{
			//    ITelescope_CommandBool = CommandBool(Command, Raw)
			//return CommandBool(command);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_CommandString) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private string ITelescope_CommandString(string command, bool Raw = false)
		//{
			//return CommandString(command, Raw);
		//}

		//UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_DestinationSideOfPier) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.PierSide ITelescope_DestinationSideOfPier(double RightAscension, double Declination)
		//{
			//UpgradeStubs.PierSide result = null;
			//NotUpgradedHelper.NotifyNotUpgradedElement("The following assignment/return was commented because it has incompatible types");
			////UPGRADE_ISSUE: (2068) PierSide object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
			//result = (UpgradeStubs.PierSide) DestinationSideOfPier(RightAscension, Declination);
			//return result;
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_FindHome) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_FindHome()
		//{
			//FindHome();
		//}

		//UPGRADE_ISSUE: (2068) TelescopeAxes object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_MoveAxis) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_MoveAxis(UpgradeStubs.TelescopeAxes axis, double rate)
		//{
			//MoveAxis(axis, rate);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_Park) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_Park()
		//{
			//Park();
		//}

		//UPGRADE_ISSUE: (2068) GuideDirections object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_PulseGuide) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_PulseGuide(UpgradeStubs.GuideDirections direction, int Duration)
		//{
			//PulseGuide(direction, Duration);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SetPark) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SetPark()
		//{
			//SetPark();
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SetupDialog) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SetupDialog()
		//{
			//SetupDialog();
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SlewToAltAz) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SlewToAltAz(double Azimuth, double altitude)
		//{
			//SlewToAltAz(Azimuth, altitude);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SlewToAltAzAsync) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SlewToAltAzAsync(double Azimuth, double altitude)
		//{
			//SlewToAltAzAsync(Azimuth, altitude);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SlewToCoordinates) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SlewToCoordinates(double RightAscension, double Declination)
		//{
			//SlewToCoordinates(RightAscension, Declination);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SlewToCoordinatesAsync) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SlewToCoordinatesAsync(double RightAscension, double Declination)
		//{
			//SlewToCoordinatesAsync(RightAscension, Declination);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SlewToTarget) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SlewToTarget()
		//{
			//SlewToTarget();
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SlewToTargetAsync) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SlewToTargetAsync()
		//{
			//SlewToTargetAsync();
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SyncToAltAz) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SyncToAltAz(double Azimuth, double altitude)
		//{
			//SyncToAltAz(Azimuth, altitude);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SyncToCoordinates) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SyncToCoordinates(double RightAscension, double Declination)
		//{
			//SyncToCoordinates(RightAscension, Declination);
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_SyncToTarget) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_SyncToTarget()
		//{
			//SyncToTarget();
		//}

		//UPGRADE_NOTE: (7001) The following declaration (ITelescope_Unpark) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void ITelescope_Unpark()
		//{
			//UnPark();
		//}
	}
}