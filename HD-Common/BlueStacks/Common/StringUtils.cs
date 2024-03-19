using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BlueStacks.Common
{
	// Token: 0x0200020D RID: 525
	public static class StringUtils
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x0000E155 File Offset: 0x0000C355
		public static string GetControlCharFreeString(string s)
		{
			return new string((from c in s
			where !char.IsControl(c)
			select c).ToArray<char>());
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003F018 File Offset: 0x0003D218
		public static string Encode(Dictionary<string, string> data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (data != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in data)
				{
					stringBuilder.AppendFormat("{0}={1}&", keyValuePair.Key, HttpUtility.UrlEncode(keyValuePair.Value));
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'&'
			});
		}
	}
}
