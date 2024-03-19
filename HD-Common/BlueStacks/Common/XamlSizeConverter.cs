using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x02000247 RID: 583
	public class XamlSizeConverter : MarkupExtension, IValueConverter
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return System.Convert.ToDouble(value, culture) * System.Convert.ToDouble(parameter, culture);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00003229 File Offset: 0x00001429
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000046FB File Offset: 0x000028FB
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
