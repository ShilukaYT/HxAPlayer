using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x02000158 RID: 344
	public class TextWrapCustomCheckBox : UserControl, IComponentConnector
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0000C682 File Offset: 0x0000A882
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0000C694 File Offset: 0x0000A894
		public SolidColorBrush CheckBoxTextBlockForeground
		{
			get
			{
				return (SolidColorBrush)base.GetValue(TextWrapCustomCheckBox.TextBlockForegroundProperty);
			}
			set
			{
				base.SetValue(TextWrapCustomCheckBox.TextBlockForegroundProperty, value);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002F250 File Offset: 0x0002D450
		private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextWrapCustomCheckBox textWrapCustomCheckBox = d as TextWrapCustomCheckBox;
			if (textWrapCustomCheckBox == null)
			{
				Logger.Debug("custom check box is null");
				return;
			}
			textWrapCustomCheckBox.mCheckBoxContent.Foreground = (e.NewValue as SolidColorBrush);
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0000C6A2 File Offset: 0x0000A8A2
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public string CheckBoxTextBlockText
		{
			get
			{
				return (string)base.GetValue(TextWrapCustomCheckBox.TextBlockTextProperty);
			}
			set
			{
				base.SetValue(TextWrapCustomCheckBox.TextBlockTextProperty, value);
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002F28C File Offset: 0x0002D48C
		private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextWrapCustomCheckBox textWrapCustomCheckBox = d as TextWrapCustomCheckBox;
			if (textWrapCustomCheckBox == null)
			{
				Logger.Debug("custom check box is null");
				return;
			}
			textWrapCustomCheckBox.mCheckBoxContent.Text = (e.NewValue as string);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		public TextWrapCustomCheckBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public bool IsChecked
		{
			get
			{
				return this.mIsChecked;
			}
			set
			{
				this.mIsChecked = value;
				this.UpdateImage();
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x0000C6EF File Offset: 0x0000A8EF
		internal CheckBoxType CheckBoxType
		{
			get
			{
				return this.mCheckBoxType;
			}
			set
			{
				this.mCheckBoxType = value;
				this.UpdateImage();
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		private void UpdateImage()
		{
			string imageName;
			if (this.CheckBoxType == CheckBoxType.White)
			{
				if (this.IsChecked)
				{
					imageName = "checked_white";
				}
				else
				{
					imageName = "unchecked_white";
				}
			}
			else if (this.IsChecked)
			{
				imageName = "checked_gray";
			}
			else
			{
				imageName = "unchecked_gray";
			}
			this.mCheckBoxImage.ImageName = imageName;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0000C6FE File Offset: 0x0000A8FE
		private void CustomCheckBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this.IsChecked)
			{
				this.IsChecked = false;
				return;
			}
			this.IsChecked = true;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002F320 File Offset: 0x0002D520
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/textwrapcustomcheckbox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002F350 File Offset: 0x0002D550
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((Grid)target).MouseDown += this.CustomCheckBox_MouseDown;
				return;
			case 2:
				this.mCheckBoxImage = (CustomPictureBox)target;
				return;
			case 3:
				this.mCheckBoxContent = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400062D RID: 1581
		private bool mIsChecked;

		// Token: 0x0400062E RID: 1582
		private CheckBoxType mCheckBoxType;

		// Token: 0x0400062F RID: 1583
		public static readonly DependencyProperty TextBlockForegroundProperty = DependencyProperty.RegisterAttached("CheckBoxTextBlockForeground", typeof(SolidColorBrush), typeof(TextWrapCustomCheckBox), new PropertyMetadata(Brushes.White, new PropertyChangedCallback(TextWrapCustomCheckBox.OnForegroundPropertyChanged)));

		// Token: 0x04000630 RID: 1584
		public static readonly DependencyProperty TextBlockTextProperty = DependencyProperty.Register("CheckBoxTextBlockText", typeof(string), typeof(TextWrapCustomCheckBox), new PropertyMetadata("Agree", new PropertyChangedCallback(TextWrapCustomCheckBox.OnTextPropertyChanged)));

		// Token: 0x04000631 RID: 1585
		internal CustomPictureBox mCheckBoxImage;

		// Token: 0x04000632 RID: 1586
		internal TextBlock mCheckBoxContent;

		// Token: 0x04000633 RID: 1587
		private bool _contentLoaded;
	}
}
