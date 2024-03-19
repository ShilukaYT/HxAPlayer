using System;

namespace BlueStacks.Common
{
	// Token: 0x02000099 RID: 153
	public interface ISubscriber
	{
		// Token: 0x06000321 RID: 801
		void SubscribeTag(BrowserControlTags args);

		// Token: 0x06000322 RID: 802
		void UnsubscribeTag(BrowserControlTags args);

		// Token: 0x06000323 RID: 803
		void Message(EventArgs eventArgs);
	}
}
