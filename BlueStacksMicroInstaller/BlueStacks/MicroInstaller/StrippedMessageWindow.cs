using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x020000A0 RID: 160
	public class StrippedMessageWindow : CustomWindow, IComponentConnector
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00017CF0 File Offset: 0x00015EF0
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public double ContentMaxWidth
		{
			get
			{
				return this.mContentMaxWidth;
			}
			set
			{
				this.mContentMaxWidth = value;
				this.mTextBlockGrid.MaxWidth = value;
				this.mBodyTextStackPanel.MaxWidth = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00017D1C File Offset: 0x00015F1C
		public bool IsWindowMinizable
		{
			set
			{
				if (value)
				{
					this.mCustomMessageBoxMinimizeButton.Visibility = Visibility.Visible;
				}
				else
				{
					this.mCustomMessageBoxMinimizeButton.Visibility = Visibility.Hidden;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00017D50 File Offset: 0x00015F50
		public bool IsWindowClosable
		{
			set
			{
				if (value)
				{
					this.mCustomMessageBoxCloseButton.Visibility = Visibility.Visible;
				}
				else
				{
					this.mCustomMessageBoxCloseButton.Visibility = Visibility.Hidden;
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00017D84 File Offset: 0x00015F84
		public bool IsWindowCloseButtonDisabled
		{
			set
			{
				if (value)
				{
					this.mCustomMessageBoxCloseButton.ToolTip = null;
					this.mCustomMessageBoxCloseButton.PreviewMouseLeftButtonUp -= this.Close_PreviewMouseLeftButtonUp;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00017DC0 File Offset: 0x00015FC0
		public bool IsWithoutButtons
		{
			set
			{
				if (value)
				{
					this.mButtonsStackPanel.Visibility = Visibility.Collapsed;
				}
				else
				{
					this.mButtonsStackPanel.Visibility = Visibility.Visible;
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public TextBlock TitleTextBlock
		{
			get
			{
				return this.mTitleText;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00017E08 File Offset: 0x00016008
		public TextBlock BodyTextBlock
		{
			get
			{
				return this.mBodyTextBlock;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00017E20 File Offset: 0x00016020
		public StrippedMessageWindow()
		{
			this.InitializeComponent();
			base.Loaded += this.CustomMessageWindow_Loaded;
			base.SizeChanged += this.CustomMessageWindow_SizeChanged;
			this.mButtonsStackPanel.Children.Clear();
			this.ContentMaxWidth = 340.0;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00017EB4 File Offset: 0x000160B4
		private void CustomMessageWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			bool flag = this.mButtonsStackPanel.ActualWidth > this.ContentMaxWidth;
			if (flag)
			{
				bool flag2 = this.button2 != null;
				if (flag2)
				{
					this.mButtonsStackPanel.Orientation = Orientation.Vertical;
					this.mButtonsStackPanel.Height = 90.0;
					this.button1.Width = this.ContentMaxWidth;
					this.button1.Height = 36.0;
					this.button2.Width = this.ContentMaxWidth;
					this.button2.Height = 36.0;
					this.button2.Margin = new Thickness(0.0, 15.0, 0.0, 0.0);
					this.FitEnlargedButtonText(this.button1);
					this.FitEnlargedButtonText(this.button2);
				}
				else
				{
					this.button1.MaxWidth = this.ContentMaxWidth;
					this.FitEnlargedButtonText(this.button1);
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00017FD4 File Offset: 0x000161D4
		private void FitEnlargedButtonText(StrippedButton button)
		{
			double num = this.ContentMaxWidth - 60.0;
			DataTemplate contentTemplate = button.Resources["TextContentTemplate"] as DataTemplate;
			button.ContentTemplate = contentTemplate;
			ContentPresenter contentPresenter = WpfUtils.FindVisualChild<ContentPresenter>(button);
			contentPresenter.ApplyTemplate();
			TextBlock textBlock = (TextBlock)contentPresenter.ContentTemplate.FindName("wrapTextBlock", contentPresenter);
			textBlock.FontSize = 15.0;
			textBlock.TextWrapping = TextWrapping.NoWrap;
			textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
			textBlock.FlowDirection = FlowDirection.LeftToRight;
			Typeface typeface = new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch);
			FormattedText formattedText = new FormattedText(textBlock.Text, Thread.CurrentThread.CurrentCulture, textBlock.FlowDirection, typeface, textBlock.FontSize, textBlock.Foreground);
			double widthIncludingTrailingWhitespace = formattedText.WidthIncludingTrailingWhitespace;
			bool flag = widthIncludingTrailingWhitespace > num;
			if (flag)
			{
				button.ToolTip = textBlock.Text;
			}
			textBlock.MaxWidth = num;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000180D4 File Offset: 0x000162D4
		public void CustomMessageWindow_Loaded(object sender, RoutedEventArgs e)
		{
			bool flag = base.Owner == null || InteropWindow.FindMainWindowState(this) == WindowState.Minimized;
			if (flag)
			{
				base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}
			bool flag2 = this.button2 != null;
			if (flag2)
			{
				base.UpdateLayout();
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001811A File Offset: 0x0001631A
		public void CloseButtonHandle(EventHandler handle, object data = null)
		{
			this.mCloseButtonEventHandler = handle;
			this.mCloseButtonEventdata = data;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001812B File Offset: 0x0001632B
		public void AddButton(ButtonColors color, string text, EventHandler handle, string image = null, bool ChangeImageAlignment = false, object data = null)
		{
			this.AddButtonInUI(new StrippedButton(color), color, text, handle, image, ChangeImageAlignment, data);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00018144 File Offset: 0x00016344
		public void AddButtonInUI(StrippedButton button, ButtonColors color, string text, EventHandler handle, string image, bool ChangeImageAlignment, object data)
		{
			bool flag = this.button1 == null;
			if (flag)
			{
				this.button1 = button;
			}
			else
			{
				this.button2 = button;
				button.Margin = new Thickness(15.0, 0.0, 0.0, 0.0);
			}
			button.Click += this.Button_Click;
			button.MinWidth = 140.0;
			button.Visibility = Visibility.Visible;
			button.Content = text;
			this.mButtonsStackPanel.Children.Add(button);
			this.mdictActions.Add(button, new Tuple<ButtonColors, EventHandler, object>(color, handle, data));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00018200 File Offset: 0x00016400
		public void Button_Click(object sender, RoutedEventArgs e)
		{
			this.ClickedButton = this.mdictActions[sender].Item1;
			bool flag = this.mdictActions[sender].Item2 != null;
			if (flag)
			{
				this.mdictActions[sender].Item2(this.mdictActions[sender].Item3, new EventArgs());
			}
			this.CloseWindow();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00018273 File Offset: 0x00016473
		public void CloseWindow()
		{
			base.Close();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00017649 File Offset: 0x00015849
		private void Minimize_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001827D File Offset: 0x0001647D
		private void Close_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.CloseWindow();
			EventHandler eventHandler = this.mCloseButtonEventHandler;
			if (eventHandler != null)
			{
				eventHandler(this.mCloseButtonEventdata, new EventArgs());
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000182A4 File Offset: 0x000164A4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/BlueStacksMicroInstaller;component/controls/strippedmessagewindow.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000182DC File Offset: 0x000164DC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000182F8 File Offset: 0x000164F8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mBorder = (Border)target;
				break;
			case 2:
				this.mMaskBorder = (Border)target;
				break;
			case 3:
				this.mParentGrid = (Grid)target;
				break;
			case 4:
				this.mTextBlockGrid = (Grid)target;
				break;
			case 5:
				this.mTitleIcon = (CustomPictureBox)target;
				break;
			case 6:
				this.mTitleText = (TextBlock)target;
				break;
			case 7:
				this.mCustomMessageBoxMinimizeButton = (CustomPictureBox)target;
				break;
			case 8:
				this.mCustomMessageBoxCloseButton = (CustomPictureBox)target;
				break;
			case 9:
				this.mBodyTextStackPanel = (StackPanel)target;
				break;
			case 10:
				this.mBodyTextBlock = (TextBlock)target;
				break;
			case 11:
				this.mUrlTextBlock = (TextBlock)target;
				break;
			case 12:
				this.mUrlLink = (Hyperlink)target;
				break;
			case 13:
				this.mButtonsStackPanel = (StackPanel)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x0400056B RID: 1387
		private Dictionary<object, Tuple<ButtonColors, EventHandler, object>> mdictActions = new Dictionary<object, Tuple<ButtonColors, EventHandler, object>>();

		// Token: 0x0400056C RID: 1388
		private EventHandler mCloseButtonEventHandler = null;

		// Token: 0x0400056D RID: 1389
		private StrippedButton button1 = null;

		// Token: 0x0400056E RID: 1390
		private StrippedButton button2 = null;

		// Token: 0x0400056F RID: 1391
		private object mCloseButtonEventdata = null;

		// Token: 0x04000570 RID: 1392
		public ButtonColors ClickedButton = ButtonColors.White;

		// Token: 0x04000571 RID: 1393
		private double mContentMaxWidth;

		// Token: 0x04000572 RID: 1394
		internal Border mBorder;

		// Token: 0x04000573 RID: 1395
		internal Border mMaskBorder;

		// Token: 0x04000574 RID: 1396
		internal Grid mParentGrid;

		// Token: 0x04000575 RID: 1397
		internal Grid mTextBlockGrid;

		// Token: 0x04000576 RID: 1398
		internal CustomPictureBox mTitleIcon;

		// Token: 0x04000577 RID: 1399
		internal TextBlock mTitleText;

		// Token: 0x04000578 RID: 1400
		internal CustomPictureBox mCustomMessageBoxMinimizeButton;

		// Token: 0x04000579 RID: 1401
		internal CustomPictureBox mCustomMessageBoxCloseButton;

		// Token: 0x0400057A RID: 1402
		internal StackPanel mBodyTextStackPanel;

		// Token: 0x0400057B RID: 1403
		internal TextBlock mBodyTextBlock;

		// Token: 0x0400057C RID: 1404
		internal TextBlock mUrlTextBlock;

		// Token: 0x0400057D RID: 1405
		internal Hyperlink mUrlLink;

		// Token: 0x0400057E RID: 1406
		internal StackPanel mButtonsStackPanel;

		// Token: 0x0400057F RID: 1407
		private bool _contentLoaded;
	}
}
