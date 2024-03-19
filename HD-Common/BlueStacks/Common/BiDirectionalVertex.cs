using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public abstract class BiDirectionalVertex<T>
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00004B52 File Offset: 0x00002D52
		[JsonIgnore]
		public List<BiDirectionalVertex<T>> Parents { get; } = new List<BiDirectionalVertex<T>>();

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00004B5A File Offset: 0x00002D5A
		[JsonIgnore]
		public List<BiDirectionalVertex<T>> Childs { get; } = new List<BiDirectionalVertex<T>>();

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00004B62 File Offset: 0x00002D62
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00004B6A File Offset: 0x00002D6A
		[JsonIgnore]
		public bool IsVisited { get; set; }

		// Token: 0x060004B7 RID: 1207 RVA: 0x00004B73 File Offset: 0x00002D73
		public void AddChild(BiDirectionalVertex<T> newChild)
		{
			this.Childs.Add(newChild);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00004B81 File Offset: 0x00002D81
		public void RemoveChild(BiDirectionalVertex<T> newChild)
		{
			this.Childs.Remove(newChild);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00004B90 File Offset: 0x00002D90
		public void AddParent(BiDirectionalVertex<T> newParent)
		{
			this.Parents.Add(newParent);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00004B9E File Offset: 0x00002D9E
		public void RemoveParent(BiDirectionalVertex<T> newParent)
		{
			this.Parents.Remove(newParent);
		}
	}
}
