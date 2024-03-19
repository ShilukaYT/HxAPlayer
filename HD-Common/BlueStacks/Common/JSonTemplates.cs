using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x020000CB RID: 203
	internal class JSonTemplates
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00019404 File Offset: 0x00017604
		public static string SuccessArrayJSonTemplate
		{
			get
			{
				if (string.IsNullOrEmpty(JSonTemplates.mSuccessArrayJsonString))
				{
					JArray jarray = new JArray();
					JObject item = new JObject
					{
						{
							"success",
							true
						},
						{
							"reason",
							""
						}
					};
					jarray.Add(item);
					JSonTemplates.mSuccessArrayJsonString = jarray.ToString(Formatting.None, new JsonConverter[0]);
				}
				return JSonTemplates.mSuccessArrayJsonString;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001946C File Offset: 0x0001766C
		public static string FailedArrayJSonTemplate
		{
			get
			{
				if (string.IsNullOrEmpty(JSonTemplates.mFailedArrayJsonString))
				{
					JArray jarray = new JArray();
					JObject item = new JObject
					{
						{
							"success",
							false
						},
						{
							"reason",
							""
						}
					};
					jarray.Add(item);
					JSonTemplates.mFailedArrayJsonString = jarray.ToString(Formatting.None, new JsonConverter[0]);
				}
				return JSonTemplates.mFailedArrayJsonString;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000194D4 File Offset: 0x000176D4
		public static string SuccessJSonTemplate
		{
			get
			{
				if (string.IsNullOrEmpty(JSonTemplates.mSuccessJsonString))
				{
					JSonTemplates.mSuccessJsonString = new JObject
					{
						{
							"success",
							true
						},
						{
							"reason",
							""
						}
					}.ToString(Formatting.None, new JsonConverter[0]);
				}
				return JSonTemplates.mSuccessJsonString;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00019530 File Offset: 0x00017730
		public static string FailedJSonTemplate
		{
			get
			{
				if (string.IsNullOrEmpty(JSonTemplates.mFailedJsonString))
				{
					JSonTemplates.mFailedJsonString = new JObject
					{
						{
							"success",
							false
						},
						{
							"reason",
							""
						}
					}.ToString(Formatting.None, new JsonConverter[0]);
				}
				return JSonTemplates.mFailedJsonString;
			}
		}

		// Token: 0x04000235 RID: 565
		private static string mSuccessArrayJsonString = string.Empty;

		// Token: 0x04000236 RID: 566
		private static string mFailedArrayJsonString = string.Empty;

		// Token: 0x04000237 RID: 567
		private static string mSuccessJsonString = string.Empty;

		// Token: 0x04000238 RID: 568
		private static string mFailedJsonString = string.Empty;
	}
}
