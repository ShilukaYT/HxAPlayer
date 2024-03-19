using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace BlueStacks.Common
{
	// Token: 0x02000009 RID: 9
	public static class BlueStacksUtils
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static bool IsSignedByBlueStacks(string filePath)
		{
			Logger.Info("Checking if {0} is signed", new object[]
			{
				filePath
			});
			bool result = false;
			try
			{
				X509Certificate certificate = X509Certificate.CreateFromSignedFile(filePath);
				X509Certificate2 x509Certificate = new X509Certificate2(certificate);
				string name = x509Certificate.IssuerName.Name;
				Logger.Debug("Certificate issuer name is: " + name);
				string name2 = x509Certificate.SubjectName.Name;
				Logger.Debug("Certificate issued by: " + name2);
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				bool flag = currentCulture.CompareInfo.IndexOf(name2, "Bluestack Systems, Inc.", CompareOptions.IgnoreCase) >= 0;
				if (flag)
				{
					Logger.Info("File signed by BlueStacks");
					bool flag2 = x509Certificate.Verify();
					if (flag2)
					{
						Logger.Info("Certificate verified");
						result = true;
					}
					else
					{
						Logger.Warning("Certificate not verified");
					}
				}
				else
				{
					Logger.Warning("File not signed by BlueStacks. Signed by {0}", new object[]
					{
						name2
					});
				}
			}
			catch (Exception ex)
			{
				Logger.Error("File not signed");
				Logger.Error(ex.ToString());
			}
			return result;
		}
	}
}
