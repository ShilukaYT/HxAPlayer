using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x02000070 RID: 112
	public static class RenderHelper
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000A3E4 File Offset: 0x000085E4
		private static bool SoftwareOnly
		{
			get
			{
				bool flag = RenderHelper.mSoftwareOnly == null;
				if (flag)
				{
					RenderHelper.mSoftwareOnly = new bool?(false);
				}
				return RenderHelper.mSoftwareOnly.Value;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A420 File Offset: 0x00008620
		public static void ChangeRenderModeToSoftware(object sender)
		{
			bool softwareOnly = RenderHelper.SoftwareOnly;
			if (softwareOnly)
			{
				Visual visual = (Visual)sender;
				bool flag = visual != null;
				if (flag)
				{
					HwndSource hwndSource = PresentationSource.FromVisual(visual) as HwndSource;
					bool flag2 = hwndSource != null;
					if (flag2)
					{
						Logger.Info("hwnd Source :" + sender.ToString());
						hwndSource.CompositionTarget.RenderMode = RenderMode.SoftwareOnly;
					}
					else
					{
						Logger.Info("sender = " + sender.ToString());
					}
				}
			}
		}

		// Token: 0x04000206 RID: 518
		private static bool? mSoftwareOnly;
	}
}
