using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x02000063 RID: 99
	[XmlRoot("dictionary")]
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00007341 File Offset: 0x00005541
		public SerializableDictionary()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000734B File Offset: 0x0000554B
		public SerializableDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007356 File Offset: 0x00005556
		public SerializableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007362 File Offset: 0x00005562
		protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007370 File Offset: 0x00005570
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007384 File Offset: 0x00005584
		public void ReadXml(XmlReader reader)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(TValue));
			bool flag = true;
			bool flag2 = reader != null;
			if (flag2)
			{
				flag = reader.IsEmptyElement;
				reader.Read();
			}
			bool flag3 = flag;
			if (!flag3)
			{
				while (reader == null || reader.NodeType != XmlNodeType.EndElement)
				{
					reader.ReadStartElement("item");
					reader.ReadStartElement("key");
					TKey key = (TKey)((object)xmlSerializer.Deserialize(reader));
					reader.ReadEndElement();
					reader.ReadStartElement("value");
					TValue value = (TValue)((object)xmlSerializer2.Deserialize(reader));
					reader.ReadEndElement();
					base.Add(key, value);
					reader.ReadEndElement();
					reader.MoveToContent();
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007468 File Offset: 0x00005668
		public void WriteXml(XmlWriter writer)
		{
			bool flag = writer != null;
			if (flag)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(TKey));
				XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(TValue));
				foreach (TKey tkey in base.Keys)
				{
					writer.WriteStartElement("item");
					writer.WriteStartElement("key");
					xmlSerializer.Serialize(writer, tkey);
					writer.WriteEndElement();
					writer.WriteStartElement("value");
					TValue tvalue = base[tkey];
					xmlSerializer2.Serialize(writer, tvalue);
					writer.WriteEndElement();
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007548 File Offset: 0x00005748
		public virtual object Clone()
		{
			Type type = base.GetType();
			IFormatter formatter = new BinaryFormatter();
			object result;
			using (Stream stream = new MemoryStream())
			{
				formatter.Serialize(stream, this);
				stream.Seek(0L, SeekOrigin.Begin);
				result = formatter.Deserialize(stream);
			}
			return result;
		}
	}
}
