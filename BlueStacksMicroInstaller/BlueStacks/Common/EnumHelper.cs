using System;

namespace BlueStacks.Common
{
	// Token: 0x02000015 RID: 21
	public static class EnumHelper
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003E7C File Offset: 0x0000207C
		public static TEnum Parse<TEnum>(string value, TEnum defaultValue)
		{
			return (value != null && Enum.IsDefined(typeof(TEnum), value)) ? ((TEnum)((object)Enum.Parse(typeof(TEnum), value))) : defaultValue;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003EC4 File Offset: 0x000020C4
		public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct, IConvertible
		{
			bool flag = value != null && Enum.IsDefined(typeof(TEnum), value);
			result = (flag ? ((TEnum)((object)Enum.Parse(typeof(TEnum), value))) : default(TEnum));
			return flag;
		}
	}
}
