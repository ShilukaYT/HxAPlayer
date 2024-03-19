using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200012B RID: 299
	public static class JsonExtensions
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00029588 File Offset: 0x00027788
		public static IEnumerable<KeyValuePair<string, string>> ToStringStringEnumerableKvp(this JToken obj)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (obj != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in obj.ToObject<Dictionary<string, string>>())
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000295F4 File Offset: 0x000277F4
		public static IDictionary<string, T> ToDictionary<T>(this JToken obj)
		{
			Dictionary<string, T> dictionary = new Dictionary<string, T>();
			if (obj != null)
			{
				foreach (KeyValuePair<string, T> keyValuePair in obj.ToObject<IDictionary<string, T>>())
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00029658 File Offset: 0x00027858
		public static SerializableDictionary<string, T> ToSerializableDictionary<T>(this JToken obj)
		{
			SerializableDictionary<string, T> serializableDictionary = new SerializableDictionary<string, T>();
			if (obj != null)
			{
				foreach (KeyValuePair<string, T> keyValuePair in obj.ToObject<SerializableDictionary<string, T>>())
				{
					serializableDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return serializableDictionary;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00008B41 File Offset: 0x00006D41
		public static IEnumerable<string> ToIenumerableString(this JToken obj)
		{
			if (obj != null)
			{
				return obj.ToObject<List<string>>();
			}
			return null;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00008B4E File Offset: 0x00006D4E
		public static bool AssignIfContains<T>(this JToken resJson, string key, Action<T> setter)
		{
			if (resJson != null && resJson[key] != null && setter != null)
			{
				setter(resJson.Value<T>(key));
				return true;
			}
			return false;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00008B6F File Offset: 0x00006D6F
		public static void AssignStringIfContains(this JToken resJson, string key, ref string result)
		{
			if (resJson != null && resJson[key] != null)
			{
				result = resJson[key].ToString();
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00008B8B File Offset: 0x00006D8B
		public static void AssignDoubleIfContains(this JToken resJson, string key, ref double result)
		{
			if (resJson != null && resJson[key] != null)
			{
				result = resJson[key].ToObject<double>();
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00008BA7 File Offset: 0x00006DA7
		public static bool IsNullOrEmptyBrackets(string str)
		{
			str = Regex.Replace(str, "\\s+", "");
			return string.IsNullOrEmpty(str) || string.Compare(str, "{}", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00008BD6 File Offset: 0x00006DD6
		public static string GetValue(this JToken obj, string key)
		{
			if (obj != null && obj[key] != null)
			{
				return obj[key].ToString();
			}
			return string.Empty;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000296C4 File Offset: 0x000278C4
		public static bool IsNullOrEmpty(this JToken token)
		{
			return token == null || (token.Type == JTokenType.Array && !token.HasValues) || (token.Type == JTokenType.Object && !token.HasValues) || (token.Type == JTokenType.String && string.IsNullOrEmpty(token.ToString())) || token.Type == JTokenType.Null;
		}
	}
}
