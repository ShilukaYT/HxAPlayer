using System;
using System.Collections.Generic;
using System.Windows.Input;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public class ShortcutConfig
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x000056AF File Offset: 0x000038AF
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x000056B7 File Offset: 0x000038B7
		public List<ShortcutKeys> Shortcut { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000056C0 File Offset: 0x000038C0
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x000056C8 File Offset: 0x000038C8
		public string DefaultModifier { get; set; } = IMAPKeys.GetStringForFile(Key.LeftCtrl) + "," + IMAPKeys.GetStringForFile(Key.LeftShift);

		// Token: 0x060005FA RID: 1530 RVA: 0x0001C260 File Offset: 0x0001A460
		public static ShortcutConfig LoadShortcutsConfig()
		{
			try
			{
				string value = string.Empty;
				if (!string.IsNullOrEmpty(RegistryManager.Instance.UserDefinedShortcuts))
				{
					value = RegistryManager.Instance.UserDefinedShortcuts;
				}
				else
				{
					if (!string.IsNullOrEmpty(value) || string.IsNullOrEmpty(RegistryManager.Instance.DefaultShortcuts))
					{
						throw new Exception("Shortcuts registry entry not found.");
					}
					value = RegistryManager.Instance.DefaultShortcuts;
				}
				return JsonConvert.DeserializeObject<ShortcutConfig>(value, Utils.GetSerializerSettings());
			}
			catch (Exception ex)
			{
				Logger.Error("SHORTCUT: Exception in loading shortcuts config: " + ex.ToString());
			}
			return null;
		}
	}
}
