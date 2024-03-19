using System;
using System.Windows;

namespace BlueStacks.Common
{
	// Token: 0x02000067 RID: 103
	public class BindingProxy : Freezable
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000324A File Offset: 0x0000144A
		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxy();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00003251 File Offset: 0x00001451
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000325E File Offset: 0x0000145E
		public object Data
		{
			get
			{
				return base.GetValue(BindingProxy.DataProperty);
			}
			set
			{
				base.SetValue(BindingProxy.DataProperty, value);
			}
		}

		// Token: 0x04000115 RID: 277
		public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
	}
}
