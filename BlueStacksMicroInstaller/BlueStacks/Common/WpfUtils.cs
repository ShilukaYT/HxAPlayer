using System;
using System.Windows;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x0200006F RID: 111
	public static class WpfUtils
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A228 File Offset: 0x00008428
		public static double PrimaryWidth
		{
			get
			{
				return SystemParameters.PrimaryScreenWidth;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A22F File Offset: 0x0000842F
		public static double PrimaryHeight
		{
			get
			{
				return SystemParameters.PrimaryScreenHeight;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A238 File Offset: 0x00008438
		public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
		{
			int i = 0;
			while (i < VisualTreeHelper.GetChildrenCount(obj))
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				bool flag = child != null && child is T;
				T result;
				if (flag)
				{
					result = (T)((object)child);
				}
				else
				{
					T t = WpfUtils.FindVisualChild<T>(child);
					bool flag2 = t != null;
					if (!flag2)
					{
						i++;
						continue;
					}
					result = t;
				}
				return result;
			}
			return default(T);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public static void GetDefaultSize(out double width, out double height, out double left, double aspectRatio, bool isGMWindow)
		{
			bool flag = WpfUtils.PrimaryWidth * 0.8 / aspectRatio <= WpfUtils.PrimaryHeight * 0.8;
			int num;
			if (flag)
			{
				num = (int)(WpfUtils.PrimaryWidth * 0.8);
			}
			else
			{
				num = (int)(WpfUtils.PrimaryHeight * 0.8 * aspectRatio);
			}
			bool flag2 = !isGMWindow;
			if (flag2)
			{
				width = (double)(num / 4 * 3);
				left = (double)((int)(WpfUtils.PrimaryWidth - (double)num) / 2);
			}
			else
			{
				width = (double)num;
				left = (double)((int)(WpfUtils.PrimaryWidth - (double)num) / 2);
			}
			bool flag3 = width < 912.0;
			if (flag3)
			{
				width = 912.0;
				left = 20.0;
			}
			height = (double)((int)width / 16 * 9);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A388 File Offset: 0x00008588
		public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
		{
			DependencyObject parent = VisualTreeHelper.GetParent(child);
			bool flag = parent == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				T t = parent as T;
				bool flag2 = t != null;
				if (flag2)
				{
					result = t;
				}
				else
				{
					result = WpfUtils.FindVisualParent<T>(parent);
				}
			}
			return result;
		}
	}
}
