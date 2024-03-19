using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x02000223 RID: 547
	public static class RenderHelper
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0000E685 File Offset: 0x0000C885
		private static bool SoftwareOnly
		{
			get
			{
				if (RenderHelper.mSoftwareOnly == null)
				{
					RenderHelper.mSoftwareOnly = new bool?(false);
				}
				return RenderHelper.mSoftwareOnly.Value;
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00040BA8 File Offset: 0x0003EDA8
		public static void ChangeRenderModeToSoftware(object sender)
		{
			if (RenderHelper.SoftwareOnly)
			{
				Visual visual = (Visual)sender;
				if (visual != null)
				{
					HwndSource hwndSource = PresentationSource.FromVisual(visual) as HwndSource;
					if (hwndSource != null)
					{
						Logger.Info("hwnd Source :" + sender.ToString());
						hwndSource.CompositionTarget.RenderMode = RenderMode.SoftwareOnly;
						return;
					}
					Logger.Info("sender = " + sender.ToString());
				}
			}
		}

		// Token: 0x04000B4C RID: 2892
		private static bool? mSoftwareOnly;
	}
}
