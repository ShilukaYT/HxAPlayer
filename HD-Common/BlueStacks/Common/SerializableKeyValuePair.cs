using System;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public struct SerializableKeyValuePair<TKey, TValue>
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0000BFE3 File Offset: 0x0000A1E3
		public SerializableKeyValuePair(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0000BFF3 File Offset: 0x0000A1F3
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0000BFFB File Offset: 0x0000A1FB
		public TKey Key { readonly get; set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0000C004 File Offset: 0x0000A204
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0000C00C File Offset: 0x0000A20C
		public TValue Value { readonly get; set; }

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002BCB4 File Offset: 0x00029EB4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			if (this.Key != null)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				TKey key = this.Key;
				stringBuilder2.Append(key.ToString());
			}
			stringBuilder.Append(", ");
			if (this.Value != null)
			{
				StringBuilder stringBuilder3 = stringBuilder;
				TValue value = this.Value;
				stringBuilder3.Append(value.ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
