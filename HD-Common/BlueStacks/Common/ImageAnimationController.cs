using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BlueStacks.Common
{
	// Token: 0x02000183 RID: 387
	public class ImageAnimationController : IDisposable
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00039EA0 File Offset: 0x000380A0
		internal ImageAnimationController(Image image, ObjectAnimationUsingKeyFrames animation, bool autoStart)
		{
			this._image = image;
			this._animation = animation;
			this._animation.Completed += this.AnimationCompleted;
			this._clock = this._animation.CreateClock();
			this._clockController = this._clock.Controller;
			ImageAnimationController._sourceDescriptor.AddValueChanged(image, new EventHandler(this.ImageSourceChanged));
			this._clockController.Pause();
			this._image.ApplyAnimationClock(Image.SourceProperty, this._clock);
			if (autoStart)
			{
				this._clockController.Resume();
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0000D245 File Offset: 0x0000B445
		private void AnimationCompleted(object sender, EventArgs e)
		{
			this._image.RaiseEvent(new RoutedEventArgs(ImageBehavior.AnimationCompletedEvent, this._image));
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0000D262 File Offset: 0x0000B462
		private void ImageSourceChanged(object sender, EventArgs e)
		{
			this.OnCurrentFrameChanged();
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0000D26A File Offset: 0x0000B46A
		public int FrameCount
		{
			get
			{
				return this._animation.KeyFrames.Count;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0000D27C File Offset: 0x0000B47C
		public bool IsPaused
		{
			get
			{
				return this._clock.IsPaused;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0000D289 File Offset: 0x0000B489
		public bool IsComplete
		{
			get
			{
				return this._clock.CurrentState == ClockState.Filling;
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00039F40 File Offset: 0x00038140
		public void GotoFrame(int index)
		{
			ObjectKeyFrame objectKeyFrame = this._animation.KeyFrames[index];
			this._clockController.Seek(objectKeyFrame.KeyTime.TimeSpan, TimeSeekOrigin.BeginTime);
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00039F7C File Offset: 0x0003817C
		public int CurrentFrame
		{
			get
			{
				TimeSpan? time = this._clock.CurrentTime;
				var <>f__AnonymousType = this._animation.KeyFrames.Cast<ObjectKeyFrame>().Select((ObjectKeyFrame f, int i) => new
				{
					Time = f.KeyTime.TimeSpan,
					Index = i
				}).FirstOrDefault(fi => fi.Time >= time);
				if (<>f__AnonymousType != null)
				{
					return <>f__AnonymousType.Index;
				}
				return -1;
			}
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0000D299 File Offset: 0x0000B499
		public void Pause()
		{
			this._clockController.Pause();
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0000D2A6 File Offset: 0x0000B4A6
		public void Play()
		{
			this._clockController.Resume();
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000EBB RID: 3771 RVA: 0x00039FF4 File Offset: 0x000381F4
		// (remove) Token: 0x06000EBC RID: 3772 RVA: 0x0003A02C File Offset: 0x0003822C
		public event EventHandler CurrentFrameChanged;

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003A064 File Offset: 0x00038264
		private void OnCurrentFrameChanged()
		{
			EventHandler currentFrameChanged = this.CurrentFrameChanged;
			if (currentFrameChanged != null)
			{
				currentFrameChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003A088 File Offset: 0x00038288
		~ImageAnimationController()
		{
			this.Dispose(false);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0000D2B3 File Offset: 0x0000B4B3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003A0B8 File Offset: 0x000382B8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._image.BeginAnimation(Image.SourceProperty, null);
				this._animation.Completed -= this.AnimationCompleted;
				ImageAnimationController._sourceDescriptor.RemoveValueChanged(this._image, new EventHandler(this.ImageSourceChanged));
				this._image.Source = null;
			}
		}

		// Token: 0x040006D9 RID: 1753
		private static readonly DependencyPropertyDescriptor _sourceDescriptor = DependencyPropertyDescriptor.FromProperty(Image.SourceProperty, typeof(Image));

		// Token: 0x040006DA RID: 1754
		private readonly Image _image;

		// Token: 0x040006DB RID: 1755
		private readonly ObjectAnimationUsingKeyFrames _animation;

		// Token: 0x040006DC RID: 1756
		private readonly AnimationClock _clock;

		// Token: 0x040006DD RID: 1757
		private readonly ClockController _clockController;
	}
}
