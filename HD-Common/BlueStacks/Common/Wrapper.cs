using System;
using System.Windows;

namespace BlueStacks.Common
{
	// Token: 0x02000068 RID: 104
	public class Wrapper : DependencyObject
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000329F File Offset: 0x0000149F
		// (set) Token: 0x0600024F RID: 591 RVA: 0x000032B1 File Offset: 0x000014B1
		public string ErrorMessage
		{
			get
			{
				return (string)base.GetValue(Wrapper.ErrorMessageProperty);
			}
			set
			{
				base.SetValue(Wrapper.ErrorMessageProperty, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000032BF File Offset: 0x000014BF
		// (set) Token: 0x06000251 RID: 593 RVA: 0x000032D1 File Offset: 0x000014D1
		public int Min
		{
			get
			{
				return (int)base.GetValue(Wrapper.MinProperty);
			}
			set
			{
				base.SetValue(Wrapper.MinProperty, value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000032E4 File Offset: 0x000014E4
		// (set) Token: 0x06000253 RID: 595 RVA: 0x000032F6 File Offset: 0x000014F6
		public int Max
		{
			get
			{
				return (int)base.GetValue(Wrapper.MaxProperty);
			}
			set
			{
				base.SetValue(Wrapper.MaxProperty, value);
			}
		}

		// Token: 0x04000116 RID: 278
		public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(Wrapper), new FrameworkPropertyMetadata(""));

		// Token: 0x04000117 RID: 279
		public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(int), typeof(Wrapper), new FrameworkPropertyMetadata(0));

		// Token: 0x04000118 RID: 280
		public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(int), typeof(Wrapper), new FrameworkPropertyMetadata(int.MaxValue));
	}
}
