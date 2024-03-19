using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace BlueStacks.Common
{
	// Token: 0x020000B3 RID: 179
	public class XTextBox : TextBox
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x00017CD8 File Offset: 0x00015ED8
		static XTextBox()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(XTextBox), new FrameworkPropertyMetadata(typeof(XTextBox)));
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000048F8 File Offset: 0x00002AF8
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000490A File Offset: 0x00002B0A
		public TextValidityOptions InputTextValidity
		{
			get
			{
				return (TextValidityOptions)base.GetValue(XTextBox.InputTextValidityProperty);
			}
			set
			{
				base.SetValue(XTextBox.InputTextValidityProperty, value);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000491D File Offset: 0x00002B1D
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000492F File Offset: 0x00002B2F
		public string WatermarkText
		{
			get
			{
				return (string)base.GetValue(XTextBox.WatermarkTextProperty);
			}
			set
			{
				base.SetValue(XTextBox.WatermarkTextProperty, value);
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00017DCC File Offset: 0x00015FCC
		private static void OnWatermarkTextChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			XTextBox xtextBox = sender as XTextBox;
			if (xtextBox != null)
			{
				xtextBox.Text = args.NewValue.ToString();
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000493D File Offset: 0x00002B3D
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000494F File Offset: 0x00002B4F
		public bool SelectAllOnStart
		{
			get
			{
				return (bool)base.GetValue(XTextBox.SelectAllOnStartProperty);
			}
			set
			{
				base.SetValue(XTextBox.SelectAllOnStartProperty, value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00004962 File Offset: 0x00002B62
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00004974 File Offset: 0x00002B74
		public bool ErrorIfNullOrEmpty
		{
			get
			{
				return (bool)base.GetValue(XTextBox.ErrorIfNullOrEmptyProperty);
			}
			set
			{
				base.SetValue(XTextBox.ErrorIfNullOrEmptyProperty, value);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017DF8 File Offset: 0x00015FF8
		protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
			base.OnGotKeyboardFocus(e);
			if (string.Equals(base.Text, this.WatermarkText, StringComparison.InvariantCulture))
			{
				base.Clear();
				return;
			}
			if (this.SelectAllOnStart)
			{
				base.Dispatcher.BeginInvoke(new Action(delegate()
				{
					base.SelectAll();
				}), DispatcherPriority.ApplicationIdle, new object[0]);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00004987 File Offset: 0x00002B87
		protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
			base.OnLostKeyboardFocus(e);
			if (string.IsNullOrEmpty(base.Text))
			{
				base.Text = this.WatermarkText;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000049A9 File Offset: 0x00002BA9
		public TextBlock TextBlock
		{
			get
			{
				if (this.mTextBlock == null)
				{
					this.mTextBlock = (TextBlock)base.Template.FindName("mTextBlock", this);
				}
				return this.mTextBlock;
			}
		}

		// Token: 0x04000214 RID: 532
		public static readonly DependencyProperty InputTextValidityProperty = DependencyProperty.Register("InputTextValidity", typeof(TextValidityOptions), typeof(XTextBox), new PropertyMetadata(TextValidityOptions.Success));

		// Token: 0x04000215 RID: 533
		public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register("WatermarkText", typeof(string), typeof(XTextBox), new PropertyMetadata("", new PropertyChangedCallback(XTextBox.OnWatermarkTextChangedCallback)));

		// Token: 0x04000216 RID: 534
		public static readonly DependencyProperty SelectAllOnStartProperty = DependencyProperty.Register("SelectAllOnStart", typeof(bool), typeof(XTextBox), new PropertyMetadata(true));

		// Token: 0x04000217 RID: 535
		public static readonly DependencyProperty ErrorIfNullOrEmptyProperty = DependencyProperty.Register("ErrorIfNullOrEmpty", typeof(bool), typeof(XTextBox), new PropertyMetadata(false));

		// Token: 0x04000218 RID: 536
		private TextBlock mTextBlock;
	}
}
