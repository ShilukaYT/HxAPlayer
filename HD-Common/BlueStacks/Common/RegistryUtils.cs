using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x02000208 RID: 520
	public static class RegistryUtils
	{
		// Token: 0x06001057 RID: 4183
		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegRenameKey(UIntPtr hKey, [MarshalAs(UnmanagedType.LPWStr)] string oldname, [MarshalAs(UnmanagedType.LPWStr)] string newname);

		// Token: 0x06001058 RID: 4184
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegOpenKeyEx(UIntPtr hKey, string subKey, int ulOptions, RegSAM samDesired, out UIntPtr hkResult);

		// Token: 0x06001059 RID: 4185 RVA: 0x0000DFBE File Offset: 0x0000C1BE
		public static RegistryKey InitKeyWithSecurityCheck(string keyName)
		{
			if (!SystemUtils.IsAdministrator())
			{
				return Registry.LocalMachine.OpenSubKey(keyName);
			}
			return Registry.LocalMachine.CreateSubKey(keyName);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003E674 File Offset: 0x0003C874
		public static RegistryKey InitKey(string keyName)
		{
			RegistryKey result;
			try
			{
				result = Registry.LocalMachine.CreateSubKey(keyName);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
		public static void DeleteKey(string hklmPath, bool throwOnError = true)
		{
			try
			{
				Registry.LocalMachine.DeleteSubKeyTree(hklmPath);
			}
			catch
			{
				if (throwOnError)
				{
					throw;
				}
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003E6DC File Offset: 0x0003C8DC
		public static object GetRegistryValue(string registryPath, string key, object defaultValue, RegistryKeyKind registryKind = RegistryKeyKind.HKEY_LOCAL_MACHINE)
		{
			RegistryKey registryKey = null;
			object result = defaultValue;
			if (registryKind != RegistryKeyKind.HKEY_LOCAL_MACHINE)
			{
				if (registryKind == RegistryKeyKind.HKEY_CURRENT_USER)
				{
					registryKey = Registry.CurrentUser.OpenSubKey(registryPath);
				}
			}
			else
			{
				registryKey = Registry.LocalMachine.OpenSubKey(registryPath);
			}
			if (registryKey != null)
			{
				result = registryKey.GetValue(key, defaultValue);
				registryKey.Close();
			}
			return result;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003E724 File Offset: 0x0003C924
		public static bool SetRegistryValue(string registryPath, string key, object value, RegistryValueKind type, RegistryKeyKind registryKind = RegistryKeyKind.HKEY_LOCAL_MACHINE)
		{
			RegistryKey registryKey = null;
			bool result = true;
			try
			{
				if (registryKind != RegistryKeyKind.HKEY_LOCAL_MACHINE)
				{
					if (registryKind == RegistryKeyKind.HKEY_CURRENT_USER)
					{
						registryKey = Registry.CurrentUser.CreateSubKey(registryPath);
					}
				}
				else
				{
					registryKey = Registry.LocalMachine.CreateSubKey(registryPath);
				}
				if (registryKey != null)
				{
					registryKey.SetValue(key, value, type);
				}
			}
			catch
			{
				Logger.Warning("Failed to set registry value at {0} for {1}:{2}", new object[]
				{
					registryPath,
					key,
					value
				});
				result = false;
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003E7B0 File Offset: 0x0003C9B0
		public static int RenameKey(string basePath, string oldName, string newName, bool deleteNewIfExist)
		{
			if (deleteNewIfExist)
			{
				try
				{
					Registry.LocalMachine.DeleteSubKeyTree(Path.Combine(basePath, newName));
				}
				catch (Exception ex)
				{
					Logger.Warning("Couldn't delete new subkeytree: {0}\\{1}, ex: {2}", new object[]
					{
						basePath,
						newName,
						ex.Message
					});
				}
			}
			UIntPtr hKey;
			int num = RegistryUtils.RegOpenKeyEx(RegistryUtils.HKEY_LOCAL_MACHINE, basePath, 0, RegSAM.Write, out hKey);
			if (num == 0)
			{
				num = RegistryUtils.RegRenameKey(hKey, oldName, newName);
			}
			return num;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003E828 File Offset: 0x0003CA28
		public static void GrantAllAccessPermission(RegistryKey rk)
		{
			object obj = new SecurityIdentifier(WellKnownSidType.WorldSid, null).Translate(typeof(NTAccount)) as NTAccount;
			RegistrySecurity registrySecurity = new RegistrySecurity();
			RegistryAccessRule rule = new RegistryAccessRule(obj.ToString(), RegistryRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
			registrySecurity.AddAccessRule(rule);
			if (rk != null)
			{
				rk.SetAccessControl(registrySecurity);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003E87C File Offset: 0x0003CA7C
		public static void MoveUnifiedInstallerRegistryFromWow64()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Strings.RegistryBaseKeyPath);
				if (registryKey == null || string.IsNullOrEmpty((string)registryKey.GetValue("Version", null)))
				{
					RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("Software", true);
					RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("Software\\WOW6432Node\\BlueStacks" + Strings.GetOemTag());
					if (registryKey3 != null)
					{
						RegistryKey registryKey4 = registryKey2.CreateSubKey("BlueStacks" + Strings.GetOemTag());
						RegistryUtils.RecurseCopyKey(registryKey3, registryKey4);
						registryKey2.DeleteSubKeyTree("WOW6432Node\\BlueStacks" + Strings.GetOemTag());
						RegistryUtils.GrantAllAccessPermission(registryKey4);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003E934 File Offset: 0x0003CB34
		private static void RecurseCopyKey(RegistryKey sourceKey, RegistryKey destinationKey)
		{
			foreach (string name in sourceKey.GetValueNames())
			{
				object value = sourceKey.GetValue(name);
				RegistryValueKind valueKind = sourceKey.GetValueKind(name);
				destinationKey.SetValue(name, value, valueKind);
			}
			foreach (string text in sourceKey.GetSubKeyNames())
			{
				RegistryKey sourceKey2 = sourceKey.OpenSubKey(text);
				RegistryKey destinationKey2 = destinationKey.CreateSubKey(text);
				RegistryUtils.RecurseCopyKey(sourceKey2, destinationKey2);
			}
		}

		// Token: 0x04000AD9 RID: 2777
		public static readonly UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(2147483650U);

		// Token: 0x04000ADA RID: 2778
		public static readonly UIntPtr HKEY_CURRENT_USER = new UIntPtr(2147483649U);
	}
}
