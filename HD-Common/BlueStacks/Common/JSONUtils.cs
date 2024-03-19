using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x020000CC RID: 204
	public static class JSONUtils
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x0001958C File Offset: 0x0001778C
		public static string GetJSONArrayString(Dictionary<string, string> dict)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (JsonWriter jsonWriter = new JsonTextWriter(new StringWriter(stringBuilder)))
			{
				jsonWriter.WriteStartArray();
				jsonWriter.WriteStartObject();
				if (dict != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in dict)
					{
						jsonWriter.WritePropertyName(keyValuePair.Key);
						jsonWriter.WriteValue(keyValuePair.Value);
					}
				}
				jsonWriter.WriteEndObject();
				jsonWriter.WriteEndArray();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00019638 File Offset: 0x00017838
		public static string GetJSONObjectString<T>(Dictionary<string, T> dict)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (JsonWriter jsonWriter = new JsonTextWriter(new StringWriter(stringBuilder)))
			{
				jsonWriter.WriteStartObject();
				if (dict != null)
				{
					foreach (KeyValuePair<string, T> keyValuePair in dict)
					{
						jsonWriter.WritePropertyName(keyValuePair.Key);
						jsonWriter.WriteValue(keyValuePair.Value);
					}
				}
				jsonWriter.WriteEndObject();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000196E0 File Offset: 0x000178E0
		public static string GetJSONObjectString(Dictionary<string, Dictionary<string, long>> dict)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (JsonWriter jsonWriter = new JsonTextWriter(new StringWriter(stringBuilder)))
			{
				jsonWriter.WriteStartObject();
				if (dict != null)
				{
					foreach (KeyValuePair<string, Dictionary<string, long>> keyValuePair in dict)
					{
						jsonWriter.WritePropertyName(keyValuePair.Key);
						jsonWriter.WriteValue(JSONUtils.GetJSONObjectString<long>(keyValuePair.Value));
					}
				}
				jsonWriter.WriteEndObject();
			}
			return stringBuilder.ToString();
		}
	}
}
