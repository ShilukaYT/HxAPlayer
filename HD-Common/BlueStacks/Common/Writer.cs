using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x0200021B RID: 539
	public class Writer : TextWriter
	{
		// Token: 0x060010CF RID: 4303 RVA: 0x0000E45D File Offset: 0x0000C65D
		public Writer(Writer.WriteFunc writeFunc)
		{
			this.writeFunc = writeFunc;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0000E46C File Offset: 0x0000C66C
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0000E473 File Offset: 0x0000C673
		public override void WriteLine(string msg)
		{
			this.writeFunc(msg);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0000E481 File Offset: 0x0000C681
		public override void WriteLine(string fmt, object obj)
		{
			this.writeFunc(string.Format(CultureInfo.InvariantCulture, fmt, new object[]
			{
				obj
			}));
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0000E4A3 File Offset: 0x0000C6A3
		public override void WriteLine(string fmt, params object[] objs)
		{
			this.writeFunc(string.Format(CultureInfo.InvariantCulture, fmt, objs));
		}

		// Token: 0x04000B44 RID: 2884
		private Writer.WriteFunc writeFunc;

		// Token: 0x0200021C RID: 540
		// (Invoke) Token: 0x060010D5 RID: 4309
		public delegate void WriteFunc(string msg);
	}
}
