using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace BlueStacks.Common
{
	// Token: 0x02000153 RID: 339
	public class CustomPictureBox : Image
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0000C334 File Offset: 0x0000A534
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0000C33C File Offset: 0x0000A53C
		public CustomPictureBox.State ButtonState { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0000C345 File Offset: 0x0000A545
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0000C34D File Offset: 0x0000A54D
		public bool IsFullImagePath { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0000C356 File Offset: 0x0000A556
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0000C368 File Offset: 0x0000A568
		public string ImageName
		{
			get
			{
				return (string)base.GetValue(CustomPictureBox.ImageNameProperty);
			}
			set
			{
				base.SetValue(CustomPictureBox.ImageNameProperty, value);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0000C376 File Offset: 0x0000A576
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x0000C388 File Offset: 0x0000A588
		public bool IsImageHover
		{
			get
			{
				return (bool)base.GetValue(CustomPictureBox.IsImageHoverProperty);
			}
			set
			{
				base.SetValue(CustomPictureBox.IsImageHoverProperty, value);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0000C39B File Offset: 0x0000A59B
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0000C3AD File Offset: 0x0000A5AD
		public bool IsAlwaysHalfSize
		{
			get
			{
				return (bool)base.GetValue(CustomPictureBox.IsAlwaysHalfSizeProperty);
			}
			set
			{
				base.SetValue(CustomPictureBox.IsAlwaysHalfSizeProperty, value);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000CA0 RID: 3232 RVA: 0x0002E31C File Offset: 0x0002C51C
		// (remove) Token: 0x06000CA1 RID: 3233 RVA: 0x0002E350 File Offset: 0x0002C550
		public static event EventHandler SourceUpdatedEvent;

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0002E384 File Offset: 0x0002C584
		public bool IsImageToBeRotated
		{
			get
			{
				return this.mIsImageToBeRotated;
			}
			set
			{
				this.mIsImageToBeRotated = value;
				if (value)
				{
					base.SizeChanged -= this.CustomPictureBox_SizeChanged;
					base.IsVisibleChanged -= this.CustomPictureBox_IsVisibleChanged;
					base.SizeChanged += this.CustomPictureBox_SizeChanged;
					base.IsVisibleChanged += this.CustomPictureBox_IsVisibleChanged;
					return;
				}
				if (this.mStoryBoard != null)
				{
					this.mStoryBoard.Stop();
				}
				base.SizeChanged -= this.CustomPictureBox_SizeChanged;
				base.IsVisibleChanged -= this.CustomPictureBox_IsVisibleChanged;
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public void SetDisabledState()
		{
			this.ButtonState = CustomPictureBox.State.disabled;
			base.Opacity = 0.4;
			this.SetDefaultImage();
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0000C3E6 File Offset: 0x0000A5E6
		public void SetNormalState()
		{
			this.ButtonState = CustomPictureBox.State.normal;
			base.Opacity = 1.0;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002E41C File Offset: 0x0002C61C
		private string AppendStringToImageName(string appendText)
		{
			if (this.ImageName.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase) || this.ImageName.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) || this.ImageName.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase) || this.ImageName.EndsWith(".ico", StringComparison.InvariantCultureIgnoreCase))
			{
				string extension = Path.GetExtension(this.ImageName);
				string directoryName = Path.GetDirectoryName(this.ImageName);
				return string.Concat(new string[]
				{
					directoryName,
					Path.DirectorySeparatorChar.ToString(),
					Path.GetFileNameWithoutExtension(this.ImageName),
					appendText,
					extension
				});
			}
			return this.ImageName + appendText;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0000C3FE File Offset: 0x0000A5FE
		private string HoverImage
		{
			get
			{
				return this.AppendStringToImageName("_hover");
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0000C40B File Offset: 0x0000A60B
		private string ClickImage
		{
			get
			{
				return this.AppendStringToImageName("_click");
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0000C418 File Offset: 0x0000A618
		private string DisabledImage
		{
			get
			{
				return this.AppendStringToImageName("_dis");
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0000C425 File Offset: 0x0000A625
		public string SelectedImage
		{
			get
			{
				return this.AppendStringToImageName("_selected");
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0000C432 File Offset: 0x0000A632
		public static string AssetsDir
		{
			get
			{
				return Path.Combine(RegistryManager.Instance.ClientInstallDir, RegistryManager.ClientThemeName);
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
		public CustomPictureBox()
		{
			base.MouseEnter += this.PictureBox_MouseEnter;
			base.MouseLeave += this.PictureBox_MouseLeave;
			base.MouseDown += this.PictureBox_MouseDown;
			base.IsEnabledChanged += this.PictureBox_IsEnabledChanged;
			base.MouseUp += this.PictureBox_MouseUp;
			RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.HighQuality);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002E544 File Offset: 0x0002C744
		public static void UpdateImagesFromNewDirectory(string path = "")
		{
			foreach (Tuple<string, bool> tuple in (from _ in CustomPictureBox.sImageAssetsDict
			select new Tuple<string, bool>(_.Key, _.Value.Item2)).ToList<Tuple<string, bool>>())
			{
				if (tuple.Item1.IndexOfAny(new char[]
				{
					Path.AltDirectorySeparatorChar,
					Path.DirectorySeparatorChar
				}) == -1)
				{
					CustomPictureBox.sImageAssetsDict.Remove(tuple.Item1);
					CustomPictureBox.GetBitmapImage(tuple.Item1, path, tuple.Item2);
				}
			}
			CustomPictureBox.NotifyUIElements();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0000C448 File Offset: 0x0000A648
		internal static void NotifyUIElements()
		{
			if (CustomPictureBox.SourceUpdatedEvent != null)
			{
				CustomPictureBox.SourceUpdatedEvent(null, null);
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002E608 File Offset: 0x0002C808
		private static void ImageNameChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = source as CustomPictureBox;
			if (!DesignerProperties.GetIsInDesignMode(customPictureBox))
			{
				customPictureBox.SetDefaultImage();
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002E62C File Offset: 0x0002C82C
		private static void IsImageHoverChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = source as CustomPictureBox;
			if (!DesignerProperties.GetIsInDesignMode(customPictureBox))
			{
				if (customPictureBox.IsImageHover)
				{
					customPictureBox.SetHoverImage();
					return;
				}
				customPictureBox.SetDefaultImage();
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002E608 File Offset: 0x0002C808
		private static void IsAlwaysHalfSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = source as CustomPictureBox;
			if (!DesignerProperties.GetIsInDesignMode(customPictureBox))
			{
				customPictureBox.SetDefaultImage();
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0000C45D File Offset: 0x0000A65D
		private void PictureBox_MouseEnter(object sender, MouseEventArgs e)
		{
			if (this.ButtonState == CustomPictureBox.State.normal && !this.IsFullImagePath)
			{
				this.SetHoverImage();
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0000C475 File Offset: 0x0000A675
		private void PictureBox_MouseLeave(object sender, MouseEventArgs e)
		{
			if (!this.IsFullImagePath)
			{
				this.SetDefaultImage();
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0000C485 File Offset: 0x0000A685
		private void PictureBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this.ButtonState == CustomPictureBox.State.normal && !this.IsFullImagePath)
			{
				this.SetClickedImage();
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0000C49D File Offset: 0x0000A69D
		private void PictureBox_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (!this.IsFullImagePath)
			{
				if (base.IsMouseOver && this.ButtonState == CustomPictureBox.State.normal)
				{
					this.SetHoverImage();
					return;
				}
				this.SetDefaultImage();
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002E660 File Offset: 0x0002C860
		public void SetHoverImage()
		{
			try
			{
				if (!this.IsFullImagePath)
				{
					CustomPictureBox.SetBitmapImage(this, this.HoverImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002E698 File Offset: 0x0002C898
		public void SetClickedImage()
		{
			try
			{
				if (!this.IsFullImagePath)
				{
					CustomPictureBox.SetBitmapImage(this, this.ClickImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
		public void SetSelectedImage()
		{
			try
			{
				if (!this.IsFullImagePath)
				{
					CustomPictureBox.SetBitmapImage(this, this.SelectedImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002E708 File Offset: 0x0002C908
		public void SetDisabledImage()
		{
			try
			{
				if (!this.IsFullImagePath)
				{
					CustomPictureBox.SetBitmapImage(this, this.DisabledImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002E740 File Offset: 0x0002C940
		public void SetDefaultImage()
		{
			try
			{
				CustomPictureBox.SetBitmapImage(this, this.ImageName, this.IsFullImagePath);
			}
			catch
			{
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002E774 File Offset: 0x0002C974
		public static BitmapImage GetBitmapImage(string fileName, string assetDirectory = "", bool isFullImagePath = false)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return null;
			}
			if (CustomPictureBox.sImageAssetsDict.ContainsKey(fileName))
			{
				return CustomPictureBox.sImageAssetsDict[fileName].Item1;
			}
			BitmapImage bitmapImage = null;
			if (fileName.IndexOfAny(new char[]
			{
				Path.AltDirectorySeparatorChar,
				Path.DirectorySeparatorChar
			}) != -1)
			{
				if (!isFullImagePath)
				{
					Logger.Warning("Full image path not marked false for image: " + fileName);
				}
				bitmapImage = CustomPictureBox.BitmapFromPath(fileName);
			}
			else if (isFullImagePath)
			{
				Logger.Warning("Full image path marked true for image: " + fileName);
			}
			if (bitmapImage == null)
			{
				if (string.IsNullOrEmpty(assetDirectory))
				{
					assetDirectory = CustomPictureBox.AssetsDir;
				}
				bitmapImage = CustomPictureBox.BitmapFromPath(Path.Combine(assetDirectory, Path.GetFileNameWithoutExtension(fileName) + ".png"));
				if (bitmapImage == null)
				{
					bitmapImage = CustomPictureBox.BitmapFromPath(Path.Combine(assetDirectory, fileName));
					if (bitmapImage == null)
					{
						bitmapImage = CustomPictureBox.BitmapFromPath(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets"), Path.GetFileNameWithoutExtension(fileName) + ".png"));
						if (bitmapImage == null)
						{
							bitmapImage = CustomPictureBox.BitmapFromPath(Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, "Assets"), Path.GetFileNameWithoutExtension(fileName) + ".png"));
						}
					}
				}
			}
			CustomPictureBox.sImageAssetsDict.Add(fileName, new Tuple<BitmapImage, bool>(bitmapImage, isFullImagePath));
			if (bitmapImage == null)
			{
				Logger.Warning("Returning a null image for {0}", new object[]
				{
					fileName
				});
			}
			return bitmapImage;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		private static BitmapImage BitmapFromPath(string path)
		{
			BitmapImage bitmapImage = null;
			if (File.Exists(path))
			{
				bitmapImage = new BitmapImage();
				FileStream fileStream = File.OpenRead(path);
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = fileStream;
				bitmapImage.EndInit();
				fileStream.Close();
				fileStream.Dispose();
			}
			return bitmapImage;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002E914 File Offset: 0x0002CB14
		public static void SetBitmapImage(Image image, string fileName, bool isFullImagePath = false)
		{
			BitmapImage bitmapImage = CustomPictureBox.GetBitmapImage(fileName, "", isFullImagePath);
			if (bitmapImage != null)
			{
				bitmapImage.Freeze();
				BlueStacksUIBinding.Bind(image, Image.SourceProperty, fileName);
				if (image is CustomPictureBox)
				{
					CustomPictureBox customPictureBox = image as CustomPictureBox;
					customPictureBox.BitmapImage = bitmapImage;
					if (customPictureBox.IsAlwaysHalfSize)
					{
						customPictureBox.maxSize = new Point(customPictureBox.MaxWidth, customPictureBox.MaxHeight);
						customPictureBox.MaxWidth = bitmapImage.Width / 2.0;
						customPictureBox.MaxHeight = bitmapImage.Height / 2.0;
					}
					else if (customPictureBox.maxSize != default(Point))
					{
						customPictureBox.MaxWidth = customPictureBox.maxSize.X;
						customPictureBox.MaxHeight = customPictureBox.maxSize.Y;
					}
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002E9E8 File Offset: 0x0002CBE8
		private void CustomPictureBox_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (base.RenderTransform != null)
			{
				RotateTransform rotateTransform = base.RenderTransform as RotateTransform;
				if (rotateTransform != null)
				{
					rotateTransform.CenterX = base.ActualWidth / 2.0;
					rotateTransform.CenterY = base.ActualHeight / 2.0;
				}
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002EA38 File Offset: 0x0002CC38
		private void CustomPictureBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (base.IsVisible && this.mIsImageToBeRotated)
			{
				if (this.mStoryBoard == null)
				{
					this.mStoryBoard = new Storyboard();
					this.animation = new DoubleAnimation
					{
						From = new double?(0.0),
						To = new double?((double)360),
						RepeatBehavior = RepeatBehavior.Forever,
						Duration = new Duration(new TimeSpan(0, 0, 1))
					};
					RotateTransform renderTransform = new RotateTransform
					{
						CenterX = base.ActualWidth / 2.0,
						CenterY = base.ActualHeight / 2.0
					};
					base.RenderTransform = renderTransform;
					Storyboard.SetTarget(this.animation, this);
					Storyboard.SetTargetProperty(this.animation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)", new object[0]));
					this.mStoryBoard.Children.Add(this.animation);
				}
				this.mStoryBoard.Begin();
				return;
			}
			Storyboard storyboard = this.mStoryBoard;
			if (storyboard == null)
			{
				return;
			}
			storyboard.Pause();
		}

		// Token: 0x170003F4 RID: 1012
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0002EB54 File Offset: 0x0002CD54
		public bool IsDisabled
		{
			set
			{
				if (value)
				{
					base.MouseEnter -= this.PictureBox_MouseEnter;
					base.MouseLeave -= this.PictureBox_MouseLeave;
					base.MouseDown -= this.PictureBox_MouseDown;
					base.MouseUp -= this.PictureBox_MouseUp;
					base.IsEnabledChanged -= this.PictureBox_IsEnabledChanged;
					base.Opacity = 0.5;
				}
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002EBD0 File Offset: 0x0002CDD0
		private void PictureBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = sender as CustomPictureBox;
			if (customPictureBox != null)
			{
				object newValue = e.NewValue;
				if (newValue is bool)
				{
					bool flag = (bool)newValue;
					if (flag)
					{
						customPictureBox.SetDefaultImage();
						return;
					}
					customPictureBox.SetDisabledImage();
				}
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002EC10 File Offset: 0x0002CE10
		public void ReloadImages()
		{
			CustomPictureBox.sImageAssetsDict.Remove(this.ClickImage);
			CustomPictureBox.sImageAssetsDict.Remove(this.ImageName);
			CustomPictureBox.sImageAssetsDict.Remove(this.HoverImage);
			CustomPictureBox.sImageAssetsDict.Remove(this.SelectedImage);
			CustomPictureBox.sImageAssetsDict.Remove(this.DisabledImage);
			this.SetDefaultImage();
			CustomPictureBox.GCCollectAsync();
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		private static void GCCollectAsync()
		{
			new Thread(delegate()
			{
				GC.Collect();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x0000C508 File Offset: 0x0000A708
		public bool AllowClickThrough
		{
			get
			{
				return (bool)base.GetValue(CustomPictureBox.AllowClickThroughProperty);
			}
			set
			{
				base.SetValue(CustomPictureBox.AllowClickThroughProperty, value);
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002EC80 File Offset: 0x0002CE80
		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			try
			{
				if (hitTestParameters != null && this.AllowClickThrough)
				{
					Point position = Mouse.GetPosition(this);
					int pixelWidth = ((BitmapSource)base.Source).PixelWidth;
					int pixelHeight = ((BitmapSource)base.Source).PixelHeight;
					double num = position.X * (double)pixelWidth / base.ActualWidth;
					double num2 = position.Y * (double)pixelHeight / base.ActualHeight;
					byte[] array = new byte[4];
					try
					{
						new CroppedBitmap((BitmapSource)base.Source, new Int32Rect((int)num, (int)num2, 1, 1)).CopyPixels(array, 4, 0);
						if ((int)array[3] < RegistryManager.Instance.AdvancedControlTransparencyLevel)
						{
							Logger.Info(string.Format("HitTestCore pixel density at Image location- (X:{0} Y:{1}) is (R:{2} B:{3} G{4} A{5})", new object[]
							{
								num,
								num2,
								array[0],
								array[1],
								array[2],
								array[3]
							}));
							return null;
						}
					}
					catch (Exception)
					{
						Logger.Info(string.Format("Unable to get HitTestCore pixel density at Image location- X:{0} Y:{1}", num, num2));
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("HitTestCore: " + ex.Message);
			}
			return base.HitTestCore(hitTestParameters);
		}

		// Token: 0x04000607 RID: 1543
		private Point maxSize;

		// Token: 0x04000608 RID: 1544
		internal BitmapImage BitmapImage;

		// Token: 0x04000609 RID: 1545
		internal DoubleAnimation animation;

		// Token: 0x0400060C RID: 1548
		public static readonly DependencyProperty ImageNameProperty = DependencyProperty.Register("ImageName", typeof(string), typeof(CustomPictureBox), new FrameworkPropertyMetadata("", new PropertyChangedCallback(CustomPictureBox.ImageNameChanged)));

		// Token: 0x0400060D RID: 1549
		public static readonly DependencyProperty IsImageHoverProperty = DependencyProperty.Register("IsImageHover", typeof(bool), typeof(CustomPictureBox), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CustomPictureBox.IsImageHoverChanged)));

		// Token: 0x0400060E RID: 1550
		public static readonly DependencyProperty IsAlwaysHalfSizeProperty = DependencyProperty.Register("IsAlwaysHalfSize", typeof(bool), typeof(CustomPictureBox), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CustomPictureBox.IsAlwaysHalfSizeChanged)));

		// Token: 0x04000610 RID: 1552
		public static readonly Dictionary<string, Tuple<BitmapImage, bool>> sImageAssetsDict = new Dictionary<string, Tuple<BitmapImage, bool>>();

		// Token: 0x04000611 RID: 1553
		private Storyboard mStoryBoard;

		// Token: 0x04000612 RID: 1554
		private bool mIsImageToBeRotated;

		// Token: 0x04000613 RID: 1555
		public static readonly DependencyProperty AllowClickThroughProperty = DependencyProperty.Register("AllowClickThrough", typeof(bool), typeof(CustomPictureBox), new FrameworkPropertyMetadata(false));

		// Token: 0x02000154 RID: 340
		public enum State
		{
			// Token: 0x04000615 RID: 1557
			normal,
			// Token: 0x04000616 RID: 1558
			disabled
		}
	}
}
