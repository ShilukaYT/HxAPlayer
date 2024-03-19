using System;
using System.Collections;
using System.Collections.Generic;

namespace BlueStacks.Common
{
	// Token: 0x020000C2 RID: 194
	public static class DictionaryExtensions
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00018670 File Offset: 0x00016870
		public static void ClearSync<TKey, TValue>(this Dictionary<TKey, TValue> dic)
		{
			if (dic != null)
			{
				object syncRoot = ((ICollection)dic).SyncRoot;
				lock (syncRoot)
				{
					dic.Clear();
				}
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000186AC File Offset: 0x000168AC
		public static void ClearAddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd)
		{
			if (dic != null)
			{
				object syncRoot = ((ICollection)dic).SyncRoot;
				lock (syncRoot)
				{
					dic.Clear();
					if (dicToAdd != null && dicToAdd.Count > 0)
					{
						foreach (KeyValuePair<TKey, TValue> keyValuePair in dicToAdd)
						{
							dic.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
			}
		}
	}
}
