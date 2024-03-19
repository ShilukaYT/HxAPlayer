using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BlueStacks.Common
{
	// Token: 0x02000117 RID: 279
	public class CustomSettingsButton : Button, IComponentConnector
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x000257D8 File Offset: 0x000239D8
		public CustomSettingsButton()
		{
			this.InitializeComponent();
			this.SetBackground();
			base.Loaded += this.CustomSettingsButton_Loaded;
			BlueStacksUIBinding.Instance.PropertyChanged += this.BlueStacksUIBinding_PropertyChanged;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000068F4 File Offset: 0x00004AF4
		private void CustomSettingsButton_Loaded(object sender, RoutedEventArgs e)
		{
			this.SetNotification();
			this.SetSelectedLine();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00006902 File Offset: 0x00004B02
		private void BlueStacksUIBinding_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "LocaleModel")
			{
				this.SetSelectedLine();
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000691C File Offset: 0x00004B1C
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00006924 File Offset: 0x00004B24
		public string Group { get; set; } = string.Empty;

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0000692D File Offset: 0x00004B2D
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00006935 File Offset: 0x00004B35
		public string ImageName { get; set; } = string.Empty;

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000693E File Offset: 0x00004B3E
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00025838 File Offset: 0x00023A38
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
			set
			{
				this.isSelected = value;
				this.SetBackground();
				this.SetForeGround();
				this.SetSelectedLine();
				if (this.IsSelected && !string.IsNullOrEmpty(this.Group))
				{
					if (CustomSettingsButton.dictSelecetedButtons.ContainsKey(this.Group))
					{
						CustomSettingsButton.dictSelecetedButtons[this.Group].IsSelected = false;
					}
					CustomSettingsButton.dictSelecetedButtons[this.Group] = this;
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00006946 File Offset: 0x00004B46
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0000694E File Offset: 0x00004B4E
		public bool ShowButtonNotification
		{
			get
			{
				return this.showButtonNotification;
			}
			set
			{
				this.showButtonNotification = value;
				this.SetNotification();
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000258AC File Offset: 0x00023AAC
		private void SetNotification()
		{
			Ellipse ellipse = (Ellipse)base.Template.FindName("mBtnNotification", this);
			if (ellipse != null)
			{
				if (this.showButtonNotification)
				{
					ellipse.Visibility = Visibility.Visible;
					return;
				}
				ellipse.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000695D File Offset: 0x00004B5D
		private void SetForeGround()
		{
			if (this.isSelected)
			{
				BlueStacksUIBinding.BindColor(this, Control.ForegroundProperty, "SettingsWindowTabMenuItemSelectedForeground");
				return;
			}
			BlueStacksUIBinding.BindColor(this, Control.ForegroundProperty, "SettingsWindowTabMenuItemForeground");
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00006988 File Offset: 0x00004B88
		private void SetBackground()
		{
			if (this.IsSelected)
			{
				BlueStacksUIBinding.BindColor(this, Control.BackgroundProperty, "SettingsWindowTabMenuItemSelectedBackground");
				return;
			}
			this.Button_MouseEvent(null, null);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000258EC File Offset: 0x00023AEC
		private void SetSelectedLine()
		{
			try
			{
				Line line = (Line)base.Template.FindName("mSelectedLine", this);
				ContentPresenter contentPresenter = (ContentPresenter)base.Template.FindName("contentPresenter", this);
				if (line != null)
				{
					if (this.isSelected)
					{
						line.Visibility = Visibility.Visible;
						TextBlock textBlock = (TextBlock)contentPresenter.Content;
						Typeface typeface = new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch);
						double widthIncludingTrailingWhitespace = new FormattedText(textBlock.Text, Thread.CurrentThread.CurrentCulture, textBlock.FlowDirection, typeface, textBlock.FontSize, textBlock.Foreground).WidthIncludingTrailingWhitespace;
						line.X2 = widthIncludingTrailingWhitespace;
					}
					else
					{
						line.Visibility = Visibility.Collapsed;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000069AB File Offset: 0x00004BAB
		private void Button_MouseEvent(object sender, MouseEventArgs e)
		{
			if (!this.IsSelected)
			{
				if (base.IsMouseOver)
				{
					BlueStacksUIBinding.BindColor(this, Control.BackgroundProperty, "SettingsWindowTabMenuItemHoverBackground");
					return;
				}
				BlueStacksUIBinding.BindColor(this, Control.BackgroundProperty, "SettingsWindowTabMenuItemBackground");
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000069DE File Offset: 0x00004BDE
		private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (string.IsNullOrEmpty(this.Group))
			{
				this.IsSelected = true;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000069F4 File Offset: 0x00004BF4
		private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (string.IsNullOrEmpty(this.Group))
			{
				this.IsSelected = false;
				this.Button_MouseEvent(null, null);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000259B8 File Offset: 0x00023BB8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customsettingsbutton.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000259E8 File Offset: 0x00023BE8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((CustomSettingsButton)target).MouseEnter += this.Button_MouseEvent;
				((CustomSettingsButton)target).MouseLeave += this.Button_MouseEvent;
				((CustomSettingsButton)target).PreviewMouseDown += this.Button_PreviewMouseDown;
				((CustomSettingsButton)target).PreviewMouseUp += this.Button_PreviewMouseUp;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400040C RID: 1036
		private static Dictionary<string, CustomSettingsButton> dictSelecetedButtons = new Dictionary<string, CustomSettingsButton>();

		// Token: 0x0400040F RID: 1039
		private bool isSelected;

		// Token: 0x04000410 RID: 1040
		private bool showButtonNotification;

		// Token: 0x04000411 RID: 1041
		private bool _contentLoaded;
	}
}
