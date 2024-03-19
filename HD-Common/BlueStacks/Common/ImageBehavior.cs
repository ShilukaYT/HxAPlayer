using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using BlueStacks.Common.Decoding;

namespace BlueStacks.Common
{
	// Token: 0x02000186 RID: 390
	public static class ImageBehavior
	{
		// Token: 0x06000EC7 RID: 3783 RVA: 0x0000D2E9 File Offset: 0x0000B4E9
		[AttachedPropertyBrowsableForType(typeof(Image))]
		public static ImageSource GetAnimatedSource(Image obj)
		{
			return (ImageSource)((obj != null) ? obj.GetValue(ImageBehavior.AnimatedSourceProperty) : null);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0000D301 File Offset: 0x0000B501
		public static void SetAnimatedSource(Image obj, ImageSource value)
		{
			if (obj != null)
			{
				obj.SetValue(ImageBehavior.AnimatedSourceProperty, value);
			}
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0000D312 File Offset: 0x0000B512
		[AttachedPropertyBrowsableForType(typeof(Image))]
		public static RepeatBehavior GetRepeatBehavior(Image obj)
		{
			return (RepeatBehavior)((obj != null) ? obj.GetValue(ImageBehavior.RepeatBehaviorProperty) : null);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0000D32A File Offset: 0x0000B52A
		public static void SetRepeatBehavior(Image obj, RepeatBehavior value)
		{
			if (obj != null)
			{
				obj.SetValue(ImageBehavior.RepeatBehaviorProperty, value);
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0000D340 File Offset: 0x0000B540
		public static bool GetAnimateInDesignMode(DependencyObject obj)
		{
			return (bool)((obj != null) ? obj.GetValue(ImageBehavior.AnimateInDesignModeProperty) : null);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0000D358 File Offset: 0x0000B558
		public static void SetAnimateInDesignMode(DependencyObject obj, bool value)
		{
			if (obj != null)
			{
				obj.SetValue(ImageBehavior.AnimateInDesignModeProperty, value);
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0000D36E File Offset: 0x0000B56E
		[AttachedPropertyBrowsableForType(typeof(Image))]
		public static bool GetAutoStart(Image obj)
		{
			return (bool)((obj != null) ? obj.GetValue(ImageBehavior.AutoStartProperty) : null);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0000D386 File Offset: 0x0000B586
		public static void SetAutoStart(Image obj, bool value)
		{
			if (obj != null)
			{
				obj.SetValue(ImageBehavior.AutoStartProperty, value);
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0000D39C File Offset: 0x0000B59C
		public static ImageAnimationController GetAnimationController(Image imageControl)
		{
			return (ImageAnimationController)((imageControl != null) ? imageControl.GetValue(ImageBehavior.AnimationControllerPropertyKey.DependencyProperty) : null);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0000D3B9 File Offset: 0x0000B5B9
		private static void SetAnimationController(DependencyObject obj, ImageAnimationController value)
		{
			if (obj != null)
			{
				obj.SetValue(ImageBehavior.AnimationControllerPropertyKey, value);
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0000D3CA File Offset: 0x0000B5CA
		public static bool GetIsAnimationLoaded(Image image)
		{
			return (bool)((image != null) ? image.GetValue(ImageBehavior.IsAnimationLoadedProperty) : null);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		private static void SetIsAnimationLoaded(Image image, bool value)
		{
			image.SetValue(ImageBehavior.IsAnimationLoadedPropertyKey, value);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		public static void AddAnimationLoadedHandler(Image image, RoutedEventHandler handler)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			image.AddHandler(ImageBehavior.AnimationLoadedEvent, handler);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0000D41F File Offset: 0x0000B61F
		public static void RemoveAnimationLoadedHandler(Image image, RoutedEventHandler handler)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			image.RemoveHandler(ImageBehavior.AnimationLoadedEvent, handler);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003A170 File Offset: 0x00038370
		public static void AddAnimationCompletedHandler(Image d, RoutedEventHandler handler)
		{
			if (d == null)
			{
				return;
			}
			d.AddHandler(ImageBehavior.AnimationCompletedEvent, handler);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003A190 File Offset: 0x00038390
		public static void RemoveAnimationCompletedHandler(Image d, RoutedEventHandler handler)
		{
			if (d == null)
			{
				return;
			}
			d.RemoveHandler(ImageBehavior.AnimationCompletedEvent, handler);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003A1B0 File Offset: 0x000383B0
		private static void AnimatedSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			Image image = o as Image;
			if (image == null)
			{
				return;
			}
			ImageSource imageSource = e.OldValue as ImageSource;
			ImageSource imageSource2 = e.NewValue as ImageSource;
			if (imageSource == imageSource2)
			{
				return;
			}
			if (imageSource != null)
			{
				image.Loaded -= ImageBehavior.ImageControlLoaded;
				image.Unloaded -= ImageBehavior.ImageControlUnloaded;
				AnimationCache.DecrementReferenceCount(imageSource, ImageBehavior.GetRepeatBehavior(image));
				ImageAnimationController animationController = ImageBehavior.GetAnimationController(image);
				if (animationController != null)
				{
					animationController.Dispose();
				}
				image.Source = null;
			}
			if (imageSource2 != null)
			{
				image.Loaded += ImageBehavior.ImageControlLoaded;
				image.Unloaded += ImageBehavior.ImageControlUnloaded;
				if (image.IsLoaded)
				{
					ImageBehavior.InitAnimationOrImage(image);
				}
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003A268 File Offset: 0x00038468
		private static void ImageControlLoaded(object sender, RoutedEventArgs e)
		{
			Image image = sender as Image;
			if (image == null)
			{
				return;
			}
			ImageBehavior.InitAnimationOrImage(image);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003A288 File Offset: 0x00038488
		private static void ImageControlUnloaded(object sender, RoutedEventArgs e)
		{
			Image image = sender as Image;
			if (image == null)
			{
				return;
			}
			ImageSource animatedSource = ImageBehavior.GetAnimatedSource(image);
			if (animatedSource != null)
			{
				AnimationCache.DecrementReferenceCount(animatedSource, ImageBehavior.GetRepeatBehavior(image));
			}
			ImageAnimationController animationController = ImageBehavior.GetAnimationController(image);
			if (animationController != null)
			{
				animationController.Dispose();
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003A2C8 File Offset: 0x000384C8
		private static void RepeatBehaviorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			Image image = o as Image;
			if (image == null)
			{
				return;
			}
			ImageSource animatedSource = ImageBehavior.GetAnimatedSource(image);
			if (animatedSource != null)
			{
				if (!object.Equals(e.OldValue, e.NewValue))
				{
					AnimationCache.DecrementReferenceCount(animatedSource, (RepeatBehavior)e.OldValue);
				}
				if (image.IsLoaded)
				{
					ImageBehavior.InitAnimationOrImage(image);
				}
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003A320 File Offset: 0x00038520
		private static void AnimateInDesignModeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			Image image = o as Image;
			if (image == null)
			{
				return;
			}
			bool flag = (bool)e.NewValue;
			if (ImageBehavior.GetAnimatedSource(image) != null && image.IsLoaded)
			{
				if (flag)
				{
					ImageBehavior.InitAnimationOrImage(image);
					return;
				}
				image.BeginAnimation(Image.SourceProperty, null);
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003A36C File Offset: 0x0003856C
		private static void InitAnimationOrImage(Image imageControl)
		{
			ImageBehavior.SetAnimationController(imageControl, null);
			ImageBehavior.SetIsAnimationLoaded(imageControl, false);
			BitmapSource source = ImageBehavior.GetAnimatedSource(imageControl) as BitmapSource;
			int isInDesignMode = DesignerProperties.GetIsInDesignMode(imageControl) ? 1 : 0;
			bool animateInDesignMode = ImageBehavior.GetAnimateInDesignMode(imageControl);
			bool flag = isInDesignMode == 0 || animateInDesignMode;
			bool flag2 = ImageBehavior.IsLoadingDeferred(source);
			if (source != null && flag && !flag2)
			{
				if (source.IsDownloading)
				{
					EventHandler handler = null;
					handler = delegate(object sender, EventArgs args)
					{
						source.DownloadCompleted -= handler;
						ImageBehavior.InitAnimationOrImage(imageControl);
					};
					source.DownloadCompleted += handler;
					imageControl.Source = source;
					return;
				}
				ObjectAnimationUsingKeyFrames animation = ImageBehavior.GetAnimation(imageControl, source);
				if (animation != null)
				{
					if (animation.KeyFrames.Count > 0)
					{
						ImageBehavior.TryTwice(delegate
						{
							imageControl.Source = (ImageSource)animation.KeyFrames[0].Value;
						});
					}
					else
					{
						imageControl.Source = source;
					}
					ImageAnimationController value = new ImageAnimationController(imageControl, animation, ImageBehavior.GetAutoStart(imageControl));
					ImageBehavior.SetAnimationController(imageControl, value);
					ImageBehavior.SetIsAnimationLoaded(imageControl, true);
					imageControl.RaiseEvent(new RoutedEventArgs(ImageBehavior.AnimationLoadedEvent, imageControl));
					return;
				}
			}
			imageControl.Source = source;
			if (source != null)
			{
				ImageBehavior.SetIsAnimationLoaded(imageControl, true);
				imageControl.RaiseEvent(new RoutedEventArgs(ImageBehavior.AnimationLoadedEvent, imageControl));
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003A550 File Offset: 0x00038750
		private static ObjectAnimationUsingKeyFrames GetAnimation(Image imageControl, BitmapSource source)
		{
			ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames = AnimationCache.GetAnimation(source, ImageBehavior.GetRepeatBehavior(imageControl));
			if (objectAnimationUsingKeyFrames != null)
			{
				return objectAnimationUsingKeyFrames;
			}
			GifFile gifMetadata;
			GifBitmapDecoder gifBitmapDecoder = ImageBehavior.GetDecoder(source, out gifMetadata) as GifBitmapDecoder;
			if (gifBitmapDecoder != null && gifBitmapDecoder.Frames.Count > 1)
			{
				ImageBehavior.Int32Size fullSize = ImageBehavior.GetFullSize(gifBitmapDecoder, gifMetadata);
				int num = 0;
				objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
				TimeSpan timeSpan = TimeSpan.Zero;
				BitmapSource baseFrame = null;
				foreach (BitmapFrame rawFrame in gifBitmapDecoder.Frames)
				{
					ImageBehavior.FrameMetadata frameMetadata = ImageBehavior.GetFrameMetadata(gifBitmapDecoder, gifMetadata, num);
					BitmapSource bitmapSource = ImageBehavior.MakeFrame(fullSize, rawFrame, frameMetadata, baseFrame);
					DiscreteObjectKeyFrame keyFrame = new DiscreteObjectKeyFrame(bitmapSource, timeSpan);
					objectAnimationUsingKeyFrames.KeyFrames.Add(keyFrame);
					timeSpan += frameMetadata.Delay;
					switch (frameMetadata.DisposalMethod)
					{
					case ImageBehavior.FrameDisposalMethod.None:
					case ImageBehavior.FrameDisposalMethod.DoNotDispose:
						baseFrame = bitmapSource;
						break;
					case ImageBehavior.FrameDisposalMethod.RestoreBackground:
						if (ImageBehavior.IsFullFrame(frameMetadata, fullSize))
						{
							baseFrame = null;
						}
						else
						{
							baseFrame = ImageBehavior.ClearArea(bitmapSource, frameMetadata);
						}
						break;
					}
					num++;
				}
				objectAnimationUsingKeyFrames.Duration = timeSpan;
				objectAnimationUsingKeyFrames.RepeatBehavior = ImageBehavior.GetActualRepeatBehavior(imageControl, gifBitmapDecoder, gifMetadata);
				AnimationCache.AddAnimation(source, ImageBehavior.GetRepeatBehavior(imageControl), objectAnimationUsingKeyFrames);
				AnimationCache.IncrementReferenceCount(source, ImageBehavior.GetRepeatBehavior(imageControl));
				return objectAnimationUsingKeyFrames;
			}
			return null;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003A6BC File Offset: 0x000388BC
		private static BitmapSource ClearArea(BitmapSource frame, ImageBehavior.FrameMetadata metadata)
		{
			DrawingVisual drawingVisual = new DrawingVisual();
			using (DrawingContext drawingContext = drawingVisual.RenderOpen())
			{
				Rect rect = new Rect(0.0, 0.0, (double)frame.PixelWidth, (double)frame.PixelHeight);
				Rect rect2 = new Rect((double)metadata.Left, (double)metadata.Top, (double)metadata.Width, (double)metadata.Height);
				PathGeometry clipGeometry = Geometry.Combine(new RectangleGeometry(rect), new RectangleGeometry(rect2), GeometryCombineMode.Exclude, null);
				drawingContext.PushClip(clipGeometry);
				drawingContext.DrawImage(frame, rect);
			}
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(frame.PixelWidth, frame.PixelHeight, frame.DpiX, frame.DpiY, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(drawingVisual);
			if (renderTargetBitmap.CanFreeze && !renderTargetBitmap.IsFrozen)
			{
				renderTargetBitmap.Freeze();
			}
			return renderTargetBitmap;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003A7A4 File Offset: 0x000389A4
		private static void TryTwice(Action action)
		{
			try
			{
				action();
			}
			catch (Exception)
			{
				action();
			}
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003A7D4 File Offset: 0x000389D4
		private static bool IsLoadingDeferred(BitmapSource source)
		{
			BitmapImage bitmapImage = source as BitmapImage;
			return bitmapImage != null && (bitmapImage.UriSource != null && !bitmapImage.UriSource.IsAbsoluteUri) && bitmapImage.BaseUri == null;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003A818 File Offset: 0x00038A18
		private static BitmapDecoder GetDecoder(BitmapSource image, out GifFile gifFile)
		{
			gifFile = null;
			BitmapDecoder bitmapDecoder = null;
			Stream stream = null;
			Uri uri = null;
			BitmapCreateOptions createOptions = BitmapCreateOptions.None;
			BitmapImage bitmapImage = image as BitmapImage;
			if (bitmapImage != null)
			{
				createOptions = bitmapImage.CreateOptions;
				if (bitmapImage.StreamSource != null)
				{
					stream = bitmapImage.StreamSource;
				}
				else if (bitmapImage.UriSource != null)
				{
					uri = bitmapImage.UriSource;
					if (bitmapImage.BaseUri != null && !uri.IsAbsoluteUri)
					{
						uri = new Uri(bitmapImage.BaseUri, uri);
					}
				}
			}
			else
			{
				BitmapFrame bitmapFrame = image as BitmapFrame;
				if (bitmapFrame != null)
				{
					bitmapDecoder = bitmapFrame.Decoder;
					Uri.TryCreate(bitmapFrame.BaseUri, bitmapFrame.ToString(CultureInfo.InvariantCulture), out uri);
				}
			}
			if (bitmapDecoder == null)
			{
				if (stream != null)
				{
					stream.Position = 0L;
					bitmapDecoder = BitmapDecoder.Create(stream, createOptions, BitmapCacheOption.OnLoad);
				}
				else if (uri != null && uri.IsAbsoluteUri)
				{
					bitmapDecoder = BitmapDecoder.Create(uri, createOptions, BitmapCacheOption.OnLoad);
				}
			}
			if (bitmapDecoder is GifBitmapDecoder && !ImageBehavior.CanReadNativeMetadata(bitmapDecoder))
			{
				if (stream != null)
				{
					stream.Position = 0L;
					gifFile = GifFile.ReadGifFile(stream, true);
				}
				else
				{
					if (!(uri != null))
					{
						throw new InvalidOperationException("Can't get URI or Stream from the source. AnimatedSource should be either a BitmapImage, or a BitmapFrame constructed from a URI.");
					}
					gifFile = ImageBehavior.DecodeGifFile(uri);
				}
			}
			if (bitmapDecoder == null)
			{
				throw new InvalidOperationException("Can't get a decoder from the source. AnimatedSource should be either a BitmapImage or a BitmapFrame.");
			}
			return bitmapDecoder;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003A94C File Offset: 0x00038B4C
		private static bool CanReadNativeMetadata(BitmapDecoder decoder)
		{
			bool result;
			try
			{
				result = (decoder.Metadata != null);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003A97C File Offset: 0x00038B7C
		private static GifFile DecodeGifFile(Uri uri)
		{
			Stream stream = null;
			if (uri.Scheme == PackUriHelper.UriSchemePack)
			{
				StreamResourceInfo streamResourceInfo;
				if (uri.Authority == "siteoforigin:,,,")
				{
					streamResourceInfo = Application.GetRemoteStream(uri);
				}
				else
				{
					streamResourceInfo = Application.GetResourceStream(uri);
				}
				if (streamResourceInfo != null)
				{
					stream = streamResourceInfo.Stream;
				}
			}
			else
			{
				using (WebClient webClient = new WebClient())
				{
					stream = webClient.OpenRead(uri);
				}
			}
			if (stream != null)
			{
				using (stream)
				{
					return GifFile.ReadGifFile(stream, true);
				}
			}
			return null;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0000D449 File Offset: 0x0000B649
		private static bool IsFullFrame(ImageBehavior.FrameMetadata metadata, ImageBehavior.Int32Size fullSize)
		{
			return metadata.Left == 0 && metadata.Top == 0 && metadata.Width == fullSize.Width && metadata.Height == fullSize.Height;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003AA20 File Offset: 0x00038C20
		private static BitmapSource MakeFrame(ImageBehavior.Int32Size fullSize, BitmapSource rawFrame, ImageBehavior.FrameMetadata metadata, BitmapSource baseFrame)
		{
			if (baseFrame == null && ImageBehavior.IsFullFrame(metadata, fullSize))
			{
				return rawFrame;
			}
			DrawingVisual drawingVisual = new DrawingVisual();
			using (DrawingContext drawingContext = drawingVisual.RenderOpen())
			{
				if (baseFrame != null)
				{
					Rect rectangle = new Rect(0.0, 0.0, (double)fullSize.Width, (double)fullSize.Height);
					drawingContext.DrawImage(baseFrame, rectangle);
				}
				Rect rectangle2 = new Rect((double)metadata.Left, (double)metadata.Top, (double)metadata.Width, (double)metadata.Height);
				drawingContext.DrawImage(rawFrame, rectangle2);
			}
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(fullSize.Width, fullSize.Height, 96.0, 96.0, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(drawingVisual);
			if (renderTargetBitmap.CanFreeze && !renderTargetBitmap.IsFrozen)
			{
				renderTargetBitmap.Freeze();
			}
			return renderTargetBitmap;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003AB10 File Offset: 0x00038D10
		private static RepeatBehavior GetActualRepeatBehavior(Image imageControl, BitmapDecoder decoder, GifFile gifMetadata)
		{
			RepeatBehavior repeatBehavior = ImageBehavior.GetRepeatBehavior(imageControl);
			if (repeatBehavior != default(RepeatBehavior))
			{
				return repeatBehavior;
			}
			int repeatCount;
			if (gifMetadata != null)
			{
				repeatCount = (int)gifMetadata.RepeatCount;
			}
			else
			{
				repeatCount = ImageBehavior.GetRepeatCount(decoder);
			}
			if (repeatCount == 0)
			{
				return RepeatBehavior.Forever;
			}
			return new RepeatBehavior((double)repeatCount);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003AB5C File Offset: 0x00038D5C
		private static int GetRepeatCount(BitmapDecoder decoder)
		{
			BitmapMetadata applicationExtension = ImageBehavior.GetApplicationExtension(decoder, "NETSCAPE2.0");
			if (applicationExtension != null)
			{
				byte[] queryOrNull = applicationExtension.GetQueryOrNull("/Data");
				if (queryOrNull != null && queryOrNull.Length >= 4)
				{
					return (int)BitConverter.ToUInt16(queryOrNull, 2);
				}
			}
			return 1;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003AB98 File Offset: 0x00038D98
		private static BitmapMetadata GetApplicationExtension(BitmapDecoder decoder, string application)
		{
			int num = 0;
			string query = "/appext";
			for (BitmapMetadata queryOrNull = decoder.Metadata.GetQueryOrNull(query); queryOrNull != null; queryOrNull = decoder.Metadata.GetQueryOrNull(query))
			{
				byte[] queryOrNull2 = queryOrNull.GetQueryOrNull("/Application");
				if (queryOrNull2 != null && Encoding.ASCII.GetString(queryOrNull2) == application)
				{
					return queryOrNull;
				}
				query = string.Format(CultureInfo.InvariantCulture, "/[{0}]appext", new object[]
				{
					++num
				});
			}
			return null;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0000D47B File Offset: 0x0000B67B
		private static ImageBehavior.FrameMetadata GetFrameMetadata(BitmapDecoder decoder, GifFile gifMetadata, int frameIndex)
		{
			if (gifMetadata != null && gifMetadata.Frames.Count > frameIndex)
			{
				return ImageBehavior.GetFrameMetadata(gifMetadata.Frames[frameIndex]);
			}
			return ImageBehavior.GetFrameMetadata(decoder.Frames[frameIndex]);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003AC14 File Offset: 0x00038E14
		private static ImageBehavior.FrameMetadata GetFrameMetadata(BitmapFrame frame)
		{
			BitmapMetadata metadata = (BitmapMetadata)frame.Metadata;
			TimeSpan delay = TimeSpan.FromMilliseconds(100.0);
			int queryOrDefault = metadata.GetQueryOrDefault("/grctlext/Delay", 10);
			if (queryOrDefault != 0)
			{
				delay = TimeSpan.FromMilliseconds((double)(queryOrDefault * 10));
			}
			ImageBehavior.FrameDisposalMethod queryOrDefault2 = (ImageBehavior.FrameDisposalMethod)metadata.GetQueryOrDefault("/grctlext/Disposal", 0);
			return new ImageBehavior.FrameMetadata
			{
				Left = metadata.GetQueryOrDefault("/imgdesc/Left", 0),
				Top = metadata.GetQueryOrDefault("/imgdesc/Top", 0),
				Width = metadata.GetQueryOrDefault("/imgdesc/Width", frame.PixelWidth),
				Height = metadata.GetQueryOrDefault("/imgdesc/Height", frame.PixelHeight),
				Delay = delay,
				DisposalMethod = queryOrDefault2
			};
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003ACCC File Offset: 0x00038ECC
		private static ImageBehavior.FrameMetadata GetFrameMetadata(GifFrame gifMetadata)
		{
			GifImageDescriptor descriptor = gifMetadata.Descriptor;
			ImageBehavior.FrameMetadata frameMetadata = new ImageBehavior.FrameMetadata
			{
				Left = descriptor.Left,
				Top = descriptor.Top,
				Width = descriptor.Width,
				Height = descriptor.Height,
				Delay = TimeSpan.FromMilliseconds(100.0),
				DisposalMethod = ImageBehavior.FrameDisposalMethod.None
			};
			GifGraphicControlExtension gifGraphicControlExtension = gifMetadata.Extensions.OfType<GifGraphicControlExtension>().FirstOrDefault<GifGraphicControlExtension>();
			if (gifGraphicControlExtension != null)
			{
				if (gifGraphicControlExtension.Delay != 0)
				{
					frameMetadata.Delay = TimeSpan.FromMilliseconds((double)gifGraphicControlExtension.Delay);
				}
				frameMetadata.DisposalMethod = (ImageBehavior.FrameDisposalMethod)gifGraphicControlExtension.DisposalMethod;
			}
			return frameMetadata;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003AD6C File Offset: 0x00038F6C
		private static ImageBehavior.Int32Size GetFullSize(BitmapDecoder decoder, GifFile gifMetadata)
		{
			if (gifMetadata != null)
			{
				GifLogicalScreenDescriptor logicalScreenDescriptor = gifMetadata.Header.LogicalScreenDescriptor;
				return new ImageBehavior.Int32Size(logicalScreenDescriptor.Width, logicalScreenDescriptor.Height);
			}
			int queryOrDefault = decoder.Metadata.GetQueryOrDefault("/logscrdesc/Width", 0);
			int queryOrDefault2 = decoder.Metadata.GetQueryOrDefault("/logscrdesc/Height", 0);
			return new ImageBehavior.Int32Size(queryOrDefault, queryOrDefault2);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0000D4B1 File Offset: 0x0000B6B1
		private static T GetQueryOrDefault<T>(this BitmapMetadata metadata, string query, T defaultValue)
		{
			if (metadata.ContainsQuery(query))
			{
				return (T)((object)Convert.ChangeType(metadata.GetQuery(query), typeof(T), CultureInfo.InvariantCulture));
			}
			return defaultValue;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003ADC4 File Offset: 0x00038FC4
		private static T GetQueryOrNull<T>(this BitmapMetadata metadata, string query) where T : class
		{
			if (metadata.ContainsQuery(query))
			{
				return metadata.GetQuery(query) as T;
			}
			return default(T);
		}

		// Token: 0x040006E2 RID: 1762
		public static readonly DependencyProperty AnimatedSourceProperty = DependencyProperty.RegisterAttached("AnimatedSource", typeof(ImageSource), typeof(ImageBehavior), new UIPropertyMetadata(null, new PropertyChangedCallback(ImageBehavior.AnimatedSourceChanged)));

		// Token: 0x040006E3 RID: 1763
		public static readonly DependencyProperty RepeatBehaviorProperty = DependencyProperty.RegisterAttached("RepeatBehavior", typeof(RepeatBehavior), typeof(ImageBehavior), new UIPropertyMetadata(default(RepeatBehavior), new PropertyChangedCallback(ImageBehavior.RepeatBehaviorChanged)));

		// Token: 0x040006E4 RID: 1764
		public static readonly DependencyProperty AnimateInDesignModeProperty = DependencyProperty.RegisterAttached("AnimateInDesignMode", typeof(bool), typeof(ImageBehavior), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(ImageBehavior.AnimateInDesignModeChanged)));

		// Token: 0x040006E5 RID: 1765
		public static readonly DependencyProperty AutoStartProperty = DependencyProperty.RegisterAttached("AutoStart", typeof(bool), typeof(ImageBehavior), new PropertyMetadata(true));

		// Token: 0x040006E6 RID: 1766
		private static readonly DependencyPropertyKey AnimationControllerPropertyKey = DependencyProperty.RegisterAttachedReadOnly("AnimationController", typeof(ImageAnimationController), typeof(ImageBehavior), new PropertyMetadata(null));

		// Token: 0x040006E7 RID: 1767
		private static readonly DependencyPropertyKey IsAnimationLoadedPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsAnimationLoaded", typeof(bool), typeof(ImageBehavior), new PropertyMetadata(false));

		// Token: 0x040006E8 RID: 1768
		public static readonly DependencyProperty IsAnimationLoadedProperty = ImageBehavior.IsAnimationLoadedPropertyKey.DependencyProperty;

		// Token: 0x040006E9 RID: 1769
		public static readonly RoutedEvent AnimationLoadedEvent = EventManager.RegisterRoutedEvent("AnimationLoaded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ImageBehavior));

		// Token: 0x040006EA RID: 1770
		public static readonly RoutedEvent AnimationCompletedEvent = EventManager.RegisterRoutedEvent("AnimationCompleted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ImageBehavior));

		// Token: 0x02000187 RID: 391
		private struct Int32Size
		{
			// Token: 0x06000EF0 RID: 3824 RVA: 0x0000D4DE File Offset: 0x0000B6DE
			public Int32Size(int width, int height)
			{
				this = default(ImageBehavior.Int32Size);
				this.Width = width;
				this.Height = height;
			}

			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0000D4F5 File Offset: 0x0000B6F5
			// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0000D4FD File Offset: 0x0000B6FD
			public int Width { readonly get; private set; }

			// Token: 0x17000427 RID: 1063
			// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0000D506 File Offset: 0x0000B706
			// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0000D50E File Offset: 0x0000B70E
			public int Height { readonly get; private set; }
		}

		// Token: 0x02000188 RID: 392
		private class FrameMetadata
		{
			// Token: 0x17000428 RID: 1064
			// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0000D517 File Offset: 0x0000B717
			// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0000D51F File Offset: 0x0000B71F
			public int Left { get; set; }

			// Token: 0x17000429 RID: 1065
			// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0000D528 File Offset: 0x0000B728
			// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x0000D530 File Offset: 0x0000B730
			public int Top { get; set; }

			// Token: 0x1700042A RID: 1066
			// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0000D539 File Offset: 0x0000B739
			// (set) Token: 0x06000EFA RID: 3834 RVA: 0x0000D541 File Offset: 0x0000B741
			public int Width { get; set; }

			// Token: 0x1700042B RID: 1067
			// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0000D54A File Offset: 0x0000B74A
			// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0000D552 File Offset: 0x0000B752
			public int Height { get; set; }

			// Token: 0x1700042C RID: 1068
			// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0000D55B File Offset: 0x0000B75B
			// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0000D563 File Offset: 0x0000B763
			public TimeSpan Delay { get; set; }

			// Token: 0x1700042D RID: 1069
			// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0000D56C File Offset: 0x0000B76C
			// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0000D574 File Offset: 0x0000B774
			public ImageBehavior.FrameDisposalMethod DisposalMethod { get; set; }
		}

		// Token: 0x02000189 RID: 393
		private enum FrameDisposalMethod
		{
			// Token: 0x040006F4 RID: 1780
			None,
			// Token: 0x040006F5 RID: 1781
			DoNotDispose,
			// Token: 0x040006F6 RID: 1782
			RestoreBackground,
			// Token: 0x040006F7 RID: 1783
			RestorePrevious
		}
	}
}
