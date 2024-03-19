using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x02000063 RID: 99
	public class EnumToBoolConverter2 : IValueConverter
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00003186 File Offset: 0x00001386
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null || value == null)
			{
				return Binding.DoNothing;
			}
			return value.ToString().Equals(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000031AB File Offset: 0x000013AB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null || value == null)
			{
				return Binding.DoNothing;
			}
			if (!value.Equals(true))
			{
				return Binding.DoNothing;
			}
			return parameter.ToString();
		}
	}
}
