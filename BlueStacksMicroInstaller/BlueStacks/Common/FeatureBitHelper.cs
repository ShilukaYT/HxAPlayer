using System;

namespace BlueStacks.Common
{
	// Token: 0x0200004F RID: 79
	public static class FeatureBitHelper
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004398 File Offset: 0x00002598
		public static bool IsFeatureEnabled(ulong featureMask, ulong feature)
		{
			return (feature & featureMask) > 0UL;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000043BC File Offset: 0x000025BC
		public static ulong EnableFeature(ulong featureMask, ulong feature)
		{
			bool flag = (feature & featureMask) > 0UL;
			ulong result;
			if (flag)
			{
				result = feature;
			}
			else
			{
				feature = (result = (feature | featureMask));
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000043E4 File Offset: 0x000025E4
		public static ulong DisableFeature(ulong featureMask, ulong feature)
		{
			bool flag = (feature & featureMask) == 0UL;
			ulong result;
			if (flag)
			{
				result = feature;
			}
			else
			{
				result = (feature & ~featureMask);
			}
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000440C File Offset: 0x0000260C
		public static ulong ToggleFeature(ulong featureMask, ulong feature)
		{
			bool flag = FeatureBitHelper.IsFeatureEnabled(featureMask, feature);
			ulong result;
			if (flag)
			{
				result = FeatureBitHelper.DisableFeature(featureMask, feature);
			}
			else
			{
				result = FeatureBitHelper.EnableFeature(featureMask, feature);
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004444 File Offset: 0x00002644
		public static bool WasFeatureChanged(ulong featureMask, ulong newFeature, ulong originalFeature, out bool isEnabled)
		{
			bool flag = FeatureBitHelper.IsFeatureEnabled(featureMask, originalFeature);
			isEnabled = FeatureBitHelper.IsFeatureEnabled(featureMask, newFeature);
			return flag != isEnabled;
		}
	}
}
