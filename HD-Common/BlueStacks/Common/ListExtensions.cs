using System;
using System.Collections;
using System.Collections.Generic;

namespace BlueStacks.Common
{
	// Token: 0x020000C3 RID: 195
	public static class ListExtensions
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x00018740 File Offset: 0x00016940
		public static void ClearSync<T>(this List<T> list)
		{
			if (list != null)
			{
				object syncRoot = ((ICollection)list).SyncRoot;
				lock (syncRoot)
				{
					list.Clear();
				}
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001877C File Offset: 0x0001697C
		public static void ClearAddRange<T>(this List<T> list, List<T> listToAdd)
		{
			if (list != null)
			{
				object syncRoot = ((ICollection)list).SyncRoot;
				lock (syncRoot)
				{
					list.Clear();
					if (listToAdd != null && listToAdd.Count > 0)
					{
						list.AddRange(listToAdd);
					}
				}
			}
		}
	}
}
