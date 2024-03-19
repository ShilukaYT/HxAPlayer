using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlueStacks.Common
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	public class DataModificationTracker
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00004BCB File Offset: 0x00002DCB
		public IList<string> ChangedProperties { get; } = new List<string>();

		// Token: 0x060004BD RID: 1213 RVA: 0x00004BD3 File Offset: 0x00002DD3
		public void Lock(object previousObject, List<string> ignoreList = null, bool isRecursive = false)
		{
			this._PreviousObject = previousObject;
			this._IgnoreList = ((ignoreList == null) ? new List<string>() : ignoreList);
			this._IgnoreList.Add("DataModificationTracker");
			this._IsRecursive = isRecursive;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00004C04 File Offset: 0x00002E04
		public bool HasChanged(object currentObject)
		{
			this.ChangedProperties.Clear();
			return !this.AreObjectsEqual(this._PreviousObject, currentObject) || this.ChangedProperties.Count > 0;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00018360 File Offset: 0x00016560
		private bool AreObjectsEqual(object objectA, object objectB)
		{
			bool result = true;
			if (objectA == null || objectB == null)
			{
				result = object.Equals(objectA, objectB);
			}
			else
			{
				Type type = objectA.GetType();
				foreach (PropertyInfo propertyInfo in from prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				where prop.CanRead && !this._IgnoreList.Contains(prop.Name)
				select prop)
				{
					object value = propertyInfo.GetValue(objectA, null);
					object value2 = propertyInfo.GetValue(objectB, null);
					if (DataModificationTracker.CanDirectlyCompare(propertyInfo.PropertyType))
					{
						if (!DataModificationTracker.AreValuesEqual(value, value2))
						{
							this.ChangedProperties.Add("Class: " + type.FullName + "\tProperty:" + propertyInfo.Name);
							result = false;
						}
					}
					else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
					{
						if ((value == null && value2 != null) || (value != null && value2 == null))
						{
							this.ChangedProperties.Add("Class: " + type.FullName + "\tProperty:" + propertyInfo.Name);
							result = false;
						}
						else if (value != null && value2 != null)
						{
							IEnumerable<object> source = ((IEnumerable)value).Cast<object>();
							IEnumerable<object> source2 = ((IEnumerable)value2).Cast<object>();
							if (source.Count<object>() != source2.Count<object>())
							{
								this.ChangedProperties.Add("Class: " + type.FullName + "\tProperty:" + propertyInfo.Name);
								result = false;
							}
							else
							{
								for (int i = 0; i < source.Count<object>(); i++)
								{
									object obj = source.ElementAt(i);
									object obj2 = source2.ElementAt(i);
									if (DataModificationTracker.CanDirectlyCompare(obj.GetType()))
									{
										if (!DataModificationTracker.AreValuesEqual(obj, obj2))
										{
											this.ChangedProperties.Add("Class: " + type.FullName + "\tProperty:" + propertyInfo.Name);
											result = false;
										}
									}
									else if (!this.AreObjectsEqual(obj, obj2))
									{
										this.ChangedProperties.Add("Class: " + type.FullName + "\tProperty:" + propertyInfo.Name);
										result = false;
									}
								}
							}
						}
					}
					else if (propertyInfo.PropertyType.IsClass && this._IsRecursive)
					{
						if (!this.AreObjectsEqual(propertyInfo.GetValue(objectA, null), propertyInfo.GetValue(objectB, null)))
						{
							this.ChangedProperties.Add("Class: " + type.FullName + "\tProperty:" + propertyInfo.Name);
							result = false;
						}
					}
					else
					{
						this.ChangedProperties.Add("Class: " + type.FullName + "\tProperty:" + propertyInfo.Name);
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00004C30 File Offset: 0x00002E30
		private static bool CanDirectlyCompare(Type type)
		{
			return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001862C File Offset: 0x0001682C
		private static bool AreValuesEqual(object valueA, object valueB)
		{
			IComparable comparable = valueA as IComparable;
			return (valueA != null || valueB == null) && (valueA == null || valueB != null) && (comparable == null || comparable.CompareTo(valueB) == 0) && object.Equals(valueA, valueB);
		}

		// Token: 0x04000223 RID: 547
		private object _PreviousObject;

		// Token: 0x04000224 RID: 548
		private List<string> _IgnoreList;

		// Token: 0x04000225 RID: 549
		private bool _IsRecursive;
	}
}
