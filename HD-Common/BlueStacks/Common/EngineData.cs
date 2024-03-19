using System;

namespace BlueStacks.Common
{
	// Token: 0x020000A8 RID: 168
	public class EngineData
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000043A3 File Offset: 0x000025A3
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x000043AB File Offset: 0x000025AB
		public GraphicsMode GraphicsMode { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x000043BC File Offset: 0x000025BC
		public bool UseAdvancedGraphicEngine { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000043C5 File Offset: 0x000025C5
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x000043CD File Offset: 0x000025CD
		public bool UseDedicatedGPU { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000043D6 File Offset: 0x000025D6
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x000043DE File Offset: 0x000025DE
		public ASTCOption ASTCOption { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000043E7 File Offset: 0x000025E7
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x000043EF File Offset: 0x000025EF
		public int Ram { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000043F8 File Offset: 0x000025F8
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00004400 File Offset: 0x00002600
		public int CpuCores { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00004409 File Offset: 0x00002609
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00004411 File Offset: 0x00002611
		public int FrameRate { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000441A File Offset: 0x0000261A
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00004422 File Offset: 0x00002622
		public bool EnableHighFrameRates { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000442B File Offset: 0x0000262B
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00004433 File Offset: 0x00002633
		public bool EnableVSync { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000443C File Offset: 0x0000263C
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00004444 File Offset: 0x00002644
		public bool DisplayFPS { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000444D File Offset: 0x0000264D
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00004455 File Offset: 0x00002655
		public ABISetting ABISetting { get; set; }
	}
}
