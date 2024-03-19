using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x0200010B RID: 267
	public static class ConsoleControl
	{
		// Token: 0x06000712 RID: 1810
		[DllImport("Kernel32")]
		private static extern bool SetConsoleCtrlHandler(ConsoleControl.Handler handler, bool Add);

		// Token: 0x06000713 RID: 1811 RVA: 0x00006170 File Offset: 0x00004370
		public static void SetHandler(ConsoleControl.Handler handler)
		{
			ConsoleControl.SetConsoleCtrlHandler(handler, true);
		}

		// Token: 0x0200010C RID: 268
		// (Invoke) Token: 0x06000715 RID: 1813
		public delegate bool Handler(CtrlType ctrlType);
	}
}
