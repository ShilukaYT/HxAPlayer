using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200008F RID: 143
	public class XamlSizeConverter : MarkupExtension, IValueConverter
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return System.Convert.ToDouble(value, culture) * System.Convert.ToDouble(parameter, culture);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
