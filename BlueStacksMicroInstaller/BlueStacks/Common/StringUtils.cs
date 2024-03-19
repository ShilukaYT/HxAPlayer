using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BlueStacks.Common
{
	// Token: 0x02000066 RID: 102
	public static class StringUtils
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public static string GetControlCharFreeString(string s)
		{
			return new string((from c in s
			where !char.IsControl(c)
			select c).ToArray<char>());
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007D24 File Offset: 0x00005F24
		public static string Encode(Dictionary<string, string> data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = data != null;
			if (flag)
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
