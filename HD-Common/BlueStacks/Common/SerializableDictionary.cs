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
	// Token: 0x02000209 RID: 521
	[XmlRoot("dictionary")]
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
	{
		// Token: 0x06001063 RID: 4195 RVA: 0x0000DFFE File Offset: 0x0000C1FE
		public SerializableDictionary()
		{
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0000E006 File Offset: 0x0000C206
		public SerializableDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
		{
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0000E00F File Offset: 0x0000C20F
		public SerializableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
		{
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0000E019 File Offset: 0x0000C219
		protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00004A00 File Offset: 0x00002C00
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003E9A8 File Offset: 0x0003CBA8
		public void ReadXml(XmlReader reader)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(TValue));
			bool flag = true;
			if (reader != null)
			{
				flag = reader.IsEmptyElement;
				reader.Read();
			}
			if (flag)
			{
				return;
			}
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

		// Token: 0x06001069 RID: 4201 RVA: 0x0003EA68 File Offset: 0x0003CC68
		public void WriteXml(XmlWriter writer)
		{
			if (writer != null)
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

		// Token: 0x0600106A RID: 4202 RVA: 0x0003EB34 File Offset: 0x0003CD34
		public virtual object Clone()
		{
			base.GetType();
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
