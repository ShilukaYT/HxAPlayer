using System;

namespace BlueStacks.Common
{
	// Token: 0x0200009A RID: 154
	public class Subscription<T> : IDisposable
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000039A6 File Offset: 0x00001BA6
		// (set) Token: 0x06000325 RID: 805 RVA: 0x000039AE File Offset: 0x00001BAE
		public Action<T> Action { get; private set; }

		// Token: 0x06000326 RID: 806 RVA: 0x000039B7 File Offset: 0x00001BB7
		public Subscription(Action<T> action)
		{
			this.Action = action;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000039C6 File Offset: 0x00001BC6
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				this.disposedValue = true;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00014A74 File Offset: 0x00012C74
		~Subscription()
		{
			this.Dispose(false);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000039D9 File Offset: 0x00001BD9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000163 RID: 355
		private bool disposedValue;
	}
}
