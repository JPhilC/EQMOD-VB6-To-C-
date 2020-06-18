using System;
using System.Runtime.InteropServices;

namespace UpgradeSolution1Support.PInvoke.SafeNative
{
	public static class kernel32
	{

		public static void GetSystemTime(ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME lpSystemTime)
		{
			UpgradeSolution1Support.PInvoke.UnsafeNative.kernel32.GetSystemTime(ref lpSystemTime);
		}
	}
}