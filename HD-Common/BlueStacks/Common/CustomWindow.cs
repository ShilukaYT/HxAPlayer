using System;
using System.Windows;
using System.Windows.Interop;

namespace BlueStacks.Common
{
	// Token: 0x02000221 RID: 545
	public class CustomWindow : Window
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0000E601 File Offset: 0x0000C801
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x0000E609 File Offset: 0x0000C809
		public bool IsClosed { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0000E612 File Offset: 0x0000C812
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x0000E61A File Offset: 0x0000C81A
		public virtual bool ShowWithParentWindow
		{
			get
			{
				return this.mShowWithParentWindow;
			}
			set
			{
				this.mShowWithParentWindow = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0000E623 File Offset: 0x0000C823
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x0000E62B File Offset: 0x0000C82B
		public bool IsShowGLWindow { get; set; }

		// Token: 0x06001100 RID: 4352 RVA: 0x0000E634 File Offset: 0x0000C834
		public CustomWindow()
		{
			this.SetWindowTitle();
			base.SourceInitialized += this.CustomWindow_SourceInitialized;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0000E654 File Offset: 0x0000C854
		private void SetWindowTitle()
		{
			base.Title = base.GetType().Name;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000068EC File Offset: 0x00004AEC
		private void CustomWindow_SourceInitialized(object sender, EventArgs e)
		{
			RenderHelper.ChangeRenderModeToSoftware(sender);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000409CC File Offset: 0x0003EBCC
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
			if (hwndSource != null)
			{
				hwndSource.AddHook(new HwndSourceHook(CustomWindow.WndProc));
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00040A04 File Offset: 0x0003EC04
		private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 260 && (wParam == (IntPtr)18 || wParam == (IntPtr)121))
			{
				handled = true;
			}
			if (msg == 262 && wParam == (IntPtr)32)
			{
				handled = true;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0000E667 File Offset: 0x0000C867
		protected override void OnClosed(EventArgs e)
		{
			this.IsClosed = true;
			base.OnClosed(e);
		}

		// Token: 0x04000B4A RID: 2890
		private bool mShowWithParentWindow;
	}
}
