using System;
using BlueStacks.Common;

// Token: 0x02000006 RID: 6
[Serializable]
public class ShortcutKeys : ShortcutConfig
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000E RID: 14 RVA: 0x000021E3 File Offset: 0x000003E3
	// (set) Token: 0x0600000F RID: 15 RVA: 0x000021EB File Offset: 0x000003EB
	public string ShortcutCategory { get; set; }

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F4 File Offset: 0x000003F4
	// (set) Token: 0x06000011 RID: 17 RVA: 0x000021FC File Offset: 0x000003FC
	public string ShortcutName { get; set; }

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002205 File Offset: 0x00000405
	// (set) Token: 0x06000013 RID: 19 RVA: 0x0000220D File Offset: 0x0000040D
	public string ShortcutKey { get; set; }

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000014 RID: 20 RVA: 0x00002216 File Offset: 0x00000416
	// (set) Token: 0x06000015 RID: 21 RVA: 0x0000221E File Offset: 0x0000041E
	public bool ReadOnlyTextbox { get; set; }
}
