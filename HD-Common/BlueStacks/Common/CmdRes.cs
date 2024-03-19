using System;

namespace BlueStacks.Common
{
	// Token: 0x02000135 RID: 309
	public class CmdRes
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0000BF92 File Offset: 0x0000A192
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0000BF9A File Offset: 0x0000A19A
		public string StdOut { get; set; } = string.Empty;

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0000BFA3 File Offset: 0x0000A1A3
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0000BFAB File Offset: 0x0000A1AB
		public string StdErr { get; set; } = string.Empty;

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public int ExitCode { get; set; }
	}
}
