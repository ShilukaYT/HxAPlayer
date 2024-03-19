using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x020000D7 RID: 215
	public partial class NotificationWindow : Window
	{
		// Token: 0x06000556 RID: 1366
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

		// Token: 0x06000557 RID: 1367 RVA: 0x0001AD3C File Offset: 0x00018F3C
		public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
		{
			IntPtr intPtr = IntPtr.Zero;
			NotificationWindow.SetLastError(0);
			int lastWin32Error;
			if (IntPtr.Size == 4)
			{
				int value = NotificationWindow.IntSetWindowLong(hWnd, nIndex, NotificationWindow.IntPtrToInt32(dwNewLong));
				lastWin32Error = Marshal.GetLastWin32Error();
				intPtr = new IntPtr(value);
			}
			else
			{
				intPtr = NotificationWindow.IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
				lastWin32Error = Marshal.GetLastWin32Error();
			}
			if (intPtr == IntPtr.Zero && lastWin32Error != 0)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return intPtr;
		}

		// Token: 0x06000558 RID: 1368
		[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
		private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x06000559 RID: 1369
		[DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
		private static extern int IntSetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		// Token: 0x0600055A RID: 1370 RVA: 0x00005160 File Offset: 0x00003360
		private static int IntPtrToInt32(IntPtr intPtr)
		{
			return (int)intPtr.ToInt64();
		}

		// Token: 0x0600055B RID: 1371
		[DllImport("kernel32.dll")]
		public static extern void SetLastError(int dwErrorCode);

		// Token: 0x0600055C RID: 1372 RVA: 0x00003C8E File Offset: 0x00001E8E
		private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000516A File Offset: 0x0000336A
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00005172 File Offset: 0x00003372
		public Dictionary<string, bool> IsOverrideDesktopNotificationSettingsDict { get; set; } = new Dictionary<string, bool>();

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000517B File Offset: 0x0000337B
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00005183 File Offset: 0x00003383
		public Dictionary<string, Dictionary<string, int>> AppNotificationCountDictForEachVM { get; set; } = new Dictionary<string, Dictionary<string, int>>();

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000518C File Offset: 0x0000338C
		public static NotificationWindow Instance
		{
			get
			{
				if (NotificationWindow.mInstance == null)
				{
					NotificationWindow.Init();
				}
				return NotificationWindow.mInstance;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000519F File Offset: 0x0000339F
		public static void Init()
		{
			NotificationWindow.mInstance = new NotificationWindow();
			SystemEvents.DisplaySettingsChanged -= NotificationWindow.mInstance.HandleDisplaySettingsChanged;
			SystemEvents.DisplaySettingsChanged += NotificationWindow.mInstance.HandleDisplaySettingsChanged;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000051D5 File Offset: 0x000033D5
		private NotificationWindow()
		{
			this.InitializeComponent();
			this.SetWindowPosition();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00005211 File Offset: 0x00003411
		public void HandleDisplaySettingsChanged(object _1, EventArgs _2)
		{
			this.SetWindowPosition();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		public void AddAlert(string imagePath, string title, string displayMsg, bool autoClose, int duration, MouseButtonEventHandler clickHandler, bool hideMute, string vmName, bool isCloudNotification, string package = "", bool isForceNotification = false, string id = "0", bool showOnlySettings = false)
		{
			if (this.mIsPopupsEnabled)
			{
				base.Dispatcher.Invoke(new Action(delegate()
				{
					MuteState muteState = NotificationManager.Instance.IsShowNotificationForKey(title, vmName);
					if (((muteState == MuteState.NotMuted || muteState == MuteState.AutoHide) | isForceNotification) || NotificationManager.Instance.IsDesktopNotificationToBeShown(title))
					{
						if (this.Visibility == Visibility.Collapsed || this.Visibility == Visibility.Hidden)
						{
							this.Show();
						}
						string key = (string.IsNullOrEmpty(package) ? title : package).ToUpper(CultureInfo.InvariantCulture);
						if (this.mDictPopups.ContainsKey(key))
						{
							this.RemovePopup(this.mDictPopups[key]);
						}
						if (this.mDictPopups.Count >= 3)
						{
							this.RemovePopup((NotificationPopup)this.mStackPanel.Children[2]);
						}
						if (!isCloudNotification & isForceNotification)
						{
							autoClose = true;
							duration = 5000;
						}
						NotificationPopup notificationPopup = NotificationPopup.InitPopup(imagePath, title, displayMsg, autoClose, duration, clickHandler, hideMute, vmName, null, null, null, false, string.Empty, id, showOnlySettings, package);
						this.mStackPanel.Children.Insert(0, notificationPopup);
						this.mDictPopups.Add(key, notificationPopup);
					}
				}), new object[0]);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001AE4C File Offset: 0x0001904C
		public void ForceShowAlert(string imagePath, string title, string displayMsg, bool autoClose, int duration, MouseButtonEventHandler clickHandler, bool hideMute, string vmName, MouseButtonEventHandler buttonClickHandler = null, MouseButtonEventHandler closeButtonHandler = null, MouseButtonEventHandler muteButtonHandler = null, bool showOnlyMute = false, string buttonText = "", string id = "0", bool showOnlySettings = false)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				NotificationPopup notificationPopup = NotificationPopup.InitPopup(imagePath, title, displayMsg, autoClose, duration, clickHandler, hideMute, vmName, buttonClickHandler, closeButtonHandler, muteButtonHandler, showOnlyMute, buttonText, id, showOnlySettings, "");
				this.mStackPanel.Children.Insert(0, notificationPopup);
				if (!this.mDictPopups.ContainsKey(title.ToUpper(CultureInfo.InvariantCulture)))
				{
					this.mDictPopups.Add(title.ToUpper(CultureInfo.InvariantCulture), notificationPopup);
				}
				this.Topmost = false;
			}), new object[0]);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001AEFC File Offset: 0x000190FC
		internal void RemovePopup(NotificationPopup popup)
		{
			string key = (string.IsNullOrEmpty(popup.PackageName) ? popup.Title : popup.PackageName).ToUpper(CultureInfo.InvariantCulture);
			this.mDictPopups.Remove(key);
			popup.mPopup.IsOpen = false;
			if (this.mStackPanel.Children.Contains(popup))
			{
				popup.StopTimer();
				this.mStackPanel.Children.Remove(popup);
			}
			if (this.mStackPanel.Children.Count >= 3)
			{
				(this.mStackPanel.Children[2] as NotificationPopup).StopTimer();
				this.mStackPanel.Children.RemoveAt(2);
			}
			foreach (object obj in this.mStackPanel.Children)
			{
				NotificationPopup notificationPopup = (NotificationPopup)obj;
				if (string.Equals(notificationPopup.Title, popup.Title, StringComparison.InvariantCultureIgnoreCase))
				{
					notificationPopup.StopTimer();
					this.mStackPanel.Children.Remove(notificationPopup);
					break;
				}
			}
			if (this.mDictPopups.Count == 0)
			{
				base.Hide();
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001B03C File Offset: 0x0001923C
		public void EnablePopups(bool visible)
		{
			if (visible)
			{
				this.mIsPopupsEnabled = true;
				return;
			}
			this.mIsPopupsEnabled = false;
			foreach (NotificationPopup popup in this.mDictPopups.Values.ToArray<NotificationPopup>())
			{
				this.RemovePopup(popup);
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001B088 File Offset: 0x00019288
		private static void GamepadNotificationButtonClick(object sender, RoutedEventArgs e)
		{
			string text = WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				WebHelper.GetServerHost(),
				"help_articles"
			}), null, null, null) + "&article=gamepad_connected_notif_help";
			Logger.Info("Launching browser with URL: {0}", new object[]
			{
				text
			});
			Process.Start(text);
			Stats.SendCommonClientStatsAsync("notification_gamepad", "notification_gamepad_click", "Android", "", "", "", "");
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001B110 File Offset: 0x00019310
		public void ShowSimplePopupForClient(string title, string description)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.ShowInTaskbar = true;
				this.Visibility = Visibility.Visible;
				this.ForceShowAlert(Path.Combine(RegistryManager.Instance.InstallDir, "default_icon.png"), title, description, true, 15000, new MouseButtonEventHandler(NotificationWindow.GamepadNotificationButtonClick), true, "Android", null, null, null, false, "", "0", false);
				Stats.SendCommonClientStatsAsync("notification_gamepad", "notification_gamepad_impression", "Android", "", "", "", "");
			}), new object[0]);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00005211 File Offset: 0x00003411
		private void Window_IsVisibleChanged(object _1, DependencyPropertyChangedEventArgs _2)
		{
			this.SetWindowPosition();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001B158 File Offset: 0x00019358
		private void SetWindowPosition()
		{
			try
			{
				base.Height = SystemParameters.WorkArea.Height;
				base.Width = SystemParameters.FullPrimaryScreenWidth * 0.2;
				base.Left = SystemParameters.FullPrimaryScreenWidth - base.Width;
				base.Top = 0.0;
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in HandleDisplaySettingsChanged. Exception: " + ex.ToString());
			}
		}

		// Token: 0x0400027C RID: 636
		private const int MAX_ALLOWED_NOTIFICATION = 3;

		// Token: 0x0400027D RID: 637
		private Dictionary<string, NotificationPopup> mDictPopups = new Dictionary<string, NotificationPopup>();

		// Token: 0x0400027E RID: 638
		private bool mIsPopupsEnabled = true;

		// Token: 0x04000281 RID: 641
		private static NotificationWindow mInstance;

		// Token: 0x020000D8 RID: 216
		[Flags]
		public enum ExtendedWindowStyles
		{
			// Token: 0x04000285 RID: 645
			WS_EX_TOOLWINDOW = 128
		}

		// Token: 0x020000D9 RID: 217
		public enum GetWindowLongFields
		{
			// Token: 0x04000287 RID: 647
			GWL_EXSTYLE = -20
		}
	}
}
