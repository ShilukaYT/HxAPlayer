using System;
using System.Windows;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x02000222 RID: 546
	public static class WpfUtils
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0000E677 File Offset: 0x0000C877
		public static double PrimaryWidth
		{
			get
			{
				return SystemParameters.PrimaryScreenWidth;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0000E67E File Offset: 0x0000C87E
		public static double PrimaryHeight
		{
			get
			{
				return SystemParameters.PrimaryScreenHeight;
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00040A5C File Offset: 0x0003EC5C
		public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is T)
				{
					return (T)((object)child);
				}
				T t = WpfUtils.FindVisualChild<T>(child);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00040AB0 File Offset: 0x0003ECB0
		public static void GetDefaultSize(out double width, out double height, out double left, double aspectRatio, bool isGMWindow)
		{
			int num;
			if (WpfUtils.PrimaryWidth * 0.8 / aspectRatio <= WpfUtils.PrimaryHeight * 0.8)
			{
				num = (int)(WpfUtils.PrimaryWidth * 0.8);
			}
			else
			{
				num = (int)(WpfUtils.PrimaryHeight * 0.8 * aspectRatio);
			}
			if (!isGMWindow)
			{
				width = (double)(num / 4 * 3);
				left = (double)((int)(WpfUtils.PrimaryWidth - (double)num) / 2);
			}
			else
			{
				width = (double)num;
				left = (double)((int)(WpfUtils.PrimaryWidth - (double)num) / 2);
			}
			if (width < 912.0)
			{
				width = 912.0;
				left = 20.0;
			}
			height = (double)((int)width / 16 * 9);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00040B68 File Offset: 0x0003ED68
		public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
		{
			DependencyObject parent = VisualTreeHelper.GetParent(child);
			if (parent == null)
			{
				return default(T);
			}
			T t = parent as T;
			if (t != null)
			{
				return t;
			}
			return WpfUtils.FindVisualParent<T>(parent);
		}
	}
}
