using System;
using System.ComponentModel;

namespace BlueStacks.Common
{
	// Token: 0x020001BA RID: 442
	public enum ABISetting
	{
		// Token: 0x04000808 RID: 2056
		[Description("4")]
		ARM,
		// Token: 0x04000809 RID: 2057
		[Description("15")]
		Auto,
		// Token: 0x0400080A RID: 2058
		[Description("15")]
		ARM64,
		// Token: 0x0400080B RID: 2059
		[Description("7")]
		Auto64,
		// Token: 0x0400080C RID: 2060
		[Description("-1")]
		Custom
	}
}
