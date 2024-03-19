using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BlueStacks.Common
{
	// Token: 0x0200006A RID: 106
	public static class NumericBehavior
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000332A File Offset: 0x0000152A
		public static bool GetIsNumericOnly(DependencyObject obj)
		{
			return (bool)((obj != null) ? obj.GetValue(NumericBehavior.IsNumericOnlyProperty) : null);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00003342 File Offset: 0x00001542
		public static void SetIsNumericOnly(DependencyObject obj, bool value)
		{
			if (obj != null)
			{
				obj.SetValue(NumericBehavior.IsNumericOnlyProperty, value);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000133F0 File Offset: 0x000115F0
		private static void OnIsNumericOnlyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			TextBox textBox = (TextBox)sender;
			if ((bool)args.NewValue)
			{
				textBox.PreviewTextInput += NumericBehavior.TextBox_PreviewTextInput;
				textBox.PreviewKeyDown += NumericBehavior.TextBox_PreviewKeyDown;
				DataObject.AddPastingHandler(textBox, new DataObjectPastingEventHandler(NumericBehavior.OnPaste));
				return;
			}
			textBox.PreviewTextInput -= NumericBehavior.TextBox_PreviewTextInput;
			textBox.PreviewKeyDown -= NumericBehavior.TextBox_PreviewKeyDown;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00013470 File Offset: 0x00011670
		private static void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(typeof(string)))
			{
				if (!NumericBehavior.IsTextAllowed((string)e.DataObject.GetData(typeof(string))))
				{
					e.CancelCommand();
					return;
				}
			}
			else
			{
				e.CancelCommand();
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00003358 File Offset: 0x00001558
		private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				e.Handled = true;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000336B File Offset: 0x0000156B
		private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !NumericBehavior.IsTextAllowed(e.Text);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00003381 File Offset: 0x00001581
		private static bool IsTextAllowed(string text)
		{
			return new Regex("^[0-9]+$").IsMatch(text) && text.IndexOf(' ') == -1;
		}

		// Token: 0x0400011A RID: 282
		public static readonly DependencyProperty IsNumericOnlyProperty = DependencyProperty.RegisterAttached("IsNumericOnlyProperty", typeof(bool), typeof(NumericBehavior), new PropertyMetadata(false, new PropertyChangedCallback(NumericBehavior.OnIsNumericOnlyChanged)));
	}
}
