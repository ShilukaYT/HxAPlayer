using System;
using System.IO;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x02000055 RID: 85
	public static class GuidUtils
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x0000491C File Offset: 0x00002B1C
		public static string ReuseOrGenerateMachineId()
		{
			string text = "";
			try
			{
				string blueStacksMachineId = GuidUtils.GetBlueStacksMachineId();
				bool flag = !string.IsNullOrEmpty(blueStacksMachineId);
				if (flag)
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

		// Token: 0x060000BA RID: 186 RVA: 0x000049A0 File Offset: 0x00002BA0
		public static string ReuseOrGenerateVersionId()
		{
			string text = "";
			try
			{
				string blueStacksVersionId = GuidUtils.GetBlueStacksVersionId();
				bool flag = !string.IsNullOrEmpty(blueStacksVersionId);
				if (flag)
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

		// Token: 0x060000BB RID: 187 RVA: 0x00004A24 File Offset: 0x00002C24
		public static string GetBlueStacksMachineId()
		{
			bool flag = string.IsNullOrEmpty(GuidUtils.sBlueStacksMachineId);
			if (flag)
			{
				GuidUtils.sBlueStacksMachineId = StringUtils.GetControlCharFreeString(GuidUtils.GetIdFromRegistryOrFile("MachineID").Trim());
			}
			return GuidUtils.sBlueStacksMachineId;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004A64 File Offset: 0x00002C64
		public static bool SetBlueStacksMachineId(string newId)
		{
			return GuidUtils.SetIdInRegistryAndFile("MachineID", newId);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004A84 File Offset: 0x00002C84
		public static string GetBlueStacksVersionId()
		{
			bool flag = string.IsNullOrEmpty(GuidUtils.sBlueStacksVersionId);
			if (flag)
			{
				GuidUtils.sBlueStacksVersionId = StringUtils.GetControlCharFreeString(GuidUtils.GetIdFromRegistryOrFile("VersionMachineId_4.250.0.1070").Trim());
			}
			return GuidUtils.sBlueStacksVersionId;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public static bool SetBlueStacksVersionId(string newId)
		{
			return GuidUtils.SetIdInRegistryAndFile("VersionMachineId_4.250.0.1070", newId);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public static string GetIdFromRegistryOrFile(string id)
		{
			string text = "";
			text = (string)RegistryUtils.GetRegistryValue("Software\\BlueStacksInstaller", id, "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			bool flag = !string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				try
				{
					string fullName = new DirectoryInfo(ShortcutHelper.CommonDesktopPath).Parent.FullName;
					string text2 = Path.Combine(fullName, "BlueStacks");
					bool flag2 = !Directory.Exists(text2);
					if (flag2)
					{
						Directory.CreateDirectory(text2);
					}
					string path = Path.Combine(text2, id);
					bool flag3 = File.Exists(path);
					if (flag3)
					{
						string text3 = File.ReadAllText(path);
						bool flag4 = !string.IsNullOrEmpty(text3);
						if (flag4)
						{
							text = text3;
						}
					}
				}
				catch
				{
				}
				bool flag5 = !string.IsNullOrEmpty(text);
				if (flag5)
				{
					result = text;
				}
				else
				{
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\BlueStacksInstaller");
					bool flag6 = registryKey != null;
					if (flag6)
					{
						text = (string)registryKey.GetValue(id, "");
					}
					result = text;
				}
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004BFC File Offset: 0x00002DFC
		public static bool SetIdInRegistryAndFile(string id, string value)
		{
			bool result = false;
			value = ((value != null) ? value.Trim() : null);
			result = RegistryUtils.SetRegistryValue("Software\\BlueStacksInstaller", id, value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			try
			{
				string fullName = new DirectoryInfo(ShortcutHelper.CommonDesktopPath).Parent.FullName;
				string text = Path.Combine(fullName, "BlueStacks");
				bool flag = !Directory.Exists(text);
				if (flag)
				{
					Directory.CreateDirectory(text);
				}
				string path = Path.Combine(text, id);
				bool flag2 = File.Exists(path);
				if (flag2)
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
				RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\BlueStacksInstaller");
				registryKey.SetValue(id, value);
				result = true;
			}
			catch (Exception ex2)
			{
				Logger.Warning("Failed to write id in HKCU. Error: " + ex2.Message);
			}
			return result;
		}

		// Token: 0x040001B2 RID: 434
		private static string sBlueStacksMachineId = null;

		// Token: 0x040001B3 RID: 435
		private static string sBlueStacksVersionId = null;
	}
}
