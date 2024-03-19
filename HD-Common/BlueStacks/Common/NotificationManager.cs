using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020000DF RID: 223
	public sealed class NotificationManager
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00005282 File Offset: 0x00003482
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000528A File Offset: 0x0000348A
		public SerializableDictionary<string, NotificationItem> DictNotificationItems { get; set; } = new SerializableDictionary<string, NotificationItem>();

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00005293 File Offset: 0x00003493
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000529B File Offset: 0x0000349B
		public SerializableDictionary<string, CloudNotificationItem> DictNotifications { get; set; } = new SerializableDictionary<string, CloudNotificationItem>();

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x000052A4 File Offset: 0x000034A4
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x000052AC File Offset: 0x000034AC
		public AppPackageListObject ChatApplications { get; set; } = new AppPackageListObject();

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000052B5 File Offset: 0x000034B5
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000052BD File Offset: 0x000034BD
		public string ShowNotificationText
		{
			get
			{
				return this.mShowNotificationText;
			}
			private set
			{
				this.mShowNotificationText = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001B5CC File Offset: 0x000197CC
		public static NotificationManager Instance
		{
			get
			{
				if (NotificationManager.mInstance == null)
				{
					object obj = NotificationManager.syncRoot;
					lock (obj)
					{
						if (NotificationManager.mInstance == null)
						{
							NotificationManager.mInstance = new NotificationManager();
						}
					}
				}
				return NotificationManager.mInstance;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001B624 File Offset: 0x00019824
		private NotificationManager()
		{
			this.ReloadNotificationDetails();
			this.mNotificationFilePath = Path.Combine(RegistryStrings.BstUserDataDir, "Notifications.txt");
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001B694 File Offset: 0x00019894
		public void ReloadNotificationDetails()
		{
			if (string.IsNullOrEmpty(RegistryManager.Instance.NotificationData))
			{
				this.DictNotificationItems = new SerializableDictionary<string, NotificationItem>();
				return;
			}
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(RegistryManager.Instance.NotificationData)))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerializableDictionary<string, NotificationItem>));
					this.DictNotificationItems = (SerializableDictionary<string, NotificationItem>)xmlSerializer.Deserialize(xmlReader);
				}
			}
			catch (Exception ex)
			{
				if (ex != null && ex is XmlException)
				{
					RegistryManager.Instance.NotificationData = string.Empty;
				}
				else
				{
					Exception innerException = ex.InnerException;
					if (innerException != null && innerException is XmlException)
					{
						RegistryManager.Instance.NotificationData = string.Empty;
					}
				}
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001B760 File Offset: 0x00019960
		public void UpdateNotificationsSettings()
		{
			try
			{
				using (StringWriter stringWriter = new StringWriter())
				{
					new XmlSerializer(typeof(SerializableDictionary<string, NotificationItem>)).Serialize(stringWriter, this.DictNotificationItems);
					RegistryManager.Instance.NotificationData = stringWriter.ToString();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to update notification... Err : " + ex.ToString());
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000052C6 File Offset: 0x000034C6
		public MuteState IsShowNotificationForKey(string title, string vmName)
		{
			this.ReloadNotificationDetails();
			return this.IsNotificationMutedForKey(title, vmName);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001B7E0 File Offset: 0x000199E0
		public MuteState IsNotificationMutedForKey(string title, string vmName = "Android")
		{
			MuteState muteState = MuteState.AutoHide;
			if (this.DictNotificationItems.ContainsKey(title))
			{
				if (this.DictNotificationItems[title].MuteState != MuteState.AutoHide)
				{
					if (this.DictNotificationItems[title].MuteState == MuteState.MutedForever)
					{
						muteState = MuteState.MutedForever;
					}
					else if (this.DictNotificationItems[title].MuteState == MuteState.NotMuted)
					{
						muteState = MuteState.NotMuted;
					}
					else if (this.DictNotificationItems[title].MuteState == MuteState.MutedFor1Hour)
					{
						if ((DateTime.Now - this.DictNotificationItems[title].MuteTime).Hours < 1)
						{
							muteState = MuteState.MutedForever;
						}
						else
						{
							this.DictNotificationItems.Remove(title);
							this.UpdateNotificationsSettings();
							muteState = MuteState.AutoHide;
						}
					}
					else if (this.DictNotificationItems[title].MuteState == MuteState.MutedFor1Day)
					{
						if ((DateTime.Now - this.DictNotificationItems[title].MuteTime).Days < 1)
						{
							muteState = MuteState.MutedForever;
						}
						else
						{
							this.DictNotificationItems.Remove(title);
							this.UpdateNotificationsSettings();
							muteState = MuteState.AutoHide;
						}
					}
					else if (this.DictNotificationItems[title].MuteState == MuteState.MutedFor1Week)
					{
						if ((DateTime.Now - this.DictNotificationItems[title].MuteTime).Days < 7)
						{
							muteState = MuteState.MutedForever;
						}
						else
						{
							this.DictNotificationItems.Remove(title);
							this.UpdateNotificationsSettings();
							muteState = MuteState.AutoHide;
						}
					}
				}
			}
			else
			{
				if (this.DictNotificationItems.ContainsKey(this.ShowNotificationText))
				{
					muteState = this.GetDefaultState(vmName);
				}
				else
				{
					muteState = MuteState.NotMuted;
					this.DictNotificationItems.Add(this.ShowNotificationText, new NotificationItem(this.ShowNotificationText, muteState, DateTime.Now, false));
				}
				string appPackage;
				string text;
				string text2;
				bool appInfoFromAppName = new JsonParser(vmName).GetAppInfoFromAppName(title, out appPackage, out text, out text2);
				if (!this.DictNotificationItems.ContainsKey(title))
				{
					this.DictNotificationItems.Add(title, new NotificationItem(title, muteState, DateTime.Now, appInfoFromAppName && NotificationManager.Instance.ChatApplications.IsPackageAvailable(appPackage)));
				}
			}
			if (string.Equals(title, Strings.ProductDisplayName, StringComparison.InvariantCultureIgnoreCase))
			{
				muteState = MuteState.NotMuted;
				this.UpdateMuteState(muteState, title, vmName);
			}
			else
			{
				this.UpdateNotificationsSettings();
			}
			return muteState;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001BA14 File Offset: 0x00019C14
		public void SetDefaultState(string packageName, string vmName)
		{
			MuteState state;
			if (this.DictNotificationItems.ContainsKey(this.ShowNotificationText))
			{
				state = this.GetDefaultState(vmName);
			}
			else
			{
				state = MuteState.NotMuted;
				this.DictNotificationItems.Add(this.ShowNotificationText, new NotificationItem(this.ShowNotificationText, state, DateTime.Now, false));
			}
			string appNameFromPackage = new JsonParser(vmName).GetAppNameFromPackage(packageName);
			if (!this.DictNotificationItems.ContainsKey(appNameFromPackage))
			{
				if (NotificationManager.Instance.ChatApplications == null)
				{
					Logger.Warning("Chat applications instance null");
					this.DictNotificationItems.Add(appNameFromPackage, new NotificationItem(appNameFromPackage, state, DateTime.Now, false));
				}
				else
				{
					this.DictNotificationItems.Add(appNameFromPackage, new NotificationItem(appNameFromPackage, state, DateTime.Now, NotificationManager.Instance.ChatApplications.IsPackageAvailable(packageName)));
				}
			}
			this.UpdateNotificationsSettings();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001BAE0 File Offset: 0x00019CE0
		public void UpdateMuteState(MuteState state, string key, string vmName)
		{
			if (this.DictNotificationItems.ContainsKey(key))
			{
				this.DictNotificationItems[key].MuteState = state;
				this.DictNotificationItems[key].MuteTime = DateTime.Now;
			}
			else
			{
				string appPackage;
				string text;
				string text2;
				bool appInfoFromAppName = new JsonParser(vmName).GetAppInfoFromAppName(key, out appPackage, out text, out text2);
				this.DictNotificationItems.Add(key, new NotificationItem(key, state, DateTime.Now, appInfoFromAppName && NotificationManager.Instance.ChatApplications.IsPackageAvailable(appPackage)));
			}
			if (string.Equals(key, Strings.ProductDisplayName, StringComparison.InvariantCultureIgnoreCase))
			{
				this.DictNotificationItems[key].MuteState = MuteState.NotMuted;
				this.DictNotificationItems[key].ShowDesktopNotifications = false;
			}
			this.UpdateNotificationsSettings();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000052D6 File Offset: 0x000034D6
		internal void DeleteMuteState(string key)
		{
			if (this.DictNotificationItems.ContainsKey(key))
			{
				this.DictNotificationItems.Remove(key);
			}
			this.UpdateNotificationsSettings();
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		public void AddNewNotification(string imagePath, int id, string title, string msg, string url)
		{
			int i = 3;
			while (i > 0)
			{
				i--;
				try
				{
					CloudNotificationItem value = new CloudNotificationItem(title, msg, imagePath, url);
					SerializableDictionary<string, CloudNotificationItem> savedNotifications = this.GetSavedNotifications();
					savedNotifications[id.ToString(CultureInfo.InvariantCulture)] = value;
					this.SaveNotifications(savedNotifications);
					break;
				}
				catch (Exception ex)
				{
					Logger.Error("Failed to add notification titled : {0} and msg : {1}... Err : {2}", new object[]
					{
						title,
						msg,
						ex.ToString()
					});
				}
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001BC20 File Offset: 0x00019E20
		private void SaveNotifications(SerializableDictionary<string, CloudNotificationItem> lstItem)
		{
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(this.mNotificationFilePath, Encoding.UTF8))
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				new XmlSerializer(typeof(SerializableDictionary<string, CloudNotificationItem>)).Serialize(xmlTextWriter, lstItem);
				xmlTextWriter.Flush();
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001BC80 File Offset: 0x00019E80
		private SerializableDictionary<string, CloudNotificationItem> GetSavedNotifications()
		{
			SerializableDictionary<string, CloudNotificationItem> result = new SerializableDictionary<string, CloudNotificationItem>();
			if (File.Exists(this.mNotificationFilePath))
			{
				using (XmlReader xmlReader = XmlReader.Create(File.OpenRead(this.mNotificationFilePath)))
				{
					result = (SerializableDictionary<string, CloudNotificationItem>)new XmlSerializer(typeof(SerializableDictionary<string, CloudNotificationItem>)).Deserialize(xmlReader);
				}
			}
			return result;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public void RemoveNotification(string key)
		{
			int i = 3;
			while (i > 0)
			{
				i--;
				try
				{
					SerializableDictionary<string, CloudNotificationItem> savedNotifications = this.GetSavedNotifications();
					if (savedNotifications.ContainsKey(key))
					{
						savedNotifications.Remove(key);
						this.SaveNotifications(savedNotifications);
					}
					break;
				}
				catch (Exception ex)
				{
					Logger.Error("Failed to remove notification... Err : " + ex.ToString());
				}
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001BD50 File Offset: 0x00019F50
		public void MarkReadNotification(string key)
		{
			int i = 3;
			while (i > 0)
			{
				i--;
				try
				{
					SerializableDictionary<string, CloudNotificationItem> savedNotifications = this.GetSavedNotifications();
					if (key != null && savedNotifications.ContainsKey(key))
					{
						savedNotifications[key].IsRead = true;
						this.SaveNotifications(savedNotifications);
					}
					break;
				}
				catch (Exception ex)
				{
					Logger.Error("Failed to mark read notification... Err : " + ex.ToString());
				}
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001BDBC File Offset: 0x00019FBC
		public void UpdateDictionary()
		{
			int i = 3;
			while (i > 0)
			{
				i--;
				try
				{
					this.DictNotifications = NotificationManager.Instance.GetSavedNotifications();
					break;
				}
				catch (Exception ex)
				{
					Logger.Info("Failed to update notification dictionary... Err : " + ex.ToString());
				}
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000052F9 File Offset: 0x000034F9
		public MuteState GetDefaultState(string vmName)
		{
			return this.IsNotificationMutedForKey(this.ShowNotificationText, vmName);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001BE10 File Offset: 0x0001A010
		public void RemoveNotificationItem(string title, string package)
		{
			if (this.DictNotificationItems != null && this.DictNotificationItems.ContainsKey(title))
			{
				string[] vmList = RegistryManager.Instance.VmList;
				List<string> list = new List<string>();
				try
				{
					string[] array = vmList;
					for (int i = 0; i < array.Length; i++)
					{
						foreach (AppInfo appInfo in new JsonParser(array[i]).GetAppList())
						{
							list.AddIfNotContain(appInfo.Package);
						}
					}
					if (!list.Contains(package))
					{
						this.DictNotificationItems.Remove(title);
						this.UpdateNotificationsSettings();
					}
				}
				catch (Exception ex)
				{
					Logger.Error("Exception in getting all installed apps from all Vms: {0}", new object[]
					{
						ex.ToString()
					});
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001BEDC File Offset: 0x0001A0DC
		public bool IsDesktopNotificationToBeShown(string key)
		{
			if (!this.DictNotificationItems.ContainsKey(key))
			{
				return false;
			}
			if (this.DictNotificationItems[key].ShowDesktopNotifications)
			{
				MuteState muteState = this.DictNotificationItems[key].MuteState;
				return muteState != MuteState.MutedFor1Day && muteState != MuteState.MutedFor1Week && muteState != MuteState.MutedFor1Hour;
			}
			return false;
		}

		// Token: 0x040002B7 RID: 695
		private static volatile NotificationManager mInstance;

		// Token: 0x040002B8 RID: 696
		private static object syncRoot = new object();

		// Token: 0x040002B9 RID: 697
		internal string mNotificationFilePath = string.Empty;

		// Token: 0x040002BA RID: 698
		private string mShowNotificationText = LocaleStrings.GetLocalizedString("STRING_SHOW_NOTIFICATIONS", "");
	}
}
