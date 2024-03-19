using System;
using System.IO;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x020001E7 RID: 487
	public static class GuidUtils
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003C9FC File Offset: 0x0003ABFC
		public static string ReuseOrGenerateMachineId()
		{
			string text = "";
			try
			{
				string blueStacksMachineId = GuidUtils.GetBlueStacksMachineId();
				if (!string.IsNullOrEmpty(blueStacksMachineId))
				{
					return blueStacksMachineId;
				}
				text = Guid.NewGuid().ToString();
				GuidUtils.SetBlueStacksMachineId(text);
			}
			catch (Exception ex)
			{
				Logger.Error("Couldn't generate/find Machine ID. Ex: {0}", new object[]
				{
					ex.Message
				});
			}
			return text;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003CA70 File Offset: 0x0003AC70
		public static string ReuseOrGenerateVersionId()
		{
			string text = "";
			try
			{
				string blueStacksVersionId = GuidUtils.GetBlueStacksVersionId();
				if (!string.IsNullOrEmpty(blueStacksVersionId))
				{
					return blueStacksVersionId;
				}
				text = Guid.NewGuid().ToString();
				GuidUtils.SetBlueStacksVersionId(text);
			}
			catch (Exception ex)
			{
				Logger.Error("Couldn't generate/find Version ID. Ex: {0}", new object[]
				{
					ex.Message
				});
			}
			return text;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		public static string GetBlueStacksMachineId()
		{
			if (string.IsNullOrEmpty(GuidUtils.sBlueStacksMachineId))
			{
				GuidUtils.sBlueStacksMachineId = StringUtils.GetControlCharFreeString(GuidUtils.GetIdFromRegistryOrFile("MachineID").Trim());
			}
			return GuidUtils.sBlueStacksMachineId;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0000DD27 File Offset: 0x0000BF27
		public static bool SetBlueStacksMachineId(string newId)
		{
			return GuidUtils.SetIdInRegistryAndFile("MachineID", newId);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public static string GetBlueStacksVersionId()
		{
			if (string.IsNullOrEmpty(GuidUtils.sBlueStacksVersionId))
			{
				GuidUtils.sBlueStacksVersionId = StringUtils.GetControlCharFreeString(GuidUtils.GetIdFromRegistryOrFile("VersionMachineId_4.250.0.1070").Trim());
			}
			return GuidUtils.sBlueStacksVersionId;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public static bool SetBlueStacksVersionId(string newId)
		{
			return GuidUtils.SetIdInRegistryAndFile("VersionMachineId_4.250.0.1070", newId);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003CAE4 File Offset: 0x0003ACE4
		public static string GetIdFromRegistryOrFile(string id)
		{
			string text = "";
			text = (string)RegistryUtils.GetRegistryValue("Software\\BlueStacksInstaller", id, "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			try
			{
				string text2 = Path.Combine(new DirectoryInfo(ShortcutHelper.CommonDesktopPath).Parent.FullName, "BlueStacks");
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				string path = Path.Combine(text2, id);
				if (File.Exists(path))
				{
					string text3 = File.ReadAllText(path);
					if (!string.IsNullOrEmpty(text3))
					{
						text = text3;
					}
				}
			}
			catch
			{
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\BlueStacksInstaller");
			if (registryKey != null)
			{
				text = (string)registryKey.GetValue(id, "");
			}
			return text;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		public static bool SetIdInRegistryAndFile(string id, string value)
		{
			bool result = false;
			value = ((value != null) ? value.Trim() : null);
			result = RegistryUtils.SetRegistryValue("Software\\BlueStacksInstaller", id, value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			try
			{
				string text = Path.Combine(new DirectoryInfo(ShortcutHelper.CommonDesktopPath).Parent.FullName, "BlueStacks");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string path = Path.Combine(text, id);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				File.WriteAllText(path, value);
				result = true;
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to write in in file. Error: " + ex.Message);
			}
			try
			{
				Registry.CurrentUser.CreateSubKey("Software\\BlueStacksInstaller").SetValue(id, value);
				result = true;
			}
			catch (Exception ex2)
			{
				Logger.Warning("Failed to write id in HKCU. Error: " + ex2.Message);
			}
			return result;
		}

		// Token: 0x040008DD RID: 2269
		private static string sBlueStacksMachineId;

		// Token: 0x040008DE RID: 2270
		private static string sBlueStacksVersionId;
	}
}
