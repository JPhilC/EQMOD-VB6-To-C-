using Microsoft.VisualBasic;
using System;
using UpgradeHelpers.Helpers;

namespace Project1
{
	internal static class Mount
	{



		[Serializable]
		public struct MountDefn
		{
			public string TotalSteps;
			public string wormsteps;
			public string offset;
			public static MountDefn CreateInstance()
			{
				MountDefn result = new MountDefn();
				result.TotalSteps = String.Empty;
				result.wormsteps = String.Empty;
				result.offset = String.Empty;
				return result;
			}
		}

		public static int gCustomMount = 0;
		public static int gCustomRA360 = 0;
		public static int gCustomDEC360 = 0;
		public static double gCustomRAWormSteps = 0;
		public static double gCustomDECWormSteps = 0;
		public static int gMountType = 0;

		internal static int CheckMount(int openstat)
		{
			EQMath.gTot_step = EQGetTotal360microstep(0);
			Goto.gRAMeridianWest = EQMath.gRAEncoder_Zero_pos + EQMath.gTot_step / 4d;
			Goto.gRAMeridianEast = EQMath.gRAEncoder_Zero_pos - EQMath.gTot_step / 4d;
			EQMath.gDECEncoder_Home_pos = EQGetTotal360microstep(1) / 4d + EQMath.gDECEncoder_Zero_pos; // totstep/4 + Homepos
			EQMath.gEQ_MAXSYNC = EQMath.gTot_step / 16d; // totalstep /16 = 22.5 degree field
			return Convert.ToInt32(Common.EQ_OK);
		}

		internal static void readCustomMount()
		{
			object HC = null;

			int i = 0;

			int NF1 = FileSystem.FreeFile();

			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{
				FileSystem.FileClose(NF1);
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				FileSystem.FileOpen(NF1, (Convert.ToDouble(HC.oPersist.GetIniPath()) + Double.Parse("\\mountparams.txt")).ToString(), OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
				for (i = 10000; i <= 10007; i++)
				{
					FileSystem.PrintLine(NF1, "0," + i.ToString() + ":" + Common.EQGP(0, i).ToString());
					FileSystem.PrintLine(NF1, "1," + i.ToString() + ":" + Common.EQGP(1, i).ToString());
				}
				FileSystem.FileClose(NF1);

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_MOUNT"));
				if (tmptxt != "")
				{
					gCustomMount = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					gCustomMount = 0;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("CUSTOM_MOUNT", "0");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_RA_STEPS_360"));
				if (tmptxt != "")
				{
					gCustomRA360 = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					gCustomRA360 = 9024000;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("CUSTOM_RA_STEPS_360", "9024000");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_DEC_STEPS_360"));
				if (tmptxt != "")
				{
					gCustomDEC360 = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					gCustomDEC360 = 9024000;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("CUSTOM_DEC_STEPS_360", "9024000");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_RA_STEPS_WORM"));
				if (tmptxt != "")
				{
					gCustomRAWormSteps = Conversion.Val(tmptxt);
				}
				else
				{
					gCustomRAWormSteps = 50133;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("CUSTOM_RA_STEPS_WORM", "50133");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_DEC_STEPS_WORM"));
				if (tmptxt != "")
				{
					gCustomDECWormSteps = Conversion.Val(tmptxt);
				}
				else
				{
					gCustomDECWormSteps = 50133;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("CUSTOM_DEC_STEPS_WORM", "50133");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_TRACKING_OFFSET_RA"));
				if (tmptxt != "")
				{
					Tracking.gCustomTrackingOffsetRA = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					Tracking.gCustomTrackingOffsetRA = 0;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("CUSTOM_TRACKING_OFFSET_RA", "0");
				}

				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("CUSTOM_TRACKING_OFFSET_DEC"));
				if (tmptxt != "")
				{
					Tracking.gCustomTrackingOffsetDEC = Convert.ToInt32(Conversion.Val(tmptxt));
				}
				else
				{
					Tracking.gCustomTrackingOffsetDEC = 0;
					//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.oPersist.WriteIniValue("CUSTOM_TRACKING_OFFSET_DEC", "0");
				}

				Common.EQSetOffsets();
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}
		internal static void writeCustomMount()
		{
			object HC = null;
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_MOUNT", gCustomMount.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_RA_STEPS_360", gCustomRA360.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_DEC_STEPS_360", gCustomDEC360.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_RA_STEPS_WORM", gCustomRAWormSteps.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_DEC_STEPS_WORM", gCustomDECWormSteps.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_TRACKING_OFFSET_RA", Tracking.gCustomTrackingOffsetRA.ToString());
			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.oPersist.WriteIniValue("CUSTOM_TRACKING_OFFSET_DEC", Tracking.gCustomTrackingOffsetDEC.ToString());
		}

		internal static string readMountType2()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("SIM_MOUNT_TYPE"));
			if (tmptxt == "")
			{
				tmptxt = "EQ6PRO";
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("SIM_MOUNT_TYPE", tmptxt);
			}

			return tmptxt;
		}

		internal static void readWormSteps()
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			string tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("RA_STEPS_PER_WORM"));
			if (tmptxt != "")
			{
				EQMath.gRAWormSteps = Conversion.Val(tmptxt);
			}
			else
			{
				EQMath.gRAWormSteps = 50133;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("RA_STEPS_PER_WORM", EQMath.gRAWormSteps.ToString());
			}

			//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			tmptxt = Convert.ToString(HC.oPersist.ReadIniValue("DEC_STEPS_PER_WORM"));
			if (tmptxt != "")
			{
				EQMath.gDECWormSteps = Conversion.Val(tmptxt);
			}
			else
			{
				EQMath.gDECWormSteps = 50133;
				//UPGRADE_TODO: (1067) Member oPersist is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.oPersist.WriteIniValue("DEC_STEPS_PER_WORM", EQMath.gDECWormSteps.ToString());
			}

		}

		// Function name    : EQGetTotal360microstep()
		// Description      : Get RA/DEC Motor Total 360 degree microstep counts
		// Return type      : Double - Stepper Counter Values
		//                     0 - 16777215  Valid Count Values
		//                     0x1000000 - Mount Not available
		//                     0x3000000 - Invalid Parameter
		// Argument         : DOUBLE motor_id
		//                     00 - RA Motor
		//                     01 - DEC Motor
		//
		internal static int EQGetTotal360microstep(int motor_id)
		{
			int ret = 0;

			if (gCustomMount == 1)
			{
				switch(motor_id)
				{
					case 0 : 
						ret = gCustomRA360; 
						break;
					case 1 : 
						ret = gCustomDEC360; 
						break;
					default:
						ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetTotal360microstep(motor_id); 
						break;
				}
			}
			else
			{
				ret = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetTotal360microstep(motor_id);
			}
			return ret;
		}
	}
}