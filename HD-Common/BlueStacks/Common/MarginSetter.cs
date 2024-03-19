using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200012E RID: 302
	public class MarginSetter : MarkupExtension
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00008CE6 File Offset: 0x00006EE6
		private static Thickness GetMargin(DependencyObject obj)
		{
			return (Thickness)obj.GetValue(MarginSetter.MarginProperty);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00008CF8 File Offset: 0x00006EF8
		public static void SetMargin(DependencyObject obj, Thickness value)
		{
			if (obj != null)
			{
				obj.SetValue(MarginSetter.MarginProperty, value);
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002A4B0 File Offset: 0x000286B0
		public static void CreateThicknesForChildren(object sender, DependencyPropertyChangedEventArgs e)
		{
			Panel panel = sender as Panel;
			if (panel == null)
			{
				return;
			}
			foreach (object obj in panel.Children)
			{
				FrameworkElement frameworkElement = obj as FrameworkElement;
				if (frameworkElement != null)
				{
					frameworkElement.Margin = MarginSetter.GetMargin(panel);
				}
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000046FB File Offset: 0x000028FB
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		// Token: 0x04000502 RID: 1282
		public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(MarginSetter), new UIPropertyMetadata(default(Thickness), new PropertyChangedCallback(MarginSetter.CreateThicknesForChildren)));
	}
}
