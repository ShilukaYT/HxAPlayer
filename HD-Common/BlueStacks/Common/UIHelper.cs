using System;
using System.Windows.Controls;
using System.Windows.Forms;

namespace BlueStacks.Common
{
	// Token: 0x02000159 RID: 345
	public static class UIHelper
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x0000C717 File Offset: 0x0000A917
		public static void SetDispatcher(UIHelper.dispatcher gameManagerWindowDispatcher)
		{
			UIHelper.obj = gameManagerWindowDispatcher;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002F42C File Offset: 0x0002D62C
		public static void RunOnUIThread(System.Windows.Forms.Control control, UIHelper.Action action)
		{
			if (UIHelper.obj == null)
			{
				if (control != null && control.InvokeRequired)
				{
					control.Invoke(action);
					return;
				}
				if (action != null)
				{
					action();
					return;
				}
			}
			else if (control != null && control.InvokeRequired)
			{
				UIHelper.obj(action, new object[0]);
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0000C71F File Offset: 0x0000A91F
		public static void AssertUIThread(System.Windows.Forms.Control control)
		{
			if (control != null && control.InvokeRequired)
			{
				throw new ApplicationException("Not running on UI thread");
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0000C737 File Offset: 0x0000A937
		public static void AssertUIThread(System.Windows.Controls.Control control)
		{
			if (control != null && !control.Dispatcher.CheckAccess())
			{
				throw new ApplicationException("Not running on UI thread");
			}
		}

		// Token: 0x04000634 RID: 1588
		private static UIHelper.dispatcher obj;

		// Token: 0x0200015A RID: 346
		// (Invoke) Token: 0x06000D01 RID: 3329
		public delegate object dispatcher(Delegate method, params object[] args);

		// Token: 0x0200015B RID: 347
		// (Invoke) Token: 0x06000D05 RID: 3333
		public delegate void Action();
	}
}
