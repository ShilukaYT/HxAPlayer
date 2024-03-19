using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace BlueStacks.Common.Interop
{
	// Token: 0x0200025A RID: 602
	public static class UUID
	{
		// Token: 0x060012A2 RID: 4770 RVA: 0x00044644 File Offset: 0x00042844
		public static string GetUserGUID()
		{
			string text = null;
			string registryBaseKeyPath = Strings.RegistryBaseKeyPath;
			RegistryKey registryKey2;
			RegistryKey registryKey = registryKey2 = Registry.CurrentUser.OpenSubKey(registryBaseKeyPath);
			try
			{
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("USER_GUID", null);
					if (text != null)
					{
						Logger.Info("TODO: Fix GUID generation. This should not happen. Detected GUID in HKCU: " + text);
						return text;
					}
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					((IDisposable)registryKey2).Dispose();
				}
			}
			registryKey = (registryKey2 = Registry.LocalMachine.OpenSubKey(registryBaseKeyPath));
			try
			{
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("USER_GUID", null);
					if (text != null)
					{
						Logger.Info("TODO: Fix GUID generation. This should not happen. Detected GUID in HKLM: " + text);
						return text;
					}
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					((IDisposable)registryKey2).Dispose();
				}
			}
			try
			{
				string path = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Bst_Guid_Backup");
				if (File.Exists(path))
				{
					string text2 = File.ReadAllText(path);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
						Logger.Info("Detected User GUID %temp%: " + text);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
			return text;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0004476C File Offset: 0x0004296C
		public static string GetGuidFromBackUp()
		{
			string text = string.Empty;
			if (FeatureManager.Instance.IsGuidBackUpEnable)
			{
				text = UUID.GetUserGUID();
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						return new Guid(text).ToString();
					}
					catch
					{
						return string.Empty;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0000F41F File Offset: 0x0000D61F
		public static string Base64Encode(string plainText)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000447D0 File Offset: 0x000429D0
		public static string Base64Decode(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0000F431 File Offset: 0x0000D631
		private static string getBluestacksID()
		{
			return "BGP";
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000447F0 File Offset: 0x000429F0
		private static RegistryKey _openSubKey(RegistryKey parentKey, string subKeyName, bool writable, UUID.RegWow64Options options)
		{
			if (parentKey == null || UUID._getRegistryKeyHandle(parentKey) == IntPtr.Zero)
			{
				return null;
			}
			int num = 131097;
			if (writable)
			{
				num = 131078;
			}
			int value;
			if (UUID.RegOpenKeyEx(UUID._getRegistryKeyHandle(parentKey), subKeyName, 0, num | (int)options, out value) != 0)
			{
				return null;
			}
			return UUID._pointerToRegistryKey((IntPtr)value, writable, false);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0000F438 File Offset: 0x0000D638
		private static IntPtr _getRegistryKeyHandle(RegistryKey registryKey)
		{
			return ((SafeHandle)typeof(RegistryKey).GetField("hkey", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(registryKey)).DangerousGetHandle();
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00044848 File Offset: 0x00042A48
		private static RegistryKey _pointerToRegistryKey(IntPtr hKey, bool writable, bool ownsHandle)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
			Type type = typeof(SafeHandleZeroOrMinusOneIsInvalid).Assembly.GetType("Microsoft.Win32.SafeHandles.SafeRegistryHandle");
			Type[] types = new Type[]
			{
				typeof(IntPtr),
				typeof(bool)
			};
			object obj = type.GetConstructor(bindingAttr, null, types, null).Invoke(new object[]
			{
				hKey,
				ownsHandle
			});
			Type typeFromHandle = typeof(RegistryKey);
			Type[] types2 = new Type[]
			{
				type,
				typeof(bool)
			};
			return (RegistryKey)typeFromHandle.GetConstructor(bindingAttr, null, types2, null).Invoke(new object[]
			{
				obj,
				writable
			});
		}

		// Token: 0x060012AA RID: 4778
		[DllImport("advapi32.dll", CharSet = CharSet.Auto)]
		public static extern int RegOpenKeyEx(IntPtr hKey, string subKey, int ulOptions, int samDesired, out int phkResult);

		// Token: 0x0200025B RID: 603
		private enum RegWow64Options
		{
			// Token: 0x04000E85 RID: 3717
			None,
			// Token: 0x04000E86 RID: 3718
			KEY_WOW64_64KEY = 256,
			// Token: 0x04000E87 RID: 3719
			KEY_WOW64_32KEY = 512
		}

		// Token: 0x0200025C RID: 604
		private enum RegistryRights
		{
			// Token: 0x04000E89 RID: 3721
			ReadKey = 131097,
			// Token: 0x04000E8A RID: 3722
			WriteKey = 131078
		}
	}
}
