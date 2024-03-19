using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x020000A1 RID: 161
	public class StrippedButton : Button, IComponentConnector, IStyleConnector
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00018414 File Offset: 0x00016614
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00018436 File Offset: 0x00016636
		public ButtonColors ButtonColor
		{
			get
			{
				return (ButtonColors)base.GetValue(StrippedButton.ButtonColorProperty);
			}
			set
			{
				base.SetValue(StrippedButton.ButtonColorProperty, value);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001844C File Offset: 0x0001664C
		private static void ButtonColorChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			StrippedButton strippedButton = source as StrippedButton;
			bool flag = !DesignerProperties.GetIsInDesignMode(strippedButton);
			if (flag)
			{
				strippedButton.SetColor(false);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00018478 File Offset: 0x00016678
		static StrippedButton()
		{
			Control.FontWeightProperty.OverrideMetadata(typeof(StrippedButton), new FrameworkPropertyMetadata(FontWeights.SemiBold));
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000184E4 File Offset: 0x000166E4
		public StrippedButton()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000184F5 File Offset: 0x000166F5
		public StrippedButton(ButtonColors color) : this()
		{
			this.ButtonColor = color;
			this.SetColor(false);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00018510 File Offset: 0x00016710
		private void SetColor(bool isPressed = false)
		{
			bool flag = !base.IsEnabled;
			if (flag)
			{
				BlueStacksUIBinding.BindColor(this, Control.BorderBrushProperty, this.ButtonColor.ToString() + "DisabledBorderBackground");
				BlueStacksUIBinding.BindColor(this, Control.BackgroundProperty, this.ButtonColor.ToString() + "DisabledGridBackGround");
				BlueStacksUIBinding.BindColor(this, Control.ForegroundProperty, this.ButtonColor.ToString() + "DisabledForeGround");
			}
			else
			{
				bool flag2 = base.IsPressed || isPressed;
				if (flag2)
				{
					BlueStacksUIBinding.BindColor(this, Control.BorderBrushProperty, this.ButtonColor.ToString() + "MouseDownBorderBackground");
					BlueStacksUIBinding.BindColor(this, Control.BackgroundProperty, this.ButtonColor.ToString() + "MouseDownGridBackGround");
					BlueStacksUIBinding.BindColor(this, Control.ForegroundProperty, this.ButtonColor.ToString() + "MouseDownForeGround");
				}
				else
				{
					bool isMouseOver = base.IsMouseOver;
					if (isMouseOver)
					{
						BlueStacksUIBinding.BindColor(this, Control.BorderBrushProperty, this.ButtonColor.ToString() + "MouseInBorderBackground");
						BlueStacksUIBinding.BindColor(this, Control.BackgroundProperty, this.ButtonColor.ToString() + "MouseInGridBackGround");
						BlueStacksUIBinding.BindColor(this, Control.ForegroundProperty, this.ButtonColor.ToString() + "MouseInForeGround");
					}
					else
					{
						BlueStacksUIBinding.BindColor(this, Control.BorderBrushProperty, this.ButtonColor.ToString() + "MouseOutBorderBackground");
						BlueStacksUIBinding.BindColor(this, Control.BackgroundProperty, this.ButtonColor.ToString() + "MouseOutGridBackGround");
						BlueStacksUIBinding.BindColor(this, Control.ForegroundProperty, this.ButtonColor.ToString() + "MouseOutForeGround");
					}
				}
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00018759 File Offset: 0x00016959
		private void mButton_LayoutUpdated(object sender, EventArgs e)
		{
			this.SetColor(false);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00018764 File Offset: 0x00016964
		private void mButton_MouseDown(object sender, EventArgs e)
		{
			this.SetColor(true);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00018770 File Offset: 0x00016970
		private void Border_Initialized(object sender, EventArgs e)
		{
			try
			{
				Border border = sender as Border;
				CornerRadius cornerRadius = new CornerRadius(0.0);
				border.CornerRadius = new CornerRadius(base.ActualHeight / cornerRadius.TopLeft, base.ActualHeight / cornerRadius.TopRight, base.ActualHeight / cornerRadius.BottomRight, base.ActualHeight / cornerRadius.BottomLeft);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000187F4 File Offset: 0x000169F4
		private void Border_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			try
			{
				Border border = sender as Border;
				CornerRadius cornerRadius = new CornerRadius(0.0);
				border.CornerRadius = new CornerRadius(base.ActualHeight / cornerRadius.TopLeft, base.ActualHeight / cornerRadius.TopRight, base.ActualHeight / cornerRadius.BottomRight, base.ActualHeight / cornerRadius.BottomLeft);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00018878 File Offset: 0x00016A78
		private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			try
			{
				Border border = sender as Border;
				CornerRadius cornerRadius = new CornerRadius(0.0);
				border.CornerRadius = new CornerRadius(base.ActualHeight / cornerRadius.TopLeft, base.ActualHeight / cornerRadius.TopRight, base.ActualHeight / cornerRadius.BottomRight, base.ActualHeight / cornerRadius.BottomLeft);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00018759 File Offset: 0x00016959
		private void mButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.SetColor(false);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000188FC File Offset: 0x00016AFC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/BlueStacksMicroInstaller;component/controls/strippedbutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00018934 File Offset: 0x00016B34
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				this._contentLoaded = true;
			}
			else
			{
				this.mButton = (StrippedButton)target;
				this.mButton.MouseEnter += new MouseEventHandler(this.mButton_LayoutUpdated);
				this.mButton.MouseLeave += new MouseEventHandler(this.mButton_LayoutUpdated);
				this.mButton.PreviewMouseDown += new MouseButtonEventHandler(this.mButton_MouseDown);
				this.mButton.MouseDown += new MouseButtonEventHandler(this.mButton_MouseDown);
				this.mButton.IsEnabledChanged += this.mButton_IsEnabledChanged;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000189DC File Offset: 0x00016BDC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 2)
			{
				if (connectionId == 3)
				{
					((Border)target).Initialized += this.Border_Initialized;
					((Border)target).SizeChanged += this.Border_SizeChanged;
					((Border)target).IsVisibleChanged += this.Border_IsVisibleChanged;
				}
			}
			else
			{
				((Border)target).Initialized += this.Border_Initialized;
				((Border)target).SizeChanged += this.Border_SizeChanged;
				((Border)target).IsVisibleChanged += this.Border_IsVisibleChanged;
			}
		}

		// Token: 0x04000580 RID: 1408
		public static readonly DependencyProperty ButtonColorProperty = DependencyProperty.Register("ButtonColor", typeof(ButtonColors), typeof(StrippedButton), new PropertyMetadata(ButtonColors.Blue, new PropertyChangedCallback(StrippedButton.ButtonColorChanged)));

		// Token: 0x04000581 RID: 1409
		internal StrippedButton mButton;

		// Token: 0x04000582 RID: 1410
		private bool _contentLoaded;
	}
}
