using System;

namespace BlueStacks.Common
{
	// Token: 0x02000047 RID: 71
	public static class ErrorCodes
	{
		// Token: 0x04000194 RID: 404
		public const int Success = 0;

		// Token: 0x04000195 RID: 405
		public const int ConnectFailure = 1;

		// Token: 0x04000196 RID: 406
		public const int ReceiveFailure = 2;

		// Token: 0x04000197 RID: 407
		public const int Timeout = 3;

		// Token: 0x04000198 RID: 408
		public const int UnexpectedHttpStatusCode = 4;

		// Token: 0x04000199 RID: 409
		public const int ResumeNotSupported = 5;

		// Token: 0x0400019A RID: 410
		public const int UnknownError = 99;
	}
}
