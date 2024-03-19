using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public class AppPackageListObject
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00003653 File Offset: 0x00001853
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000365B File Offset: 0x0000185B
		[JsonProperty(PropertyName = "app_pkg_list", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public List<AppPackageObject> CloudPackageList { get; set; } = new List<AppPackageObject>();

		// Token: 0x060002AD RID: 685 RVA: 0x00003664 File Offset: 0x00001864
		public AppPackageListObject()
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00003677 File Offset: 0x00001877
		public AppPackageListObject(List<AppPackageObject> packageList)
		{
			if (packageList != null)
			{
				this.CloudPackageList = packageList;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0001452C File Offset: 0x0001272C
		public AppPackageListObject(List<string> packageList)
		{
			if (packageList != null)
			{
				List<AppPackageObject> list = new List<AppPackageObject>();
				foreach (string package in packageList)
				{
					list.Add(new AppPackageObject
					{
						Package = package
					});
				}
				this.CloudPackageList = list;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000145A8 File Offset: 0x000127A8
		public bool IsPackageAvailable(string appPackage)
		{
			foreach (AppPackageObject appPackageObject in this.CloudPackageList)
			{
				string text = appPackageObject.Package;
				if (appPackageObject.Package.EndsWith("*", StringComparison.InvariantCulture))
				{
					text = appPackageObject.Package.TrimEnd(new char[]
					{
						'*'
					});
				}
				if (text.StartsWith("~", StringComparison.InvariantCulture))
				{
					if (appPackage.StartsWith(text.Substring(1), StringComparison.InvariantCulture))
					{
						return false;
					}
				}
				else if (appPackage.StartsWith(text, StringComparison.InvariantCulture))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00014658 File Offset: 0x00012858
		public AppPackageObject GetAppPackageObject(string appPackage)
		{
			foreach (AppPackageObject appPackageObject in this.CloudPackageList)
			{
				string text = appPackageObject.Package;
				if (appPackageObject.Package.EndsWith("*", StringComparison.InvariantCulture))
				{
					text = appPackageObject.Package.TrimEnd(new char[]
					{
						'*'
					});
				}
				if (text.StartsWith("~", StringComparison.InvariantCulture))
				{
					if (appPackage.StartsWith(text.Substring(1), StringComparison.InvariantCulture))
					{
						return null;
					}
				}
				else if (appPackage.StartsWith(text, StringComparison.InvariantCulture))
				{
					return appPackageObject;
				}
			}
			return null;
		}
	}
}
