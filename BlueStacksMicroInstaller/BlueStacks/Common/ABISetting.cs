using System;
using System.ComponentModel;

namespace BlueStacks.Common
{
	// Token: 0x02000029 RID: 41
	public enum ABISetting
	{
		// Token: 0x040000E1 RID: 225
		[Description("4")]
		ARM,
		// Token: 0x040000E2 RID: 226
		[Description("15")]
		Auto,
		// Token: 0x040000E3 RID: 227
		[Description("15")]
		ARM64,
		// Token: 0x040000E4 RID: 228
		[Description("7")]
		Auto64,
		// Token: 0x040000E5 RID: 229
		[Description("-1")]
		Custom
	}
}
