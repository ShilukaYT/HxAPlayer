using System;
using System.Windows.Forms;

namespace BlueStacks.Common
{
	// Token: 0x02000168 RID: 360
	internal class OldWindow : IWin32Window
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x0000C931 File Offset: 0x0000AB31
		public OldWindow(IntPtr handle)
		{
			this._handle = handle;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0000C940 File Offset: 0x0000AB40
		IntPtr IWin32Window.Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x04000651 RID: 1617
		private readonly IntPtr _handle;
	}
}
