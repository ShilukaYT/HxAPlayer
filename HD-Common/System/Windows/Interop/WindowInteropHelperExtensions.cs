using System;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Interop
{
	// Token: 0x02000026 RID: 38
	public static class WindowInteropHelperExtensions
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00011638 File Offset: 0x0000F838
		public static IntPtr EnsureHandle(this WindowInteropHelper helper)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}
			if (helper.Handle == IntPtr.Zero)
			{
				Window target = (Window)typeof(WindowInteropHelper).InvokeMember("_window", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, helper, null, CultureInfo.InvariantCulture);
				try
				{
					typeof(Window).InvokeMember("SafeCreateWindow", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, target, null, CultureInfo.InvariantCulture);
				}
				catch (MissingMethodException)
				{
					typeof(Window).InvokeMember("CreateSourceWindow", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, target, new object[]
					{
						false
					}, CultureInfo.InvariantCulture);
				}
			}
			return helper.Handle;
		}
	}
}
