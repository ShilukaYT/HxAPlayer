using System;

namespace BlueStacks.Common
{
	// Token: 0x020000F2 RID: 242
	public static class Tasks
	{
		// Token: 0x020000F3 RID: 243
		public enum Parameter
		{
			// Token: 0x04000334 RID: 820
			Create,
			// Token: 0x04000335 RID: 821
			Delete,
			// Token: 0x04000336 RID: 822
			Query,
			// Token: 0x04000337 RID: 823
			Run,
			// Token: 0x04000338 RID: 824
			End
		}

		// Token: 0x020000F4 RID: 244
		public enum Frequency
		{
			// Token: 0x0400033A RID: 826
			MINUTE,
			// Token: 0x0400033B RID: 827
			HOURLY,
			// Token: 0x0400033C RID: 828
			DAILY,
			// Token: 0x0400033D RID: 829
			WEEKLY,
			// Token: 0x0400033E RID: 830
			MONTHLY,
			// Token: 0x0400033F RID: 831
			ONCE,
			// Token: 0x04000340 RID: 832
			ONSTART,
			// Token: 0x04000341 RID: 833
			ONLOGON,
			// Token: 0x04000342 RID: 834
			ONIDLE,
			// Token: 0x04000343 RID: 835
			ONEVENT
		}

		// Token: 0x020000F5 RID: 245
		public enum Modifiers
		{
			// Token: 0x04000345 RID: 837
			MON,
			// Token: 0x04000346 RID: 838
			TUE,
			// Token: 0x04000347 RID: 839
			WED,
			// Token: 0x04000348 RID: 840
			THU,
			// Token: 0x04000349 RID: 841
			FRI,
			// Token: 0x0400034A RID: 842
			SAT,
			// Token: 0x0400034B RID: 843
			SUN
		}

		// Token: 0x020000F6 RID: 246
		public enum Days
		{
			// Token: 0x0400034D RID: 845
			MON,
			// Token: 0x0400034E RID: 846
			TUE,
			// Token: 0x0400034F RID: 847
			WED,
			// Token: 0x04000350 RID: 848
			THU,
			// Token: 0x04000351 RID: 849
			FRI,
			// Token: 0x04000352 RID: 850
			SAT,
			// Token: 0x04000353 RID: 851
			SUN
		}
	}
}
