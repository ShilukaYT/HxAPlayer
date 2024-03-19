using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BlueStacks.Common
{
	// Token: 0x02000119 RID: 281
	public class CustomToastPopupControl : UserControl, IComponentConnector
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00006A46 File Offset: 0x00004C46
		public CustomToastPopupControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00025A90 File Offset: 0x00023C90
		public CustomToastPopupControl(Window window)
		{
			this.InitializeComponent();
			if (window != null)
			{
				this.ParentWindow = window;
				Grid grid = new Grid();
				object content = window.Content;
				window.Content = grid;
				grid.Children.Add(content as UIElement);
				grid.Children.Add(this);
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00025AE8 File Offset: 0x00023CE8
		public CustomToastPopupControl(UserControl control)
		{
			this.InitializeComponent();
			if (control != null)
			{
				this.ParentControl = control;
				Grid grid = new Grid();
				object content = control.Content;
				control.Content = grid;
				grid.Children.Add(content as UIElement);
				grid.Children.Add(this);
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00025B40 File Offset: 0x00023D40
		public void Init(Window window, string text, Brush background = null, Brush borderBackground = null, HorizontalAlignment horizontalAlign = HorizontalAlignment.Center, VerticalAlignment verticalAlign = VerticalAlignment.Bottom, Thickness? margin = null, int cornerRadius = 12, Thickness? toastTextMargin = null, Brush toastTextForeground = null, bool isShowCloseIcon = false, bool isShowVerticalSeparator = false)
		{
			this.mToastIcon.Visibility = Visibility.Collapsed;
			if (window != null)
			{
				this.ParentWindow = window;
			}
			if (isShowCloseIcon)
			{
				this.mToastCloseIcon.Visibility = Visibility.Visible;
			}
			else
			{
				this.mToastCloseIcon.Visibility = Visibility.Collapsed;
			}
			this.mVerticalSeparator.Visibility = (isShowVerticalSeparator ? Visibility.Visible : Visibility.Collapsed);
			this.InitProperties(0, text, background, borderBackground, horizontalAlign, verticalAlign, margin, cornerRadius, toastTextMargin, toastTextForeground, isShowCloseIcon, isShowVerticalSeparator);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00025BB0 File Offset: 0x00023DB0
		public void Init(UserControl control, string text, Brush background = null, Brush borderBackground = null, HorizontalAlignment horizontalAlign = HorizontalAlignment.Center, VerticalAlignment verticalAlign = VerticalAlignment.Bottom, Thickness? margin = null, int cornerRadius = 12, Thickness? toastTextMargin = null, Brush toastTextForeground = null)
		{
			this.mToastIcon.Visibility = Visibility.Collapsed;
			if (control != null)
			{
				this.ParentControl = control;
			}
			this.InitProperties(1, text, background, borderBackground, horizontalAlign, verticalAlign, margin, cornerRadius, toastTextMargin, toastTextForeground, false, false);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00025BEC File Offset: 0x00023DEC
		private void InitProperties(int callType, string text, Brush background = null, Brush borderBackground = null, HorizontalAlignment horizontalAlign = HorizontalAlignment.Center, VerticalAlignment verticalAlign = VerticalAlignment.Bottom, Thickness? margin = null, int cornerRadius = 12, Thickness? toastTextMargin = null, Brush toastTextForeground = null, bool isCloseIconVisible = false, bool isVerticalSeparatorVisible = false)
		{
			if (background == null)
			{
				this.mToastPopupBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AE000000"));
			}
			else
			{
				this.mToastPopupBorder.Background = background;
			}
			if (borderBackground == null)
			{
				this.mToastPopupBorder.BorderThickness = new Thickness(0.0);
			}
			else
			{
				this.mToastPopupBorder.BorderBrush = borderBackground;
				this.mToastPopupBorder.BorderThickness = new Thickness(1.0);
			}
			if (margin == null)
			{
				this.mToastPopupBorder.Margin = new Thickness(0.0, 0.0, 0.0, 40.0);
			}
			else
			{
				this.mToastPopupBorder.Margin = margin.Value;
			}
			if (toastTextMargin == null)
			{
				this.mToastTextblock.Margin = new Thickness(0.0);
			}
			else
			{
				this.mToastTextblock.Margin = toastTextMargin.Value;
			}
			if (toastTextForeground == null)
			{
				this.mToastTextblock.Foreground = Brushes.White;
			}
			else
			{
				this.mToastTextblock.Foreground = toastTextForeground;
			}
			this.mToastTextblock.FontSize = 15.0;
			this.mVerticalSeparator.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#46474A"));
			this.mToastPopupBorder.CornerRadius = new CornerRadius((double)cornerRadius);
			this.mToastTextblock.Text = text;
			this.mToastPopupBorder.VerticalAlignment = verticalAlign;
			this.mToastPopupBorder.HorizontalAlignment = horizontalAlign;
			this.mToastTextblock.TextWrapping = TextWrapping.WrapWithOverflow;
			this.mToastCloseIcon.Margin = (isVerticalSeparatorVisible ? new Thickness(20.0, 0.0, 0.0, 0.0) : new Thickness(8.0, 0.0, 2.0, 0.0));
			this.mToastCloseIcon.Height = (double)(isVerticalSeparatorVisible ? 16 : 12);
			this.mToastCloseIcon.Width = (double)(isVerticalSeparatorVisible ? 16 : 12);
			if (callType == 0)
			{
				this.mToastTextblock.MaxWidth = this.ParentWindow.ActualWidth - (double)cornerRadius - 15.0;
			}
			else
			{
				this.mToastTextblock.MaxWidth = this.ParentControl.ActualWidth - (double)cornerRadius - 15.0;
			}
			this.mToastTextblock.TextAlignment = TextAlignment.Center;
			if (isCloseIconVisible)
			{
				if (isVerticalSeparatorVisible)
				{
					this.mToastTextblock.FontSize = 16.0;
					this.mToastTextblock.Margin = new Thickness(0.0, 6.0, 0.0, 6.0);
					Style style = base.FindResource("ShadowBorder") as Style;
					if (style != null)
					{
						this.mToastPopupBorder.Style = style;
					}
					this.mToastTextblock.MaxWidth = this.ParentWindow.ActualWidth - (double)cornerRadius - 90.0;
					return;
				}
				this.mToastTextblock.MaxWidth = this.ParentWindow.ActualWidth - (double)cornerRadius - 30.0;
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00025F38 File Offset: 0x00024138
		public void AddImage(string imageName, double height = 0.0, double width = 0.0, Thickness? margin = null)
		{
			this.mToastIcon.ImageName = imageName;
			if (height != 0.0)
			{
				this.mToastIcon.Height = height;
			}
			if (width != 0.0)
			{
				this.mToastIcon.Width = width;
			}
			if (margin != null)
			{
				this.mToastIcon.Margin = margin.Value;
			}
			this.mToastIcon.Visibility = Visibility.Visible;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00025FA8 File Offset: 0x000241A8
		public void ShowPopup(double seconds = 1.3)
		{
			base.Visibility = Visibility.Visible;
			base.Opacity = 0.0;
			DoubleAnimation doubleAnimation = new DoubleAnimation
			{
				From = new double?(0.0),
				To = new double?(seconds),
				Duration = new Duration(TimeSpan.FromSeconds(0.3))
			};
			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(doubleAnimation);
			Storyboard.SetTarget(doubleAnimation, this);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(UIElement.OpacityProperty));
			storyboard.Completed += delegate(object <p0>, EventArgs <p1>)
			{
				this.Visibility = Visibility.Visible;
				DoubleAnimation doubleAnimation2 = new DoubleAnimation
				{
					From = new double?(seconds),
					To = new double?(0.0),
					FillBehavior = FillBehavior.Stop,
					BeginTime = new TimeSpan?(TimeSpan.FromSeconds(seconds)),
					Duration = new Duration(TimeSpan.FromSeconds(seconds / 2.0))
				};
				Storyboard storyboard2 = new Storyboard();
				storyboard2.Children.Add(doubleAnimation2);
				Storyboard.SetTarget(doubleAnimation2, this);
				Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(UIElement.OpacityProperty));
				storyboard2.Completed += delegate(object <p0>, EventArgs <p1>)
				{
					this.Visibility = Visibility.Collapsed;
				};
				storyboard2.Begin();
			};
			storyboard.Begin();
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00006A54 File Offset: 0x00004C54
		private void ToastCloseIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00026064 File Offset: 0x00024264
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customtoastpopupcontrol.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00026094 File Offset: 0x00024294
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mToastPopupBorder = (Border)target;
				return;
			case 2:
				this.mToastIcon = (CustomPictureBox)target;
				return;
			case 3:
				this.mToastTextblock = (TextBlock)target;
				return;
			case 4:
				this.mVerticalSeparator = (Grid)target;
				return;
			case 5:
				this.mToastCloseIcon = (CustomPictureBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000414 RID: 1044
		private Window ParentWindow;

		// Token: 0x04000415 RID: 1045
		private UserControl ParentControl;

		// Token: 0x04000416 RID: 1046
		internal Border mToastPopupBorder;

		// Token: 0x04000417 RID: 1047
		internal CustomPictureBox mToastIcon;

		// Token: 0x04000418 RID: 1048
		internal TextBlock mToastTextblock;

		// Token: 0x04000419 RID: 1049
		internal Grid mVerticalSeparator;

		// Token: 0x0400041A RID: 1050
		internal CustomPictureBox mToastCloseIcon;

		// Token: 0x0400041B RID: 1051
		private bool _contentLoaded;
	}
}
