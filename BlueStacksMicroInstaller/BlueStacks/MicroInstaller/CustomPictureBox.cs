using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x02000093 RID: 147
	public class CustomPictureBox : Image
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000E806 File Offset: 0x0000CA06
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000E80E File Offset: 0x0000CA0E
		public CustomPictureBox.State ButtonState { get; set; } = CustomPictureBox.State.normal;

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000E817 File Offset: 0x0000CA17
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000E81F File Offset: 0x0000CA1F
		public bool IsFullImagePath { get; set; } = false;

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000E828 File Offset: 0x0000CA28
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000E83A File Offset: 0x0000CA3A
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000E849 File Offset: 0x0000CA49
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000E85B File Offset: 0x0000CA5B
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000E86F File Offset: 0x0000CA6F
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000E881 File Offset: 0x0000CA81
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

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060002B1 RID: 689 RVA: 0x0000E898 File Offset: 0x0000CA98
		// (remove) Token: 0x060002B2 RID: 690 RVA: 0x0000E8CC File Offset: 0x0000CACC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler SourceUpdatedEvent;

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000E8FF File Offset: 0x0000CAFF
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000E908 File Offset: 0x0000CB08
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
				}
				else
				{
					bool flag = this.mStoryBoard != null;
					if (flag)
					{
						this.mStoryBoard.Stop();
					}
					base.SizeChanged -= this.CustomPictureBox_SizeChanged;
					base.IsVisibleChanged -= this.CustomPictureBox_IsVisibleChanged;
				}
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000E9B5 File Offset: 0x0000CBB5
		public void SetDisabledState()
		{
			this.ButtonState = CustomPictureBox.State.disabled;
			base.Opacity = 0.4;
			this.SetDefaultImage();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000E9D7 File Offset: 0x0000CBD7
		public void SetNormalState()
		{
			this.ButtonState = CustomPictureBox.State.normal;
			base.Opacity = 1.0;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		private string AppendStringToImageName(string appendText)
		{
			bool flag = this.ImageName.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase) || this.ImageName.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) || this.ImageName.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase) || this.ImageName.EndsWith(".ico", StringComparison.InvariantCultureIgnoreCase);
			string result;
			if (flag)
			{
				string extension = Path.GetExtension(this.ImageName);
				string directoryName = Path.GetDirectoryName(this.ImageName);
				result = string.Concat(new string[]
				{
					directoryName,
					Path.DirectorySeparatorChar.ToString(),
					Path.GetFileNameWithoutExtension(this.ImageName),
					appendText,
					extension
				});
			}
			else
			{
				result = this.ImageName + appendText;
			}
			return result;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		private string HoverImage
		{
			get
			{
				return this.AppendStringToImageName("_hover");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000EAC5 File Offset: 0x0000CCC5
		private string ClickImage
		{
			get
			{
				return this.AppendStringToImageName("_click");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000EAD2 File Offset: 0x0000CCD2
		private string DisabledImage
		{
			get
			{
				return this.AppendStringToImageName("_dis");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000EADF File Offset: 0x0000CCDF
		public string SelectedImage
		{
			get
			{
				return this.AppendStringToImageName("_selected");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public static string AssetsDir
		{
			get
			{
				return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assetss");
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000EB04 File Offset: 0x0000CD04
		public CustomPictureBox()
		{
			base.MouseEnter += this.PictureBox_MouseEnter;
			base.MouseLeave += this.PictureBox_MouseLeave;
			base.MouseDown += this.PictureBox_MouseDown;
			base.IsEnabledChanged += this.PictureBox_IsEnabledChanged;
			base.MouseUp += this.PictureBox_MouseUp;
			RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.HighQuality);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		public static void UpdateImagesFromNewDirectory(string path = "")
		{
			List<Tuple<string, bool>> list = (from _ in CustomPictureBox.sImageAssetsDict
			select new Tuple<string, bool>(_.Key, _.Value.Item2)).ToList<Tuple<string, bool>>();
			foreach (Tuple<string, bool> tuple in list)
			{
				bool flag = tuple.Item1.IndexOfAny(new char[]
				{
					Path.AltDirectorySeparatorChar,
					Path.DirectorySeparatorChar
				}) == -1;
				if (flag)
				{
					CustomPictureBox.sImageAssetsDict.Remove(tuple.Item1);
					CustomPictureBox.GetBitmapImage(tuple.Item1, path, tuple.Item2);
				}
			}
			CustomPictureBox.NotifyUIElements();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000EC80 File Offset: 0x0000CE80
		internal static void NotifyUIElements()
		{
			bool flag = CustomPictureBox.SourceUpdatedEvent != null;
			if (flag)
			{
				CustomPictureBox.SourceUpdatedEvent(null, null);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		private static void ImageNameChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = source as CustomPictureBox;
			bool flag = !DesignerProperties.GetIsInDesignMode(customPictureBox);
			if (flag)
			{
				customPictureBox.SetDefaultImage();
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		private static void IsImageHoverChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = source as CustomPictureBox;
			bool flag = !DesignerProperties.GetIsInDesignMode(customPictureBox);
			if (flag)
			{
				bool isImageHover = customPictureBox.IsImageHover;
				if (isImageHover)
				{
					customPictureBox.SetHoverImage();
				}
				else
				{
					customPictureBox.SetDefaultImage();
				}
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		private static void IsAlwaysHalfSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = source as CustomPictureBox;
			bool flag = !DesignerProperties.GetIsInDesignMode(customPictureBox);
			if (flag)
			{
				customPictureBox.SetDefaultImage();
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000ED48 File Offset: 0x0000CF48
		private void PictureBox_MouseEnter(object sender, MouseEventArgs e)
		{
			bool flag = this.ButtonState == CustomPictureBox.State.normal && !this.IsFullImagePath;
			if (flag)
			{
				this.SetHoverImage();
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000ED78 File Offset: 0x0000CF78
		private void PictureBox_MouseLeave(object sender, MouseEventArgs e)
		{
			bool flag = !this.IsFullImagePath;
			if (flag)
			{
				this.SetDefaultImage();
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000ED9C File Offset: 0x0000CF9C
		private void PictureBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = this.ButtonState == CustomPictureBox.State.normal && !this.IsFullImagePath;
			if (flag)
			{
				this.SetClickedImage();
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		private void PictureBox_MouseUp(object sender, MouseButtonEventArgs e)
		{
			bool flag = !this.IsFullImagePath;
			if (flag)
			{
				bool flag2 = base.IsMouseOver && this.ButtonState == CustomPictureBox.State.normal;
				if (flag2)
				{
					this.SetHoverImage();
				}
				else
				{
					this.SetDefaultImage();
				}
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000EE18 File Offset: 0x0000D018
		public void SetHoverImage()
		{
			try
			{
				bool flag = !this.IsFullImagePath;
				if (flag)
				{
					CustomPictureBox.SetBitmapImage(this, this.HoverImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public void SetClickedImage()
		{
			try
			{
				bool flag = !this.IsFullImagePath;
				if (flag)
				{
					CustomPictureBox.SetBitmapImage(this, this.ClickImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		public void SetSelectedImage()
		{
			try
			{
				bool flag = !this.IsFullImagePath;
				if (flag)
				{
					CustomPictureBox.SetBitmapImage(this, this.SelectedImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
		public void SetDisabledImage()
		{
			try
			{
				bool flag = !this.IsFullImagePath;
				if (flag)
				{
					CustomPictureBox.SetBitmapImage(this, this.DisabledImage, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000EF28 File Offset: 0x0000D128
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

		// Token: 0x060002CC RID: 716 RVA: 0x0000EF64 File Offset: 0x0000D164
		public static BitmapImage GetBitmapImage(string fileName, string assetDirectory = "", bool isFullImagePath = false)
		{
			bool flag = string.IsNullOrEmpty(fileName);
			BitmapImage result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = CustomPictureBox.sImageAssetsDict.ContainsKey(fileName);
				if (flag2)
				{
					result = CustomPictureBox.sImageAssetsDict[fileName].Item1;
				}
				else
				{
					BitmapImage bitmapImage = null;
					bool flag3 = fileName.IndexOfAny(new char[]
					{
						Path.AltDirectorySeparatorChar,
						Path.DirectorySeparatorChar
					}) != -1;
					if (flag3)
					{
						bool flag4 = !isFullImagePath;
						if (flag4)
						{
							Logger.Warning("Full image path not marked false for image: " + fileName);
						}
						bitmapImage = CustomPictureBox.BitmapFromPath(fileName);
					}
					else if (isFullImagePath)
					{
						Logger.Warning("Full image path marked true for image: " + fileName);
					}
					bool flag5 = bitmapImage == null;
					if (flag5)
					{
						bool flag6 = string.IsNullOrEmpty(assetDirectory);
						if (flag6)
						{
							assetDirectory = CustomPictureBox.AssetsDir;
						}
						bitmapImage = CustomPictureBox.BitmapFromPath(Path.Combine(assetDirectory, Path.GetFileNameWithoutExtension(fileName) + ".png"));
						bool flag7 = bitmapImage == null;
						if (flag7)
						{
							bitmapImage = CustomPictureBox.BitmapFromPath(Path.Combine(assetDirectory, fileName));
							bool flag8 = bitmapImage == null;
							if (flag8)
							{
								string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
								bitmapImage = CustomPictureBox.BitmapFromPath(Path.Combine(path, Path.GetFileNameWithoutExtension(fileName) + ".png"));
							}
						}
					}
					CustomPictureBox.sImageAssetsDict.Add(fileName, new Tuple<BitmapImage, bool>(bitmapImage, isFullImagePath));
					bool flag9 = bitmapImage == null;
					if (flag9)
					{
						Logger.Warning("Returning a null image for {0}", new object[]
						{
							fileName
						});
					}
					result = bitmapImage;
				}
			}
			return result;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		private static BitmapImage BitmapFromPath(string path)
		{
			BitmapImage bitmapImage = null;
			bool flag = File.Exists(path);
			if (flag)
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

		// Token: 0x060002CE RID: 718 RVA: 0x0000F148 File Offset: 0x0000D348
		public static void SetBitmapImage(Image image, string fileName, bool isFullImagePath = false)
		{
			BitmapImage bitmapImage = CustomPictureBox.GetBitmapImage(fileName, "", isFullImagePath);
			bool flag = bitmapImage != null;
			if (flag)
			{
				bitmapImage.Freeze();
				BlueStacksUIBinding.Bind(image, Image.SourceProperty, fileName);
				bool flag2 = image is CustomPictureBox;
				if (flag2)
				{
					CustomPictureBox customPictureBox = image as CustomPictureBox;
					customPictureBox.BitmapImage = bitmapImage;
					bool isAlwaysHalfSize = customPictureBox.IsAlwaysHalfSize;
					if (isAlwaysHalfSize)
					{
						customPictureBox.maxSize = new Point(customPictureBox.MaxWidth, customPictureBox.MaxHeight);
						customPictureBox.MaxWidth = bitmapImage.Width / 2.0;
						customPictureBox.MaxHeight = bitmapImage.Height / 2.0;
					}
					else
					{
						bool flag3 = customPictureBox.maxSize != default(Point);
						if (flag3)
						{
							customPictureBox.MaxWidth = customPictureBox.maxSize.X;
							customPictureBox.MaxHeight = customPictureBox.maxSize.Y;
						}
					}
				}
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000F23C File Offset: 0x0000D43C
		private void CustomPictureBox_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			bool flag = base.RenderTransform != null;
			if (flag)
			{
				RotateTransform rotateTransform = base.RenderTransform as RotateTransform;
				bool flag2 = rotateTransform != null;
				if (flag2)
				{
					rotateTransform.CenterX = base.ActualWidth / 2.0;
					rotateTransform.CenterY = base.ActualHeight / 2.0;
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		private void CustomPictureBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			bool flag = base.IsVisible && this.mIsImageToBeRotated;
			if (flag)
			{
				bool flag2 = this.mStoryBoard == null;
				if (flag2)
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
			}
			else
			{
				Storyboard storyboard = this.mStoryBoard;
				if (storyboard != null)
				{
					storyboard.Pause();
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
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

		// Token: 0x060002D2 RID: 722 RVA: 0x0000F45C File Offset: 0x0000D65C
		private void PictureBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			CustomPictureBox customPictureBox = sender as CustomPictureBox;
			bool flag;
			bool flag2;
			if (customPictureBox != null)
			{
				object newValue = e.NewValue;
				if (newValue is bool)
				{
					flag = (bool)newValue;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			else
			{
				flag2 = false;
			}
			bool flag3 = flag2;
			if (flag3)
			{
				bool flag4 = flag;
				if (flag4)
				{
					customPictureBox.SetDefaultImage();
				}
				else
				{
					customPictureBox.SetDisabledImage();
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
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

		// Token: 0x060002D4 RID: 724 RVA: 0x0000F524 File Offset: 0x0000D724
		private static void GCCollectAsync()
		{
			Thread thread = new Thread(delegate()
			{
				GC.Collect();
			})
			{
				IsBackground = true
			};
			thread.Start();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000F566 File Offset: 0x0000D766
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000F578 File Offset: 0x0000D778
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

		// Token: 0x060002D7 RID: 727 RVA: 0x0000F58C File Offset: 0x0000D78C
		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			try
			{
				bool flag = hitTestParameters != null && this.AllowClickThrough;
				if (flag)
				{
					Point position = Mouse.GetPosition(this);
					int pixelWidth = ((BitmapSource)base.Source).PixelWidth;
					int pixelHeight = ((BitmapSource)base.Source).PixelHeight;
					double num = position.X * (double)pixelWidth / base.ActualWidth;
					double num2 = position.Y * (double)pixelHeight / base.ActualHeight;
					byte[] array = new byte[4];
					try
					{
						CroppedBitmap croppedBitmap = new CroppedBitmap((BitmapSource)base.Source, new Int32Rect((int)num, (int)num2, 1, 1));
						croppedBitmap.CopyPixels(array, 4, 0);
						bool flag2 = array[3] < 50;
						if (flag2)
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

		// Token: 0x040004F2 RID: 1266
		private Point maxSize = default(Point);

		// Token: 0x040004F3 RID: 1267
		internal BitmapImage BitmapImage = null;

		// Token: 0x040004F4 RID: 1268
		internal DoubleAnimation animation;

		// Token: 0x040004F7 RID: 1271
		public static readonly DependencyProperty ImageNameProperty = DependencyProperty.Register("ImageName", typeof(string), typeof(CustomPictureBox), new FrameworkPropertyMetadata("", new PropertyChangedCallback(CustomPictureBox.ImageNameChanged)));

		// Token: 0x040004F8 RID: 1272
		public static readonly DependencyProperty IsImageHoverProperty = DependencyProperty.Register("IsImageHover", typeof(bool), typeof(CustomPictureBox), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CustomPictureBox.IsImageHoverChanged)));

		// Token: 0x040004F9 RID: 1273
		public static readonly DependencyProperty IsAlwaysHalfSizeProperty = DependencyProperty.Register("IsAlwaysHalfSize", typeof(bool), typeof(CustomPictureBox), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CustomPictureBox.IsAlwaysHalfSizeChanged)));

		// Token: 0x040004FB RID: 1275
		public static readonly Dictionary<string, Tuple<BitmapImage, bool>> sImageAssetsDict = new Dictionary<string, Tuple<BitmapImage, bool>>();

		// Token: 0x040004FC RID: 1276
		private Storyboard mStoryBoard = null;

		// Token: 0x040004FD RID: 1277
		private bool mIsImageToBeRotated = false;

		// Token: 0x040004FE RID: 1278
		public static readonly DependencyProperty AllowClickThroughProperty = DependencyProperty.Register("AllowClickThrough", typeof(bool), typeof(CustomPictureBox), new FrameworkPropertyMetadata(false));

		// Token: 0x020000E3 RID: 227
		public enum State
		{
			// Token: 0x04000804 RID: 2052
			normal,
			// Token: 0x04000805 RID: 2053
			disabled
		}
	}
}
