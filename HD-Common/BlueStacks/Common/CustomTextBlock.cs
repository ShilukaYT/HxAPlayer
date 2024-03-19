using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x020000B0 RID: 176
	public class CustomTextBlock : TextBlock, IComponentConnector
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x00004706 File Offset: 0x00002906
		public CustomTextBlock()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00004714 File Offset: 0x00002914
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00004726 File Offset: 0x00002926
		public bool ForcedTooltip
		{
			get
			{
				return (bool)base.GetValue(CustomTextBlock.ForcedTooltipProperty);
			}
			set
			{
				base.SetValue(CustomTextBlock.ForcedTooltipProperty, value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00004739 File Offset: 0x00002939
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000474B File Offset: 0x0000294B
		public bool SetToolTip
		{
			get
			{
				return (bool)base.GetValue(CustomTextBlock.SetToolTipProperty);
			}
			set
			{
				base.SetValue(CustomTextBlock.SetToolTipProperty, value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00004739 File Offset: 0x00002939
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000474B File Offset: 0x0000294B
		public bool HoverForegroundProperty
		{
			get
			{
				return (bool)base.GetValue(CustomTextBlock.SetToolTipProperty);
			}
			set
			{
				base.SetValue(CustomTextBlock.SetToolTipProperty, value);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000475E File Offset: 0x0000295E
		private static void OnSetToolTipChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as CustomTextBlock).OnSetToolTipChanged(e);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00017960 File Offset: 0x00015B60
		private void OnSetToolTipChanged(DependencyPropertyChangedEventArgs args)
		{
			if (!this.ForcedTooltip)
			{
				bool flag;
				if (bool.TryParse(args.NewValue.ToString(), out flag) && flag && this.IsTextTrimmed())
				{
					ToolTipService.SetIsEnabled(this, true);
					if (base.ToolTip == null)
					{
						base.ToolTip = base.Text;
						return;
					}
				}
				else
				{
					ToolTipService.SetIsEnabled(this, false);
				}
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000179B8 File Offset: 0x00015BB8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customtextblock.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000476C File Offset: 0x0000296C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000206 RID: 518
		public static readonly DependencyProperty SetToolTipProperty = DependencyProperty.Register("SetToolTip", typeof(bool), typeof(CustomTextBlock), new PropertyMetadata(false, new PropertyChangedCallback(CustomTextBlock.OnSetToolTipChanged)));

		// Token: 0x04000207 RID: 519
		public static readonly DependencyProperty MouseOverForegroundChangedProperty = DependencyProperty.RegisterAttached("HoverForegroundProperty", typeof(bool), typeof(CustomTextBlock), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

		// Token: 0x04000208 RID: 520
		public static readonly DependencyProperty ForcedTooltipProperty = DependencyProperty.Register("ForcedTooltip", typeof(bool), typeof(CustomButton), new PropertyMetadata(false));

		// Token: 0x04000209 RID: 521
		private bool _contentLoaded;
	}
}
