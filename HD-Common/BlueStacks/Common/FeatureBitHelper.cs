using System;

namespace BlueStacks.Common
{
	// Token: 0x020001E0 RID: 480
	public static class FeatureBitHelper
	{
		// Token: 0x06000FBC RID: 4028 RVA: 0x00006E4D File Offset: 0x0000504D
		public static bool IsFeatureEnabled(ulong featureMask, ulong feature)
		{
			return (feature & featureMask) != 0UL;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public static ulong EnableFeature(ulong featureMask, ulong feature)
		{
			if ((feature & featureMask) != 0UL)
			{
				return feature;
			}
			return feature |= featureMask;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0000DC7B File Offset: 0x0000BE7B
		public static ulong DisableFeature(ulong featureMask, ulong feature)
		{
			if ((feature & featureMask) == 0UL)
			{
				return feature;
			}
			return feature & ~featureMask;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003C47C File Offset: 0x0003A67C
		public static ulong ToggleFeature(ulong featureMask, ulong feature)
		{
			ulong result;
			if (FeatureBitHelper.IsFeatureEnabled(featureMask, feature))
			{
				result = FeatureBitHelper.DisableFeature(featureMask, feature);
			}
			else
			{
				result = FeatureBitHelper.EnableFeature(featureMask, feature);
			}
			return result;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public static bool WasFeatureChanged(ulong featureMask, ulong newFeature, ulong originalFeature, out bool isEnabled)
		{
			bool flag = FeatureBitHelper.IsFeatureEnabled(featureMask, originalFeature);
			isEnabled = FeatureBitHelper.IsFeatureEnabled(featureMask, newFeature);
			return flag != isEnabled;
		}
	}
}
