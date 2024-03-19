using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x02000062 RID: 98
	public static class RegistryUtils
	{
		// Token: 0x0600011D RID: 285
		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegRenameKey(UIntPtr hKey, [MarshalAs(UnmanagedType.LPWStr)] string oldname, [MarshalAs(UnmanagedType.LPWStr)] string newname);

		// Token: 0x0600011E RID: 286
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegOpenKeyEx(UIntPtr hKey, string subKey, int ulOptions, RegSAM samDesired, out UIntPtr hkResult);

		// Token: 0x0600011F RID: 287 RVA: 0x00006F08 File Offset: 0x00005108
		public static RegistryKey InitKeyWithSecurityCheck(string keyName)
		{
			bool flag = !SystemUtils.IsAdministrator();
			RegistryKey result;
			if (flag)
			{
				result = Registry.LocalMachine.OpenSubKey(keyName);
			}
			else
			{
				result = Registry.LocalMachine.CreateSubKey(keyName);
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006F44 File Offset: 0x00005144
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

		// Token: 0x06000121 RID: 289 RVA: 0x00006F78 File Offset: 0x00005178
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

		// Token: 0x06000122 RID: 290 RVA: 0x00006FB4 File Offset: 0x000051B4
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
			bool flag = registryKey != null;
			if (flag)
			{
				result = registryKey.GetValue(key, defaultValue);
				registryKey.Close();
			}
			return result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007014 File Offset: 0x00005214
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
				bool flag = registryKey != null;
				if (flag)
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
				bool flag2 = registryKey != null;
				if (flag2)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000070CC File Offset: 0x000052CC
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
			bool flag = num == 0;
			if (flag)
			{
				num = RegistryUtils.RegRenameKey(hKey, oldName, newName);
			}
			return num;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007160 File Offset: 0x00005360
		public static void GrantAllAccessPermission(RegistryKey rk)
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			NTAccount ntaccount = securityIdentifier.Translate(typeof(NTAccount)) as NTAccount;
			RegistrySecurity registrySecurity = new RegistrySecurity();
			RegistryAccessRule rule = new RegistryAccessRule(ntaccount.ToString(), RegistryRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
			registrySecurity.AddAccessRule(rule);
			if (rk != null)
			{
				rk.SetAccessControl(registrySecurity);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000071BC File Offset: 0x000053BC
		public static void MoveUnifiedInstallerRegistryFromWow64()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Strings.RegistryBaseKeyPath);
				bool flag = registryKey != null;
				if (flag)
				{
					string value = (string)registryKey.GetValue("Version", null);
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						return;
					}
				}
				RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("Software", true);
				RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("Software\\WOW6432Node\\BlueStacks" + Strings.GetOemTag());
				bool flag3 = registryKey3 != null;
				if (flag3)
				{
					RegistryKey registryKey4 = registryKey2.CreateSubKey("BlueStacks" + Strings.GetOemTag());
					RegistryUtils.RecurseCopyKey(registryKey3, registryKey4);
					registryKey2.DeleteSubKeyTree("WOW6432Node\\BlueStacks" + Strings.GetOemTag());
					RegistryUtils.GrantAllAccessPermission(registryKey4);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007298 File Offset: 0x00005498
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

		// Token: 0x040001DD RID: 477
		public static readonly UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(2147483650U);

		// Token: 0x040001DE RID: 478
		public static readonly UIntPtr HKEY_CURRENT_USER = new UIntPtr(2147483649U);
	}
}
