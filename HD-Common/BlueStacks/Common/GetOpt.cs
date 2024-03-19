using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x020001F6 RID: 502
	public class GetOpt
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003D250 File Offset: 0x0003B450
		public void Parse(string[] args)
		{
			int i = 0;
			if (args == null)
			{
				return;
			}
			while (i < args.Length)
			{
				int num = this.OptionPos(args[i]);
				if (num > 0)
				{
					if (this.GetOption(args, ref i, num))
					{
						int count = this.Count;
						this.Count = count + 1;
					}
					else
					{
						this.InvalidOption(args[Math.Min(i, args.Length - 1)]);
					}
				}
				else
				{
					if (this.Args == null)
					{
						this.Args = new ArrayList();
					}
					this.Args.Add(args[i]);
					if (!this.IsValidArg(args[i]))
					{
						this.InvalidOption(args[i]);
					}
				}
				i++;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0000DE04 File Offset: 0x0000C004
		public IList InvalidArgs
		{
			get
			{
				return this.mInvalidArgs;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public bool NoArgs
		{
			get
			{
				return this.ArgCount == 0 && this.Count == 0;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0000DE21 File Offset: 0x0000C021
		public int ArgCount
		{
			get
			{
				if (this.Args != null)
				{
					return this.Args.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0000DE38 File Offset: 0x0000C038
		public bool IsInValid
		{
			get
			{
				return this.IsInvalid;
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003D2E8 File Offset: 0x0003B4E8
		protected virtual int OptionPos(string opt)
		{
			if (opt == null || opt.Length < 2)
			{
				return 0;
			}
			char[] array;
			if (opt.Length > 2)
			{
				array = opt.ToCharArray(0, 3);
				if (array[0] == '-' && array[1] == '-' && this.IsOptionNameChar(array[2]))
				{
					return 2;
				}
			}
			else
			{
				array = opt.ToCharArray(0, 2);
			}
			if (array[0] == '-' && this.IsOptionNameChar(array[1]))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0000DE40 File Offset: 0x0000C040
		protected virtual bool IsOptionNameChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '?';
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0000DE51 File Offset: 0x0000C051
		protected virtual void InvalidOption(string name)
		{
			this.mInvalidArgs.Add(name);
			this.IsInvalid = true;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0000346F File Offset: 0x0000166F
		protected virtual bool IsValidArg(string arg)
		{
			return true;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003D350 File Offset: 0x0003B550
		protected virtual bool MatchName(MemberInfo field, string name)
		{
			object[] array = (field != null) ? field.GetCustomAttributes(typeof(ArgAttribute), true) : null;
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Compare(((ArgAttribute)array[i]).Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003D39C File Offset: 0x0003B59C
		protected virtual PropertyInfo GetMemberProperty(string name)
		{
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (string.Compare(propertyInfo.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return propertyInfo;
				}
				if (this.MatchName(propertyInfo, name))
				{
					return propertyInfo;
				}
			}
			return null;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0003D3E8 File Offset: 0x0003B5E8
		protected virtual FieldInfo GetMemberField(string name)
		{
			foreach (FieldInfo fieldInfo in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (string.Compare(fieldInfo.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return fieldInfo;
				}
				if (this.MatchName(fieldInfo, name))
				{
					return fieldInfo;
				}
			}
			return null;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003D434 File Offset: 0x0003B634
		protected virtual object GetOptionValue(MemberInfo field)
		{
			object[] array = (field != null) ? field.GetCustomAttributes(typeof(ArgAttribute), true) : null;
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Console.WriteLine(array2[i]);
			}
			if (array.Length != 0)
			{
				return ((ArgAttribute)array[0]).Value;
			}
			return null;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003D484 File Offset: 0x0003B684
		protected virtual bool GetOption(string[] args, ref int index, int pos)
		{
			try
			{
				object obj = null;
				string text;
				if (args == null)
				{
					text = null;
				}
				else
				{
					string text2 = args[index];
					text = ((text2 != null) ? text2.Substring(pos, args[index].Length - pos) : null);
				}
				string name = text;
				this.SplitOptionAndValue(ref name, ref obj);
				FieldInfo memberField = this.GetMemberField(name);
				if (memberField != null)
				{
					object obj2 = this.GetOptionValue(memberField);
					if (obj2 == null)
					{
						if (memberField.FieldType == typeof(bool))
						{
							obj2 = true;
						}
						else if (memberField.FieldType == typeof(string))
						{
							object obj3;
							if (obj == null)
							{
								int num = index + 1;
								index = num;
								obj3 = args[num];
							}
							else
							{
								obj3 = obj;
							}
							obj2 = obj3;
							memberField.SetValue(this, Convert.ChangeType(obj2, memberField.FieldType, CultureInfo.InvariantCulture));
							string text3 = (string)obj2;
							if (text3 == null || text3.Length == 0)
							{
								return false;
							}
							return true;
						}
						else if (memberField.FieldType.IsEnum)
						{
							obj2 = Enum.Parse(memberField.FieldType, (string)obj, true);
						}
						else
						{
							object obj4;
							if (obj == null)
							{
								int num = index + 1;
								index = num;
								obj4 = args[num];
							}
							else
							{
								obj4 = obj;
							}
							obj2 = obj4;
						}
					}
					memberField.SetValue(this, Convert.ChangeType(obj2, memberField.FieldType, CultureInfo.InvariantCulture));
					return true;
				}
				PropertyInfo memberProperty = this.GetMemberProperty(name);
				if (memberProperty != null)
				{
					object obj5 = this.GetOptionValue(memberProperty);
					if (obj5 == null)
					{
						if (memberProperty.PropertyType == typeof(bool))
						{
							obj5 = true;
						}
						else if (memberProperty.PropertyType == typeof(string))
						{
							object obj6;
							if (obj == null)
							{
								int num = index + 1;
								index = num;
								obj6 = args[num];
							}
							else
							{
								obj6 = obj;
							}
							obj5 = obj6;
							memberProperty.SetValue(this, Convert.ChangeType(obj5, memberProperty.PropertyType, CultureInfo.InvariantCulture), null);
							string text4 = (string)obj5;
							if (text4 == null || text4.Length == 0)
							{
								return false;
							}
							return true;
						}
						else if (memberProperty.PropertyType.IsEnum)
						{
							obj5 = Enum.Parse(memberProperty.PropertyType, (string)obj, true);
						}
						else
						{
							object obj7;
							if (obj == null)
							{
								int num = index + 1;
								index = num;
								obj7 = args[num];
							}
							else
							{
								obj7 = obj;
							}
							obj5 = obj7;
						}
					}
					memberProperty.SetValue(this, Convert.ChangeType(obj5, memberProperty.PropertyType, CultureInfo.InvariantCulture), null);
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		protected virtual void SplitOptionAndValue(ref string opt, ref object val)
		{
			int num = -1;
			if (opt != null)
			{
				num = opt.IndexOfAny(new char[]
				{
					':',
					'='
				});
			}
			if (num < 1)
			{
				return;
			}
			val = opt.Substring(num + 1);
			opt = opt.Substring(0, num);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0000DE67 File Offset: 0x0000C067
		public virtual void Help()
		{
			Console.WriteLine(this.GetHelpText());
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0003D724 File Offset: 0x0003B924
		public virtual string GetHelpText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			FieldInfo[] fields = base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
			char c = '-';
			foreach (FieldInfo fieldInfo in fields)
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(ArgAttribute), true);
				if (customAttributes.Length != 0)
				{
					ArgAttribute argAttribute = (ArgAttribute)customAttributes[0];
					if (argAttribute.Description != null)
					{
						string text = "";
						if (argAttribute.Value == null)
						{
							if (fieldInfo.FieldType == typeof(int))
							{
								text = "[Integer]";
							}
							else if (fieldInfo.FieldType == typeof(float))
							{
								text = "[Float]";
							}
							else if (fieldInfo.FieldType == typeof(string))
							{
								text = "[String]";
							}
							else if (fieldInfo.FieldType == typeof(bool))
							{
								text = "[Boolean]";
							}
						}
						stringBuilder.AppendFormat("{0}{1,-20}\n\t{2}", c, fieldInfo.Name + text, argAttribute.Description);
						if (argAttribute.Name != null)
						{
							stringBuilder.AppendFormat(" (Name format: {0}{1}{2})", c, argAttribute.Name, text);
						}
						stringBuilder.Append(Environment.NewLine);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0000DE74 File Offset: 0x0000C074
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x0000DE7C File Offset: 0x0000C07C
		protected ArrayList Args { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0000DE85 File Offset: 0x0000C085
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0000DE8D File Offset: 0x0000C08D
		protected bool IsInvalid { get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0000DE96 File Offset: 0x0000C096
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x0000DE9E File Offset: 0x0000C09E
		public int Count { get; set; }

		// Token: 0x04000AA2 RID: 2722
		private ArrayList mInvalidArgs = new ArrayList();
	}
}
