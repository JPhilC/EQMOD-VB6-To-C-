using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UpgradeHelpers.Helpers;

namespace Project1
{
	internal static class Common
	{



		//------------------- EQCONTRL.DLL Constants -----------------------
		public const double EQ_OK = 0x0;
		public const double EQ_COMNOTOPEN = 0x1;
		public const double EQ_COMTIMEOUT = 0x3;
		public const double EQ_MOTORBUSY = 0x10;
		public const double EQ_NOTINITIALIZED = 0xC8;
		public const double EQ_INVALIDCOORDINATE = 0x1000000;
		public const double EQ_INVALID = 0x3000000;

		// Protocol types
		public const int CURMOUNT = 0; //Detected Current Mount
		public const int EQMOUNT = 1; //EQG Protocol
		public const int NXMOUNT = 2; //NexStar Protocol
		public const int LXMOUNT = 3; //LX200 Protocol
		public const int TKMOUNT = 4; //Takahashi Protocol
		public const int HBXMOUNT = 5; //Meade HBX

		// coordinate types
		public const int CT_STEP = 0;
		public const int CT_RADEC = 1;
		public const int CT_AZALT = 2;
		//------------------------------------------------------------------

		//Virtual Desktop sizes
		const int SM_XVIRTUALSCREEN = 76; //Virtual Left
		const int SM_YVIRTUALSCREEN = 77; //Virtual Top
		const int SM_CXVIRTUALSCREEN = 78; //Virtual Width
		const int SM_CYVIRTUALSCREEN = 79; //Virtual Height
		const int SM_CMONITORS = 80; //Get number of monitors
		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int GetSystemMetrics(int nIndex);


		const int HWND_TOPMOST = -1;
		const int HWND_NOTTOPMOST = -2;
		const int SWP_NOMOVE = 0x2;
		const int SWP_NOSIZE = 0x1;

		[Serializable]
		public struct ASCOM_COMPLIANCE
		{
			public bool SlewWithTrackingOff;
			public bool AllowPulseGuide;
			public bool AllowExceptions;
			public bool AllowPulseGuideExceptions;
			public bool BlockPark;
			public bool AllowSiteWrites;
			public short Epoch;
			public short SideOfPier;
			public bool SwapPointingSideOfPier;
			public bool SwapPhysicalSideOfPier;
			public bool Strict;
		}


		public static ASCOM_COMPLIANCE gAscomCompatibility = new ASCOM_COMPLIANCE();
		//UPGRADE_ISSUE: (2068) DriverHelper.Profile object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public static DriverHelper.Profile oProfile = null;
		public const string oID = "EQMOD.Telescope";
		public static Telescope m_telescope = null;
		public static double[] gPresetSlewRates = new double[10];
		public static int[] gRateButtons = new int[4];
		public static int gPresetSlewRatesCount = 0;
		public static int gCurrentRatePreset = 0;
		public static double gPoleStarRa = 0;
		public static double gPoleStarDec = 0;
		public static double gPoleStarRaJ2000 = 0;
		public static double gPoleStarDecJ2000 = 0;
		public static double gPoleStarReticuleDec = 0;
		public static double gPolarReticuleEpoch = 0;
		public static double gPolHa = 0;
		public static string gVersion = "";
		public static int gShowPolarAlign = 0;
		public static int gAlignmentMode = 0;
		public static int gCoordType = 0;
		public static double gDllVer = 0;
		public static int g3PointAlgorithm = 0;
		public static int gAdvanced = 0;
		public static int gPointFilter = 0;
		public static int gBacklashDec = 0;
		public static int gDriftComp = 0;
		public static int gPoleStarIdx = 0;
		public static int gLstDisplayMode = 0;

		public static double gPulseguideRateRa = 0;
		public static double gPulseguideRateDec = 0;

		public static int gCommErrorStop = 0;

		public static int ClientCount = 0;
		public static int gInitResult = 0;

		public static bool gDisableSyncLimit = false;

		private const string oDESC = "EQMOD ASCOM Scope Driver";


		private const int SC_CLOSE = 0xF060;
		private const int MIIM_STATE = 0x1;
		private const int MIIM_ID = 0x2;
		private const int MFS_GRAYED = 0x3;
		private const int WM_NCACTIVATE = 0x86;




		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int GetSystemMenu(int hwnd, int bRevert);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		////UPGRADE_TODO: (1050) Structure MENUITEMINFO may require marshalling attributes to be passed as an argument in this Declare statement. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1050
		//[DllImport("user32.dll", EntryPoint = "GetMenuItemInfoA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int GetMenuItemInfo(int hMenu, int un, bool b, ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO lpMenuItemInfo);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		////UPGRADE_TODO: (1050) Structure MENUITEMINFO may require marshalling attributes to be passed as an argument in this Declare statement. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1050
		//[DllImport("user32.dll", EntryPoint = "SetMenuItemInfoA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int SetMenuItemInfo(int hMenu, int un, bool bool_Renamed, ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO lpcMenuItemInfo);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int SendMessage(int hwnd, int wMsg, int wParam, System.IntPtr lParam);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int IsWindow(int hwnd);


		// Locale Info for Regional Settings processing



		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);



