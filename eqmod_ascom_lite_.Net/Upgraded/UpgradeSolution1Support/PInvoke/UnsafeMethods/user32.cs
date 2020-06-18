using System;
using System.Runtime.InteropServices;

namespace UpgradeSolution1Support.PInvoke.UnsafeNative
{
	[System.Security.SuppressUnmanagedCodeSecurity]
	public static class user32
	{

		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int FindWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpWindowName);
		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int GetForegroundWindow();
		[DllImport("user32.dll", EntryPoint = "GetMenuItemInfoA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int GetMenuItemInfo(int hMenu, int un, bool b, ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO lpMenuItemInfo);
		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int GetSystemMenu(int hwnd, int bRevert);
		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int GetSystemMetrics(int nIndex);
		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int IsWindow(int hwnd);
		[DllImport("user32.dll", EntryPoint = "PostMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int PostMessage(int hwnd, int wMsg, int wParam, int lParam);
		[DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int SendMessage(int hwnd, int wMsg, int wParam, System.IntPtr lParam);
		//UPGRADE_TODO: (1050) Structure MENUITEMINFO may require marshalling attributes to be passed as an argument in this Declare statement. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1050
		[DllImport("user32.dll", EntryPoint = "SetMenuItemInfoA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int SetMenuItemInfo(int hMenu, int un, bool bool_Renamed, ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO lpcMenuItemInfo);
		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
		[DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		extern public static bool SystemParametersInfo(int uAction, int uParam, System.IntPtr lpvParam, int fuWinIni);
	}
}