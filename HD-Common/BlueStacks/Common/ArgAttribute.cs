using System;

namespace BlueStacks.Common
{
	// Token: 0x020001F7 RID: 503
	[AttributeUsage(AttributeTargets.Field)]
	public class ArgAttribute : Attribute
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0000DEBA File Offset: 0x0000C0BA
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x0000DEC2 File Offset: 0x0000C0C2
		public string Name { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0000DECB File Offset: 0x0000C0CB
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x0000DED3 File Offset: 0x0000C0D3
		public object Value { get; set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		public string Description { get; set; }
	}
}
