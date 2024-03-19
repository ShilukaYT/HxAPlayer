using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200012D RID: 301
	public class AppInfo
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00008C07 File Offset: 0x00006E07
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00008C0F File Offset: 0x00006E0F
		public string Name { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00008C18 File Offset: 0x00006E18
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00008C20 File Offset: 0x00006E20
		public string Img { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00008C29 File Offset: 0x00006E29
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00008C31 File Offset: 0x00006E31
		public string Package { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00008C3A File Offset: 0x00006E3A
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00008C42 File Offset: 0x00006E42
		public string Activity { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00008C4B File Offset: 0x00006E4B
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00008C53 File Offset: 0x00006E53
		public string System { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00008C5C File Offset: 0x00006E5C
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00008C64 File Offset: 0x00006E64
		public string Url { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00008C6D File Offset: 0x00006E6D
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00008C75 File Offset: 0x00006E75
		public string Appstore { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00008C7E File Offset: 0x00006E7E
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00008C86 File Offset: 0x00006E86
		public string Version { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00008C8F File Offset: 0x00006E8F
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00008C97 File Offset: 0x00006E97
		public string VersionName { get; set; } = "Unknown";

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00008CA0 File Offset: 0x00006EA0
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public bool Gl3Required { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00008CB1 File Offset: 0x00006EB1
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00008CB9 File Offset: 0x00006EB9
		public bool VideoPresent { get; set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00008CC2 File Offset: 0x00006EC2
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00008CCA File Offset: 0x00006ECA
		public bool IsGamepadCompatible { get; set; }

		// Token: 0x060009D8 RID: 2520 RVA: 0x00008CD3 File Offset: 0x00006ED3
		public AppInfo()
		{
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002A290 File Offset: 0x00028490
		public AppInfo(JObject app)
		{
			this.Name = ((app != null) ? app["name"].ToString() : null);
			this.Img = app["img"].ToString();
			this.Package = app["package"].ToString();
			this.Activity = app["activity"].ToString();
			this.System = app["system"].ToString();
			this.Url = (app.ContainsKey("url") ? app["url"].ToString() : null);
			this.Appstore = (app.ContainsKey("appstore") ? app["appstore"].ToString() : "Unknown");
			this.Version = (app.ContainsKey("version") ? app["version"].ToString() : "Unknown");
			this.Gl3Required = (app.ContainsKey("gl3required") && app["gl3required"].ToObject<bool>());
			this.VideoPresent = (app.ContainsKey("videopresent") && app["videopresent"].ToObject<bool>());
			this.IsGamepadCompatible = (app.ContainsKey("isgamepadcompatible") && app["isgamepadcompatible"].ToObject<bool>());
			if (app.ContainsKey("versionName"))
			{
				this.VersionName = app["versionName"].ToString();
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002A434 File Offset: 0x00028634
		public AppInfo(string InName, string InImage, string InPackage, string InActivity, string InSystem, string InAppStore, string InVersion, bool InGl3required, bool InVideoPresent, string appVersionName, bool isGamepadCompatible = false)
		{
			this.Name = InName;
			this.Img = InImage;
			this.Package = InPackage;
			this.Activity = InActivity;
			this.System = InSystem;
			this.Url = null;
			this.Appstore = InAppStore;
			this.Version = InVersion;
			this.Gl3Required = InGl3required;
			this.VideoPresent = InVideoPresent;
			this.VersionName = appVersionName;
			this.IsGamepadCompatible = isGamepadCompatible;
		}
	}
}
