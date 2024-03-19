using System;

namespace BlueStacks.Common
{
	// Token: 0x020001A6 RID: 422
	public static class EnumHelper
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0000D806 File Offset: 0x0000BA06
		public static TEnum Parse<TEnum>(string value, TEnum defaultValue)
		{
			if (value == null || !Enum.IsDefined(typeof(TEnum), value))
			{
				return defaultValue;
			}
			return (TEnum)((object)Enum.Parse(typeof(TEnum), value));
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003C42C File Offset: 0x0003A62C
		public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct, IConvertible
		{
			bool flag = value != null && Enum.IsDefined(typeof(TEnum), value);
			result = (flag ? ((TEnum)((object)Enum.Parse(typeof(TEnum), value))) : default(TEnum));
			return flag;
		}
	}
}
