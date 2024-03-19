using System;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x0200022B RID: 555
	public class Countdown
	{
		// Token: 0x06001154 RID: 4436 RVA: 0x0000EA9A File Offset: 0x0000CC9A
		public Countdown()
		{
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0000EAAD File Offset: 0x0000CCAD
		public Countdown(int initialCount)
		{
			this._value = initialCount;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0000EAC7 File Offset: 0x0000CCC7
		public void Signal()
		{
			this.AddCount(-1);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000417EC File Offset: 0x0003F9EC
		public void AddCount(int amount)
		{
			object locker = this._locker;
			lock (locker)
			{
				this._value += amount;
				if (this._value <= 0)
				{
					Monitor.PulseAll(this._locker);
				}
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00041844 File Offset: 0x0003FA44
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

		// Token: 0x04000CC1 RID: 3265
		private object _locker = new object();

		// Token: 0x04000CC2 RID: 3266
		private int _value;
	}
}
