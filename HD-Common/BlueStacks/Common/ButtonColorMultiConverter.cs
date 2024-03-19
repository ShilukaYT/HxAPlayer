using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000B5 RID: 181
	public class ButtonColorMultiConverter : IMultiValueConverter
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x00017E50 File Offset: 0x00016050
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null || values == null)
			{
				return Binding.DoNothing;
			}
			string[] array = parameter.ToString().Split(new char[]
			{
				'_'
			});
			if (!BlueStacksUIBinding.Instance.ColorModel.ContainsKey(values[0].ToString() + array[0] + array[1]))
			{
				return BlueStacksUIBinding.Instance.ColorModel[values[0].ToString() + "MouseOut" + array[1]];
			}
			return BlueStacksUIBinding.Instance.ColorModel[values[0].ToString() + array[0] + array[1]];
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00004A00 File Offset: 0x00002C00
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
