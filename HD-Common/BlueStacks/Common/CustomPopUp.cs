using System;
using System.Windows.Controls.Primitives;

namespace BlueStacks.Common
{
	// Token: 0x02000116 RID: 278
	public class CustomPopUp : Popup
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x000068D2 File Offset: 0x00004AD2
		public CustomPopUp()
		{
			base.Opened += this.CustomPopUp_Initialized;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000068EC File Offset: 0x00004AEC
		private void CustomPopUp_Initialized(object sender, EventArgs e)
		{
			RenderHelper.ChangeRenderModeToSoftware(sender);
		}
	}
}
