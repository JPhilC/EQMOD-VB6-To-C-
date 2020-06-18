using System;
using System.Runtime.InteropServices;

namespace Project1
{
	internal static class EQCONTRL
	{





		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_Init([MarshalAs(UnmanagedType.VBByRefStr)] ref string COMPORT, int baud, int timeout, int retry);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_End();


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_InitMotors(int RA, int DEC);


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_GetMotorValues(int motor_id);


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_GetMotorStatus(int motor_id);


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SetMotorValues(int motor_id, int motor_val);


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_StartMoveMotor(int motor_id, int hemisphere, int direction, int Steps, int stepslowdown);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_Slew(int motor_id, int hemisphere, int direction, int rate);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_StartRATrack(int trackrate, int hemisphere, int direction);


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SendGuideRate(int motor_id, int trackrate, int guiderate, int guidedir, int hemisphere, int direction);




		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SendCustomTrackRate(int motor_id, int trackrate, int trackoffset, int trackdir, int hemisphere, int direction);



		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SetCustomTrackRate(int motor_id, int trackmode, int trackoffset, int trackbase, int hemisphere, int direction);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_MotorStop(int value);



		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SetAutoguiderPortRate(int motor_id, int guideportrate);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_GetTotal360microstep(int value);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_GetMountVersion();

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_GetMountStatus();

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_DriverVersion();

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_GP(int motor_id, int p_id);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_WP(int motor_id, int p_id, int value);


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SetOffset(int motor_id, int doffset);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SetMountType(int motor_type);


		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_WriteByte(byte bData);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SendMountCommand(int motor_id, byte command, int params_Renamed, int Count);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_QueryMount(int ptx, int prx, int sz);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_DebugLog([MarshalAs(UnmanagedType.VBByRefStr)] ref string FileName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string comment, int operation);

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int EQ_SetAxisRate(int motor_id, double rate, ref int hemisphere, ref int direction);


		internal static int EQ_GetMountFeatures()
		{

			int res = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GP(0, 10009);

			if (res != 999)
			{
				return res;
			}
			else
			{
				return 0;
			}

		}
	}
}