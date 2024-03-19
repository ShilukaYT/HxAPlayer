using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020000FD RID: 253
	public class BluestacksUIColor
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00005C0F File Offset: 0x00003E0F
		internal static string ThemeFilePath
		{
			get
			{
				return Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, RegistryManager.ClientThemeName), "ThemeFile");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00005C2F File Offset: 0x00003E2F
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x00005C37 File Offset: 0x00003E37
		public SerializableDictionary<string, string> DictCategory { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00005C40 File Offset: 0x00003E40
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00005C48 File Offset: 0x00003E48
		public SerializableDictionary<string, Brush> DictBrush { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00005C51 File Offset: 0x00003E51
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00005C59 File Offset: 0x00003E59
		public SerializableDictionary<string, Geometry> DictGeometry { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00005C62 File Offset: 0x00003E62
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00005C6A File Offset: 0x00003E6A
		public SerializableDictionary<string, Transform> DictTransform { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00005C73 File Offset: 0x00003E73
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00005C7B File Offset: 0x00003E7B
		public SerializableDictionary<string, CornerRadius> DictCornerRadius { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00005C84 File Offset: 0x00003E84
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00005C8C File Offset: 0x00003E8C
		public SerializableDictionary<string, string> DictThemeAvailable { get; set; }

		// Token: 0x06000675 RID: 1653 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
		public BluestacksUIColor()
		{
			this.DictCategory = new SerializableDictionary<string, string>();
			this.DictBrush = new SerializableDictionary<string, Brush>();
			this.DictGeometry = new SerializableDictionary<string, Geometry>();
			this.DictTransform = new SerializableDictionary<string, Transform>();
			this.DictCornerRadius = new SerializableDictionary<string, CornerRadius>();
			this.DictThemeAvailable = new SerializableDictionary<string, string>();
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000676 RID: 1654 RVA: 0x0001D148 File Offset: 0x0001B348
		// (remove) Token: 0x06000677 RID: 1655 RVA: 0x0001D17C File Offset: 0x0001B37C
		internal static event EventHandler SourceUpdatedEvent;

		// Token: 0x06000678 RID: 1656 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
		internal static BluestacksUIColor Load(string themePath)
		{
			BluestacksUIColor bluestacksUIColor = new BluestacksUIColor();
			try
			{
				if (File.Exists(themePath))
				{
					using (XmlReader xmlReader = XmlReader.Create(themePath, new XmlReaderSettings
					{
						XmlResolver = null
					}))
					{
						bluestacksUIColor = (BluestacksUIColor)XamlReader.Load(xmlReader);
						if (bluestacksUIColor.AddNewParameters())
						{
							bluestacksUIColor.Save(themePath);
						}
						goto IL_53;
					}
					goto IL_48;
					IL_53:
					return bluestacksUIColor;
				}
				IL_48:
				throw new Exception("Theme file not found exception");
			}
			catch (Exception ex)
			{
				Logger.Error("Error loading Theme file from path : " + themePath + ex.ToString());
				if (string.Compare(BlueStacksUIColorManager.GetThemeFilePath("Assets"), themePath, StringComparison.OrdinalIgnoreCase) == 0)
				{
					bluestacksUIColor.InitalizeDefault();
				}
			}
			return bluestacksUIColor;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001D264 File Offset: 0x0001B464
		private bool AddNewParameters()
		{
			BluestacksUIColor bluestacksUIColor = new BluestacksUIColor();
			bluestacksUIColor.InitalizeDefault();
			bool result = false;
			foreach (KeyValuePair<string, Brush> keyValuePair in bluestacksUIColor.DictBrush)
			{
				if (!this.DictBrush.ContainsKey(keyValuePair.Key))
				{
					result = true;
					this.DictBrush.Add(keyValuePair.Key, keyValuePair.Value);
					if (!this.DictCategory.ContainsKey(keyValuePair.Key))
					{
						this.DictCategory.Add(keyValuePair.Key, "*New Color*");
					}
				}
			}
			foreach (KeyValuePair<string, CornerRadius> keyValuePair2 in bluestacksUIColor.DictCornerRadius)
			{
				if (!this.DictCornerRadius.ContainsKey(keyValuePair2.Key))
				{
					result = true;
					this.DictCornerRadius.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			foreach (KeyValuePair<string, Geometry> keyValuePair3 in bluestacksUIColor.DictGeometry)
			{
				if (!this.DictGeometry.ContainsKey(keyValuePair3.Key))
				{
					result = true;
					this.DictGeometry.Add(keyValuePair3.Key, keyValuePair3.Value);
				}
			}
			foreach (KeyValuePair<string, Transform> keyValuePair4 in bluestacksUIColor.DictTransform)
			{
				if (!this.DictTransform.ContainsKey(keyValuePair4.Key))
				{
					result = true;
					this.DictTransform.Add(keyValuePair4.Key, keyValuePair4.Value);
				}
			}
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair5 in bluestacksUIColor.DictThemeAvailable)
				{
					if (!this.DictThemeAvailable.ContainsKey(keyValuePair5.Key))
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

		// Token: 0x0600067A RID: 1658 RVA: 0x00005C95 File Offset: 0x00003E95
		public void NotifyUIElements()
		{
			if (BluestacksUIColor.SourceUpdatedEvent != null)
			{
				BluestacksUIColor.SourceUpdatedEvent(this, null);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
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
					if (this.DictCategory.Count == 0)
					{
						this.DictCategory.Add(string.Empty, string.Empty);
					}
					XamlWriter.Save(this, manager);
					if (string.IsNullOrEmpty(saveFilePath))
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

		// Token: 0x0600067C RID: 1660 RVA: 0x0001D5CC File Offset: 0x0001B7CC
		private Color GetMainColor(string colorTheme)
		{
			ColorUtils mainThemeColor = this.MainThemeColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#1E2138"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, mainThemeColor);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001D61C File Offset: 0x0001B81C
		private Color GetForegroundColor(string colorTheme)
		{
			ColorUtils mainForeGroundColor = this.MainForeGroundColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#F8F8EE"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, mainForeGroundColor);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001D66C File Offset: 0x0001B86C
		private Color GetContrastColor(string colorTheme)
		{
			ColorUtils contrastThemeColor = this.ContrastThemeColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#585A6C"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, contrastThemeColor);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001D6BC File Offset: 0x0001B8BC
		private Color GetContrastForegroundColor(string colorTheme)
		{
			ColorUtils contrastForegroundColor = this.ContrastForegroundColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#20233A"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, contrastForegroundColor);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001D70C File Offset: 0x0001B90C
		private Color GetHighlighterColor(string colorTheme)
		{
			ColorUtils applicationHighlighterColor = this.ApplicationHighlighterColor;
			ColorUtils mainColorTheme = new ColorUtils((Color)ColorConverter.ConvertFromString("#F87C06"));
			Tuple<double, double, double, double> unitColor = BluestacksUIColor.GetUnitColor(colorTheme, mainColorTheme);
			return BluestacksUIColor.Compute(unitColor.Item1, unitColor.Item2, unitColor.Item3, unitColor.Item4, applicationHighlighterColor);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00005CAA File Offset: 0x00003EAA
		private static Color Compute(double r, double g, double b, double a, ColorUtils c)
		{
			return ColorUtils.FromHSLA((double)c.H / r, (double)c.S / g, (double)c.L / b, (double)c.A / a).WPFColor;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001D75C File Offset: 0x0001B95C
		private static Tuple<double, double, double, double> GetUnitColor(string colorString, ColorUtils mainColorTheme)
		{
			if (!colorString.Contains('#'))
			{
				colorString = "#" + colorString;
			}
			ColorUtils colorUtils = new ColorUtils((Color)ColorConverter.ConvertFromString(colorString));
			return new Tuple<double, double, double, double>((double)(mainColorTheme.H / colorUtils.H), (double)(mainColorTheme.S / colorUtils.S), (double)(mainColorTheme.L / colorUtils.L), (double)(mainColorTheme.A / colorUtils.A));
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00005CDE File Offset: 0x00003EDE
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x00005CFF File Offset: 0x00003EFF
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00005D1C File Offset: 0x00003F1C
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00005D3D File Offset: 0x00003F3D
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

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00005D5A File Offset: 0x00003F5A
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00005D7B File Offset: 0x00003F7B
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00005D98 File Offset: 0x00003F98
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00005DB9 File Offset: 0x00003FB9
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00005DD6 File Offset: 0x00003FD6
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00005DF7 File Offset: 0x00003FF7
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

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00005E14 File Offset: 0x00004014
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00005E2B File Offset: 0x0000402B
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

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00005E3E File Offset: 0x0000403E
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00005E55 File Offset: 0x00004055
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

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00005E68 File Offset: 0x00004068
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00005E7F File Offset: 0x0000407F
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00005E92 File Offset: 0x00004092
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00005EA9 File Offset: 0x000040A9
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00005EBC File Offset: 0x000040BC
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00005ECE File Offset: 0x000040CE
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

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00005EE1 File Offset: 0x000040E1
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00005EF3 File Offset: 0x000040F3
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

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00005F06 File Offset: 0x00004106
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00005F1D File Offset: 0x0000411D
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

		// Token: 0x0600069B RID: 1691 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		private void InitalizeDefault()
		{
			this.DictBrush["MainThemeColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E2138"));
			this.DictBrush["MainForeGroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8F8EE"));
			this.DictBrush["ContrastThemeColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#585A6C"));
			this.DictBrush["ContrastForegroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF232642"));
			this.DictBrush["ApplicationHighlighterColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F87C06"));
			this.DictThemeAvailable["UserAvailable"] = "true";
			this.DictThemeAvailable["themeName"] = RegistryManager.ClientThemeName;
			this.DictThemeAvailable["ThemeDisplayName"] = "BlueStacks";
			this.DictThemeAvailable["LocationUnrestricted"] = "true";
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

		// Token: 0x0600069C RID: 1692 RVA: 0x0001DC90 File Offset: 0x0001BE90
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

		// Token: 0x0600069D RID: 1693 RVA: 0x0001DD00 File Offset: 0x0001BF00
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

		// Token: 0x0600069E RID: 1694 RVA: 0x0001F764 File Offset: 0x0001D964
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

		// Token: 0x0600069F RID: 1695 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		public static void ScrollBarScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (sender != null)
			{
				ScrollViewer scrollViewer = sender as ScrollViewer;
				double verticalOffset = scrollViewer.VerticalOffset;
				if (scrollViewer.ComputedVerticalScrollBarVisibility != Visibility.Visible)
				{
					scrollViewer.OpacityMask = null;
					return;
				}
				if (verticalOffset < 10.0)
				{
					scrollViewer.OpacityMask = BluestacksUIColor.mTopOpacityMask;
					return;
				}
				if (scrollViewer.ExtentHeight - scrollViewer.ActualHeight - 10.0 <= verticalOffset)
				{
					scrollViewer.OpacityMask = BluestacksUIColor.mBottomOpacityMask;
					return;
				}
				scrollViewer.OpacityMask = BluestacksUIColor.mScrolledOpacityMask;
			}
		}

		// Token: 0x04000358 RID: 856
		[XmlIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public const string ThemeFileName = "ThemeFile";

		// Token: 0x04000360 RID: 864
		public static readonly LinearGradientBrush mScrolledOpacityMask = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
		{
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.0),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.15),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.8),
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 1.0)
		}), new Point(0.0, 0.0), new Point(0.0, 1.0));

		// Token: 0x04000361 RID: 865
		public static readonly LinearGradientBrush mTopOpacityMask = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
		{
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.0),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.8),
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 1.0)
		}), new Point(0.0, 0.0), new Point(0.0, 1.0));

		// Token: 0x04000362 RID: 866
		public static readonly LinearGradientBrush mBottomOpacityMask = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
		{
			new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.0),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 0.15),
			new GradientStop(Color.FromArgb(byte.MaxValue, 0, 0, 0), 1.0)
		}), new Point(0.0, 0.0), new Point(0.0, 1.0));
	}
}
