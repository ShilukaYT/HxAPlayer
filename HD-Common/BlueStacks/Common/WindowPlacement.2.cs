using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200017F RID: 383
	public static class WindowPlacement
	{
		// Token: 0x06000E85 RID: 3717
		[DllImport("user32.dll")]
		private static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

		// Token: 0x06000E86 RID: 3718
		[DllImport("user32.dll")]
		private static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

		// Token: 0x06000E87 RID: 3719
		[DllImport("user32.dll")]
		public static extern IntPtr MonitorFromRect([In] ref RECT lprc, uint dwFlags);

		// Token: 0x06000E88 RID: 3720
		[DllImport("user32.dll")]
		private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

		// Token: 0x06000E89 RID: 3721 RVA: 0x00039320 File Offset: 0x00037520
		private static RECT PlaceOnScreen(RECT monitorRect, RECT windowRect)
		{
			int num = monitorRect.Right - monitorRect.Left;
			int num2 = monitorRect.Bottom - monitorRect.Top;
			if (windowRect.Left < monitorRect.Left)
			{
				int num3 = windowRect.Right - windowRect.Left;
				if (num3 > num)
				{
					num3 = num;
				}
				windowRect.Left = monitorRect.Left;
				windowRect.Right = windowRect.Left + num3;
			}
			else if (windowRect.Right > monitorRect.Right)
			{
				int num4 = windowRect.Right - windowRect.Left;
				if (num4 > num)
				{
					num4 = num;
				}
				windowRect.Right = monitorRect.Right;
				windowRect.Left = windowRect.Right - num4;
			}
			if (windowRect.Top < monitorRect.Top)
			{
				int num5 = windowRect.Bottom - windowRect.Top;
				if (num5 > num2)
				{
					num5 = num2;
				}
				windowRect.Top = monitorRect.Top;
				windowRect.Bottom = windowRect.Top + num5;
			}
			else if (windowRect.Bottom > monitorRect.Bottom)
			{
				int num6 = windowRect.Bottom - windowRect.Top;
				if (num6 > num2)
				{
					num6 = num2;
				}
				windowRect.Bottom = monitorRect.Bottom;
				windowRect.Top = windowRect.Bottom - num6;
			}
			return windowRect;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003946C File Offset: 0x0003766C
		private static RECT PlaceOnScreenIfEntirelyOutside(RECT monitorRect, RECT windowRect)
		{
			int num = monitorRect.Right - monitorRect.Left;
			int num2 = monitorRect.Bottom - monitorRect.Top;
			if (windowRect.Right < monitorRect.Left)
			{
				int num3 = windowRect.Right - windowRect.Left;
				if (num3 > num)
				{
					num3 = num;
				}
				windowRect.Left = monitorRect.Left;
				windowRect.Right = windowRect.Left + num3;
			}
			else if (windowRect.Left > monitorRect.Right)
			{
				int num4 = windowRect.Right - windowRect.Left;
				if (num4 > num)
				{
					num4 = num;
				}
				windowRect.Right = monitorRect.Right;
				windowRect.Left = windowRect.Right - num4;
			}
			if (windowRect.Bottom < monitorRect.Top)
			{
				int num5 = windowRect.Bottom - windowRect.Top;
				if (num5 > num2)
				{
					num5 = num2;
				}
				windowRect.Top = monitorRect.Top;
				windowRect.Bottom = windowRect.Top + num5;
			}
			else if (windowRect.Top > monitorRect.Bottom)
			{
				int num6 = windowRect.Bottom - windowRect.Top;
				if (num6 > num2)
				{
					num6 = num2;
				}
				windowRect.Bottom = monitorRect.Bottom;
				windowRect.Top = windowRect.Bottom - num6;
			}
			return windowRect;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000395B8 File Offset: 0x000377B8
		private static bool RectangleEntirelyInside(RECT parent, RECT child)
		{
			return child.Left >= parent.Left && child.Right <= parent.Right && child.Top <= parent.Top && child.Bottom >= parent.Bottom;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00039608 File Offset: 0x00037808
		private static bool RectanglesIntersect(RECT a, RECT b)
		{
			return a.Left <= b.Right && a.Right >= b.Left && a.Top <= b.Bottom && a.Bottom >= b.Top;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003965C File Offset: 0x0003785C
		public static void SetPlacement(IntPtr windowHandle, RECT placementRect)
		{
			try
			{
				WINDOWPLACEMENT windowplacement;
				using (XmlReader xmlReader = XmlReader.Create(new MemoryStream(WindowPlacement.encoding.GetBytes(WindowPlacement.GetPlacement(windowHandle)))))
				{
					windowplacement = (WINDOWPLACEMENT)WindowPlacement.serializer.Deserialize(xmlReader);
				}
				windowplacement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
				windowplacement.flags = 0;
				windowplacement.showCmd = ((windowplacement.showCmd == 2) ? 1 : windowplacement.showCmd);
				IntPtr hMonitor = WindowPlacement.MonitorFromRect(ref placementRect, 2U);
				MONITORINFO monitorinfo = new MONITORINFO
				{
					cbSize = Marshal.SizeOf(typeof(MONITORINFO))
				};
				if (WindowPlacement.GetMonitorInfo(hMonitor, ref monitorinfo) && !WindowPlacement.RectangleEntirelyInside(monitorinfo.rcMonitor, placementRect))
				{
					windowplacement.normalPosition = WindowPlacement.PlaceOnScreen(monitorinfo.rcMonitor, placementRect);
				}
				WindowPlacement.SetWindowPlacement(windowHandle, ref windowplacement);
			}
			catch (Exception ex)
			{
				Logger.Warning("Exception in SetPlacement. Exception: " + ex.ToString());
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00039770 File Offset: 0x00037970
		public static Size GetMaxWidthAndHeightOfMonitor(IntPtr handle)
		{
			Screen screen = Screen.FromHandle(handle);
			return new Size((double)screen.Bounds.Width, (double)screen.Bounds.Height);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000397A8 File Offset: 0x000379A8
		public static void SetPlacement(IntPtr windowHandle, string placementXml)
		{
			if (string.IsNullOrEmpty(placementXml))
			{
				return;
			}
			byte[] bytes = WindowPlacement.encoding.GetBytes(placementXml);
			try
			{
				WINDOWPLACEMENT windowplacement;
				using (XmlReader xmlReader = XmlReader.Create(new MemoryStream(bytes)))
				{
					windowplacement = (WINDOWPLACEMENT)WindowPlacement.serializer.Deserialize(xmlReader);
				}
				windowplacement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
				windowplacement.flags = 0;
				windowplacement.showCmd = ((windowplacement.showCmd == 2) ? 1 : windowplacement.showCmd);
				RECT normalPosition = windowplacement.normalPosition;
				IntPtr hMonitor = WindowPlacement.MonitorFromRect(ref normalPosition, 2U);
				MONITORINFO monitorinfo = new MONITORINFO
				{
					cbSize = Marshal.SizeOf(typeof(MONITORINFO))
				};
				if (WindowPlacement.GetMonitorInfo(hMonitor, ref monitorinfo) && !WindowPlacement.RectangleEntirelyInside(monitorinfo.rcMonitor, windowplacement.normalPosition))
				{
					windowplacement.normalPosition = WindowPlacement.PlaceOnScreen(monitorinfo.rcMonitor, windowplacement.normalPosition);
				}
				WindowPlacement.SetWindowPlacement(windowHandle, ref windowplacement);
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000398C4 File Offset: 0x00037AC4
		public static string GetPlacement(IntPtr windowHandle)
		{
			WINDOWPLACEMENT windowplacement = default(WINDOWPLACEMENT);
			WindowPlacement.GetWindowPlacement(windowHandle, out windowplacement);
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
				{
					WindowPlacement.serializer.Serialize(xmlTextWriter, windowplacement);
					byte[] bytes = memoryStream.ToArray();
					@string = WindowPlacement.encoding.GetString(bytes);
				}
			}
			return @string;
		}

		// Token: 0x040006C3 RID: 1731
		private static Encoding encoding = new UTF8Encoding();

		// Token: 0x040006C4 RID: 1732
		private static XmlSerializer serializer = new XmlSerializer(typeof(WINDOWPLACEMENT));

		// Token: 0x040006C5 RID: 1733
		private const uint MONITOR_DEFAULTTONEAREST = 2U;

		// Token: 0x040006C6 RID: 1734
		private const int SW_SHOWNORMAL = 1;

		// Token: 0x040006C7 RID: 1735
		private const int SW_SHOWMINIMIZED = 2;
	}
}
