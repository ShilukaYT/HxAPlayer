using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x02000066 RID: 102
	public class JsonFormattingConverter : JsonConverter
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000321A File Offset: 0x0000141A
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(List<double>);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00003229 File Offset: 0x00001429
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00003230 File Offset: 0x00001430
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (writer != null)
			{
				writer.WriteRawValue(JsonConvert.SerializeObject(value, Formatting.None));
			}
		}
	}
}