		internal static void PutWindowOnTop(Form pFrm)
		{

			int lngWindowPosition = UpgradeSolution1Support.PInvoke.SafeNative.user32.SetWindowPos(pFrm.Handle.ToInt32(), HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

		}
		internal static void PutWindowNormal(Form pFrm)
		{

			int lngWindowPosition = UpgradeSolution1Support.PInvoke.SafeNative.user32.SetWindowPos(pFrm.Handle.ToInt32(), HWND_NOTTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

		}




		internal static int EnableCloseButton(int hwnd, bool Enable)
		{
			int result = 0;
			const int xSC_CLOSE = -10;

			// Check that the window handle passed is valid

			result = -1;
			if (UpgradeSolution1Support.PInvoke.SafeNative.user32.IsWindow(hwnd) == 0)
			{
				return result;
			}

			// Retrieve a handle to the window's system menu

			int hMenu = UpgradeSolution1Support.PInvoke.SafeNative.user32.GetSystemMenu(hwnd, 0);

			// Retrieve the menu item information for the close menu item/button

			UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO MII = UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO.CreateInstance();
			//UPGRADE_WARNING: (2081) Len has a new behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2081
			MII.cbSize = Marshal.SizeOf(MII);
			MII.dwTypeData = new string((char) 0, 80);
			MII.cch = Strings.Len(MII.dwTypeData);
			MII.fMask = MIIM_STATE;

			if (Enable)
			{
				MII.wID = xSC_CLOSE;
			}
			else
			{
				MII.wID = SC_CLOSE;
			}

			result = 0;
			if (UpgradeSolution1Support.PInvoke.SafeNative.user32.GetMenuItemInfo(hMenu, MII.wID, false, ref MII) == 0)
			{
				return result;
			}

			// Switch the ID of the menu item so that VB can not undo the action itself

			int lngMenuID = MII.wID;

			if (Enable)
			{
				MII.wID = SC_CLOSE;
			}
			else
			{
				MII.wID = xSC_CLOSE;
			}

			MII.fMask = MIIM_ID;
			result = -2;
			if (UpgradeSolution1Support.PInvoke.SafeNative.user32.SetMenuItemInfo(hMenu, lngMenuID, false, ref MII) == 0)
			{
				return result;
			}

			// Set the enabled / disabled state of the menu item

			if (Enable)
			{
				MII.fState = (MII.fState | MFS_GRAYED);
				MII.fState -= MFS_GRAYED;
			}
			else
			{
				MII.fState = (MII.fState | MFS_GRAYED);
			}

			MII.fMask = MIIM_STATE;
			result = -3;
			if (UpgradeSolution1Support.PInvoke.SafeNative.user32.SetMenuItemInfo(hMenu, MII.wID, false, ref MII) == 0)
			{
				return result;
			}

			// Activate the non-client area of the window to update the titlebar, and
			// draw the close button in its new state.

			int tempRefParam = 0;
			UpgradeSolution1Support.PInvoke.SafeNative.user32.SendMessage(hwnd, WM_NCACTIVATE, -1, ref tempRefParam);

			return 0;

		}

		internal static void Main_Renamed()
		{
			object DriverHelper = null;
			//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
			UpgradeStubs.DriveRates driveSidereal = null;

			ClientCount = 0;

			oProfile = new DriverHelper.Profile();
			//Dim m_telescope As Telescope
			m_telescope = new Telescope();

			//UPGRADE_TODO: (1067) Member DeviceType is not defined in type Profile. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			oProfile.DeviceType = "Telescope";

			Tracking.g_TrackingRates = new TrackingRates();
			Tracking.g_TrackingRates.Add(driveSidereal);
			//    g_TrackingRates.Add driveLunar
			//    g_TrackingRates.Add driveSolar


			//UPGRADE_ISSUE: (2070) Constant vbSModeStandalone was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2070
			//UPGRADE_ISSUE: (2070) Constant App was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2070
			//UPGRADE_ISSUE: (2064) App property App.StartMode was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2064
			if (UpgradeStubs.VB_Global.getApp().getStartMode() == ((int) UpgradeStubs.VBRUN_ApplicationStartConstants.getvbSModeStandalone()))
			{
				MessageBox.Show(Definitions.AppName + " is an ASCOM driver. It cannot be run stand-alone", FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileDescription, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

		}



		internal static bool SyncToRADEC(double RightAscension, double Declination, double pLongitude, int pHemisphere)
		{
			bool result = false;
			object oLangDll = null;
			object HC = null;


			double targetRAEncoder = 0;
			double targetDECEncoder = 0;
			double currentRAEncoder = 0;
			double currentDECEncoder = 0;
			double SaveRaSync = 0;
			double SaveDecSync = 0;


			double tRa = 0;
			double tha = 0;
			double tPier = 0;

			eqmodvector.Coordt tmpcoord = new eqmodvector.Coordt();

			result = true;

			//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToDouble(HC.ListSyncMode.ListIndex) == 1)
			{
				// Append via sync mode!
				return Alignment.EQ_NPointAppend(RightAscension, Declination, pLongitude, pHemisphere);
			}
			else
			{
				// its an ascom sync - shift whole model
				SaveDecSync = EQMath.gDECSync01;
				SaveRaSync = EQMath.gRASync01;
				EQMath.gRASync01 = 0;
				EQMath.gDECSync01 = 0;

				//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.EncoderTimer.Enabled = false;

				if (!Alignment.gThreeStarEnable)
				{
					currentRAEncoder = EQGetMotorValues(0) + EQMath.gRA1Star;
					currentDECEncoder = EQGetMotorValues(1) + EQMath.gDEC1Star;
				}
				else
				{


					switch(gAlignmentMode)
					{
						case 2 : 
							// nearest 
							tmpcoord = EQMath.DeltaSync_Matrix_Map(EQGetMotorValues(0), EQGetMotorValues(1)); 
							currentRAEncoder = tmpcoord.x; 
							currentDECEncoder = tmpcoord.Y; 
							 
							break;
						case 1 : 
							// n-star 
							tmpcoord = EQMath.Delta_Matrix_Reverse_Map(EQGetMotorValues(0), EQGetMotorValues(1)); 
							currentRAEncoder = tmpcoord.x; 
							currentDECEncoder = tmpcoord.Y; 
							 
							break;
						default:
							//n-star+nearest 
							tmpcoord = EQMath.Delta_Matrix_Reverse_Map(EQGetMotorValues(0), EQGetMotorValues(1)); 
							currentRAEncoder = tmpcoord.x; 
							currentDECEncoder = tmpcoord.Y; 
							 
							if (tmpcoord.f == 0)
							{
								tmpcoord = EQMath.DeltaSync_Matrix_Map(EQGetMotorValues(0), EQGetMotorValues(1));
								currentRAEncoder = tmpcoord.x;
								currentDECEncoder = tmpcoord.Y;
							} 
							 
							break;
					}

				}

				//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.EncoderTimer.Enabled = true;
				tha = EQMath.RangeHA(RightAscension - EQMath.EQnow_lst(pLongitude * EQMath.DEG_RAD));

				if (tha < 0)
				{
					if (pHemisphere == 0)
					{
						tPier = 1;
					}
					else
					{
						tPier = 0;
					}
					tRa = EQMath.Range24(RightAscension - 12);
				}
				else
				{
					if (pHemisphere == 0)
					{
						tPier = 0;
					}
					else
					{
						tPier = 1;
					}
					tRa = RightAscension;
				}

				//Compute for Sync RA/DEC Encoder Values

				targetRAEncoder = EQMath.Get_RAEncoderfromRA(tRa, 0, pLongitude, EQMath.gRAEncoder_Zero_pos, EQMath.gTot_RA, pHemisphere);
				targetDECEncoder = EQMath.Get_DECEncoderfromDEC(Declination, tPier, EQMath.gDECEncoder_Zero_pos, EQMath.gTot_DEC, pHemisphere);

				if (gDisableSyncLimit)
				{
					EQMath.gRASync01 = targetRAEncoder - currentRAEncoder;
					EQMath.gDECSync01 = targetDECEncoder - currentDECEncoder;
				}
				else
				{
					if ((Math.Abs(targetRAEncoder - currentRAEncoder) > EQMath.gEQ_MAXSYNC) || (Math.Abs(targetDECEncoder - currentDECEncoder) > EQMath.gEQ_MAXSYNC))
					{
						//UPGRADE_TODO: (1067) Member GetLangString is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message(oLangDll.GetLangString(6004));
						EQMath.gDECSync01 = SaveDecSync;
						EQMath.gRASync01 = SaveRaSync;
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("RA=" + EQMath.FmtSexa(EQMath.gRA, false) + " " + currentRAEncoder.ToString());
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("SyncRA=" + EQMath.FmtSexa(RightAscension, false) + " " + targetRAEncoder.ToString());
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("DEC=" + EQMath.FmtSexa(EQMath.gDec, true) + " " + currentDECEncoder.ToString());
						//UPGRADE_TODO: (1067) Member Add_Message is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Add_Message("Sync   DEC=" + EQMath.FmtSexa(Declination, true) + " " + targetDECEncoder.ToString());
						result = false;
					}
					else
					{
						EQMath.gRASync01 = targetRAEncoder - currentRAEncoder;
						EQMath.gDECSync01 = targetDECEncoder - currentDECEncoder;
					}
				}

				WriteSyncMap();
				EQMath.gEmulOneShot = true; // Re Sync Display
				//UPGRADE_TODO: (1067) Member DxSalbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.DxSalbl.Caption = StringsHelper.Format(Conversion.Str(EQMath.gRASync01), "000000000");
				//UPGRADE_TODO: (1067) Member DxSblbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.DxSblbl.Caption = StringsHelper.Format(Conversion.Str(EQMath.gDECSync01), "000000000");
			}
			return result;
		}

		internal static void readlastpos()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("LASTPOS_RA"));
			if (tmptxt != "")
			{
				EQMath.gRAEncoderlastpos = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				EQMath.gRAEncoderlastpos = Convert.ToInt32(EQMath.RAEncoder_Home_pos);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("LASTPOS_DEC"));
			if (tmptxt != "")
			{
				EQMath.gDECEncoderlastpos = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				EQMath.gDECEncoderlastpos = Convert.ToInt32(EQMath.gDECEncoder_Home_pos);
			}

		}

		internal static void writelastpos()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("LASTPOS_RA", EQMath.gRAEncoderlastpos.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("LASTPOS_DEC", EQMath.gDECEncoderlastpos.ToString());

		}
		internal static void WriteSyncMap()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("RSYNC01", EQMath.gRASync01.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("DSYNC01", EQMath.gDECSync01.ToString());

		}

		internal static void WriteAlignMap()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("RALIGN01", EQMath.gRA1Star.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("DALIGN01", EQMath.gDEC1Star.ToString());

		}

		internal static void readPolarHomeGoto()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("POLARHOME_GOTO_RA"));
			if (tmptxt != "")
			{
				EQMath.gRAEncoderPolarHomeGoto = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				EQMath.gRAEncoderPolarHomeGoto = 0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("POLARHOME_GOTO_DEC"));
			if (tmptxt != "")
			{
				EQMath.gDECEncoderPolarHomeGoto = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				EQMath.gDECEncoderPolarHomeGoto = 0;
			}

		}

		internal static void writePolarHomeGoto(int StartPos)
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("POLARHOME_GOTO_RA", EQMath.gRAEncoderPolarHomeGoto.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("POLARHOME_GOTO_DEC", EQMath.gDECEncoderPolarHomeGoto.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("POLARHOME_RETICULE_START", StartPos.ToString());

		}



		internal static void resetsync()
		{
			object HC = null;

			EQMath.gRASync01 = 0;
			EQMath.gDECSync01 = 0;

			WriteSyncMap();

			//UPGRADE_TODO: (1067) Member DxSalbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.DxSalbl.Caption = StringsHelper.Format(Conversion.Str(EQMath.gRASync01), "000000000");
			//UPGRADE_TODO: (1067) Member DxSblbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.DxSblbl.Caption = StringsHelper.Format(Conversion.Str(EQMath.gDECSync01), "000000000");

		}

		internal static void writeratebarstateHC()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member VScrollRASlewRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR01_1", Convert.ToString(HC.VScrollRASlewRate.value));
			//UPGRADE_TODO: (1067) Member VScrollDecSlewRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR01_2", Convert.ToString(HC.VScrollDecSlewRate.value));
			//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR01_3", Convert.ToString(HC.HScrollRARate.value));
			//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR01_4", Convert.ToString(HC.HScrollDecRate.value));
			//UPGRADE_TODO: (1067) Member HScrollRAOride is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR01_5", Convert.ToString(HC.HScrollRAOride.value));
			//UPGRADE_TODO: (1067) Member HScrollDecOride is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR01_6", Convert.ToString(HC.HScrollDecOride.value));

		}

