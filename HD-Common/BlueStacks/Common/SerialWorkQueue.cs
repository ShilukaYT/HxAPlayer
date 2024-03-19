using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Timers;

namespace BlueStacks.Common
{
	// Token: 0x02000137 RID: 311
	public class SerialWorkQueue
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0002BD3C File Offset: 0x00029F3C
		public SerialWorkQueue()
		{
			string name = string.Format(CultureInfo.InvariantCulture, "SerialWorkQueue.{0}", new object[]
			{
				Interlocked.Increment(ref SerialWorkQueue.sAutoId)
			});
			this.Initialize(name);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0000C015 File Offset: 0x0000A215
		public SerialWorkQueue(string name)
		{
			this.Initialize(name);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0000C024 File Offset: 0x0000A224
		private void Initialize(string name)
		{
			this.mQueue = new Queue<SerialWorkQueue.Work>();
			this.mLock = new object();
			this.mThread = new Thread(new ThreadStart(this.Run))
			{
				Name = name,
				IsBackground = true
			};
		}

		// Token: 0x170003DE RID: 990
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0000C061 File Offset: 0x0000A261
		public SerialWorkQueue.ExceptionHandlerCallback ExceptionHandler
		{
			set
			{
				this.mExceptionHandler = value;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0000C06A File Offset: 0x0000A26A
		public void Start()
		{
			if (!this.mThread.IsAlive)
			{
				this.mThread.Start();
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0000C084 File Offset: 0x0000A284
		public void Join()
		{
			this.mThread.Join();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0000C091 File Offset: 0x0000A291
		public void Stop()
		{
			this.Enqueue(null);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002BD80 File Offset: 0x00029F80
		public void Enqueue(SerialWorkQueue.Work work)
		{
			object obj = this.mLock;
			lock (obj)
			{
				this.mQueue.Enqueue(work);
				Monitor.PulseAll(this.mLock);
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0000C09A File Offset: 0x0000A29A
		public void DispatchAsync(SerialWorkQueue.Work work)
		{
			this.Enqueue(work);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002BDCC File Offset: 0x00029FCC
		public void DispatchAfter(double delay, SerialWorkQueue.Work work)
		{
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += delegate(object source, ElapsedEventArgs evt)
			{
				this.DispatchSync(work);
				timer.Close();
			};
			timer.Interval = delay;
			timer.Enabled = true;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002BE28 File Offset: 0x0002A028
		public void DispatchSync(SerialWorkQueue.Work work)
		{
			EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
			this.Enqueue(delegate
			{
				work();
				waitHandle.Set();
			});
			waitHandle.WaitOne();
			waitHandle.Close();
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0000C0A3 File Offset: 0x0000A2A3
		public bool IsCurrentWorkQueue()
		{
			return Thread.CurrentThread == this.mThread;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002BE78 File Offset: 0x0002A078
		private void Run()
		{
			for (;;)
			{
				object obj = this.mLock;
				SerialWorkQueue.Work work;
				lock (obj)
				{
					while (this.mQueue.Count == 0)
					{
						Monitor.Wait(this.mLock);
					}
					work = this.mQueue.Dequeue();
				}
				if (work != null)
				{
					try
					{
						work();
						continue;
					}
					catch (Exception exc)
					{
						if (this.mExceptionHandler != null)
						{
							this.mExceptionHandler(exc);
							continue;
						}
						throw;
					}
					break;
				}
				break;
			}
		}

		// Token: 0x04000577 RID: 1399
		private static int sAutoId;

		// Token: 0x04000578 RID: 1400
		private Thread mThread;

		// Token: 0x04000579 RID: 1401
		private Queue<SerialWorkQueue.Work> mQueue;

		// Token: 0x0400057A RID: 1402
		private object mLock;

		// Token: 0x0400057B RID: 1403
		private SerialWorkQueue.ExceptionHandlerCallback mExceptionHandler;

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x06000C22 RID: 3106
		public delegate void Work();

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x06000C26 RID: 3110
		public delegate void ExceptionHandlerCallback(Exception exc);
	}
}
