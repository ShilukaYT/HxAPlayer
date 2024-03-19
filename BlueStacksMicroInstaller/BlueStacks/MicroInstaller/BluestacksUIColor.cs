using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x02000098 RID: 152
	public class BluestacksUIColor
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00010055 File Offset: 0x0000E255
		internal static string ThemeFilePath
		{
			get
			{
				return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ThemeFile");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0001006B File Offset: 0x0000E26B
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00010073 File Offset: 0x0000E273
		public SerializableDictionary<string, string> DictCategory { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0001007C File Offset: 0x0000E27C
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00010084 File Offset: 0x0000E284
		public SerializableDictionary<string, Brush> DictBrush { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0001008D File Offset: 0x0000E28D
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00010095 File Offset: 0x0000E295
		public SerializableDictionary<string, Geometry> DictGeometry { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001009E File Offset: 0x0000E29E
		// (set) Token: 0x06000305 RID: 773 RVA: 0x000100A6 File Offset: 0x0000E2A6
		public SerializableDictionary<string, Transform> DictTransform { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000100AF File Offset: 0x0000E2AF
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000100B7 File Offset: 0x0000E2B7
		public SerializableDictionary<string, CornerRadius> DictCornerRadius { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000100C0 File Offset: 0x0000E2C0
		// (set) Token: 0x06000309 RID: 777 RVA: 0x000100C8 File Offset: 0x0000E2C8
		public SerializableDictionary<string, string> DictThemeAvailable { get; set; }

		// Token: 0x0600030A RID: 778 RVA: 0x000100D4 File Offset: 0x0000E2D4
		public BluestacksUIColor()
		{
			this.DictCategory = new SerializableDictionary<string, string>();
			this.DictBrush = new SerializableDictionary<string, Brush>();
			this.DictGeometry = new SerializableDictionary<string, Geometry>();
			this.DictTransform = new SerializableDictionary<string, Transform>();
			this.DictCornerRadius = new SerializableDictionary<string, CornerRadius>();
			this.DictThemeAvailable = new SerializableDictionary<string, string>();
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600030B RID: 779 RVA: 0x00010134 File Offset: 0x0000E334
		// (remove) Token: 0x0600030C RID: 780 RVA: 0x00010168 File Offset: 0x0000E368
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal static event EventHandler SourceUpdatedEvent;

		// Token: 0x0600030D RID: 781 RVA: 0x0001019C File Offset: 0x0000E39C
		internal static BluestacksUIColor Load(string themePath)
		{
			BluestacksUIColor bluestacksUIColor = new BluestacksUIColor();
			try
			{
				bool flag = File.Exists(themePath);
				if (!flag)
				{
					throw new Exception("Theme file not found exception");
				}
				using (XmlReader xmlReader = XmlReader.Create(themePath, new XmlReaderSettings
				{
					XmlResolver = null
				}))
				{
					bluestacksUIColor = (BluestacksUIColor)XamlReader.Load(xmlReader);
					bool flag2 = bluestacksUIColor.AddNewParameters();
					if (flag2)
					{
						bluestacksUIColor.Save(themePath);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error loading Theme file from path : " + themePath + ex.ToString());
				bool flag3 = string.Compare(BlueStacksUIColorManager.ThemeFilePath, themePath, StringComparison.OrdinalIgnoreCase) == 0;
				if (flag3)
				{
					bluestacksUIColor.InitalizeDefault();
				}
			}
			return bluestacksUIColor;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010270 File Offset: 0x0000E470
		private bool AddNewParameters()
		{
			BluestacksUIColor bluestacksUIColor = new BluestacksUIColor();
			bluestacksUIColor.InitalizeDefault();
			bool result = false;
			foreach (KeyValuePair<string, Brush> keyValuePair in bluestacksUIColor.DictBrush)
			{
				bool flag = !this.DictBrush.ContainsKey(keyValuePair.Key);
				if (flag)
				{
					result = true;
					this.DictBrush.Add(keyValuePair.Key, keyValuePair.Value);
					bool flag2 = !this.DictCategory.ContainsKey(keyValuePair.Key);
					if (flag2)
					{
						this.DictCategory.Add(keyValuePair.Key, "*New Color*");
					}
				}
			}
			foreach (KeyValuePair<string, CornerRadius> keyValuePair2 in bluestacksUIColor.DictCornerRadius)
			{
				bool flag3 = !this.DictCornerRadius.ContainsKey(keyValuePair2.Key);
				if (flag3)
				{
					result = true;
					this.DictCornerRadius.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			foreach (KeyValuePair<string, Geometry> keyValuePair3 in bluestacksUIColor.DictGeometry)
			{
				bool flag4 = !this.DictGeometry.ContainsKey(keyValuePair3.Key);
				if (flag4)
				{
					result = true;
					this.DictGeometry.Add(keyValuePair3.Key, keyValuePair3.Value);
				}
			}
			foreach (KeyValuePair<string, Transform> keyValuePair4 in bluestacksUIColor.DictTransform)
			{
				bool flag5 = !this.DictTransform.ContainsKey(keyValuePair4.Key);
				if (flag5)
				{
					result = true;
					this.DictTransform.Add(keyValuePair4.Key, keyValuePair4.Value);
				}
			}
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair5 in bluestacksUIColor.DictThemeAvailable)
				{
					bool flag6 = !this.DictThemeAvailable.ContainsKey(keyValuePair5.Key);
					if (flag6)
					{
						result = true;
						this.DictThemeAvailable.Add(keyValuePair5.Key, keyValuePair5.Value);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in adding theme availability:" + ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00010560 File Offset: 0x0000E760
		public void NotifyUIElements()
		{
			bool flag = BluestacksUIColor.SourceUpdatedEvent != null;
			if (flag)
			{
				BluestacksUIColor.SourceUpdatedEvent(this, null);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001058C File Offset: 0x0000E78C
		public void Save(string saveFilePath)
		{
			try
			{
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Indent = true,
					NewLineOnAttributes = true,
					ConformanceLevel = ConformanceLevel.Fragment
				};
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
				{
					XamlDesignerSerializationManager manager = new XamlDesignerSerializationManager(xmlWriter)
					{
						XamlWriterMode = XamlWriterMode.Expression
					};
					bool flag = this.DictCategory.Count == 0;
					if (flag)
					{
						this.DictCategory.Add(string.Empty, string.Empty);
					}
					XamlWriter.Save(this, manager);
					bool flag2 = string.IsNullOrEmpty(saveFilePath);
					if (flag2)
					{
						File.WriteAllText(BluestacksUIColor.ThemeFilePath, stringBuilder.ToString());
					}
					else
					{
						File.WriteAllText(saveFilePath, stringBuilder.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error Saving Theme file " + ex.ToString());
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00010684 File Offset: 0x0000E884
		private Color GetMainColor(string colorTheme)
		{
			ColorUtils mainThemeColor = this.MainThemeColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#1E2138"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, mainThemeColor);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000106D8 File Offset: 0x0000E8D8
		private Color GetForegroundColor(string colorTheme)
		{
			ColorUtils mainForeGroundColor = this.MainForeGroundColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#F8F8EE"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, mainForeGroundColor);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001072C File Offset: 0x0000E92C
		private Color GetContrastColor(string colorTheme)
		{
			ColorUtils contrastThemeColor = this.ContrastThemeColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#585A6C"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, contrastThemeColor);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010780 File Offset: 0x0000E980
		private Color GetContrastForegroundColor(string colorTheme)
		{
			ColorUtils contrastForegroundColor = this.ContrastForegroundColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#20233A"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, contrastForegroundColor);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000107D4 File Offset: 0x0000E9D4
		private Color GetHighlighterColor(string colorTheme)
		{
			ColorUtils applicationHighlighterColor = this.ApplicationHighlighterColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#F87C06"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, applicationHighlighterColor);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010828 File Offset: 0x0000EA28
		private static Color Compute(double r, double g, double b, double a, ColorUtils c)
		{
			return ColorUtils.FromHSLA((double)c.H / r, (double)c.S / g, (double)c.L / b, (double)c.A / a).WPFColor;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001086C File Offset: 0x0000EA6C
		private static Tuple<double, double, double, double> GetUnitColor(string colorString, ColorUtils mainColorTheme)
		{
			bool flag = !colorString.Contains('#');
			if (flag)
			{
				colorString = "#" + colorString;
			}
			Color value = (Color)ColorConverter.ConvertFromString(colorString);
			ColorUtils colorUtils = new ColorUtils(value);
			return new Tuple<double, double, double, double>((double)(mainColorTheme.H / colorUtils.H), (double)(mainColorTheme.S / colorUtils.S), (double)(mainColorTheme.L / colorUtils.L), (double)(mainColorTheme.A / colorUtils.A));
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000108F0 File Offset: 0x0000EAF0
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00010911 File Offset: 0x0000EB11
		internal ColorUtils ApplicationHighlighterColor
		{
			get
			{
				return new ColorUtils((this.DictBrush["ApplicationHighlighterColor"] as SolidColorBrush).Color);
			}
			set
			{
				this.DictBrush["ApplicationHighlighterColor"] = new SolidColorBrush(value.WPFColor);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0001092F File Offset: 0x0000EB2F
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00010950 File Offset: 0x0000EB50
		internal ColorUtils MainThemeColor
		{
			get
			{
				return new ColorUtils((this.DictBrush["MainThemeColor"] as SolidColorBrush).Color);
			}
			set
			{
				this.DictBrush["MainThemeColor"] = new SolidColorBrush(value.WPFColor);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0001096E File Offset: 0x0000EB6E
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0001098F File Offset: 0x0000EB8F
		internal ColorUtils MainForeGroundColor
		{
			get
			{
				return new ColorUtils((this.DictBrush["MainForeGroundColor"] as SolidColorBrush).Color);
			}
			set
			{
				this.DictBrush["MainForeGroundColor"] = new SolidColorBrush(value.WPFColor);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000109AD File Offset: 0x0000EBAD
		// (set) Token: 0x0600031F RID: 799 RVA: 0x000109CE File Offset: 0x0000EBCE
		internal ColorUtils ContrastThemeColor
		{
			get
			{
				return new ColorUtils((this.DictBrush["ContrastThemeColor"] as SolidColorBrush).Color);
			}
			set
			{
				this.DictBrush["ContrastThemeColor"] = new SolidColorBrush(value.WPFColor);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000109EC File Offset: 0x0000EBEC
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00010A0D File Offset: 0x0000EC0D
		internal ColorUtils ContrastForegroundColor
		{
			get
			{
				return new ColorUtils((this.DictBrush["ContrastForegroundColor"] as SolidColorBrush).Color);
			}
			set
			{
				this.DictBrush["ContrastForegroundColor"] = new SolidColorBrush(value.WPFColor);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00010A2B File Offset: 0x0000EC2B
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00010A42 File Offset: 0x0000EC42
		[XmlIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SkewTransform TextBoxTransForm
		{
			get
			{
				return (SkewTransform)this.DictTransform["TextBoxTransForm"];
			}
			set
			{
				this.DictTransform["TextBoxTransForm"] = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00010A56 File Offset: 0x0000EC56
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00010A6D File Offset: 0x0000EC6D
		[XmlIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SkewTransform TextBoxAntiTransForm
		{
			get
			{
				return (SkewTransform)this.DictTransform["TextBoxAntiTransForm"];
			}
			set
			{
				this.DictTransform["TextBoxAntiTransForm"] = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010A81 File Offset: 0x0000EC81
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00010A98 File Offset: 0x0000EC98
		[XmlIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SkewTransform TabTransform
		{
			get
			{
				return (SkewTransform)this.DictTransform["TabTransform"];
			}
			set
			{
				this.DictTransform["TabTransform"] = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00010AAC File Offset: 0x0000ECAC
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00010AC3 File Offset: 0x0000ECC3
		[XmlIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SkewTransform TabTransformPortrait
		{
			get
			{
				return (SkewTransform)this.DictTransform["TabTransformPortrait"];
			}
			set
			{
				this.DictTransform["TabTransformPortrait"] = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00010AD7 File Offset: 0x0000ECD7
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00010AE9 File Offset: 0x0000ECE9
		internal CornerRadius TabRadius
		{
			get
			{
				return this.DictCornerRadius["TabRadius"];
			}
			set
			{
				this.DictCornerRadius["TabRadius"] = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00010AFD File Offset: 0x0000ECFD
		// (set) Token: 0x0600032D RID: 813 RVA: 0x00010B0F File Offset: 0x0000ED0F
		internal CornerRadius TextBoxRadius
		{
			get
			{
				return this.DictCornerRadius["TextBoxRadius"];
			}
			set
			{
				this.DictCornerRadius["TextBoxRadius"] = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00010B23 File Offset: 0x0000ED23
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00010B3A File Offset: 0x0000ED3A
		[XmlIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RectangleGeometry AppIconRectangleGeometry
		{
			get
			{
				return (RectangleGeometry)this.DictGeometry["AppIconRectangleGeometry"];
			}
			set
			{
				this.DictGeometry["AppIconRectangleGeometry"] = value;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010B50 File Offset: 0x0000ED50
		private void InitalizeDefault()
		{
			this.DictBrush["MainThemeColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E2138"));
			this.DictBrush["MainForeGroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8F8EE"));
			this.DictBrush["ContrastThemeColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#585A6C"));
			this.DictBrush["ContrastForegroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF232642"));
			this.DictBrush["ApplicationHighlighterColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F87C06"));
			this.DictGeometry["AppIconRectangleGeometry"] = new RectangleGeometry();
			(this.DictGeometry["AppIconRectangleGeometry"] as RectangleGeometry).RadiusX = 10.0;
			(this.DictGeometry["AppIconRectangleGeometry"] as RectangleGeometry).RadiusY = 10.0;
			(this.DictGeometry["AppIconRectangleGeometry"] as RectangleGeometry).Rect = new Rect(0.0, 0.0, 68.0, 68.0);
			this.DictTransform["TabTransform"] = new SkewTransform(0.0, 0.0);
			this.DictTransform["TabTransformPortrait"] = new SkewTransform(0.0, 0.0);
			this.DictTransform["TextBoxTransForm"] = new SkewTransform(0.0, 0.0);
			this.DictTransform["TextBoxAntiTransForm"] = new SkewTransform(0.0, 0.0);
			this.DictCornerRadius["TabRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["TextBoxRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["SearchButtonPadding"] = new CornerRadius(0.0, 0.0, 0.0, 0.0);
			this.DictCornerRadius["SearchButtonMargin"] = new CornerRadius(0.0, 0.0, -40.0, 8.0);
			this.DictCornerRadius["TabMarginLandScape"] = new CornerRadius(2.0, 0.0, 0.0, 0.0);
			this.DictCornerRadius["TabMarginPortrait"] = new CornerRadius(2.0, 0.0, 0.0, 0.0);
			this.DictCornerRadius["CloseTabButtonLandScape"] = new CornerRadius(3.0, 3.0, 10.0, 3.0);
			this.DictCornerRadius["CloseTabButtonDropDown"] = new CornerRadius(0.0);
			this.DictCornerRadius["SpeedUpTipsRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["SettingsWindowRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["MessageWindowRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["PreferenceDropDownRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["BeginnerGuideRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["PopupRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["ButtonCornerRadius"] = new CornerRadius(0.0);
			this.DictCornerRadius["SidebarElementCornerRadius"] = new CornerRadius(0.0);
			this.CalculateAndNotify(false);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public void CalculateAndNotify(bool isNotify = true)
		{
			foreach (KeyValuePair<string, Brush> keyValuePair in this.IntializeFurther())
			{
				this.DictBrush[keyValuePair.Key] = keyValuePair.Value;
			}
			if (isNotify)
			{
				this.NotifyUIElements();
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00011054 File Offset: 0x0000F254
		internal SerializableDictionary<string, Brush> IntializeFurther()
		{
			SerializableDictionary<string, Brush> serializableDictionary = new SerializableDictionary<string, Brush>();
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			serializableDictionary["ApplicationBorderBrush"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["DimOverlayColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A0000000"));
			serializableDictionary["DimOverlayForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#8AFFFFFF"));
			serializableDictionary["ViewXpackPopupColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99000000"));
			serializableDictionary["ViewXpackPopupHoverColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BF000000"));
			serializableDictionary["ViewXpackPopupClickedColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
			serializableDictionary["GuidanceKeyWarningBorderColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F09200"));
			serializableDictionary["GuidanceKeyWarningBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF121429"));
			serializableDictionary["LightBandingColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF232642"));
			serializableDictionary["DarkBandingColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF34375C"));
			serializableDictionary["WidgetBarBorder"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF34375C"));
			serializableDictionary["DualTextBlockLightForegroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
			serializableDictionary["OverlayLabelBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B3000000"));
			serializableDictionary["OverlayLabelBorderColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66FFFFFF"));
			serializableDictionary["XPackPopupColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF402F"));
			serializableDictionary["PopupBorderBrush"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["ApplicationBackgroundBrush"] = new SolidColorBrush(this.GetMainColor("#FF121429"));
			serializableDictionary["AppIconTextColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF8F8EE"));
			serializableDictionary["AppIconDropShadowBrush"] = new SolidColorBrush(this.GetMainColor("#FF02A6F4"));
			serializableDictionary["BeginnersGuideTextColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF8F8EE"));
			serializableDictionary["BluestacksTitleColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF8F8EE"));
			serializableDictionary["ModalShadowColor"] = new SolidColorBrush(this.GetMainColor("#99000000"));
			serializableDictionary["PopupShadowColor"] = new SolidColorBrush(this.GetMainColor("#99000000"));
			serializableDictionary["TopBarColor"] = new SolidColorBrush(this.GetContrastColor("#FF232642"));
			linearGradientBrush = new LinearGradientBrush();
			serializableDictionary["StreamingTopBarColor"] = linearGradientBrush;
			linearGradientBrush.StartPoint = new Point(0.0, 0.0);
			linearGradientBrush.EndPoint = new Point(1.0, 0.0);
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetMainColor("#741BFF"), 0.0));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetMainColor("#4C07B7"), 1.0));
			serializableDictionary["BottomBarColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			linearGradientBrush = new LinearGradientBrush();
			serializableDictionary["GuidanceVideoDescriptionColor"] = linearGradientBrush;
			linearGradientBrush.StartPoint = new Point(0.0, 0.38);
			linearGradientBrush.EndPoint = new Point(1.0, 0.64);
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetMainColor("#B909BC"), -0.03));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetMainColor("#8013B3"), 0.64));
			serializableDictionary["SelectedTabBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF121429"));
			serializableDictionary["SelectedTabForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#B4FFFFFF"));
			serializableDictionary["SelectedTabBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["AppTabBorderBrush"] = new SolidColorBrush(this.GetMainColor("#FF464A75"));
			serializableDictionary["AppTabsPopupBorder"] = new SolidColorBrush(this.GetContrastColor("#FF34375C"));
			serializableDictionary["AppTabsPopupbackground"] = new SolidColorBrush(this.GetContrastColor("#FF232642"));
			serializableDictionary["TabBackgroundColor"] = new SolidColorBrush(this.GetContrastColor("#FF34375C"));
			serializableDictionary["TabForegroundColor"] = new SolidColorBrush(this.GetContrastForegroundColor("#8CFFFFFF"));
			serializableDictionary["TabBackgroundHoverColor"] = new SolidColorBrush(this.GetContrastColor("#FF464A75"));
			serializableDictionary["HomeAppTabBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF266AB8"));
			serializableDictionary["HomeAppTabForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FF8B9FC2"));
			serializableDictionary["HomeAppTabBackgroundHoverColor"] = new SolidColorBrush(this.GetMainColor("#FF0B7DDA"));
			serializableDictionary["HomeAppTabForegroundHoverColor"] = new SolidColorBrush(this.GetForegroundColor("#FFC6CEE1"));
			serializableDictionary["SelectedHomeAppTabBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF328CF2"));
			serializableDictionary["SelectedHomeAppTabForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFEAF2FB"));
			serializableDictionary["HomeAppBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF1E2138"));
			serializableDictionary["HomeAppTabButtonBaseColor"] = new SolidColorBrush(this.GetMainColor("#FF151833"));
			serializableDictionary["BeginnerGuideBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF283055"));
			serializableDictionary["ContextMenuItemBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["ContextMenuItemBackgroundHighlighterColor"] = new SolidColorBrush(this.GetMainColor("#FF222949"));
			serializableDictionary["ContextMenuItemForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["FrontendPopupTitleColor"] = new SolidColorBrush(this.GetForegroundColor("#FFCBD6EF"));
			serializableDictionary["ContextMenuItemForegroundDimColor"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["ContextMenuItemForegroundDimDimColor"] = new SolidColorBrush(this.GetForegroundColor("#FF626480"));
			serializableDictionary["ContextMenuItemForegroundHighlighterColor"] = new SolidColorBrush(this.GetHighlighterColor("#FFF09200"));
			serializableDictionary["ContextMenuItemForegroundGreenColor"] = new SolidColorBrush(this.GetHighlighterColor("#FF2BBD00"));
			serializableDictionary["PopupWindowWarningForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFED5547"));
			serializableDictionary["ContextMenuItemBackgroundHoverColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["SliderButtonColor"] = new SolidColorBrush(this.GetMainColor("#FF328CF2"));
			serializableDictionary["HorizontalSeparator"] = new SolidColorBrush(this.GetContrastColor("#50757B9F"));
			serializableDictionary["ProgressBarForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FF01D328"));
			serializableDictionary["ProgressBarBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF121429"));
			serializableDictionary["ProgressBarBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF121429"));
			serializableDictionary["ProgressBarProgressColor"] = new SolidColorBrush(this.GetMainColor("#FF038CEF"));
			serializableDictionary["BootPromotionTextColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF8F8EE"));
			serializableDictionary["GenericBrushLight"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
			serializableDictionary["TextBoxBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["TextBoxBorderColor"] = new SolidColorBrush(this.GetForegroundColor("#FF626480"));
			serializableDictionary["TextBoxHoverBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF008BEF"));
			serializableDictionary["TextBoxFocussedBackgroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FF121429"));
			serializableDictionary["TextBoxFocussedBorderColor"] = new SolidColorBrush(this.GetForegroundColor("#FF008BEF"));
			serializableDictionary["TextBoxFocussedForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["TextBoxErrorBackgroundColor"] = new SolidColorBrush(this.GetForegroundColor("#20FF402F"));
			serializableDictionary["TextBoxErrorBorderColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFF402F"));
			serializableDictionary["TextBoxWarningBackgroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FF121429"));
			serializableDictionary["TextBoxWarningBorderColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF09200"));
			serializableDictionary["SearchTextBoxBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000"));
			serializableDictionary["SearchGridForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#8CFFFFFF"));
			serializableDictionary["SearchGridForegroundFocusedColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF8F8EE"));
			serializableDictionary["SearchGridBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#DC232642"));
			serializableDictionary["SearchGridBorderColor"] = new SolidColorBrush(this.GetMainColor("#6EFFFFFF"));
			serializableDictionary["FullScreenTopBarBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["FullScreenTopBarButtonBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF328CF2"));
			serializableDictionary["FullScreenTopBarButtonBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF328CF2"));
			serializableDictionary["FullScreenTopBarForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF8F8EE"));
			serializableDictionary["BlockerAdControlBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["BlockerAdControlHighlightedForegroundColor"] = new SolidColorBrush(this.GetHighlighterColor("#FFF09200"));
			serializableDictionary["BlockerAdControlForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFF8F8EE"));
			serializableDictionary["ComboBoxBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["ComboBoxBorderColor"] = new SolidColorBrush(this.GetMainColor("#FFA5A7C2"));
			serializableDictionary["ComboBoxForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["ComboBoxItemBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["ComboBoxScrollBarColor"] = new SolidColorBrush(this.GetMainColor("#FF464A75"));
			linearGradientBrush = new LinearGradientBrush();
			serializableDictionary["ComboBoxHorizontalScrollBarBackgroundColor"] = linearGradientBrush;
			linearGradientBrush.StartPoint = new Point(0.0, 0.0);
			linearGradientBrush.EndPoint = new Point(0.0, 1.0);
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFFFC3C3"), 0.0));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFFFDBDB"), 0.2));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFFFDBDB"), 0.8));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFFFC7C7"), 1.0));
			linearGradientBrush = new LinearGradientBrush();
			serializableDictionary["ComboBoxVerticalScrollBarBackgroundColor"] = linearGradientBrush;
			linearGradientBrush.StartPoint = new Point(0.0, 0.0);
			linearGradientBrush.EndPoint = new Point(0.0, 1.0);
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FF464A75"), 0.0));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FF464A75"), 0.2));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FF464A75"), 0.8));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FF464A75"), 1.0));
			serializableDictionary["WidgetBarBackground"] = new SolidColorBrush(this.GetMainColor("#E6232642"));
			serializableDictionary["WidgetBarForeground"] = new SolidColorBrush(this.GetForegroundColor("#FF94A3C9"));
			serializableDictionary["SettingsWindowBackground"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["SettingsWindowTitleBarBackground"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["SettingsWindowTitleBarForeGround"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["SettingsWindowForegroundDimColor"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["SettingsWindowForegroundDimDimColor"] = new SolidColorBrush(this.GetForegroundColor("#FF626480"));
			serializableDictionary["SettingsWindowBorderColor"] = new SolidColorBrush(this.GetHighlighterColor("#FF34375C"));
			serializableDictionary["SettingsWindowTextBoxBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF3D4566"));
			serializableDictionary["SettingsWindowTextBoxBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["SettingsWindowForegroundDimDimDimColor"] = new SolidColorBrush(this.GetForegroundColor("#FF626480"));
			serializableDictionary["SettingsWindowForegroundChangeColor"] = new SolidColorBrush(this.GetForegroundColor("#FFDC143C"));
			serializableDictionary["SettingsWindowBorderBrushColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["SettingsWindowTabMenuBackground"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["SettingsWindowTabMenuItemForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["SettingsWindowTabMenuItemSelectedForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["SettingsWindowTabMenuItemLegendForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["SettingsWindowTabMenuItemUnderline"] = new SolidColorBrush(this.GetForegroundColor("#FF008BEF"));
			serializableDictionary["VerticalSeparator"] = new SolidColorBrush(this.GetForegroundColor("#FF121429"));
			serializableDictionary["SettingsWindowTabMenuItemBackground"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["SettingsWindowTabMenuItemHoverBackground"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["SettingsWindowTabMenuItemSelectedBackground"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["WarningGridBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["WarningGridBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["CustomSliderBrush"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["CustomSlideRoundButtonrBrush"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["CustomSliderForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["CustomSliderThumbColor"] = new SolidColorBrush(this.GetMainColor("#FF008BEF"));
			serializableDictionary["CustomSliderThumbBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF43ACED"));
			serializableDictionary["MultiInstanceManagerForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FF34375C"));
			serializableDictionary["MultiInstanceManagerBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF283055"));
			serializableDictionary["MultiInstanceManagerBorderColor"] = new SolidColorBrush(this.GetContrastColor("#FF34375C"));
			serializableDictionary["MultiInstanceManagerInstanceColor"] = new SolidColorBrush(this.GetMainColor("#FF3E446D"));
			serializableDictionary["MultiInstanceManagerTextBoxBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF252B4A"));
			serializableDictionary["MultiInstanceManagerTextBoxBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["ScrollViewerDisabledBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF4F4F4"));
			serializableDictionary["ScrollViewerBorderColor"] = new SolidColorBrush(this.GetContrastColor("#FF34375C"));
			serializableDictionary["ScrollViewerBackgroundHoverColor"] = new SolidColorBrush(this.GetContrastColor("#FFCBD6EF"));
			serializableDictionary["NoInternetControlBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["NoInternetControlBorderColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["NoInternetControlForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FF34375C"));
			serializableDictionary["NoInternetControlTitleForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["AdvancedGameControlBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["AdvancedGameControlHeaderBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["AdvancedGameControlHeaderForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["AdvancedGameControlActionHeaderForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFA9B9CC"));
			serializableDictionary["AdvancedGameControlButtonGridBackground"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["KeymapCanvasWindowBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#40000000"));
			serializableDictionary["GameControlWindowFooterForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["GameControlWindowBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["GameControlWindowBorderColor"] = new SolidColorBrush(this.GetContrastColor("#FF34375C"));
			serializableDictionary["GameControlHeaderBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000"));
			serializableDictionary["GameControlWindowHeaderForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["GameControlWindowFooterTitleForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["GameControlWindowHeaderColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["GameControlWindowGuidanceKeyForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFB80000"));
			serializableDictionary["GameControlWindowBottomBarForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["GameControlCategoryHeaderForeground"] = new SolidColorBrush(this.GetContrastColor("#FFA5A7C2"));
			serializableDictionary["GameControlSelectedCategoryHeaderForeground"] = new SolidColorBrush(this.GetMainColor("#FFFFFFFF"));
			serializableDictionary["GameControlCategoryGroupBoxForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["GuidanceVideoElementHeaderForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["GuidanceVideoElementForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFA9B9CC"));
			serializableDictionary["GameControlWindowVerticalDividerColor"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["AdvancedSettingsItemPanelBorder"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["AdvancedSettingsItemPanelBackground"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["AdvancedSettingsItemPanelForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["GuidanceKeyForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["GuidanceKeyTextboxBorder"] = new SolidColorBrush(this.GetContrastColor("#FF34375C"));
			serializableDictionary["GuidanceKeyTextboxSelectedBorder"] = new SolidColorBrush(this.GetMainColor("#FF4A90E2"));
			serializableDictionary["GuidanceKeyTextboxSelectedBackground"] = new SolidColorBrush(this.GetMainColor("#FF121429"));
			serializableDictionary["GuidanceTextColorForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["GoogleSigninPopupTextColor"] = new SolidColorBrush(this.GetForegroundColor("#FF858585"));
			linearGradientBrush = new LinearGradientBrush();
			serializableDictionary["GuidanceTextBorderBrush"] = linearGradientBrush;
			linearGradientBrush.StartPoint = new Point(0.0, 0.0);
			linearGradientBrush.EndPoint = new Point(0.0, 20.0);
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFA5A7C2"), 0.0));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFA5A7C2"), 0.05));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFA5A7C2"), 0.07));
			linearGradientBrush.GradientStops.Add(new GradientStop(this.GetContrastColor("#FFA5A7C2"), 1.0));
			RadialGradientBrush radialGradientBrush = new RadialGradientBrush
			{
				GradientOrigin = new Point(0.5, 0.0),
				Center = new Point(0.5, 0.0),
				RadiusX = 5.0,
				RadiusY = 0.5
			};
			radialGradientBrush.GradientStops.Add(new GradientStop(this.GetMainColor("#99393C65"), 1.0));
			serializableDictionary["GameControlNavigationBackgroundColor"] = radialGradientBrush;
			radialGradientBrush = new RadialGradientBrush
			{
				GradientOrigin = new Point(0.5, 0.0),
				Center = new Point(0.5, 0.0),
				RadiusX = 0.5,
				RadiusY = 0.5
			};
			radialGradientBrush.GradientStops.Add(new GradientStop(this.GetMainColor("#FF232642"), 1.0));
			serializableDictionary["GameControlContentBackgroundColor"] = radialGradientBrush;
			serializableDictionary["GuidanceVideoElementBorder"] = new SolidColorBrush(this.GetContrastColor("#FF5B5C6F"));
			serializableDictionary["GuidanceVideoElementBackground"] = new SolidColorBrush(this.GetMainColor("#FF1D1E36"));
			serializableDictionary["KeymapExtraSettingsWindowBorder"] = new SolidColorBrush(this.GetMainColor("#FF4A90E2"));
			serializableDictionary["KeymapExtraSettingsWindowBackground"] = new SolidColorBrush(this.GetMainColor("#FF232642"));
			serializableDictionary["KeymapExtraSettingsWindowForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFA5A7C2"));
			serializableDictionary["KeymapDummyMobaForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFFFCD41"));
			serializableDictionary["CanvasElementsBackgroundColor"] = new SolidColorBrush(this.GetMainColor("#FFFFFFFF"));
			serializableDictionary["DualTextblockControlBackground"] = new SolidColorBrush(this.GetMainColor("#FF121429"));
			serializableDictionary["DualTextblockControlOuterBackground"] = new SolidColorBrush(this.GetMainColor("#FF34375C"));
			serializableDictionary["DualTextBlockForeground"] = new SolidColorBrush(this.GetForegroundColor("#FFFFFFFF"));
			serializableDictionary["GameControlSchemeForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FFA9B9CC"));
			serializableDictionary["GuidanceKeyBorderBackgroundColor"] = new SolidColorBrush(this.GetContrastColor("#FF51546E"));
			serializableDictionary["DeleteComboTextForeground"] = new SolidColorBrush(this.GetForegroundColor("FFFF402F"));
			serializableDictionary["HyperLinkForegroundColor"] = new SolidColorBrush(this.GetForegroundColor("#FF047CD2"));
			serializableDictionary["InstallerWindowBorderBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF34375C"));
			serializableDictionary["InstallerTextForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF636363"));
			serializableDictionary["InstallerWindowBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF283155"));
			serializableDictionary["InstallerWindowTextForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF008BEF"));
			serializableDictionary["InstallerWindowMouseOverTextForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0A98FF"));
			serializableDictionary["InstallerWindowLightTextForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBD6EF"));
			serializableDictionary["InstallerWindowWhiteTextColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
			serializableDictionary["InstallerWindowTextBoxBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1A2147"));
			serializableDictionary["MaterialDesignBlueBtnBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF008BEF"));
			serializableDictionary["MaterialDesignBlueBtnBorderBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF008BFF"));
			serializableDictionary["MaterialDesignBlueBtnMouseOverBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0A98FF"));
			serializableDictionary["MaterialDesignBlueBtnMouseDownBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007CD6"));
			serializableDictionary["MaterialDesignRedBtnBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF402F"));
			serializableDictionary["MaterialDesignRedBtnBorderBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF404F"));
			serializableDictionary["MaterialDesignRedBtnMouseOverBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF5749"));
			serializableDictionary["MaterialDesignRedBtnMouseDownBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF2916"));
			serializableDictionary["SidebarBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF232642"));
			serializableDictionary["SidebarElementNormal"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF232642"));
			serializableDictionary["SidebarElementHover"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF232642"));
			serializableDictionary["SidebarElementClick"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF232642"));
			serializableDictionary["MacroPlayRecorderControlBorder"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBD6EF"));
			serializableDictionary["LogCollectorWatermarkTextColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF34375C"));
			serializableDictionary["SyncHyperlinkForegroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4A90E2"));
			this.AddButtonsColor(serializableDictionary);
			return serializableDictionary;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00012BB0 File Offset: 0x00010DB0
		private void AddButtonsColor(Dictionary<string, Brush> dict)
		{
			dict["RedMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF403F");
			dict["RedMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF402F");
			dict["RedMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["RedMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF5749");
			dict["RedMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF5749");
			dict["RedMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["RedMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF2910");
			dict["RedMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF2910");
			dict["RedMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["RedDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FF403F");
			dict["RedDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FF402F");
			dict["RedDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["WhiteMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["WhiteMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["WhiteMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["WhiteMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF1F1F1");
			dict["WhiteMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF1F1F1");
			dict["WhiteMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["WhiteMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEAEAEA");
			dict["WhiteMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEAEAEA");
			dict["WhiteMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["WhiteDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["WhiteDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["WhiteDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80008BEF");
			dict["WhiteWithGreyFGMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["WhiteWithGreyFGMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["WhiteWithGreyFGMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF858585");
			dict["WhiteWithGreyFGMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF1F1F1");
			dict["WhiteWithGreyFGMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF1F1F1");
			dict["WhiteWithGreyFGMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF858585");
			dict["WhiteWithGreyFGMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEAEAEA");
			dict["WhiteWithGreyFGMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEAEAEA");
			dict["WhiteWithGreyFGMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF858585");
			dict["WhiteWithGreyFGDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["WhiteWithGreyFGDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["WhiteWithGreyFGDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80858585");
			dict["BlueMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["BlueMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["BlueMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BlueMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0A98FF");
			dict["BlueMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0A98FF");
			dict["BlueMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BlueMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF007CD6");
			dict["BlueMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF007CD6");
			dict["BlueMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BlueDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80008BEF");
			dict["BlueDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80008BEF");
			dict["BlueDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["OrangeMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF09200");
			dict["OrangeMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF09200");
			dict["OrangeMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["OrangeMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF9F0B");
			dict["OrangeMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF9F0B");
			dict["OrangeMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["OrangeMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD78200");
			dict["OrangeMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD78200");
			dict["OrangeMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["OrangeDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80F09200");
			dict["OrangeDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80F09200");
			dict["OrangeDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["BackgroundMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF34375C");
			dict["BackgroundMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF34375C");
			dict["BackgroundMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF444B6F");
			dict["BackgroundMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF444B6F");
			dict["BackgroundMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2B3255");
			dict["BackgroundMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2B3255");
			dict["BackgroundMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#8030385F");
			dict["BackgroundDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#8030385F");
			dict["BackgroundDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["BackgroundBlueBorderMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["BackgroundBlueBorderMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#802B3255");
			dict["BackgroundBlueBorderMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["BackgroundBlueBorderMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["BackgroundBlueBorderMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF444B6F");
			dict["BackgroundBlueBorderMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundBlueBorderMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF008BEF");
			dict["BackgroundBlueBorderMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF34375C");
			dict["BackgroundBlueBorderMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundBlueDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80008BEF");
			dict["BackgroundBlueDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#802B3255");
			dict["BackgroundBlueDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["BackgroundOrangeBorderMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF09200");
			dict["BackgroundOrangeBorderMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF09200");
			dict["BackgroundOrangeBorderMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundOrangeBorderMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF9F0B");
			dict["BackgroundOrangeBorderMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF9F0B");
			dict["BackgroundOrangeBorderMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundOrangeBorderMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD78200");
			dict["BackgroundOrangeBorderMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD78200");
			dict["BackgroundOrangeBorderMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundOrangeBorderDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00000000");
			dict["BackgroundOrangeBorderDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#1AFFFFFF");
			dict["BackgroundOrangeBorderDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#33FFFFFF");
			dict["BackgroundWhiteBorderMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundWhiteBorderMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BackgroundWhiteBorderMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundWhiteBorderMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundWhiteBorderMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BackgroundWhiteBorderMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundWhiteBorderMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundWhiteBorderMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BackgroundWhiteBorderMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundWhiteBorderDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["BackgroundWhiteBorderDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BackgroundWhiteBorderDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["GreenMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF26AA00");
			dict["GreenMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF26AA00");
			dict["GreenMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["GreenMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF40C319");
			dict["GreenMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF40C319");
			dict["GreenMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["GreenMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2BBD00");
			dict["GreenMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2BBD00");
			dict["GreenMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["GreenDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#8026AA00");
			dict["GreenDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#8026AA00");
			dict["GreenDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["BorderMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF555E73");
			dict["BorderMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BorderMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCBD6EF");
			dict["BorderMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF8A92A5");
			dict["BorderMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BorderMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCBD6EF");
			dict["BorderMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCBD6EF");
			dict["BorderMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BorderMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCBD6EF");
			dict["BorderDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80555E73");
			dict["BorderDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["BorderDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80CBD6EF");
			dict["BorderRedBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#20FF402F");
			dict["TransparentMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#96FFFFFF");
			dict["TransparentMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["TransparentMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#C8FFFFFF");
			dict["TransparentDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["TransparentDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#80FFFFFF");
			dict["OverlayMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["OverlayMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#01FFFFFF");
			dict["OverlayMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#96FFFFFF");
			dict["OverlayMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["OverlayMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#01FFFFFF");
			dict["OverlayMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["OverlayMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");
			dict["OverlayMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#01FFFFFF");
			dict["OverlayMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#C8FFFFFF");
			dict["OverlayDisabledBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#00000000");
			dict["OverlayDisabledGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#1AFFFFFF");
			dict["OverlayDisabledForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#33FFFFFF");
			dict["HyperlinkForeground"] = new SolidColorBrush(this.GetMainColor("#FF328CF2"));
			dict["DarkRedMouseOutBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF671000");
			dict["DarkRedMouseOutGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF671000");
			dict["DarkRedMouseOutForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["DarkRedMouseInBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF991700");
			dict["DarkRedMouseInGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF991700");
			dict["DarkRedMouseInForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
			dict["DarkRedMouseDownBorderBackground"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF4D0B00");
			dict["DarkRedMouseDownGridBackGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF4D0B00");
			dict["DarkRedMouseDownForeGround"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001409C File Offset: 0x0001229C
		public static void ScrollBarScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			bool flag = sender != null;
			if (flag)
			{
				ScrollViewer scrollViewer = sender as ScrollViewer;
				double verticalOffset = scrollViewer.VerticalOffset;
				bool flag2 = scrollViewer.ComputedVerticalScrollBarVisibility > Visibility.Visible;
				if (flag2)
				{
					scrollViewer.OpacityMask = null;
				}
				else
				{
					bool flag3 = verticalOffset < 10.0;
					if (flag3)
					{
						scrollViewer.OpacityMask = BluestacksUIColor.mTopOpacityMask;
					}
					else
					{
						bool flag4 = scrollViewer.ExtentHeight - scrollViewer.ActualHeight - 10.0 <= verticalOffset;
						if (flag4)
						{
							scrollViewer.OpacityMask = BluestacksUIColor.mBottomOpacityMask;
						}
						else
						{
							scrollViewer.OpacityMask = BluestacksUIColor.mScrolledOpacityMask;
						}
					}
				}
			}
		}

		// Token: 0x04000513 RID: 1299
		[XmlIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public const string ThemeFileName = "ThemeFile";

		// Token: 0x0400051B RID: 1307
		public static readonly LinearGradientBrush mScrolledOpacityMask = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
		{
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.0),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.15),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.8),
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 1.0)
		}), new Point(0.0, 0.0), new Point(0.0, 1.0));

		// Token: 0x0400051C RID: 1308
		public static readonly LinearGradientBrush mTopOpacityMask = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
		{
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.0),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.8),
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 1.0)
		}), new Point(0.0, 0.0), new Point(0.0, 1.0));

		// Token: 0x0400051D RID: 1309
		public static readonly LinearGradientBrush mBottomOpacityMask = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
		{
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.0),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.15),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 1.0)
		}), new Point(0.0, 0.0), new Point(0.0, 1.0));
	}
}
