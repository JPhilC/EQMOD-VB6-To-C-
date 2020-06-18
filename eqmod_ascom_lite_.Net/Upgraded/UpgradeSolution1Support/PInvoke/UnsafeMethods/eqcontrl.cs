using System;
using System.Runtime.InteropServices;

namespace UpgradeSolution1Support.PInvoke.UnsafeNative
{
	[System.Security.SuppressUnmanagedCodeSecurity]
	public static class eqcontrl
	{

		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_DebugLog([MarshalAs(UnmanagedType.VBByRefStr)] ref string FileName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string comment, int operation);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_DriverVersion();
		[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_End();
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_GetMotorStatus(int motor_id);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_GetMotorValues(int motor_id);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_GetMountStatus();
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_GetMountVersion();
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_GetTotal360microstep(int value);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_GP(int motor_id, int p_id);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_Init([MarshalAs(UnmanagedType.VBByRefStr)] ref string COMPORT, int baud, int timeout, int retry);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_InitMotors(int RA, int DEC);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_MotorStop(int value);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_QueryMount(int ptx, int prx, int sz);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SendCustomTrackRate(int motor_id, int trackrate, int trackoffset, int trackdir, int hemisphere, int direction);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SendGuideRate(int motor_id, int trackrate, int guiderate, int guidedir, int hemisphere, int direction);
		[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SendMountCommand(int motor_id, byte command, int params_Renamed, int Count);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SetAutoguiderPortRate(int motor_id, int guideportrate);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SetAxisRate(int motor_id, double rate, ref int hemisphere, ref int direction);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SetCustomTrackRate(int motor_id, int trackmode, int trackoffset, int trackbase, int hemisphere, int direction);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SetMotorValues(int motor_id, int motor_val);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SetMountType(int motor_type);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_SetOffset(int motor_id, int doffset);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_Slew(int motor_id, int hemisphere, int direction, int rate);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_StartMoveMotor(int motor_id, int hemisphere, int direction, int Steps, int stepslowdown);
		[DllImport("EQCONTRL.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_StartRATrack(int trackrate, int hemisphere, int direction);
		[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_WP(int motor_id, int p_id, int value);
		[DllImport("EQContrl.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int EQ_WriteByte(byte bData);
	}
}