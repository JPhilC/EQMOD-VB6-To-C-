using System;
using System.Runtime.InteropServices;

namespace UpgradeSolution1Support.PInvoke.SafeNative
{
	public static class user32
	{

		public static int GetForegroundWindow()
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.GetForegroundWindow();
		}
		public static int GetMenuItemInfo(int hMenu, int un, bool b, ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO lpMenuItemInfo)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.GetMenuItemInfo(hMenu, un, b, ref lpMenuItemInfo);
		}
		public static int GetSystemMenu(int hwnd, int bRevert)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.GetSystemMenu(hwnd, bRevert);
		}
		public static int GetSystemMetrics(int nIndex)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.GetSystemMetrics(nIndex);
		}
		public static int IsWindow(int hwnd)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.IsWindow(hwnd);
		}
		public static int PostMessage(int hwnd, int wMsg, int wParam, int lParam)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.PostMessage(hwnd, wMsg, wParam, lParam);
		}
		public static int SendMessage(int hwnd, int wMsg, int wParam, ref int lParam)
		{
			int result = 0;
			GCHandle handle = GCHandle.Alloc(lParam, GCHandleType.Pinned);
			try
			{
				IntPtr tmpPtr = handle.AddrOfPinnedObject();
				result = UpgradeSolution1Support.PInvoke.UnsafeNative.user32.SendMessage(hwnd, wMsg, wParam, tmpPtr);
				lParam = Marshal.ReadInt16(tmpPtr);
			}
			finally
			{
				handle.Free();
			}
			return result;
		}
		public static int SetMenuItemInfo(int hMenu, int un, bool bool_Renamed, ref UpgradeSolution1Support.PInvoke.UnsafeNative.Structures.MENUITEMINFO lpcMenuItemInfo)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.SetMenuItemInfo(hMenu, un, bool_Renamed, ref lpcMenuItemInfo);
		}
		public static int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags)
		{
			return UpgradeSolution1Support.PInvoke.UnsafeNative.user32.SetWindowPos(hwnd, hWndInsertAfter, x, Y, cx, cy, wFlags);
		}
		public static bool SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, int fuWinIni)
		{
			bool result = false;
			GCHandle handle = GCHandle.Alloc(lpvParam, GCHandleType.Pinned);
			try
			{
				IntPtr tmpPtr = handle.AddrOfPinnedObject();
				//UPGRADE_WARNING: (8007) Trying to marshal a non Bittable Type (bool). A special conversion might be required at this point. Moreover use 'External Marshalling attributes for Structs' feature enabled if required More Information: https://www.mobilize.net/vbtonet/ewis/ewi8007
				result = UpgradeSolution1Support.PInvoke.UnsafeNative.user32.SystemParametersInfo(uAction, uParam, tmpPtr, fuWinIni);
			}
			finally
			{
				handle.Free();
			}
			return result;
		}
	}
}