using System;
using System.Collections.Specialized;

namespace BlueStacks.Common
{
	// Token: 0x02000129 RID: 297
	public class RequestData
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00008A96 File Offset: 0x00006C96
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00008A9E File Offset: 0x00006C9E
		public NameValueCollection Headers { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00008AA7 File Offset: 0x00006CA7
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00008AAF File Offset: 0x00006CAF
		public NameValueCollection QueryString { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00008AB8 File Offset: 0x00006CB8
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public NameValueCollection Data { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00008AC9 File Offset: 0x00006CC9
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00008AD1 File Offset: 0x00006CD1
		public NameValueCollection Files { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00008ADA File Offset: 0x00006CDA
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x00008AE2 File Offset: 0x00006CE2
		public string RequestVmName { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00008AEB File Offset: 0x00006CEB
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00008AF3 File Offset: 0x00006CF3
		public int RequestVmId { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00008AFC File Offset: 0x00006CFC
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00008B04 File Offset: 0x00006D04
		public string Oem { get; set; }

		// Token: 0x06000998 RID: 2456 RVA: 0x00008B0D File Offset: 0x00006D0D
		public RequestData()
		{
			this.Headers = new NameValueCollection();
			this.QueryString = new NameValueCollection();
			this.Data = new NameValueCollection();
			this.Files = new NameValueCollection();
		}
	}
}
