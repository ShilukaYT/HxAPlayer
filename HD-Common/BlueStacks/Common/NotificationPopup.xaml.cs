using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x020000D6 RID: 214
	public partial class NotificationPopup : System.Windows.Controls.UserControl, IDisposable
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00004F34 File Offset: 0x00003134
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00004F3C File Offset: 0x0000313C
		public string Title
		{
			get
			{
				return this.mTitle;
			}
			set
			{
				this.mTitle = value;
				this.mLblHeader.Text = this.mTitle;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00004F56 File Offset: 0x00003156
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00004F63 File Offset: 0x00003163
		public bool AutoClose
		{
			get
			{
				return this.mTimer.Enabled;
			}
			set
			{
				this.mTimer.Enabled = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00004F71 File Offset: 0x00003171
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00004F79 File Offset: 0x00003179
		public int Duration
		{
			get
			{
				return this.mDuration;
			}
			set
			{
				this.mDuration = value;
				this.mTimer.Interval = this.mDuration;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00004F93 File Offset: 0x00003193
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00004F9B File Offset: 0x0000319B
		private string AppName { get; set; } = string.Empty;

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00004FA4 File Offset: 0x000031A4
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00004FAC File Offset: 0x000031AC
		public string VmName { get; set; } = string.Empty;

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00004FB5 File Offset: 0x000031B5
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00004FBD File Offset: 0x000031BD
		public string PackageName { get; set; } = string.Empty;

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00004FC6 File Offset: 0x000031C6
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00004FCE File Offset: 0x000031CE
		public string AndroidNotificationId { get; private set; }

		// Token: 0x06000537 RID: 1335 RVA: 0x00019FA4 File Offset: 0x000181A4
		private void MyTimer_Tick(object sender, EventArgs e)
		{
			this.mTimer.Enabled = false;
			if (this.mMutePopup.IsOpen)
			{
				this.mMutePopup.Closed += this.MutePopup_Closed;
				return;
			}
			if (this.mPopup.IsMouseOver)
			{
				this.mPopup.MouseLeave += this.PopupConrol_MouseLeave;
				return;
			}
			this.Close();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001A010 File Offset: 0x00018210
		private void SetProperties()
		{
			this.mImgSettings.ToolTip = LocaleStrings.GetLocalizedString("STRING_MANAGE_NOTIFICATION", "");
			this.mImgMute.ToolTip = LocaleStrings.GetLocalizedString("STRING_MUTE_NOTIFICATION_TOOLTIP", "");
			this.mImgDismiss.ToolTip = LocaleStrings.GetLocalizedString("STRING_DISMISS_TOOLTIP", "");
			this.mLbl1Hour.Text = LocaleStrings.GetLocalizedString("STRING_HOUR", "");
			this.mLbl1Day.Text = LocaleStrings.GetLocalizedString("STRING_DAY", "");
			this.mLbl1Week.Text = LocaleStrings.GetLocalizedString("STRING_WEEK", "");
			this.mLblForever.Text = LocaleStrings.GetLocalizedString("STRING_FOREVER", "");
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001A0D4 File Offset: 0x000182D4
		private NotificationPopup(string imagePath, string title, string displayMsg, bool autoClose, int duration, MouseButtonEventHandler clickHandler, bool hideMute, string vmName, MouseButtonEventHandler buttonClickHandler = null, MouseButtonEventHandler closeButtonHandler = null, MouseButtonEventHandler muteButtonHandler = null, bool showOnlyMute = false, string buttonText = "", string id = "0", bool showOnlySettings = false, string package = "")
		{
			this.InitializeComponent();
			this.SetProperties();
			base.Width = NotificationWindow.Instance.ActualWidth;
			this.mTimer.Tick += this.MyTimer_Tick;
			this.mTimer.Interval = duration;
			this.Title = title;
			this.mLblContent.Text = displayMsg;
			this.VmName = vmName;
			this.PackageName = package;
			this.AndroidNotificationId = id;
			if (clickHandler != null)
			{
				this.mPopup.MouseUp += clickHandler;
				this.mClickHandler = clickHandler;
			}
			if (buttonClickHandler != null)
			{
				this.mButton.PreviewMouseLeftButtonUp += buttonClickHandler;
				this.mButton.Visibility = Visibility.Visible;
				if (!string.IsNullOrEmpty(buttonText))
				{
					this.mButton.Content = buttonText;
				}
			}
			if (hideMute)
			{
				this.mImgMute.Visibility = Visibility.Hidden;
				this.mImgSettings.Visibility = Visibility.Hidden;
			}
			if (showOnlyMute)
			{
				this.mImgMute.Visibility = Visibility.Visible;
				this.mImgSettings.Visibility = Visibility.Collapsed;
			}
			if (showOnlySettings)
			{
				this.mImgMute.Visibility = Visibility.Collapsed;
				this.mImgSettings.Visibility = Visibility.Visible;
			}
			if (closeButtonHandler != null)
			{
				this.mImgDismiss.MouseLeftButtonUp += closeButtonHandler;
			}
			if (muteButtonHandler != null)
			{
				this.mOuterGridPopUp.PreviewMouseLeftButtonUp += muteButtonHandler;
			}
			this.AutoClose = autoClose;
			if (!NotificationWindow.Instance.AppNotificationCountDictForEachVM.ContainsKey(this.VmName))
			{
				NotificationWindow.Instance.AppNotificationCountDictForEachVM[this.VmName] = new Dictionary<string, int>();
			}
			if (!NotificationWindow.Instance.AppNotificationCountDictForEachVM[this.VmName].ContainsKey(this.Title))
			{
				NotificationWindow.Instance.AppNotificationCountDictForEachVM[this.VmName].Add(this.Title, 0);
			}
			Dictionary<string, int> dictionary = NotificationWindow.Instance.AppNotificationCountDictForEachVM[this.VmName];
			string title2 = this.Title;
			int num = dictionary[title2];
			dictionary[title2] = num + 1;
			JsonParser jsonParser = new JsonParser(vmName);
			if (!string.IsNullOrEmpty(jsonParser.GetAppNameFromPackage(this.PackageName)))
			{
				this.AppName = jsonParser.GetAppNameFromPackage(this.PackageName);
			}
			else
			{
				this.AppName = title;
			}
			CustomPictureBox.SetBitmapImage(this.mImage, "bluestackslogo", false);
			if (!string.IsNullOrEmpty(imagePath))
			{
				CustomPictureBox.SetBitmapImage(this.mImage, imagePath, true);
				return;
			}
			try
			{
				if (!string.IsNullOrEmpty(this.AppName))
				{
					AppInfo appInfoFromPackageName = jsonParser.GetAppInfoFromPackageName(this.PackageName);
					Logger.Info("For notification {0}: AppName-{1} Package-{2}", new object[]
					{
						id,
						this.AppName,
						this.PackageName
					});
					if (appInfoFromPackageName != null)
					{
						Logger.Info("For notification {0}: ImageName-{1}", new object[]
						{
							id,
							appInfoFromPackageName.Img
						});
						if (File.Exists(Path.Combine(RegistryStrings.GadgetDir, appInfoFromPackageName.Img)))
						{
							CustomPictureBox.SetBitmapImage(this.mImage, Path.Combine(RegistryStrings.GadgetDir, appInfoFromPackageName.Img), true);
						}
					}
				}
			}
			catch
			{
				Logger.Error("Error loading app icon file");
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00004FD7 File Offset: 0x000031D7
		public static void SettingsImageClickedHandle(EventHandler handle, object data = null)
		{
			NotificationPopup.mSettingsImageClickedHandler = handle;
			NotificationPopup.mSettingsImageClickedEventData = data;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001A414 File Offset: 0x00018614
		internal static NotificationPopup InitPopup(string imagePath, string title, string displayMsg, bool autoClose, int duration, MouseButtonEventHandler clickHandler, bool hideMute, string vmName, MouseButtonEventHandler buttonClickHandler = null, MouseButtonEventHandler closeButtonHandler = null, MouseButtonEventHandler muteButtonHandler = null, bool showOnlyMute = false, string buttonText = "", string id = "0", bool showOnlySettings = false, string package = "")
		{
			return new NotificationPopup(imagePath, title, displayMsg, autoClose, duration, clickHandler, hideMute, vmName, buttonClickHandler, closeButtonHandler, muteButtonHandler, showOnlyMute, buttonText, id, showOnlySettings, package);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001A444 File Offset: 0x00018644
		internal void UpdatePopup(string displayMsg, bool autoClose, int duration, MouseButtonEventHandler clickHandler)
		{
			if (autoClose)
			{
				this.Duration = duration;
			}
			this.mLblContent.Text = displayMsg;
			if (this.mClickHandler != null)
			{
				this.mPopup.MouseUp -= this.mClickHandler;
			}
			if (clickHandler != null)
			{
				this.mPopup.MouseUp += clickHandler;
				this.mClickHandler = clickHandler;
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001A49C File Offset: 0x0001869C
		private void mPopupConrol_LayoutUpdated(object sender, EventArgs e)
		{
			this.mPopup.VerticalOffset += 1.0;
			this.mPopup.VerticalOffset -= 1.0;
			this.mMutePopup.VerticalOffset += 1.0;
			this.mMutePopup.VerticalOffset -= 1.0;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00004FE5 File Offset: 0x000031E5
		private void ImgMute_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.mPopup.MouseLeave -= this.PopupConrol_MouseLeave;
			this.mMutePopup.IsOpen = !this.mMutePopup.IsOpen;
			e.Handled = true;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000501E File Offset: 0x0000321E
		private void ImgDismiss_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.Close();
			e.Handled = true;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000502D File Offset: 0x0000322D
		private void Close()
		{
			this.mMutePopup.IsOpen = false;
			this.mTimer.Enabled = false;
			NotificationWindow.Instance.RemovePopup(this);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00005052 File Offset: 0x00003252
		public void StopTimer()
		{
			this.mTimer.Enabled = false;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00005060 File Offset: 0x00003260
		private void ImgSetting_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.Close();
			if (NotificationPopup.mSettingsImageClickedHandler != null)
			{
				NotificationPopup.mSettingsImageClickedHandler(NotificationPopup.mSettingsImageClickedEventData, new EventArgs());
			}
			e.Handled = true;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001A518 File Offset: 0x00018718
		private void mPopupConrol_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this.mMutePopup.IsOpen)
			{
				this.mMutePopup.IsOpen = false;
				return;
			}
			if (this.mClickHandler == null)
			{
				try
				{
					Dictionary<string, string> data = new Dictionary<string, string>
					{
						{
							"vmname",
							this.VmName
						},
						{
							"id",
							this.AndroidNotificationId
						}
					};
					HTTPUtils.SendRequestToClient("markNotificationInDrawer", data, MultiInstanceStrings.VmName, 0, null, false, 1, 0, "bgp");
					if (string.Compare(this.AppName, "Successfully copied files:", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.AppName, "Cannot copy files:", StringComparison.OrdinalIgnoreCase) == 0)
					{
						NotificationPopup.LaunchExplorer(this.mLblContent.Text);
						return;
					}
					Logger.Info("launching " + this.AppName);
					string text = "com.bluestacks.appmart";
					string text2 = "com.bluestacks.appmart.StartTopAppsActivity";
					string fileName = RegistryStrings.InstallDir + "\\HD-RunApp.exe";
					string text3;
					if (!new JsonParser(this.VmName).GetAppInfoFromAppName(this.AppName, out text, out text3, out text2))
					{
						Logger.Error("Failed to launch app: {0}. No info found in json. Starting home app", new object[]
						{
							this.AppName
						});
						if (!string.IsNullOrEmpty(this.PackageName))
						{
							Process.Start(fileName, string.Format(CultureInfo.InvariantCulture, "-p {0} -a {1} -vmname:{2}", new object[]
							{
								this.PackageName,
								text2,
								this.VmName
							}));
						}
					}
					else
					{
						JObject jobject = new JObject
						{
							{
								"app_icon_url",
								""
							},
							{
								"app_name",
								this.AppName
							},
							{
								"app_url",
								""
							},
							{
								"app_pkg",
								this.PackageName
							}
						};
						string text4 = "-json \"" + jobject.ToString(Formatting.None, new JsonConverter[0]).Replace("\"", "\\\"") + "\"";
						Process.Start(fileName, string.Format(CultureInfo.InvariantCulture, "{0} -vmname {1}", new object[]
						{
							text4,
							this.VmName
						}));
					}
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			}
			this.Close();
			e.Handled = true;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001A764 File Offset: 0x00018964
		public static void LaunchExplorer(string message)
		{
			try
			{
				string[] array = (message != null) ? message.Split(new char[]
				{
					'\n'
				}) : null;
				string fullName = Directory.GetParent(array[0]).FullName;
				string fileName = "explorer.exe";
				string arguments;
				if (array.Length == 1)
				{
					arguments = string.Format(CultureInfo.InvariantCulture, "/Select, {0}", new object[]
					{
						array[0]
					});
				}
				else
				{
					arguments = fullName;
				}
				Process.Start(fileName, arguments);
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error Occured, Err : {0}", new object[]
				{
					ex.ToString()
				}));
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001A808 File Offset: 0x00018A08
		private void Lbl1Hour_MouseUp(object sender, MouseButtonEventArgs e)
		{
			NotificationManager.Instance.UpdateMuteState(MuteState.MutedFor1Hour, this.mTitle, this.VmName);
			Stats.SendCommonClientStatsAsync("notification_mode", "app_notifications_snoozed", this.VmName, this.PackageName, "Muted_" + (sender as TextBlock).Text, "desktop_notification", "");
			this.Close();
			e.Handled = true;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001A874 File Offset: 0x00018A74
		private void Lbl1Day_MouseUp(object sender, MouseButtonEventArgs e)
		{
			NotificationManager.Instance.UpdateMuteState(MuteState.MutedFor1Day, this.mTitle, this.VmName);
			Stats.SendCommonClientStatsAsync("notification_mode", "app_notifications_snoozed", this.VmName, this.PackageName, "Muted_" + (sender as TextBlock).Text, "desktop_notification", "");
			this.Close();
			e.Handled = true;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001A8E0 File Offset: 0x00018AE0
		private void Lbl1Week_MouseUp(object sender, MouseButtonEventArgs e)
		{
			NotificationManager.Instance.UpdateMuteState(MuteState.MutedFor1Week, this.mTitle, this.VmName);
			Stats.SendCommonClientStatsAsync("notification_mode", "app_notifications_snoozed", this.VmName, this.PackageName, "Muted_" + (sender as TextBlock).Text, "desktop_notification", "");
			this.Close();
			e.Handled = true;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001A94C File Offset: 0x00018B4C
		private void LblForever_MouseUp(object sender, MouseButtonEventArgs e)
		{
			NotificationManager.Instance.UpdateMuteState(MuteState.MutedForever, this.mTitle, this.VmName);
			if (NotificationManager.Instance.DictNotificationItems.ContainsKey(this.mTitle))
			{
				NotificationManager.Instance.DictNotificationItems[this.mTitle].ShowDesktopNotifications = false;
			}
			NotificationManager.Instance.UpdateNotificationsSettings();
			Stats.SendCommonClientStatsAsync("notification_mode", "app_notifications_snoozed", this.VmName, this.PackageName, "Muted_" + (sender as TextBlock).Text, "desktop_notification", "");
			this.Close();
			e.Handled = true;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000508A File Offset: 0x0000328A
		private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			((Grid)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#262c4b"));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000050AB File Offset: 0x000032AB
		private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			((Grid)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34375C"));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00003C8E File Offset: 0x00001E8E
		private void mButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		~NotificationPopup()
		{
			this.Dispose(false);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000050CC File Offset: 0x000032CC
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				Timer timer = this.mTimer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this.disposedValue = true;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000050F0 File Offset: 0x000032F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000050FF File Offset: 0x000032FF
		private void mPopup_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			base.Height = (sender as Grid).ActualHeight;
			this.mPopup.Height = (sender as Grid).ActualHeight;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00005128 File Offset: 0x00003328
		private void MutePopup_Closed(object sender, EventArgs e)
		{
			this.mPopup.MouseLeave += this.PopupConrol_MouseLeave;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00005141 File Offset: 0x00003341
		private void PopupConrol_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Close();
			this.mPopup.MouseLeave -= this.PopupConrol_MouseLeave;
		}

		// Token: 0x0400025C RID: 604
		private Timer mTimer = new Timer();

		// Token: 0x0400025D RID: 605
		private string mTitle = string.Empty;

		// Token: 0x0400025E RID: 606
		private MouseButtonEventHandler mClickHandler;

		// Token: 0x0400025F RID: 607
		private static EventHandler mSettingsImageClickedHandler;

		// Token: 0x04000260 RID: 608
		private static object mSettingsImageClickedEventData;

		// Token: 0x04000261 RID: 609
		private int mDuration = int.MinValue;

		// Token: 0x04000266 RID: 614
		private bool disposedValue;
	}
}
