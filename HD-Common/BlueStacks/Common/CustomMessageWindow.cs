using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlueStacks.Common
{
	// Token: 0x02000114 RID: 276
	public class CustomMessageWindow : CustomWindow, IComponentConnector
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0000660D File Offset: 0x0000480D
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x00006615 File Offset: 0x00004815
		public EventHandler MinimizeEventHandler { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0000661E File Offset: 0x0000481E
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x00006626 File Offset: 0x00004826
		public ButtonColors ClickedButton { get; set; } = ButtonColors.Background;

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000662F File Offset: 0x0000482F
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x00006637 File Offset: 0x00004837
		public double ContentMaxWidth
		{
			get
			{
				return this.mContentMaxWidth;
			}
			set
			{
				this.mContentMaxWidth = value;
				this.mTitleGrid.MaxWidth = value;
				this.mBodyTextStackPanel.MaxWidth = value;
				this.mProgressGrid.MaxWidth = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x00006664 File Offset: 0x00004864
		public bool ProgressBarEnabled
		{
			set
			{
				this.mProgressGrid.Visibility = (value ? Visibility.Visible : Visibility.Collapsed);
			}
		}

		// Token: 0x170001FA RID: 506
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00006678 File Offset: 0x00004878
		public bool ProgressStatusEnabled
		{
			set
			{
				this.mProgressUpdatesGrid.Visibility = (value ? Visibility.Visible : Visibility.Collapsed);
			}
		}

		// Token: 0x170001FB RID: 507
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0000668C File Offset: 0x0000488C
		public bool IsWindowMinizable
		{
			set
			{
				if (value)
				{
					this.mCustomMessageBoxMinimizeButton.Visibility = Visibility.Visible;
					return;
				}
				this.mCustomMessageBoxMinimizeButton.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x170001FC RID: 508
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x000066AA File Offset: 0x000048AA
		public bool IsWindowClosable
		{
			set
			{
				if (value)
				{
					this.mCustomMessageBoxCloseButton.Visibility = Visibility.Visible;
					return;
				}
				this.mCustomMessageBoxCloseButton.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x170001FD RID: 509
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x000066C8 File Offset: 0x000048C8
		public bool IsWindowCloseButtonDisabled
		{
			set
			{
				if (value)
				{
					this.mCustomMessageBoxCloseButton.ToolTip = null;
					this.mCustomMessageBoxCloseButton.IsDisabled = true;
					this.mCustomMessageBoxCloseButton.PreviewMouseLeftButtonUp -= this.Close_PreviewMouseLeftButtonUp;
				}
			}
		}

		// Token: 0x170001FE RID: 510
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x000066FC File Offset: 0x000048FC
		public string ImageName
		{
			set
			{
				this.mTitleIcon.ImageName = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.mTitleIcon.Visibility = Visibility.Visible;
				}
			}
		}

		// Token: 0x170001FF RID: 511
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0000671E File Offset: 0x0000491E
		public bool IsWithoutButtons
		{
			set
			{
				if (value)
				{
					this.mStackPanel.Visibility = Visibility.Collapsed;
					return;
				}
				this.mStackPanel.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000673C File Offset: 0x0000493C
		public TextBlock TitleTextBlock
		{
			get
			{
				return this.mTitleText;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00006744 File Offset: 0x00004944
		public CustomPictureBox MessageIcon
		{
			get
			{
				return this.mMessageIcon;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000674C File Offset: 0x0000494C
		public TextBlock BodyTextBlockTitle
		{
			get
			{
				return this.mBodyTextBlockTitle;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00006754 File Offset: 0x00004954
		public TextBlock BodyTextBlock
		{
			get
			{
				return this.mBodyTextBlock;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0000675C File Offset: 0x0000495C
		public TextBlock BodyWarningTextBlock
		{
			get
			{
				return this.mBodyWarningTextBlock;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00006764 File Offset: 0x00004964
		public TextBlock AboveBodyWarningTextBlock
		{
			get
			{
				return this.mAboveBodyWarningTextBlock;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0000676C File Offset: 0x0000496C
		public CustomPictureBox CloseButton
		{
			get
			{
				return this.mCustomMessageBoxCloseButton;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00006774 File Offset: 0x00004974
		public TextBlock UrlTextBlock
		{
			get
			{
				return this.mUrlTextBlock;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0000677C File Offset: 0x0000497C
		public Hyperlink UrlLink
		{
			get
			{
				return this.mUrlLink;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00006784 File Offset: 0x00004984
		public CustomCheckbox CheckBox
		{
			get
			{
				return this.mCheckBox;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0000678C File Offset: 0x0000498C
		public BlueProgressBar CustomProgressBar
		{
			get
			{
				return this.mProgressbar;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00006794 File Offset: 0x00004994
		public TextBlock ProgressStatusTextBlock
		{
			get
			{
				return this.mProgressStatus;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0000679C File Offset: 0x0000499C
		public Label ProgressPercentageTextBlock
		{
			get
			{
				return this.mProgressPercentage;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000067A4 File Offset: 0x000049A4
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x000067AC File Offset: 0x000049AC
		public bool IsDraggable { get; set; }

		// Token: 0x0600078C RID: 1932 RVA: 0x000067B5 File Offset: 0x000049B5
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000250E8 File Offset: 0x000232E8
		public CustomMessageWindow()
		{
			this.InitializeComponent();
			base.Loaded += this.CustomMessageWindow_Loaded;
			base.SizeChanged += this.CustomMessageWindow_SizeChanged;
			this.mStackPanel.Children.Clear();
			this.ContentMaxWidth = 340.0;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00025158 File Offset: 0x00023358
		private void CustomMessageWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.mStackPanel.ActualWidth > this.ContentMaxWidth)
			{
				if (this.mButton2 != null)
				{
					this.mStackPanel.Orientation = Orientation.Vertical;
					this.mStackPanel.Height = 90.0;
					this.mButton1.Width = this.ContentMaxWidth;
					this.mButton1.Height = 35.0;
					this.mButton2.Width = this.ContentMaxWidth;
					this.mButton2.Height = 35.0;
					this.mButton2.Margin = new Thickness(0.0, 15.0, 0.0, 0.0);
					return;
				}
				this.mButton1.MaxWidth = this.ContentMaxWidth;
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000067BE File Offset: 0x000049BE
		public void CustomMessageWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (base.Owner == null || InteropWindow.FindMainWindowState(this) == WindowState.Minimized)
			{
				base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}
			if (this.mButton2 != null)
			{
				base.UpdateLayout();
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000067E6 File Offset: 0x000049E6
		public void CloseButtonHandle(Predicate<object> handle, object data = null)
		{
			this.mCloseButtonEventHandler = handle;
			this.mCloseButtonEventData = data;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00025238 File Offset: 0x00023438
		public void CloseButtonHandle(EventHandler handle, object data = null)
		{
			this.mCloseButtonEventHandler = delegate(object o)
			{
				if (handle != null)
				{
					handle(o, new EventArgs());
				}
				return false;
			};
			this.mCloseButtonEventData = data;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002526C File Offset: 0x0002346C
		private void HandleMouseDrag(object sender, MouseButtonEventArgs e)
		{
			if (this.IsDraggable && e.OriginalSource.GetType() != typeof(CustomPictureBox))
			{
				try
				{
					base.DragMove();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000067F6 File Offset: 0x000049F6
		public void AddWarning(string title, string imageName = "")
		{
			this.mBodyWarningTextBlock.Text = title;
			if (!string.IsNullOrEmpty(imageName))
			{
				this.mMessageIcon.Visibility = Visibility.Visible;
				this.mMessageIcon.ImageName = imageName;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00006824 File Offset: 0x00004A24
		public void AddAboveBodyWarning(string title)
		{
			this.mAboveBodyWarningTextBlock.Text = title;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000252B4 File Offset: 0x000234B4
		public void AddButton(ButtonColors color, string text, EventHandler handle, string image = null, bool ChangeImageAlignment = false, object data = null, bool isEnabled = true)
		{
			this.AddButtonInUI(new CustomButton(color), color, text, handle, image, ChangeImageAlignment, data, isEnabled);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000252D8 File Offset: 0x000234D8
		public void AddButtonInUI(CustomButton button, ButtonColors color, string text, EventHandler handle, string image, bool ChangeImageAlignment, object data, bool isEnabled)
		{
			if (button != null)
			{
				if (this.mButton1 == null)
				{
					this.mButton1 = button;
				}
				else
				{
					this.mButton2 = button;
					button.Margin = new Thickness(15.0, 0.0, 0.0, 0.0);
				}
				button.IsEnabled = isEnabled;
				button.Click += this.Button_Click;
				button.MinWidth = 100.0;
				button.Visibility = Visibility.Visible;
				BlueStacksUIBinding.Bind(button, text);
				if (image != null)
				{
					button.ImageName = image;
					button.ImageMargin = new Thickness(0.0, 6.0, 5.0, 6.0);
					if (ChangeImageAlignment)
					{
						button.ImageOrder = ButtonImageOrder.AfterText;
						button.ImageMargin = new Thickness(5.0, 6.0, 0.0, 6.0);
					}
				}
			}
			this.mStackPanel.Children.Add(button);
			this.mDictActions.Add(button, new Tuple<ButtonColors, EventHandler, object>(color, handle, data));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00025408 File Offset: 0x00023608
		public void AddHyperLinkInUI(string text, Uri navigateUri, RequestNavigateEventHandler handle)
		{
			Hyperlink hyperlink = new Hyperlink(new Run(text))
			{
				NavigateUri = navigateUri
			};
			hyperlink.RequestNavigate += handle.Invoke;
			hyperlink.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#047CD2"));
			this.mUrlTextBlock.Inlines.Clear();
			this.mUrlTextBlock.Inlines.Add(hyperlink);
			this.mUrlTextBlock.Visibility = Visibility.Visible;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00025484 File Offset: 0x00023684
		public void Button_Click(object sender, RoutedEventArgs e)
		{
			this.ClickedButton = this.mDictActions[sender].Item1;
			if (this.mDictActions[sender].Item2 != null)
			{
				this.mDictActions[sender].Item2(this.mDictActions[sender].Item3, new EventArgs());
			}
			this.CloseWindow();
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00006832 File Offset: 0x00004A32
		private void Close_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (this.mCloseButtonEventHandler != null && this.mCloseButtonEventHandler(this.mCloseButtonEventData))
			{
				return;
			}
			this.CloseWindow();
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00006856 File Offset: 0x00004A56
		public void CloseWindow()
		{
			base.Close();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000685E File Offset: 0x00004A5E
		private void Minimize_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			EventHandler minimizeEventHandler = this.MinimizeEventHandler;
			if (minimizeEventHandler != null)
			{
				minimizeEventHandler(this, null);
			}
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000254F0 File Offset: 0x000236F0
		public void AddBulletInBody(string text)
		{
			Ellipse ellipse = new Ellipse
			{
				Width = 9.0,
				Height = 9.0,
				VerticalAlignment = VerticalAlignment.Center
			};
			BlueStacksUIBinding.BindColor(ellipse, Shape.FillProperty, "ContextMenuItemForegroundDimColor");
			TextBlock textBlock = new TextBlock
			{
				FontSize = 18.0,
				MaxWidth = 300.0,
				FontWeight = FontWeights.Regular
			};
			BlueStacksUIBinding.BindColor(textBlock, Control.ForegroundProperty, "ContextMenuItemForegroundDimColor");
			textBlock.TextWrapping = TextWrapping.Wrap;
			textBlock.Text = text;
			textBlock.HorizontalAlignment = HorizontalAlignment.Left;
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
			BulletDecorator element = new BulletDecorator
			{
				Bullet = ellipse,
				Child = textBlock
			};
			this.mBodyTextStackPanel.Children.Add(element);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000687A File Offset: 0x00004A7A
		private void mMessageIcon_IsVisibleChanged(object _1, DependencyPropertyChangedEventArgs _2)
		{
			if (this.mMessageIcon.Visibility == Visibility.Visible)
			{
				this.mTitleGrid.MaxWidth = this.ContentMaxWidth + 85.0;
				return;
			}
			this.mTitleGrid.MaxWidth = this.ContentMaxWidth;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000255E8 File Offset: 0x000237E8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/custommessagewindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00025618 File Offset: 0x00023818
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mMaskBorder = (Border)target;
				return;
			case 2:
				this.mParentGrid = (Grid)target;
				this.mParentGrid.MouseDown += this.HandleMouseDrag;
				return;
			case 3:
				this.mTitleGrid = (Grid)target;
				return;
			case 4:
				this.mTitleIcon = (CustomPictureBox)target;
				return;
			case 5:
				this.mTitleText = (TextBlock)target;
				return;
			case 6:
				this.mCustomMessageBoxMinimizeButton = (CustomPictureBox)target;
				return;
			case 7:
				this.mCustomMessageBoxCloseButton = (CustomPictureBox)target;
				return;
			case 8:
				this.mMessageIcon = (CustomPictureBox)target;
				return;
			case 9:
				this.mTextBlockGrid = (Grid)target;
				return;
			case 10:
				this.mBodyTextStackPanel = (StackPanel)target;
				return;
			case 11:
				this.mBodyTextBlockTitle = (TextBlock)target;
				return;
			case 12:
				this.mAboveBodyWarningTextBlock = (TextBlock)target;
				return;
			case 13:
				this.mBodyTextBlock = (TextBlock)target;
				return;
			case 14:
				this.mBodyWarningTextBlock = (TextBlock)target;
				return;
			case 15:
				this.mUrlTextBlock = (TextBlock)target;
				return;
			case 16:
				this.mUrlLink = (Hyperlink)target;
				return;
			case 17:
				this.mCheckBox = (CustomCheckbox)target;
				return;
			case 18:
				this.mProgressGrid = (Grid)target;
				return;
			case 19:
				this.mProgressbar = (BlueProgressBar)target;
				return;
			case 20:
				this.mProgressUpdatesGrid = (Grid)target;
				return;
			case 21:
				this.mProgressStatus = (TextBlock)target;
				return;
			case 22:
				this.mProgressPercentage = (Label)target;
				return;
			case 23:
				this.mStackPanel = (StackPanel)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x040003EA RID: 1002
		private Dictionary<object, Tuple<ButtonColors, EventHandler, object>> mDictActions = new Dictionary<object, Tuple<ButtonColors, EventHandler, object>>();

		// Token: 0x040003EB RID: 1003
		private Predicate<object> mCloseButtonEventHandler;

		// Token: 0x040003ED RID: 1005
		private CustomButton mButton1;

		// Token: 0x040003EE RID: 1006
		private CustomButton mButton2;

		// Token: 0x040003EF RID: 1007
		private object mCloseButtonEventData;

		// Token: 0x040003F1 RID: 1009
		private double mContentMaxWidth;

		// Token: 0x040003F3 RID: 1011
		internal Border mMaskBorder;

		// Token: 0x040003F4 RID: 1012
		internal Grid mParentGrid;

		// Token: 0x040003F5 RID: 1013
		internal Grid mTitleGrid;

		// Token: 0x040003F6 RID: 1014
		internal CustomPictureBox mTitleIcon;

		// Token: 0x040003F7 RID: 1015
		internal TextBlock mTitleText;

		// Token: 0x040003F8 RID: 1016
		internal CustomPictureBox mCustomMessageBoxMinimizeButton;

		// Token: 0x040003F9 RID: 1017
		internal CustomPictureBox mCustomMessageBoxCloseButton;

		// Token: 0x040003FA RID: 1018
		internal CustomPictureBox mMessageIcon;

		// Token: 0x040003FB RID: 1019
		internal Grid mTextBlockGrid;

		// Token: 0x040003FC RID: 1020
		internal StackPanel mBodyTextStackPanel;

		// Token: 0x040003FD RID: 1021
		internal TextBlock mBodyTextBlockTitle;

		// Token: 0x040003FE RID: 1022
		internal TextBlock mAboveBodyWarningTextBlock;

		// Token: 0x040003FF RID: 1023
		internal TextBlock mBodyTextBlock;

		// Token: 0x04000400 RID: 1024
		internal TextBlock mBodyWarningTextBlock;

		// Token: 0x04000401 RID: 1025
		internal TextBlock mUrlTextBlock;

		// Token: 0x04000402 RID: 1026
		internal Hyperlink mUrlLink;

		// Token: 0x04000403 RID: 1027
		internal CustomCheckbox mCheckBox;

		// Token: 0x04000404 RID: 1028
		internal Grid mProgressGrid;

		// Token: 0x04000405 RID: 1029
		internal BlueProgressBar mProgressbar;

		// Token: 0x04000406 RID: 1030
		internal Grid mProgressUpdatesGrid;

		// Token: 0x04000407 RID: 1031
		internal TextBlock mProgressStatus;

		// Token: 0x04000408 RID: 1032
		internal Label mProgressPercentage;

		// Token: 0x04000409 RID: 1033
		internal StackPanel mStackPanel;

		// Token: 0x0400040A RID: 1034
		private bool _contentLoaded;
	}
}
