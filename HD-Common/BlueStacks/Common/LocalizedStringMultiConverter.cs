using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x020000AE RID: 174
	public class LocalizedStringMultiConverter : MarkupExtension, IMultiValueConverter
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x000046D7 File Offset: 0x000028D7
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null)
			{
				return string.Empty;
			}
			return LocaleStrings.GetLocalizedString(values[0].ToString(), "");
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000046F4 File Offset: 0x000028F4
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000046FB File Offset: 0x000028FB
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
