using System;

namespace BlueStacks.Common
{
	// Token: 0x0200005C RID: 92
	[AttributeUsage(AttributeTargets.Field)]
	public class ArgAttribute : Attribute
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005DF2 File Offset: 0x00003FF2
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00005DFA File Offset: 0x00003FFA
		public string Name { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005E03 File Offset: 0x00004003
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00005E0B File Offset: 0x0000400B
		public object Value { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005E14 File Offset: 0x00004014
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005E1C File Offset: 0x0000401C
		public string Description { get; set; }
	}
}
