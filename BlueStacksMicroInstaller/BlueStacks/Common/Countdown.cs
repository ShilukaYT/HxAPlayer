using System;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x02000076 RID: 118
	public class Countdown
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000B69F File Offset: 0x0000989F
		public Countdown()
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B6B4 File Offset: 0x000098B4
		public Countdown(int initialCount)
		{
			this._value = initialCount;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public void Signal()
		{
			this.AddCount(-1);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B6DC File Offset: 0x000098DC
		public void AddCount(int amount)
		{
			object locker = this._locker;
			lock (locker)
			{
				this._value += amount;
				bool flag = this._value <= 0;
				if (flag)
				{
					Monitor.PulseAll(this._locker);
				}
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B740 File Offset: 0x00009940
		public void Wait()
		{
			object locker = this._locker;
			lock (locker)
			{
				while (this._value > 0)
				{
					Monitor.Wait(this._locker);
				}
			}
		}

		// Token: 0x0400036D RID: 877
		private object _locker = new object();

		// Token: 0x0400036E RID: 878
		private int _value;
	}
}
