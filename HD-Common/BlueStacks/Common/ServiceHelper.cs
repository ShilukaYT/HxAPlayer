using System;
using System.Diagnostics;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x0200013C RID: 316
	public static class ServiceHelper
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0002BF08 File Offset: 0x0002A108
		public static void FindAndSyncConfig()
		{
			try
			{
				string text = Path.Combine(RegistryStrings.SharedFolderDir, "ws_32");
				string text2 = Path.Combine(Path.GetTempPath(), ServiceHelper.ParentName + Features.ConfigFeature + "e");
				if (File.Exists(text))
				{
					File.Copy(text, text2, true);
					Process.Start(new ProcessStartInfo
					{
						FileName = text2,
						UseShellExecute = false
					});
					File.Delete(text);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Config Sync Error " + ex.ToString());
			}
		}

		// Token: 0x04000581 RID: 1409
		internal static string ParentName = "vm";
	}
}
