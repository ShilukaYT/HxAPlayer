using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x0200015C RID: 348
	public static class UsefulExtensionMethod
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x0002F47C File Offset: 0x0002D67C
		public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			return "{" + string.Join(",", dictionary.Select(delegate(KeyValuePair<TKey, TValue> kv)
			{
				TKey key = kv.Key;
				string str = (key != null) ? key.ToString() : null;
				string str2 = "=";
				TValue value = kv.Value;
				return str + str2 + ((value != null) ? value.ToString() : null);
			}).ToArray<string>()) + "}";
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0000C754 File Offset: 0x0000A954
		public static void AddIfNotContain<T>(this IList<T> list, T item)
		{
			if (list != null && !list.Contains(item))
			{
				list.Add(item);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002F4CC File Offset: 0x0002D6CC
		public static void AddIfNotContain<T>(this IList<T> list, IList<T> itemList)
		{
			if (itemList != null)
			{
				foreach (T item in itemList)
				{
					list.AddIfNotContain(item);
				}
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0000C769 File Offset: 0x0000A969
		public static T RandomElement<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.RandomElementUsing(new Random());
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002F518 File Offset: 0x0002D718
		public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
		{
			int index = 0;
			if (rand != null)
			{
				index = rand.Next(0, enumerable.Count<T>());
			}
			return enumerable.ElementAt(index);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0000C776 File Offset: 0x0000A976
		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source != null && source.IndexOf(toCheck, comp) >= 0;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002F540 File Offset: 0x0002D740
		public static string GetDescription(this Enum value)
		{
			Enum value2 = value;
			DescriptionAttribute descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute((value2 != null) ? value2.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Single((FieldInfo x) => x.GetValue(null).Equals(value)) : null, typeof(DescriptionAttribute));
			if (descriptionAttribute != null)
			{
				return descriptionAttribute.Description;
			}
			return value.ToString();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		public static void LoadViewFromUri(this UserControl userControl, string baseUri)
		{
			Uri uri = new Uri(baseUri, UriKind.Relative);
			Stream stream = ((PackagePart)typeof(Application).GetMethod("GetResourceOrContentPart", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[]
			{
				uri
			})).GetStream();
			Uri baseUri2 = new Uri((Uri)typeof(BaseUriHelper).GetProperty("PackAppBaseUri", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null), uri);
			ParserContext parserContext = new ParserContext
			{
				BaseUri = baseUri2
			};
			typeof(XamlReader).GetMethod("LoadBaml", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[]
			{
				stream,
				parserContext,
				userControl,
				true
			});
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0000C78B File Offset: 0x0000A98B
		public static T GetObjectOfType<T>(this string val, T defaultValue)
		{
			if (!string.IsNullOrEmpty(val))
			{
				return (T)((object)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(val));
			}
			return defaultValue;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002F664 File Offset: 0x0002D864
		public static T DeepCopy<T>(this T other)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, other);
				memoryStream.Position = 0L;
				result = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			return result;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002F6BC File Offset: 0x0002D8BC
		public static void SetPlacement(this Window window, string placementXml)
		{
			try
			{
				WindowPlacement.SetPlacement(new WindowInteropHelper(window).Handle, placementXml);
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in SetPlacement.Exception: " + ex.ToString());
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002F704 File Offset: 0x0002D904
		public static void SetPlacement(this Window window, double scalingFactor)
		{
			try
			{
				if (window != null)
				{
					RECT placementRect = new RECT((int)Math.Floor(window.Left * scalingFactor), (int)Math.Floor(window.Top * scalingFactor), (int)Math.Floor((window.Left + window.ActualWidth) * scalingFactor), (int)Math.Floor((window.Top + window.ActualHeight) * scalingFactor));
					WindowPlacement.SetPlacement(new WindowInteropHelper(window).Handle, placementRect);
				}
			}
			catch (Exception ex)
			{
				string str = "Exception in SetPlacement. ";
				Exception ex2 = ex;
				Logger.Warning(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0000C7B1 File Offset: 0x0000A9B1
		public static string GetPlacement(this Window window)
		{
			return WindowPlacement.GetPlacement(new WindowInteropHelper(window).Handle);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		public static object GetPropValue(this object obj, string name, out Type objType)
		{
			objType = typeof(string);
			if (name != null)
			{
				foreach (string name2 in name.Split(new char[]
				{
					'.'
				}))
				{
					if (obj == null)
					{
						return null;
					}
					PropertyInfo property = obj.GetType().GetProperty(name2);
					if (property == null)
					{
						return null;
					}
					obj = property.GetValue(obj, null);
					objType = property.PropertyType;
				}
			}
			return obj;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002F810 File Offset: 0x0002DA10
		public static T GetPropValue<T>(this object obj, string name)
		{
			Type type;
			object propValue = obj.GetPropValue(name, out type);
			if (propValue == null)
			{
				return default(T);
			}
			return (T)((object)propValue);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002F83C File Offset: 0x0002DA3C
		public static object ChangeType(this object obj, Type type)
		{
			if (obj.IsList())
			{
				IEnumerable<object> source = ((IEnumerable)obj).Cast<object>().ToList<object>();
				Type containedType = (type != null) ? type.GetGenericArguments().First<Type>() : null;
				return (from item in source
				select Convert.ChangeType(item, containedType, CultureInfo.InvariantCulture)).ToList<object>();
			}
			return Convert.ChangeType(obj, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0000C7C3 File Offset: 0x0000A9C3
		public static bool IsList(this object o)
		{
			return o != null && (o is IList && o.GetType().IsGenericType) && o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002F8A4 File Offset: 0x0002DAA4
		public static bool? SetTextblockTooltip(this TextBlock textBlock)
		{
			if (textBlock == null)
			{
				return null;
			}
			if (textBlock.IsTextTrimmed())
			{
				ToolTipService.SetIsEnabled(textBlock, true);
				return new bool?(true);
			}
			ToolTipService.SetIsEnabled(textBlock, false);
			return new bool?(false);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002F8E4 File Offset: 0x0002DAE4
		public static bool IsTextTrimmed(this CustomComboBox comboBox, string text)
		{
			if (comboBox != null)
			{
				Typeface typeface = new Typeface(comboBox.FontFamily, comboBox.FontStyle, comboBox.FontWeight, comboBox.FontStretch);
				return new FormattedText(text, Thread.CurrentThread.CurrentCulture, comboBox.FlowDirection, typeface, comboBox.FontSize, comboBox.Foreground)
				{
					MaxTextWidth = comboBox.ActualWidth,
					Trimming = TextTrimming.None
				}.Height > comboBox.ActualHeight;
			}
			return false;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002F958 File Offset: 0x0002DB58
		public static bool IsTextTrimmed(this TextBlock textBlock)
		{
			if (textBlock != null)
			{
				Typeface typeface = new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch);
				return new FormattedText(textBlock.Text, Thread.CurrentThread.CurrentCulture, textBlock.FlowDirection, typeface, textBlock.FontSize, textBlock.Foreground)
				{
					MaxTextWidth = textBlock.ActualWidth,
					Trimming = TextTrimming.None
				}.Height > textBlock.ActualHeight;
			}
			return false;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002F9D0 File Offset: 0x0002DBD0
		public static void SaveUserDefinedShortcuts(this ShortcutConfig mShortcutsConfigInstance)
		{
			if (mShortcutsConfigInstance != null)
			{
				JsonSerializerSettings serializerSettings = Utils.GetSerializerSettings();
				serializerSettings.Formatting = Formatting.Indented;
				string userDefinedShortcuts = JsonConvert.SerializeObject(mShortcutsConfigInstance, serializerSettings);
				RegistryManager.Instance.UserDefinedShortcuts = userDefinedShortcuts;
			}
		}
	}
}
