using System;
using System.Runtime.InteropServices;

namespace UpgradeSolution1Support.PInvoke.UnsafeNative
{
	[System.Security.SuppressUnmanagedCodeSecurity]
	public static class kernel32
	{

		//UPGRADE_TODO: (1050) Structure SYSTEMTIME may require marshalling attributes to be passed as an argument in this Declare statement. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1050
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static void GetSystemTime(ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.SYSTEMTIME lpSystemTime);
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int GetTickCount();
	}
}