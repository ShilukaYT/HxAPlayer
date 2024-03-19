using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace BlueStacks.Common
{
	// Token: 0x02000181 RID: 385
	internal static class AnimationCache
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x00039C48 File Offset: 0x00037E48
		public static void IncrementReferenceCount(ImageSource source, RepeatBehavior repeatBehavior)
		{
			AnimationCache.CacheKey key = new AnimationCache.CacheKey(source, repeatBehavior);
			int num;
			AnimationCache._referenceCount.TryGetValue(key, out num);
			num++;
			AnimationCache._referenceCount[key] = num;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00039C7C File Offset: 0x00037E7C
		public static void DecrementReferenceCount(ImageSource source, RepeatBehavior repeatBehavior)
		{
			AnimationCache.CacheKey key = new AnimationCache.CacheKey(source, repeatBehavior);
			int num;
			AnimationCache._referenceCount.TryGetValue(key, out num);
			if (num > 0)
			{
				num--;
				AnimationCache._referenceCount[key] = num;
			}
			if (num == 0)
			{
				AnimationCache._animationCache.Remove(key);
				AnimationCache._referenceCount.Remove(key);
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00039CD0 File Offset: 0x00037ED0
		public static void AddAnimation(ImageSource source, RepeatBehavior repeatBehavior, ObjectAnimationUsingKeyFrames animation)
		{
			AnimationCache.CacheKey key = new AnimationCache.CacheKey(source, repeatBehavior);
			AnimationCache._animationCache[key] = animation;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00039CF4 File Offset: 0x00037EF4
		public static void RemoveAnimation(ImageSource source, RepeatBehavior repeatBehavior, ObjectAnimationUsingKeyFrames _)
		{
			AnimationCache.CacheKey key = new AnimationCache.CacheKey(source, repeatBehavior);
			AnimationCache._animationCache.Remove(key);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00039D18 File Offset: 0x00037F18
		public static ObjectAnimationUsingKeyFrames GetAnimation(ImageSource source, RepeatBehavior repeatBehavior)
		{
			AnimationCache.CacheKey key = new AnimationCache.CacheKey(source, repeatBehavior);
			ObjectAnimationUsingKeyFrames result;
			AnimationCache._animationCache.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x040006D5 RID: 1749
		private static readonly Dictionary<AnimationCache.CacheKey, ObjectAnimationUsingKeyFrames> _animationCache = new Dictionary<AnimationCache.CacheKey, ObjectAnimationUsingKeyFrames>();

		// Token: 0x040006D6 RID: 1750
		private static readonly Dictionary<AnimationCache.CacheKey, int> _referenceCount = new Dictionary<AnimationCache.CacheKey, int>();

		// Token: 0x02000182 RID: 386
		private class CacheKey
		{
			// Token: 0x06000EAA RID: 3754 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
			public CacheKey(ImageSource source, RepeatBehavior repeatBehavior)
			{
				this._source = source;
				this._repeatBehavior = repeatBehavior;
			}

			// Token: 0x06000EAB RID: 3755 RVA: 0x0000D1EA File Offset: 0x0000B3EA
			private bool Equals(AnimationCache.CacheKey other)
			{
				return AnimationCache.CacheKey.ImageEquals(this._source, other._source) && object.Equals(this._repeatBehavior, other._repeatBehavior);
			}

			// Token: 0x06000EAC RID: 3756 RVA: 0x0000D21C File Offset: 0x0000B41C
			public override bool Equals(object obj)
			{
				return obj != null && (this == obj || (obj.GetType() == base.GetType() && this.Equals((AnimationCache.CacheKey)obj)));
			}

			// Token: 0x06000EAD RID: 3757 RVA: 0x00039D3C File Offset: 0x00037F3C
			public override int GetHashCode()
			{
				return AnimationCache.CacheKey.ImageGetHashCode(this._source) * 397 ^ this._repeatBehavior.GetHashCode();
			}

			// Token: 0x06000EAE RID: 3758 RVA: 0x00039D70 File Offset: 0x00037F70
			private static int ImageGetHashCode(ImageSource image)
			{
				if (image != null)
				{
					Uri uri = AnimationCache.CacheKey.GetUri(image);
					if (uri != null)
					{
						return uri.GetHashCode();
					}
				}
				return 0;
			}

			// Token: 0x06000EAF RID: 3759 RVA: 0x00039D98 File Offset: 0x00037F98
			private static bool ImageEquals(ImageSource x, ImageSource y)
			{
				if (object.Equals(x, y))
				{
					return true;
				}
				if (x == null != (y == null))
				{
					return false;
				}
				if (x.GetType() != y.GetType())
				{
					return false;
				}
				Uri uri = AnimationCache.CacheKey.GetUri(x);
				Uri uri2 = AnimationCache.CacheKey.GetUri(y);
				return uri != null && uri == uri2;
			}

			// Token: 0x06000EB0 RID: 3760 RVA: 0x00039DEC File Offset: 0x00037FEC
			private static Uri GetUri(ImageSource image)
			{
				BitmapImage bitmapImage = image as BitmapImage;
				if (bitmapImage != null && bitmapImage.UriSource != null)
				{
					if (bitmapImage.UriSource.IsAbsoluteUri)
					{
						return bitmapImage.UriSource;
					}
					if (bitmapImage.BaseUri != null)
					{
						return new Uri(bitmapImage.BaseUri, bitmapImage.UriSource);
					}
				}
				BitmapFrame bitmapFrame = image as BitmapFrame;
				if (bitmapFrame != null)
				{
					string text = bitmapFrame.ToString(CultureInfo.InvariantCulture);
					Uri uri;
					if (text != bitmapFrame.GetType().FullName && Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out uri))
					{
						if (uri.IsAbsoluteUri)
						{
							return uri;
						}
						if (bitmapFrame.BaseUri != null)
						{
							return new Uri(bitmapFrame.BaseUri, uri);
						}
					}
				}
				return null;
			}

			// Token: 0x040006D7 RID: 1751
			private readonly ImageSource _source;

			// Token: 0x040006D8 RID: 1752
			private readonly RepeatBehavior _repeatBehavior;
		}
	}
}
