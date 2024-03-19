using System;
using System.Windows;
using System.Windows.Interop;

namespace BlueStacks.Common
{
	// Token: 0x0200006E RID: 110
	public class CustomWindow : Window
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000A0DA File Offset: 0x000082DA
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000A0E2 File Offset: 0x000082E2
		public bool IsClosed { get; private set; } = false;

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000A0EB File Offset: 0x000082EB
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000A0F3 File Offset: 0x000082F3
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A0FC File Offset: 0x000082FC
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000A104 File Offset: 0x00008304
		public bool IsShowGLWindow { get; set; } = false;

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A10D File Offset: 0x0000830D
		public CustomWindow()
		{
			this.SetWindowTitle();
			base.SourceInitialized += this.CustomWindow_SourceInitialized;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A146 File Offset: 0x00008346
		private void SetWindowTitle()
		{
			base.Title = base.GetType().Name;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A15B File Offset: 0x0000835B
		private void CustomWindow_SourceInitialized(object sender, EventArgs e)
		{
			RenderHelper.ChangeRenderModeToSoftware(sender);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A168 File Offset: 0x00008368
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
			bool flag = hwndSource != null;
			if (flag)
			{
				hwndSource.AddHook(new HwndSourceHook(CustomWindow.WndProc));
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A1A8 File Offset: 0x000083A8
		private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			bool flag = msg == 260 && (wParam == (IntPtr)18 || wParam == (IntPtr)121);
			if (flag)
			{
				handled = true;
			}
			bool flag2 = msg == 262 && wParam == (IntPtr)32;
			if (flag2)
			{
				handled = true;
			}
			return IntPtr.Zero;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A215 File Offset: 0x00008415
		protected override void OnClosed(EventArgs e)
		{
			this.IsClosed = true;
			base.OnClosed(e);
		}

		// Token: 0x04000204 RID: 516
		private bool mShowWithParentWindow = false;
	}
}