		internal static void readratebarstateHC()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR01_1"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member VScrollRASlewRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.VScrollRASlewRate.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR01_2"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member VScrollDecSlewRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.VScrollDecSlewRate.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR01_3"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScrollRARate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollRARate.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR01_4"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScrollDecRate is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollDecRate.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR01_5"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScrollRAOride is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollRAOride.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR01_6"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScrollDecOride is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollDecOride.value = Conversion.Val(tmptxt);
			}
		}

		internal static void writeratebarstateAlign()
		{
			object Align = null;
			object HC = null;

			//UPGRADE_TODO: (1067) Member HScroll1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR02_1", Convert.ToString(Align.HScroll1.value));
			//UPGRADE_TODO: (1067) Member HScroll2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR02_2", Convert.ToString(Align.HScroll2.value));


		}

		internal static void readratebarstateAlign()
		{
			object Align = null;
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR02_1"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScroll1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				Align.HScroll1.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR02_2"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScroll2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				Align.HScroll2.value = Conversion.Val(tmptxt);
			}


		}

		internal static void writeratebarstatePad()
		{
			object Slewpad = null;
			object HC = null;

			//UPGRADE_TODO: (1067) Member VScroll1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR03_1", Convert.ToString(Slewpad.VScroll1.value));
			//UPGRADE_TODO: (1067) Member VScroll2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAR03_2", Convert.ToString(Slewpad.VScroll2.value));


		}

		internal static void writeOnTop()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member HCOnTop is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ON_TOP1", Convert.ToString(HC.HCOnTop.value));

		}

		internal static void readOnTop()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ON_TOP1"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HCOnTop is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HCOnTop.value = Conversion.Val(tmptxt);
			}

		}

		internal static void writeAlignCheck1()
		{
			object HC = null;
			switch(gAlignmentMode)
			{
				case 0 : 
					// n-star+nearset 
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.oPersist.WriteIniValue("SYNCNSTAR", "0"); 
					break;
				case 1 : 
					// n-star - no longer used so force to n-star_nearest 
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.oPersist.WriteIniValue("SYNCNSTAR", "0"); 
					break;
				case 2 : 
					// nearest 
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.oPersist.WriteIniValue("SYNCNSTAR", "2"); 
					break;
			}
		}

		internal static void writeAlignCheck2()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("APPENDSYNCNSTAR", Convert.ToString(HC.ListSyncMode.ListIndex));
			//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			switch(Convert.ToInt32(HC.ListSyncMode.ListIndex))
			{
				case 0 : 
					// ascom standard 
					//UPGRADE_TODO: (1067) Member CommandAddPoint is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.CommandAddPoint.Visible = true; 
					break;
				case 1 : 
					// append syncs 
					//UPGRADE_TODO: (1067) Member CommandAddPoint is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.CommandAddPoint.Visible = false; 
					break;
			}
		}

		internal static void readAlignCheck()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("SYNCNSTAR"));
			if (tmptxt != "")
			{
				switch(tmptxt)
				{
					case "0" : 
						// nstar+nearest 
						gAlignmentMode = 0; 
						//UPGRADE_TODO: (1067) Member ListAlignMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
						HC.ListAlignMode.ListIndex = 0;  // N-Star+nearest 
						break;
					case "1" : 
						// nstar - no longer provided! 
						gAlignmentMode = 0;  // use nstar+nearest insted 
						//UPGRADE_TODO: (1067) Member ListAlignMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
						HC.ListAlignMode.ListIndex = 0;  // N-Star+nearest 
						break;
					case "2" : 
						// nearest 
						gAlignmentMode = 2; 
						//UPGRADE_TODO: (1067) Member ListAlignMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
						HC.ListAlignMode.ListIndex = 1;  // nearest 
						break;
				}
			}
			else
			{
				gAlignmentMode = 0;
				//UPGRADE_TODO: (1067) Member ListAlignMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.ListAlignMode.ListIndex = 0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("APPENDSYNCNSTAR"));
			switch(tmptxt)
			{
				case "0" : 
					// ascom standard 
					//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.ListSyncMode.ListIndex = 0; 
					break;
				case "1" : 
					// append syncs 
					//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.ListSyncMode.ListIndex = 1; 
					break;
				default:
					// default = append syncs 
					//UPGRADE_TODO: (1067) Member ListSyncMode is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.ListSyncMode.ListIndex = 1; 
					// write default to ini file 
					writeAlignCheck2(); 
					break;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("NSTAR_MAXCOMBINATION"));
			if (tmptxt != "")
			{
				Alignment.gMaxCombinationCount = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				Alignment.gMaxCombinationCount = Alignment.MAX_COMBINATION_COUNT;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("NSTAR_MAXCOMBINATION", Alignment.gMaxCombinationCount.ToString());
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ALIGN_PROXIMITY"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScrollProximity is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollProximity.value = Conversion.Val(tmptxt);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member HScrollProximity is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollProximity.value = 0;
				writeAlignProximity();
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ALIGN_SELECTION"));
			if (tmptxt != "")
			{

				gPointFilter = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				gPointFilter = 0;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("ALIGN_SELECTION", "0");
			}
			//UPGRADE_TODO: (1067) Member ComboActivePoints is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.ComboActivePoints.ListIndex = gPointFilter;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ALIGN_LOCALTOPIER"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member CheckLocalPier is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckLocalPier.value = Conversion.Val(tmptxt);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member CheckLocalPier is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckLocalPier.value = 1;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("ALIGN_LOCALTOPIER", "1");
			}

		}

		internal static void writeAlignProximity()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member HScrollProximity is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ALIGN_PROXIMITY", Convert.ToString(HC.HScrollProximity.value));
		}

		internal static void readAlignProximity()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ALIGN_PROXIMITY"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member HScrollProximity is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollProximity.value = Conversion.Val(tmptxt);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member HScrollProximity is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollProximity.value = 0;
				writeAlignProximity();
			}
			//UPGRADE_TODO: (1067) Member HScrollProximity is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			Alignment.CalcPromximityLimits(Convert.ToInt32(HC.HScrollProximity.value));
		}

		internal static void writeColorDat(int a1, int a2, int a3, int b1, int b2, int b3, int F1)
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("FOR_R", a1.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("FOR_G", a2.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("FOR_B", a3.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAK_R", b1.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAK_G", b2.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("BAK_B", b3.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("FONT_S", F1.ToString());

		}


		internal static void readColorDat()
		{
			object HC = null;

			int i = 0;
			int j = 0;
			int k = 0;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("FOR_R"));
			if (tmptxt != "")
			{
				i = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				i = 0xFF;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("FOR_G"));
			if (tmptxt != "")
			{
				j = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				j = 0x80;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("FOR_B"));
			if (tmptxt != "")
			{
				k = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				k = 0x0;
			}

			i = i & 0xFF;
			j = (j * 256) & 0xFF00;
			k = (k * 65536) & 0xFF0000;

			//UPGRADE_TODO: (1067) Member HCMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.HCMessage.ForeColor = i + j + k;
			//    HC.HCTextAlign.ForeColor = i + j + k

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAK_R"));
			if (tmptxt != "")
			{
				i = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				i = 0x80;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAK_G"));
			if (tmptxt != "")
			{
				j = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				j = 0x0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAK_B"));
			if (tmptxt != "")
			{
				k = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				k = 0x0;
			}

			i = i & 0xFF;
			j = (j * 256) & 0xFF00;
			k = (k * 65536) & 0xFF0000;

			//UPGRADE_TODO: (1067) Member HCMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.HCMessage.BackColor = i + j + k;
			//    HC.HCTextAlign.BackColor = i + j + k

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("FONT_S"));
			if (tmptxt != "")
			{
				i = Convert.ToInt32(Conversion.Val(tmptxt));
			}
			else
			{
				i = 7;
			}

			//UPGRADE_TODO: (1067) Member HCMessage is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.HCMessage.FontSize = i;
			//    HC.HCTextAlign.FontSize = i


		}


		internal static void readratebarstatePad()
		{
			object HC = null;
			object Slewpad = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR03_1"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member VScroll1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				Slewpad.VScroll1.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("BAR03_2"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member VScroll2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				Slewpad.VScroll2.value = Conversion.Val(tmptxt);
			}

		}

		internal static void readportrate()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string raval = Convert.ToString(HC.oPersist.ReadIniValue("AUTOGUIDER_RA"));
			switch(raval)
			{
				case "x1.00" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(0, 3); 
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.RAGuideRateList.ListIndex = 3; 
					break;
				case "x0.75" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(0, 2); 
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.RAGuideRateList.ListIndex = 2; 
					break;
				case "x0.50" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(0, 1); 
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.RAGuideRateList.ListIndex = 1; 
					break;
				case "x0.25" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(0, 0); 
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.RAGuideRateList.ListIndex = 0; 
					break;
				default:
					//UPGRADE_TODO: (1067) Member RAGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.RAGuideRateList.ListIndex = 4; 
					break;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string decval = Convert.ToString(HC.oPersist.ReadIniValue("AUTOGUIDER_DEC"));
			switch(decval)
			{
				case "x1.00" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(1, 3); 
					//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.DECGuideRateList.ListIndex = 3; 
					break;
				case "x0.75" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(1, 2); 
					//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.DECGuideRateList.ListIndex = 2; 
					break;
				case "x0.50" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(1, 1); 
					//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.DECGuideRateList.ListIndex = 1; 
					break;
				case "x0.25" : 
					EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetAutoguiderPortRate(1, 0); 
					//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.DECGuideRateList.ListIndex = 0; 
					break;
				default:
					//UPGRADE_TODO: (1067) Member DECGuideRateList is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					HC.DECGuideRateList.ListIndex = 4; 
					break;
			}


		}


		internal static void writeportrateRa(string strRate)
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("AUTOGUIDER_RA", strRate);

		}
		internal static void writeportrateDec(string strRate)
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("AUTOGUIDER_DEC", strRate);

		}

		internal static void writePulseguidepwidth()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member PltimerHscroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("PULSEGUIDE_TIMER_INTERVAL", Convert.ToString(HC.PltimerHscroll.value));
		}

		internal static void readPulseguidepwidth()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("PULSEGUIDE_TIMER_INTERVAL"));

			if (tmptxt == "")
			{
				//UPGRADE_TODO: (1067) Member PltimerHscroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.PltimerHscroll.value = 20;
				//UPGRADE_TODO: (1067) Member Label40 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label40.Caption = " 20";
				//UPGRADE_TODO: (1067) Member Pulseguide_Timer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Pulseguide_Timer.Interval = 20;
				EQMath.gpl_interval = 20;
			}
			else
			{
				EQMath.gpl_interval = Convert.ToInt32(Conversion.Val(tmptxt));
				//UPGRADE_TODO: (1067) Member PltimerHscroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (EQMath.gpl_interval < Convert.ToDouble(HC.PltimerHscroll.min))
				{
					//UPGRADE_TODO: (1067) Member PltimerHscroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					EQMath.gpl_interval = Convert.ToInt32(HC.PltimerHscroll.min);
				}
				else
				{
					//UPGRADE_TODO: (1067) Member PltimerHscroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					if (EQMath.gpl_interval > Convert.ToDouble(HC.PltimerHscroll.max))
					{
						//UPGRADE_TODO: (1067) Member PltimerHscroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						EQMath.gpl_interval = Convert.ToInt32(HC.PltimerHscroll.max);
					}
				}
				//UPGRADE_TODO: (1067) Member PltimerHscroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.PltimerHscroll.value = EQMath.gpl_interval;
				//UPGRADE_TODO: (1067) Member Label40 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label40.Caption = tmptxt;
				//UPGRADE_TODO: (1067) Member Pulseguide_Timer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Pulseguide_Timer.Interval = EQMath.gpl_interval;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("DEC_BACKLASH"));
			if (tmptxt == "")
			{
				gBacklashDec = 0;
			}
			else
			{
				gBacklashDec = Convert.ToInt32(Conversion.Val(tmptxt));
				if (gBacklashDec > 2000 || gBacklashDec < 0)
				{
					gBacklashDec = 0;
				}
			}
			//UPGRADE_TODO: (1067) Member HScrollBacklashDec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.HScrollBacklashDec.value = gBacklashDec;
			//UPGRADE_TODO: (1067) Member LabelBacklashDec is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.LabelBacklashDec.Caption = gBacklashDec.ToString();

		}
		internal static void writeRASyncCheckVal()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member CheckRASync is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("AUTOSYNCRA", Convert.ToString(HC.CheckRASync.value));
		}
		internal static void readRASyncCheckVal()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("AUTOSYNCRA"));
			if (tmptxt == "")
			{
				//UPGRADE_TODO: (1067) Member CheckRASync is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckRASync.value = 1;
				writeRASyncCheckVal();
			}
			else
			{
				//UPGRADE_TODO: (1067) Member CheckRASync is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckRASync.value = Conversion.Val(tmptxt);
			}

		}

		internal static void writeDriftVal()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("RA_DRIFT_VAL", gDriftComp.ToString());
		}

		internal static void readDriftVal()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("RA_DRIFT_VAL"));

			if (tmptxt == "")
			{
				gDriftComp = 0;
				//UPGRADE_TODO: (1067) Member DriftScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.DriftScroll.value = 0;
				//UPGRADE_TODO: (1067) Member Driftlbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Driftlbl.Caption = "0";
			}
			else
			{
				gDriftComp = Convert.ToInt32(Conversion.Val(tmptxt));
				//UPGRADE_TODO: (1067) Member DriftScroll is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.DriftScroll.value = gDriftComp;
				//UPGRADE_TODO: (1067) Member Driftlbl is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Driftlbl.Caption = tmptxt;
			}
			EQSetOffsets();

		}


		internal static void writeAxisRevRA()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member RA_inv is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("RA_REVERSE", Convert.ToString(HC.RA_inv.value));

		}


		internal static void writeAxisRevDEC()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member DEC_Inv is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("DEC_REVERSE", Convert.ToString(HC.DEC_Inv.value));

		}


		internal static void readDevelopmentOptions()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToString(HC.oPersist.ReadIniValue("Advanced")) == "1")
			{
				gAdvanced = 1;
				//UPGRADE_TODO: (1067) Member Combo3PointAlgorithm is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Combo3PointAlgorithm.Visible = true;
				//UPGRADE_TODO: (1067) Member CheckRASync is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckRASync.Visible = true;
				//UPGRADE_TODO: (1067) Member Label35 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label35.Visible = true;
				//UPGRADE_TODO: (1067) Member CheckLocalPier is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckLocalPier.Visible = true;
				//UPGRADE_TODO: (1067) Member FrameAdvanced is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.FrameAdvanced.Visible = true;
				//UPGRADE_TODO: (1067) Member FramePGAvanced is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.FramePGAvanced.Visible = true;
				//UPGRADE_TODO: (1067) Member LabelSlewLimit is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.LabelSlewLimit.Visible = true;
				//UPGRADE_TODO: (1067) Member Label31 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label31.Visible = true;
				//UPGRADE_TODO: (1067) Member HScrollSlewLimit is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollSlewLimit.Visible = true;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("Advanced", "0");
				gAdvanced = 0;
				//UPGRADE_TODO: (1067) Member Combo3PointAlgorithm is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Combo3PointAlgorithm.Visible = false;
				//UPGRADE_TODO: (1067) Member CheckRASync is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckRASync.Visible = false;
				//UPGRADE_TODO: (1067) Member Label35 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label35.Visible = false;
				//UPGRADE_TODO: (1067) Member CheckLocalPier is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.CheckLocalPier.Visible = false;
				//UPGRADE_TODO: (1067) Member FrameAdvanced is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.FrameAdvanced.Visible = false;
				//UPGRADE_TODO: (1067) Member FramePGAvanced is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.FramePGAvanced.Visible = false;
				//UPGRADE_TODO: (1067) Member LabelSlewLimit is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.LabelSlewLimit.Visible = false;
				//UPGRADE_TODO: (1067) Member Label31 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label31.Visible = false;
				//UPGRADE_TODO: (1067) Member HScrollSlewLimit is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollSlewLimit.Visible = false;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToString(HC.oPersist.ReadIniValue("POLAR_ALIGNMENT")) == "1")
			{
				gShowPolarAlign = 1;
				//UPGRADE_TODO: (1067) Member puPolar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.puPolar.Visible = true;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("POLAR_ALIGNMENT", "0");
				gShowPolarAlign = 0;
				//UPGRADE_TODO: (1067) Member puPolar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.puPolar.Visible = false;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			if (Convert.ToString(HC.oPersist.ReadIniValue("3POINT_ALGORITHM")) == "1")
			{
				g3PointAlgorithm = 1;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("3POINT_ALGORITHM", "0");
				g3PointAlgorithm = 0;
			}
			//UPGRADE_TODO: (1067) Member Combo3PointAlgorithm is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Combo3PointAlgorithm.ListIndex = g3PointAlgorithm;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmp = Convert.ToString(HC.oPersist.ReadIniValue("MAX_GOTO_INTERATIONS"));
			if (tmp != "")
			{
				Goto.gMaxSlewCount = Convert.ToInt32(Conversion.Val(tmp));
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("MAX_GOTO_INTERATIONS", "5");
				Goto.gMaxSlewCount = 5;
			}
			//UPGRADE_TODO: (1067) Member HScrollSlewRetries is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.HScrollSlewRetries.value = Goto.gMaxSlewCount;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmp = Convert.ToString(HC.oPersist.ReadIniValue("GOTO_RESOLUTION"));
			if (tmp != "")
			{
				Goto.gGotoResolution = Convert.ToInt32(Conversion.Val(tmp));
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("GOTO_RESOLUTION", "20");
				Goto.gGotoResolution = 20;
			}
			//UPGRADE_TODO: (1067) Member HScrollGotoRes is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.HScrollGotoRes.value = Goto.gGotoResolution;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmp = Convert.ToString(HC.oPersist.ReadIniValue("GOTO_RA_COMPENSATE"));
			if (tmp != "")
			{
				Goto.gRA_Compensate = Convert.ToInt32(Conversion.Val(tmp));
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("GOTO_RA_COMPENSATE", "40");
				Goto.gRA_Compensate = 40;
			}
			//UPGRADE_TODO: (1067) Member HScrollSlewAdjust is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.HScrollSlewAdjust.value = Goto.gRA_Compensate;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmp = Convert.ToString(HC.oPersist.ReadIniValue("COMMS_ERROR_STOP"));
			if (tmp != "")
			{
				gCommErrorStop = Convert.ToInt32(Conversion.Val(tmp));
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("COMMS_ERROR_STOP", "0");
				gCommErrorStop = 0;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmp = Convert.ToString(HC.oPersist.ReadIniValue("LST_DISPLAY_MODE"));
			if (tmp != "")
			{
				gLstDisplayMode = Convert.ToInt32(Conversion.Val(tmp));
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("LST_DISPLAY_MODE", "0");
				gLstDisplayMode = 0;
			}


		}


		internal static void readAscomCompatibiity()
		{
			object HC = null;
			string tmptxt = "";

			try
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_SLEWTRACKOFF"));
				if (tmptxt != "")
				{
					gAscomCompatibility.SlewWithTrackingOff = (Boolean.TryParse(tmptxt, out gAscomCompatibility.SlewWithTrackingOff)) ? gAscomCompatibility.SlewWithTrackingOff : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					gAscomCompatibility.SlewWithTrackingOff = true;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_SLEWTRACKOFF", gAscomCompatibility.SlewWithTrackingOff.ToString());
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_PULSEGUIDE"));
				if (tmptxt != "")
				{
					gAscomCompatibility.AllowPulseGuide = (Boolean.TryParse(tmptxt, out gAscomCompatibility.AllowPulseGuide)) ? gAscomCompatibility.AllowPulseGuide : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					gAscomCompatibility.AllowPulseGuide = true;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_PULSEGUIDE", gAscomCompatibility.AllowPulseGuide.ToString());
				}

				if (gAscomCompatibility.AllowPulseGuide)
				{
					//UPGRADE_TODO: (1067) Member Frame5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Frame5.Visible = true;
					//UPGRADE_TODO: (1067) Member Frame6 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Frame6.Visible = false;
				}
				else
				{
					//UPGRADE_TODO: (1067) Member Frame6 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Frame6.Visible = true;
					//UPGRADE_TODO: (1067) Member Frame5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.Frame5.Visible = false;
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_EXCEPTIONS"));
				if (tmptxt != "")
				{
					gAscomCompatibility.AllowExceptions = (Boolean.TryParse(tmptxt, out gAscomCompatibility.AllowExceptions)) ? gAscomCompatibility.AllowExceptions : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					gAscomCompatibility.AllowExceptions = true;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_EXCEPTIONS", gAscomCompatibility.AllowExceptions.ToString());
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_PG_EXCEPTIONS"));
				if (tmptxt != "")
				{
					gAscomCompatibility.AllowPulseGuideExceptions = (Boolean.TryParse(tmptxt, out gAscomCompatibility.AllowPulseGuideExceptions)) ? gAscomCompatibility.AllowPulseGuideExceptions : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					gAscomCompatibility.AllowPulseGuideExceptions = true;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_PG_EXCEPTIONS", gAscomCompatibility.AllowPulseGuideExceptions.ToString());
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_BLOCK_PARK"));
				if (tmptxt != "")
				{
					gAscomCompatibility.BlockPark = (Boolean.TryParse(tmptxt, out gAscomCompatibility.BlockPark)) ? gAscomCompatibility.BlockPark : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					gAscomCompatibility.BlockPark = false;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_BLOCK_PARK", gAscomCompatibility.BlockPark.ToString());
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_SITEWRITES"));
				if (tmptxt != "")
				{
					gAscomCompatibility.AllowSiteWrites = (Boolean.TryParse(tmptxt, out gAscomCompatibility.AllowSiteWrites)) ? gAscomCompatibility.AllowSiteWrites : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					gAscomCompatibility.AllowSiteWrites = false;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_SITEWRITES", gAscomCompatibility.AllowSiteWrites.ToString());
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_EPOCH"));
				if (tmptxt != "")
				{
					gAscomCompatibility.Epoch = Convert.ToInt16(Conversion.Val(tmptxt));
				}
				else
				{
					gAscomCompatibility.Epoch = 0;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_EPOCH", gAscomCompatibility.Epoch.ToString());
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_SOP"));
				if (tmptxt != "")
				{
					gAscomCompatibility.SideOfPier = Convert.ToInt16(Conversion.Val(tmptxt));
				}
				else
				{
					gAscomCompatibility.SideOfPier = 0;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_SOP", gAscomCompatibility.SideOfPier.ToString());
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_SWAP_PSOP"));
				if (tmptxt != "")
				{
					gAscomCompatibility.SwapPointingSideOfPier = tmptxt == "1";
				}
				else
				{
					gAscomCompatibility.SwapPointingSideOfPier = false;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_SWAP_PSOP", "0");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_SWAP_SOP"));
				if (tmptxt != "")
				{
					gAscomCompatibility.SwapPhysicalSideOfPier = tmptxt == "1";
				}
				else
				{
					gAscomCompatibility.SwapPhysicalSideOfPier = false;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_SWAP_SOP", "0");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ASCOM_COMPAT_STRICT"));
				if (tmptxt != "")
				{
					if (tmptxt == "1")
					{
						gAscomCompatibility.Strict = true;
						gAscomCompatibility.SideOfPier = 0;

					}
					else
					{
						gAscomCompatibility.Strict = false;
					}
				}
				else
				{
					gAscomCompatibility.Strict = false;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("ASCOM_COMPAT_STRICT", "0");
					gAscomCompatibility.AllowExceptions = true;
				}
			}
			catch
			{


				gAscomCompatibility.Strict = false;
				gAscomCompatibility.SlewWithTrackingOff = true;
				gAscomCompatibility.AllowPulseGuide = true;
				gAscomCompatibility.AllowExceptions = true;
				gAscomCompatibility.AllowPulseGuideExceptions = true;
				gAscomCompatibility.Epoch = 0;
				gAscomCompatibility.SideOfPier = 0;
				gAscomCompatibility.BlockPark = false;
				WriteAscomCompatibiity();
			}


		}
		internal static void WriteAscomCompatibiity()
		{
			object HC = null;
			if (gAscomCompatibility.Strict)
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("ASCOM_COMPAT_STRICT", "1");
			}
			else
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("ASCOM_COMPAT_STRICT", "0");
			}
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_SLEWTRACKOFF", gAscomCompatibility.SlewWithTrackingOff.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_SLEWTRACKOFF", gAscomCompatibility.SlewWithTrackingOff.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_PULSEGUIDE", gAscomCompatibility.AllowPulseGuide.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_EXCEPTIONS", gAscomCompatibility.AllowExceptions.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_PG_EXCEPTIONS", gAscomCompatibility.AllowPulseGuideExceptions.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_BLOCK_PARK", gAscomCompatibility.BlockPark.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_SITEWRITES", gAscomCompatibility.AllowSiteWrites.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_EPOCH", gAscomCompatibility.Epoch.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("ASCOM_COMPAT_SOP", gAscomCompatibility.SideOfPier.ToString());

		}

		internal static void readAutoFlipData()
		{
			object HC = null;
			string tmptxt = "";

			try
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("FLIP_AUTO_ALLOWED"));
				if (tmptxt != "")
				{
					Limits.gAutoFlipAllowed = (Boolean.TryParse(tmptxt, out Limits.gAutoFlipAllowed)) ? Limits.gAutoFlipAllowed : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					// default to allow slews when not tracking - not ASCOM compliant but is CDC compliant!

					Limits.gAutoFlipAllowed = false;
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("FLIP_AUTO_ENABLED"));
				if (tmptxt != "")
				{
					Limits.gAutoFlipEnabled = (Boolean.TryParse(tmptxt, out Limits.gAutoFlipEnabled)) ? Limits.gAutoFlipEnabled : Convert.ToBoolean(Double.Parse(tmptxt));
				}
				else
				{
					// default to allow slews when not tracking - not ASCOM compliant but is CDC compliant!
					Limits.gAutoFlipEnabled = false;
				}

				WriteAutoFlipData();
			}
			catch
			{

				Limits.gAutoFlipEnabled = false;
				Limits.gAutoFlipAllowed = false;
				WriteAutoFlipData();
			}
		}

		internal static void WriteAutoFlipData()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("FLIP_AUTO_ALLOWED", Limits.gAutoFlipAllowed.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("FLIP_AUTO_ENABLED", Limits.gAutoFlipEnabled.ToString());
		}

		internal static void readAxisRev()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("RA_REVERSE"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member RA_inv is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.RA_inv.value = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("DEC_REVERSE"));
			if (tmptxt != "")
			{
				//UPGRADE_TODO: (1067) Member DEC_Inv is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.DEC_Inv.value = Conversion.Val(tmptxt);
			}

		}

		internal static void writePresetSlewRates()
		{
			object HC = null;
			string valstr = "";
			int Count = 0;

			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Ini = Convert.ToString(HC.oPersist.GetIniPath) + "\\EQMOD.ini";

			string key = "[slewrates]";

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValueEx("COUNT", gPresetSlewRatesCount.ToString(), key, Ini);

			int tempForEndVar = gPresetSlewRatesCount;
			for (Count = 1; Count <= tempForEndVar; Count++)
			{
				valstr = "RATE_" + Count.ToString();
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx(valstr, gPresetSlewRates[Count - 1].ToString(), key, Ini);
			}

			for (Count = 1; Count <= 4; Count++)
			{
				valstr = "RATEBTN_" + Count.ToString();
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx(valstr, gRateButtons[Count - 1].ToString(), key, Ini);
			}

		}
		internal static void readPresetSlewRates()
		{
			object HC = null;

			string valstr = "";
			int Count = 0;
			int[] DefaultRates = new int[10];

			DefaultRates[0] = 1;
			DefaultRates[1] = 8;
			DefaultRates[2] = 64;
			DefaultRates[3] = 800;
			DefaultRates[4] = 0;
			DefaultRates[5] = 0;
			DefaultRates[6] = 0;
			DefaultRates[7] = 0;
			DefaultRates[8] = 0;
			DefaultRates[9] = 0;

			//UPGRADE_TODO: (1067) Member PresetRateCombo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PresetRateCombo.Clear();
			//UPGRADE_TODO: (1067) Member PresetRate2Combo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PresetRate2Combo.Clear();

			// set up a file path for the align.ini file
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string Ini = Convert.ToString(HC.oPersist.GetIniPath) + "\\EQMOD.ini";

			string key = "[slewrates]";

			// read preset count
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("COUNT", key, Ini));
			if (tmptxt != "")
			{
				gPresetSlewRatesCount = Convert.ToInt32(Conversion.Val(tmptxt));
				if (gPresetSlewRatesCount > 10)
				{
					gPresetSlewRatesCount = 10;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValueEx("COUNT", "10", key, Ini);
				}
			}
			else
			{
				gPresetSlewRatesCount = 4;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("COUNT", "4", key, Ini);
			}

			int tempForEndVar = gPresetSlewRatesCount;
			for (Count = 1; Count <= tempForEndVar; Count++)
			{
				valstr = "RATE_" + Count.ToString();
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx(valstr, key, Ini));
				if (tmptxt != "")
				{
					gPresetSlewRates[Count - 1] = Conversion.Val(tmptxt);
				}
				else
				{
					gPresetSlewRates[Count - 1] = DefaultRates[Count - 1];
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValueEx(valstr, gPresetSlewRates[Count - 1].ToString(), key, Ini);
				}
				// add preset to combo
				//UPGRADE_TODO: (1067) Member PresetRateCombo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.PresetRateCombo.AddItem(Count.ToString());
				//UPGRADE_TODO: (1067) Member PresetRate2Combo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.PresetRate2Combo.AddItem(Count.ToString());
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx("InitalPreset", key, Ini));
			if (tmptxt != "")
			{
				gCurrentRatePreset = Convert.ToInt32(Conversion.Val(tmptxt));
				if (gCurrentRatePreset > 10)
				{
					gCurrentRatePreset = 1;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValueEx("InitalPreset", "1", key, Ini);
				}
			}
			else
			{
				gCurrentRatePreset = 1;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValueEx("InitalPreset", "1", key, Ini);
			}

			//UPGRADE_TODO: (1067) Member PresetRateCombo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PresetRateCombo.ListIndex = gCurrentRatePreset - 1;
			//UPGRADE_TODO: (1067) Member PresetRate2Combo is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.PresetRate2Combo.ListIndex = gCurrentRatePreset - 1;


			for (Count = 1; Count <= 4; Count++)
			{
				valstr = "RATEBTN_" + Count.ToString();
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValueEx(valstr, key, Ini));
				if (tmptxt != "")
				{
					gRateButtons[Count - 1] = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					gRateButtons[Count - 1] = Count;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValueEx(valstr, Count.ToString(), key, Ini);
				}
			}


		}

		internal static void readPoleStar()
		{
			double J2000 = 0;
			object[, , , ] Precess = null;
			object HC = null;
			object[] now_mjd = null;
			double RA = 0;
			double DEC = 0;

			//J2000 = RA: 02h31m50.209s DE:+89°15'50.86"

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("PoleStarId"));
			if (tmptxt != "")
			{
				gPoleStarIdx = Convert.ToInt32(Conversion.Val(tmptxt));
				//UPGRADE_TODO: (1067) Member ComboPoleStar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (gPoleStarIdx >= Convert.ToDouble(HC.ComboPoleStar.ListCount))
				{
					gPoleStarIdx = 0;
				}
			}
			else
			{
				gPoleStarIdx = 0;
			}

			switch(gPoleStarIdx)
			{
				case 0 : 
					// polaris 
					RA = 2.53019444444444d; 
					DEC = 89.2641666666667d; 
					break;
				case 1 : 
					// Chi Oct 
					RA = 18.91286139d; 
					DEC = -87.60628056d; 
					break;
				case 2 : 
					//Tau Oct 
					RA = 23.46775278d; 
					DEC = -87.48219167d; 
					break;
				case 3 : 
					// Sigma Oct 
					RA = 21.146498333333d; 
					DEC = -88.9547972222d; 

					 
					break;
				default:
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("POLE_STAR_J2000RA")); 
					if (tmptxt != "")
					{
						RA = Double.Parse(tmptxt);
					}
					else
					{
						RA = 2.53061361d;
						//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.oPersist.WriteIniValue("POLE_STAR_J2000RA", RA.ToString());
					} 
					 
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("POLE_STAR_J2000DEC")); 
					if (tmptxt != "")
					{
						DEC = Double.Parse(tmptxt);
					}
					else
					{
						DEC = 89.2641278d;
						//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.oPersist.WriteIniValue("POLE_STAR_J2000DEC", DEC.ToString());
					} 
					break;
			}

			//    HC.oPersist.WriteIniValue "POLE_STAR_J2000RA", CStr(RA)
			//    HC.oPersist.WriteIniValue "POLE_STAR_J2000DEC", CStr(DEC)

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("PolarReticuleEpoch"));
			if (tmptxt != "")
			{
				gPolarReticuleEpoch = Conversion.Val(tmptxt);
			}
			else
			{
				gPolarReticuleEpoch = 2000;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("PolarReticuleEpoch", "2000");
			}

			//UPGRADE_TODO: (1067) Member ComboPoleStar is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.ComboPoleStar.ListIndex = gPoleStarIdx;

			gPoleStarRaJ2000 = RA;
			gPoleStarDecJ2000 = DEC;
			double RA2 = RA;
			double DEC2 = DEC;
			double epochnow = 2000 + (Convert.ToDouble(now_mjd) - J2000) / 365.25d;
			object tempAuxVar = Precess[Convert.ToInt32(RA2), Convert.ToInt32(DEC2), 2000, Convert.ToInt32(epochnow)];
			gPoleStarRa = RA2;
			gPoleStarDec = DEC2;
			object tempAuxVar2 = Precess[Convert.ToInt32(RA), Convert.ToInt32(DEC), 2000, Convert.ToInt32(gPolarReticuleEpoch)];
			gPoleStarReticuleDec = DEC;


		}
		internal static void writePoleStar()
		{
			double J2000 = 0;
			object[, , , ] Precess = null;
			object HC = null;
			object[] now_mjd = null;
			string tmptxt = "";
			double RA = 0;
			double DEC = 0;

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("PoleStarId", gPoleStarIdx.ToString());
			switch(gPoleStarIdx)
			{
				case 0 : 
					// polaris 
					RA = 2.53019444444444d;  // 2.53061361 ' 2.53019444444444 
					DEC = 89.2641666666667d;  // 89.2641278  ' 89.2641666666667 
					 
					break;
				case 1 : 
					// Chi Oct 
					RA = 18.91286139d; 
					DEC = -87.60628056d; 
					 
					break;
				case 2 : 
					//Tau Oct 
					RA = 23.46775278d; 
					DEC = -87.48219167d; 
					 
					break;
				default:
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("POLE_STAR_J2000RA")); 
					if (tmptxt != "")
					{
						RA = Double.Parse(tmptxt);
					}
					else
					{
						RA = 2.53061361d;
						//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.oPersist.WriteIniValue("POLE_STAR_J2000RA", RA.ToString());
					} 
					 
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067 
					tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("POLE_STAR_J2000DEC")); 
					if (tmptxt != "")
					{
						DEC = Double.Parse(tmptxt);
					}
					else
					{
						DEC = 89.2641278d;
						//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.oPersist.WriteIniValue("POLE_STAR_J2000DEC", DEC.ToString());
					} 
					break;
			}

			//    HC.oPersist.WriteIniValue "POLE_STAR_J2000RA", CStr(RA)
			//    HC.oPersist.WriteIniValue "POLE_STAR_J2000DEC", CStr(DEC)

			double epochnow = 2000 + (Convert.ToDouble(now_mjd) - J2000) / 365.25d;
			object tempAuxVar = Precess[Convert.ToInt32(RA), Convert.ToInt32(DEC), 2000, Convert.ToInt32(epochnow)];
			gPoleStarRa = RA;
			gPoleStarDec = DEC;
		}

		internal static void readExtendedMountFunctions()
		{
			object HC = null;
			string tmptxt = "";
			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{

				//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label5(3).Caption = EQMath.printhex(Convert.ToDouble(EQMath.gMount_Features));

				if ((EQMath.gMount_Features & 0x10000) != 0)
				{
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("POLAR_SCOPE_BRIGHTNESS"));
					if (tmptxt != "")
					{
						//UPGRADE_TODO: (1067) Member HScrollPolarLed is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.HScrollPolarLed.value = Conversion.Val(tmptxt);
					}
					else
					{
						//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.oPersist.WriteIniValue("POLAR_SCOPE_BRIGHTNESS", "125");
						//UPGRADE_TODO: (1067) Member HScrollPolarLed is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.HScrollPolarLed.value = 125;
					}
					// write  to mount.
					//UPGRADE_TODO: (1067) Member HScrollPolarLed is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_WP(0, 10006, Convert.ToInt32(HC.HScrollPolarLed.value));
				}

				if ((EQMath.gMount_Features & 0x30) != 0)
				{
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("ENABLE_HW_ENCODERS"));
					if (tmptxt == "0")
					{
						//UPGRADE_TODO: (1067) Member Combo1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Combo1.ListIndex = 0;
					}
					else
					{
						//UPGRADE_TODO: (1067) Member Combo1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.Combo1.ListIndex = 1;
						//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						HC.oPersist.WriteIniValue("ENABLE_HW_ENCODERS", "1");
					}
				}
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}
		}

		internal static void ShowExtendedMountFunctions()
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Label5(3).Caption = EQMath.printhex(Convert.ToDouble(EQMath.gMount_Features));

			if ((EQMath.gMount_Features & 0x10000) != 0)
			{
				//UPGRADE_TODO: (1067) Member HScrollPolarLed is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollPolarLed.Visible = true;
				//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label5(0).Visible = true;
				//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label5(1).Visible = true;
				//UPGRADE_TODO: (1067) Member Command1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Command1(3).Visible = true;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member HScrollPolarLed is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.HScrollPolarLed.Visible = false;
				//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label5(0).Visible = false;
				//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label5(1).Visible = false;
				//UPGRADE_TODO: (1067) Member Command1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Command1(3).Visible = false;
			}

			//UPGRADE_TODO: (1067) Member Combo1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Combo1.Visible = (EQMath.gMount_Features & 0x30) != 0;

			// PPEC
			if ((EQMath.gMount_Features & 0x4) != 0)
			{
				//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Check1(0).Visible = true;
				//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Check1(1).Visible = true;
				//UPGRADE_TODO: (1067) Member Command1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Command1(0).Visible = true;
				//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label5(4).Visible = true;
			}
			else
			{
				//UPGRADE_TODO: (1067) Member Command1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Command1(0).Visible = false;
				//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Check1(0).Visible = false;
				//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Check1(1).Visible = false;
				//UPGRADE_TODO: (1067) Member Label5 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Label5(4).Visible = false;
			}

			// SNAP1
			//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Check1(2).Visible = (EQMath.gMount_Features & 0x1) != 0;

			// SNAP3
			//UPGRADE_TODO: (1067) Member Check1 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Check1(3).Visible = (EQMath.gMount_Features & 0x2) != 0;

		}

		internal static double GetEmulRA()
		{

			double emulinc = 0;

			// Compute for elapsed Time

			if (EQMath.gTrackingStatus == 1)
			{

				EQMath.gCurrent_time = EQMath.EQnow_lst_norange();
				if (EQMath.gLast_time == 0)
				{
					EQMath.gCurrent_time = 0.000002d;
				}
				if (EQMath.gEmulRA_Init == 0)
				{
					EQMath.gEmulRA_Init = EQMath.gEmulRA;
				}

				if (EQMath.gLast_time > EQMath.gCurrent_time)
				{ // Counter wrap around ?
					EQMath.gLast_time = EQMath.EQnow_lst_norange();
					EQMath.gCurrent_time = EQMath.gLast_time;
					EQMath.gEmulRA_Init = EQMath.gEmulRA;
				}

				// Compute Elapste stepper count based on Elapsed Local Sidreal time (PC time)

				emulinc = EQMath.gEMUL_RATE2 * (EQMath.gCurrent_time - EQMath.gLast_time);
				//       If gRA_LastRate = 0 Then
				//          emulinc = gEMUL_RATE2 * (gCurrent_time - gLast_time)
				//       Else  ' PEC tracking
				//          emulinc = (gRightAscensionRate / gARCSECSTEP) * (gCurrent_time - gLast_time)
				//          emulinc = (gCurrent_time - gLast_time) * gTot_RA / (1296000 / gRightAscensionRate)
				//       End If

				if (EQMath.gHemisphere == 0)
				{
					return EQMath.gEmulRA_Init + emulinc;
				}
				else
				{
					return EQMath.gEmulRA_Init - emulinc;
				}

			}
			else
			{
				return EQMath.gEmulRA;
			}

		}


		internal static double GetEmulRA_EQ()
		{

			double emulinc = 0;
			double tmpEmulRA = 0;
			double tmpgRA_Hours = 0;

			double tmpgRA_Encoder = 0;
			double tmpgDec_Encoder = 0;


			eqmodvector.Coordt tmpcoord = new eqmodvector.Coordt();

			//Compute for elapsed Time

			if (EQMath.gTrackingStatus == 1)
			{

				EQMath.gCurrent_time = EQMath.EQnow_lst_norange();
				if (EQMath.gLast_time == 0)
				{
					EQMath.gCurrent_time = 0.000002d;
				}
				if (EQMath.gEmulRA_Init == 0)
				{
					EQMath.gEmulRA_Init = EQMath.gEmulRA;
				}

				if (EQMath.gLast_time > EQMath.gCurrent_time)
				{ // Counter wrap around ?
					EQMath.gLast_time = EQMath.EQnow_lst_norange();
					EQMath.gCurrent_time = EQMath.gLast_time;
					EQMath.gEmulRA_Init = EQMath.gEmulRA;
				}

				// Compute Elapste stepper count based on Elapsed Local Sidreal time (PC time)

				emulinc = EQMath.gEMUL_RATE2 * (EQMath.gCurrent_time - EQMath.gLast_time);
				//        If gRA_LastRate = 0 Then
				//           emulinc = gEMUL_RATE2 * (gCurrent_time - gLast_time)
				//        Else  ' PEC tracking
				//           emulinc = (gRightAscensionRate / gARCSECSTEP) * (gCurrent_time - gLast_time)
				//           emulinc = (gCurrent_time - gLast_time) * gTot_RA / (1296000 / gRightAscensionRate)
				//        End If

				if (EQMath.gHemisphere == 0)
				{
					tmpEmulRA = EQMath.gEmulRA_Init + emulinc;
				}
				else
				{
					tmpEmulRA = EQMath.gEmulRA_Init - emulinc;
				}
			}
			else
			{
				tmpEmulRA = EQMath.gEmulRA;
			}

			if (!Alignment.gThreeStarEnable)
			{
				tmpgRA_Encoder = EQMath.Delta_RA_Map(tmpEmulRA);
				tmpgDec_Encoder = EQMath.Delta_DEC_Map(EQMath.gEmulDEC);
			}
			else
			{


				switch(gAlignmentMode)
				{
					case 2 : 
						// nearest 
						tmpcoord = EQMath.DeltaSync_Matrix_Map(tmpEmulRA, EQMath.gEmulDEC); 
						tmpgRA_Encoder = tmpcoord.x; 
						tmpgDec_Encoder = tmpcoord.Y; 
						 
						break;
					case 1 : 
						// n-star+nearest 
						tmpcoord = EQMath.Delta_Matrix_Reverse_Map(tmpEmulRA, EQMath.gEmulDEC); 
						tmpgRA_Encoder = tmpcoord.x; 
						tmpgDec_Encoder = tmpcoord.Y; 
						 
						break;
					default:
						tmpcoord = EQMath.Delta_Matrix_Reverse_Map(tmpEmulRA, EQMath.gEmulDEC); 
						tmpgRA_Encoder = tmpcoord.x; 
						tmpgDec_Encoder = tmpcoord.Y; 
						 
						if (tmpcoord.f == 0)
						{

							tmpcoord = EQMath.DeltaSync_Matrix_Map(tmpEmulRA, EQMath.gEmulDEC);
							tmpgRA_Encoder = tmpcoord.x;
							tmpgDec_Encoder = tmpcoord.Y;

						} 
						 
						break;
				}

			}

			if ((tmpgRA_Encoder < 0x1000000))
			{
				tmpgRA_Hours = EQMath.Get_EncoderHours(EQMath.gRAEncoder_Zero_pos, tmpgRA_Encoder, EQMath.gTot_RA, EQMath.gHemisphere);
			}

			double tRa = EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD) + tmpgRA_Hours;

			double tmpgDec_DegNoAdjust = EQMath.Get_EncoderDegrees(EQMath.gDECEncoder_Zero_pos, tmpgDec_Encoder, EQMath.gTot_DEC, EQMath.gHemisphere);

			if (EQMath.gHemisphere == 0)
			{
				if ((tmpgDec_DegNoAdjust > 90) && (tmpgDec_DegNoAdjust <= 270))
				{
					tRa -= 12;
				}
			}
			else
			{
				if ((tmpgDec_DegNoAdjust <= 90) || (tmpgDec_DegNoAdjust > 270))
				{
					tRa += 12;
				}
			}

			return EQMath.Range24(tRa);

		}

		// checks that a guiding rate commanded is in range
		internal static bool RateIsInRange(double rate, Rates Rates)
		{
			int i = 0;
			Rate r = null;

			int tempForEndVar = Rates.Count;
			for (i = 1; i <= tempForEndVar; i++)
			{
				r = Rates[i];
				if (Math.Abs(rate) > r.Maximum || Math.Abs(rate) < r.Minimum)
				{
					return false;
				}
			}
			return true;
		}

		internal static int EQGP(int motor_id, int p_id)
		{
			int ret = 0;

			switch(p_id)
			{
				case 10006 : 
					// get worm steps from ini file this way we can easilly simulate heq5 
					if (Mount.gCustomMount == 1)
					{
						switch(motor_id)
						{
							case 0 : 
								ret = Convert.ToInt32(Mount.gCustomRAWormSteps); 
								break;
							case 1 : 
								ret = Convert.ToInt32(Mount.gCustomDECWormSteps); 
								break;
							default:
								ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GP(motor_id, p_id); 
								break;
						}
					}
					else
					{
						ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GP(motor_id, p_id);
					} 
					break;
				default:
					ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GP(motor_id, p_id); 
					break;
			}
			return ret;
		}

		internal static void ReadFormPosition()
		{
			object HC = null;
			float tmp = 0;
			int DesktopLeft = 0;
			int DesktopTop = 0;
			int DesktopWidth = 0;
			int DesktopHeight = 0;
			int DesktopRight = 0;
			int DesktopBottom = 0;

			if (UpgradeSolution1Support.PInvoke.SafeNative.user32.GetSystemMetrics(SM_CMONITORS) == 0)
			{
				//No multi monitor
				DesktopLeft = 0;
				DesktopRight = Screen.PrimaryScreen.Bounds.Width * 15;
				DesktopTop = 0;
				DesktopBottom = Screen.PrimaryScreen.Bounds.Height * 15;
			}
			else
			{
				DesktopLeft = UpgradeSolution1Support.PInvoke.SafeNative.user32.GetSystemMetrics(SM_XVIRTUALSCREEN);
				DesktopLeft *= 15;
				DesktopTop = UpgradeSolution1Support.PInvoke.SafeNative.user32.GetSystemMetrics(SM_YVIRTUALSCREEN);
				DesktopTop *= 15;
				DesktopWidth = UpgradeSolution1Support.PInvoke.SafeNative.user32.GetSystemMetrics(SM_CXVIRTUALSCREEN) * 15;
				DesktopHeight = UpgradeSolution1Support.PInvoke.SafeNative.user32.GetSystemMetrics(SM_CYVIRTUALSCREEN) * 15;
				DesktopRight = DesktopLeft + DesktopWidth;
				DesktopBottom = DesktopTop + DesktopHeight;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("form_height"));
			if (tmptxt == "")
			{
				//UPGRADE_TODO: (1067) Member Height is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("form_height", HC.Height);
			}
			else
			{
				//UPGRADE_TODO: (1067) Member Height is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Height = Conversion.Val(tmptxt);
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("form_top"));
			if (tmptxt == "")
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("form_top", 0);
				//UPGRADE_TODO: (1067) Member Top is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Top = 0;
			}
			else
			{
				tmp = (float) Conversion.Val(tmptxt);
				if (tmp < DesktopTop)
				{
					tmp = DesktopTop;
				}
				//UPGRADE_TODO: (1067) Member Height is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (tmp > ((float) (DesktopBottom - Convert.ToDouble(HC.Height))))
				{
					//UPGRADE_TODO: (1067) Member Height is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					tmp = (float) (DesktopBottom - Convert.ToDouble(HC.Height));
				}
				//UPGRADE_TODO: (1067) Member Top is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Top = tmp;
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("form_left"));
			if (tmptxt == "")
			{
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("form_left", 0);
				//UPGRADE_TODO: (1067) Member Left is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Left = 0;
			}
			else
			{
				tmp = (float) Conversion.Val(tmptxt);
				if (tmp < DesktopLeft)
				{
					tmp = DesktopLeft;
				}
				//UPGRADE_TODO: (1067) Member width is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				if (tmp > ((float) (DesktopRight - Convert.ToDouble(HC.width))))
				{
					//UPGRADE_TODO: (1067) Member width is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					tmp = (float) (DesktopRight - Convert.ToDouble(HC.width));
				}
				//UPGRADE_TODO: (1067) Member Left is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.Left = tmp;
			}

		}

		internal static void WriteFormPosition()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member Height is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("form_height", HC.Height);
			//UPGRADE_TODO: (1067) Member Top is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("form_top", HC.Top);
			//UPGRADE_TODO: (1067) Member Left is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("form_left", HC.Left);
		}

		internal static void ResetFormPosition()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("form_dleft"));
			//UPGRADE_TODO: (1067) Member Left is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Left = Conversion.Val(tmptxt);
			//UPGRADE_TODO: (1067) Member Top is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Top = Conversion.Val(Convert.ToString(HC.oPersist.ReadIniValue("form_dtop")));
			//UPGRADE_TODO: (1067) Member Height is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.Height = Conversion.Val(Convert.ToString(HC.oPersist.ReadIniValue("form_dheight")));
			WriteFormPosition();
		}

		internal static void GetDllVer()
		{
			int dllver = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_DriverVersion();

			string tmpstr = (Convert.ToInt32((dllver & 0xF000) / 4096d) & 0xF).ToString("X") + (Convert.ToInt32((dllver & 0xF00) / 256d) & 0xF).ToString("X");
			tmpstr = tmpstr + "." + (Convert.ToInt32((dllver & 0xF0) / 16d) & 0xF).ToString("X") + (dllver & 0xF).ToString("X");
			//    tmpstr = Hex$((dllver And &HF00000) / 1048576 And &HF) + Hex$((dllver And &HF0000) / 65536 And &HF)
			gDllVer = Conversion.Val(tmpstr);

		}

		// Interceptor functions for different mount types

		internal static int EQGetMotorValues(int motor_id)
		{
			int result = 0;
			int ret = 0;

			try
			{

				ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMotorValues(motor_id);
				return ret;
			}
			catch
			{

				result = Convert.ToInt32(EQ_INVALID);
			}
			return result;
		}


		internal static int EQSetMotorValues(int motor_id, int motor_val)
		{

			int result = 0;
			int ret = 0;

			try
			{

				ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetMotorValues(motor_id, motor_val);
				return ret;
			}
			catch
			{

				result = Convert.ToInt32(EQ_INVALID);
			}
			return result;
		}

		internal static int EQStartMoveMotor(int motor_id, int hemisphere, int direction, int Steps, int stepslowdown)
		{


			int ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_StartMoveMotor(motor_id, hemisphere, direction, Steps, stepslowdown);
			return ret;

		}

		internal static void EQSetOffsets()
		{

			if (Mount.gCustomMount == 0)
			{
				// apply drift compenstation only to standard mounts
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetOffset(0, gDriftComp * -1);
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetOffset(1, 0);
			}
			else
			{
				// for customised mounts apply tracking offsets
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetOffset(0, (Tracking.gCustomTrackingOffsetRA + gDriftComp) * -1);
				EQMath.eqres = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_SetOffset(1, (Tracking.gCustomTrackingOffsetDEC) * -1);
			}

		}

		internal static string StripPath(string str)
		{
			int i = str.LastIndexOf("\\") + 1;
			return str.Substring(Math.Max(str.Length - (Strings.Len(str) - i), 0));
		}

		internal static string GetPath(string str)
		{
			int i = str.LastIndexOf("\\") + 1;
			return str.Substring(0, Math.Min(i, str.Length));
		}

		internal static string ByteArrayToString(byte[] bytArray)
		{

			//UPGRADE_WARNING: (1059) Code was upgraded to use UpgradeHelpers.Helpers.StringsHelper.ByteArrayToString() which may not have the same behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1059
			string sAns = StringsHelper.StrConv(StringsHelper.ByteArrayToString(bytArray), StringsHelper.VbStrConvEnum.VbUnicode, 0);
			int iPos = (sAns.IndexOf(Strings.Chr(0).ToString()) + 1);
			if (iPos > 0)
			{
				sAns = sAns.Substring(0, Math.Min(iPos - 1, sAns.Length));
			}
			return sAns;
		}
	}
}