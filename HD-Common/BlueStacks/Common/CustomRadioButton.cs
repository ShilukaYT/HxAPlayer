using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x020000B1 RID: 177
	public class CustomRadioButton : RadioButton, IComponentConnector, IStyleConnector
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x00004775 File Offset: 0x00002975
		public CustomRadioButton()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00004783 File Offset: 0x00002983
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00004795 File Offset: 0x00002995
		public Thickness TextMargin
		{
			get
			{
				return (Thickness)base.GetValue(CustomRadioButton.TextMarginProperty);
			}
			set
			{
				base.SetValue(CustomRadioButton.TextMarginProperty, value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000047A8 File Offset: 0x000029A8
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000047BA File Offset: 0x000029BA
		public string ImageName
		{
			get
			{
				return (string)base.GetValue(CustomRadioButton.ImageNameProperty);
			}
			set
			{
				base.SetValue(CustomRadioButton.ImageNameProperty, value);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000047C8 File Offset: 0x000029C8
		public CustomPictureBox RadioBtnImage
		{
			get
			{
				return (CustomPictureBox)base.Template.FindName("mRadioBtnImage", this);
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00017A90 File Offset: 0x00015C90
		private void ContentPresenter_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			try
			{
				if (sender != null)
				{
					TextBlock textBlock = VisualTreeHelper.GetChild(sender as ContentPresenter, 0) as TextBlock;
					if (textBlock != null)
					{
						if (textBlock.IsTextTrimmed())
						{
							ToolTipService.SetIsEnabled(this, true);
						}
						else
						{
							ToolTipService.SetIsEnabled(this, false);
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00017AE4 File Offset: 0x00015CE4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customradiobutton.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000047E0 File Offset: 0x000029E0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000047E9 File Offset: 0x000029E9
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((ContentPresenter)target).SizeChanged += this.ContentPresenter_SizeChanged;
			}
		}

		// Token: 0x0400020A RID: 522
		public static readonly DependencyProperty ImageNameProperty = DependencyProperty.Register("ImageName", typeof(string), typeof(CustomRadioButton), new PropertyMetadata(""));

		// Token: 0x0400020B RID: 523
		public static readonly DependencyProperty TextMarginProperty = DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(CustomRadioButton), new PropertyMetadata(new Thickness(10.0, 0.0, 0.0, 0.0)));

		// Token: 0x0400020C RID: 524
		private bool _contentLoaded;
	}
}
