using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x02000069 RID: 105
	public class Writer : TextWriter
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00009A37 File Offset: 0x00007C37
		public Writer(Writer.WriteFunc writeFunc)
		{
			this.writeFunc = writeFunc;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00009A48 File Offset: 0x00007C48
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00009A4F File Offset: 0x00007C4F
		public override void WriteLine(string msg)
		{
			this.writeFunc(msg);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009A5F File Offset: 0x00007C5F
		public override void WriteLine(string fmt, object obj)
		{
			this.writeFunc(string.Format(CultureInfo.InvariantCulture, fmt, new object[]
			{
				obj
			}));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009A83 File Offset: 0x00007C83
		public override void WriteLine(string fmt, params object[] objs)
		{
			this.writeFunc(string.Format(CultureInfo.InvariantCulture, fmt, objs));
		}

		// Token: 0x040001FE RID: 510
		private Writer.WriteFunc writeFunc;

		// Token: 0x020000DC RID: 220
		// (Invoke) Token: 0x0600044F RID: 1103
		public delegate void WriteFunc(string msg);
	}
}
