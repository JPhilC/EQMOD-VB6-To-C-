using System;
using System.Runtime.InteropServices;

namespace Project1
{
	internal static class Monitor
	{


		//Turning the Computer Screen-Monitor on and off

		//To switch a monitor on/off or to standby, use the following code. Note, this may
		//not work on some NT machines.

		public static int gMonitorState = 0;
		public static int gMonitorMode = 0;

		private const int SC_MONITORPOWER = 0xF170;
		private const int SPI_GETSCREENSAVEACTIVE = 0x10;
		private const int SPI_GETSCREENSAVERRUNNING = 0x72;

		const int WM_CLOSE = 0x10;
		const int WM_SYSCOMMAND = 0x112;
		const int SC_SCREENSAVE = 0xF140;
		const int HWND_BROADCAST = 65535;
		const int MONITOR_ON = -1;
		const int MONITOR_OFF = 2;

		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static bool SystemParametersInfo(int uAction, int uParam, System.IntPtr lpvParam, int fuWinIni);
		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int GetForegroundWindow();
		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int FindWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpWindowName);
		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int SendMessage(int hwnd, int wMsg, int wParam, System.IntPtr lParam);
		//UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
		//[DllImport("user32.dll", EntryPoint = "PostMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//extern public static int PostMessage(int hwnd, int wMsg, int wParam, int lParam);


		internal static void ToggleMonitorPower()
		{
			object[] EQ_Beep = null;
			object HC = null;

			switch(gMonitorMode)
			{
				case 0 : 
					// power on/off 
					if (gMonitorState == 0)
					{
						UpgradeSolution1Support.PInvoke.SafeNative.user32.PostMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_ON);
						gMonitorState = 1;
						object tempAuxVar = EQ_Beep[35];
					}
					else
					{
						UpgradeSolution1Support.PInvoke.SafeNative.user32.PostMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF);
						gMonitorState = 0;
						object tempAuxVar2 = EQ_Beep[36];
					} 
					 
					break;
				case 1 : 
					// screenscaver on/off 
					if (IsScreenSaverActivated())
					{
						// close screensaver
						int tempRefParam = 0;
						UpgradeSolution1Support.PInvoke.SafeNative.user32.SendMessage(UpgradeSolution1Support.PInvoke.SafeNative.user32.GetForegroundWindow(), WM_CLOSE, 0, ref tempRefParam);
						object tempAuxVar3 = EQ_Beep[36];
					}
					else
					{
						// start screensaver
						//UPGRADE_TODO: (1067) Member hwnd is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
						int tempRefParam2 = 1;
						UpgradeSolution1Support.PInvoke.SafeNative.user32.SendMessage(Convert.ToInt32(HC.hwnd), WM_SYSCOMMAND, SC_SCREENSAVE, ref tempRefParam2);
						object tempAuxVar4 = EQ_Beep[35];
					} 
					 
					break;
			}
		}

		internal static bool IsScreenSaverActivated()
		{
			bool a = false;
			UpgradeSolution1Support.PInvoke.SafeNative.user32.SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0, ref a, 0);
			return a;
		}
	}
}