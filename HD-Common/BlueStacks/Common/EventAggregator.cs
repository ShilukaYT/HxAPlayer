using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000097 RID: 151
	public static class EventAggregator
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00014708 File Offset: 0x00012908
		public static void Publish<TMessageType>(TMessageType message)
		{
			Type typeFromHandle = typeof(TMessageType);
			if (EventAggregator.mSubscriber.ContainsKey(typeFromHandle))
			{
				foreach (object obj in ((IEnumerable)new List<Subscription<TMessageType>>(EventAggregator.mSubscriber[typeFromHandle].Cast<Subscription<TMessageType>>())))
				{
					((Subscription<TMessageType>)obj).Action(message);
				}
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001478C File Offset: 0x0001298C
		public static Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
		{
			Type typeFromHandle = typeof(TMessageType);
			Subscription<TMessageType> subscription = new Subscription<TMessageType>(action);
			IList list;
			if (!EventAggregator.mSubscriber.TryGetValue(typeFromHandle, out list))
			{
				list = new List<Subscription<TMessageType>>();
				list.Add(subscription);
				EventAggregator.mSubscriber.Add(typeFromHandle, list);
			}
			else
			{
				list.Add(subscription);
			}
			return subscription;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000147E0 File Offset: 0x000129E0
		public static void Unsubscribe<TMessageType>(Subscription<TMessageType> subscription)
		{
			Type typeFromHandle = typeof(TMessageType);
			if (EventAggregator.mSubscriber.ContainsKey(typeFromHandle))
			{
				EventAggregator.mSubscriber[typeFromHandle].Remove(subscription);
			}
		}

		// Token: 0x04000161 RID: 353
		private static Dictionary<Type, IList> mSubscriber = new Dictionary<Type, IList>();
	}
}
