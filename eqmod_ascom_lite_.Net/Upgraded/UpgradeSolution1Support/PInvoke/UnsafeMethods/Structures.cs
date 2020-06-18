using System;
using System.Runtime.InteropServices;

namespace UpgradeSolution1Support.PInvoke.UnsafeNative
{
	[System.Security.SuppressUnmanagedCodeSecurity]
	public static class Structures
	{

		[Serializable][StructLayout(LayoutKind.Sequential)]
		public struct SYSTEMTIME
		{
			public short wYear;
			public short wMonth;
			public short wDayOfWeek;
			public short wDay;
			public short wHour;
			public short wMinute;
			public short wSecond;
			public short wMilliseconds;
		}
		[Serializable][StructLayout(LayoutKind.Sequential)]
		public struct MENUITEMINFO
		{
			public int cbSize;
			public int fMask;
			public int fType;
			public int fState;
			public int wID;
			public int hSubMenu;
			public int hbmpChecked;
			public int hbmpUnchecked;
			public int dwItemData;
			public string dwTypeData;
			public int cch;
			private static void InitStruct(ref MENUITEMINFO result, bool init)
			{
				if (init)
				{
					result.dwTypeData = String.Empty;
				}
			}
			public static MENUITEMINFO CreateInstance()
			{
				MENUITEMINFO result = new MENUITEMINFO();
				InitStruct(ref result, true);
				return result;
			}
			public MENUITEMINFO Clone()
			{
				MENUITEMINFO result = this;
				InitStruct(ref result, false);
				return result;
			}
		}
	}
}