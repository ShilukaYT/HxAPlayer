using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000B4 RID: 180
	public class FilePathToFileNameConverter : IValueConverter
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x000049E5 File Offset: 0x00002BE5
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				return Path.GetFileName(value.ToString());
			}
			return Binding.DoNothing;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00003229 File Offset: 0x00001429
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
