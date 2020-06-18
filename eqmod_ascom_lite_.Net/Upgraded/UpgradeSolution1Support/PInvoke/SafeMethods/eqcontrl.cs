using System;
using System.Runtime.InteropServices;

namespace UpgradeSolution1Support.PInvoke.SafeNative
{
	public static class eqcontrl
	{

		public static int EQ_DriverVersion()
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_DriverVersion();
		}
		public static int EQ_End()
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_End();
		}
		public static int EQ_GetMotorStatus(int motor_id)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_GetMotorStatus(motor_id);
		}
		public static int EQ_GetMotorValues(int motor_id)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_GetMotorValues(motor_id);
		}
		public static int EQ_GetMountStatus()
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_GetMountStatus();
		}
		public static int EQ_GetMountVersion()
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_GetMountVersion();
		}
		public static int EQ_GetTotal360microstep(int value)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_GetTotal360microstep(value);
		}
		public static int EQ_GP(int motor_id, int p_id)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_GP(motor_id, p_id);
		}
		public static int EQ_Init(ref string COMPORT, int baud, int timeout, int retry)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_Init(ref COMPORT, baud, timeout, retry);
		}
		public static int EQ_InitMotors(int RA, int DEC)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_InitMotors(RA, DEC);
		}
		public static int EQ_MotorStop(int value)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_MotorStop(value);
		}
		public static int EQ_QueryMount(int ptx, int prx, int sz)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_QueryMount(ptx, prx, sz);
		}
		public static int EQ_SendGuideRate(int motor_id, int trackrate, int guiderate, int guidedir, int hemisphere, int direction)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_SendGuideRate(motor_id, trackrate, guiderate, guidedir, hemisphere, direction);
		}
		public static int EQ_SetAutoguiderPortRate(int motor_id, int guideportrate)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_SetAutoguiderPortRate(motor_id, guideportrate);
		}
		public static int EQ_SetCustomTrackRate(int motor_id, int trackmode, int trackoffset, int trackbase, int hemisphere, int direction)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_SetCustomTrackRate(motor_id, trackmode, trackoffset, trackbase, hemisphere, direction);
		}
		public static int EQ_SetMotorValues(int motor_id, int motor_val)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_SetMotorValues(motor_id, motor_val);
		}
		public static int EQ_SetOffset(int motor_id, int doffset)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_SetOffset(motor_id, doffset);
		}
		public static int EQ_Slew(int motor_id, int hemisphere, int direction, int rate)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_Slew(motor_id, hemisphere, direction, rate);
		}
		public static int EQ_StartMoveMotor(int motor_id, int hemisphere, int direction, int Steps, int stepslowdown)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_StartMoveMotor(motor_id, hemisphere, direction, Steps, stepslowdown);
		}
		public static int EQ_StartRATrack(int trackrate, int hemisphere, int direction)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_StartRATrack(trackrate, hemisphere, direction);
		}
		public static int EQ_WP(int motor_id, int p_id, int value)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.eqcontrl.EQ_WP(motor_id, p_id, value);
		}
	}
}