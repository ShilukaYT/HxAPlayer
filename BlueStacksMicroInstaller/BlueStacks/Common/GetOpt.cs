using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x0200005B RID: 91
	public class GetOpt
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00005508 File Offset: 0x00003708
		public void Parse(string[] args)
		{
			int i = 0;
			bool flag = args == null;
			if (!flag)
			{
				while (i < args.Length)
				{
					int num = this.OptionPos(args[i]);
					bool flag2 = num > 0;
					if (flag2)
					{
						bool option = this.GetOption(args, ref i, num);
						if (option)
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
						bool flag3 = this.Args == null;
						if (flag3)
						{
							this.Args = new ArrayList();
						}
						this.Args.Add(args[i]);
						bool flag4 = !this.IsValidArg(args[i]);
						if (flag4)
						{
							this.InvalidOption(args[i]);
						}
					}
					i++;
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000055DF File Offset: 0x000037DF
		public IList InvalidArgs
		{
			get
			{
				return this.mInvalidArgs;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000055E7 File Offset: 0x000037E7
		public bool NoArgs
		{
			get
			{
				return this.ArgCount == 0 && this.Count == 0;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000055FD File Offset: 0x000037FD
		public int ArgCount
		{
			get
			{
				return (this.Args == null) ? 0 : this.Args.Count;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005615 File Offset: 0x00003815
		public bool IsInValid
		{
			get
			{
				return this.IsInvalid;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005620 File Offset: 0x00003820
		protected virtual int OptionPos(string opt)
		{
			bool flag = opt == null || opt.Length < 2;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = opt.Length > 2;
				char[] array;
				if (flag2)
				{
					array = opt.ToCharArray(0, 3);
					bool flag3 = array[0] == '-' && array[1] == '-' && this.IsOptionNameChar(array[2]);
					if (flag3)
					{
						return 2;
					}
				}
				else
				{
					array = opt.ToCharArray(0, 2);
				}
				bool flag4 = array[0] == '-' && this.IsOptionNameChar(array[1]);
				if (flag4)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000056B8 File Offset: 0x000038B8
		protected virtual bool IsOptionNameChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '?';
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000056DA File Offset: 0x000038DA
		protected virtual void InvalidOption(string name)
		{
			this.mInvalidArgs.Add(name);
			this.IsInvalid = true;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000056F4 File Offset: 0x000038F4
		protected virtual bool IsValidArg(string arg)
		{
			return true;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005708 File Offset: 0x00003908
		protected virtual bool MatchName(MemberInfo field, string name)
		{
			object[] array = (field != null) ? field.GetCustomAttributes(typeof(ArgAttribute), true) : null;
			foreach (ArgAttribute argAttribute in array)
			{
				bool flag = string.Compare(argAttribute.Name, name, StringComparison.OrdinalIgnoreCase) == 0;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005770 File Offset: 0x00003970
		protected virtual PropertyInfo GetMemberProperty(string name)
		{
			Type type = base.GetType();
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo[] array = properties;
			int i = 0;
			while (i < array.Length)
			{
				PropertyInfo propertyInfo = array[i];
				bool flag = string.Compare(propertyInfo.Name, name, StringComparison.OrdinalIgnoreCase) == 0;
				PropertyInfo result;
				if (flag)
				{
					result = propertyInfo;
				}
				else
				{
					bool flag2 = this.MatchName(propertyInfo, name);
					if (!flag2)
					{
						i++;
						continue;
					}
					result = propertyInfo;
				}
				return result;
			}
			return null;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000057E4 File Offset: 0x000039E4
		protected virtual FieldInfo GetMemberField(string name)
		{
			Type type = base.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			FieldInfo[] array = fields;
			int i = 0;
			while (i < array.Length)
			{
				FieldInfo fieldInfo = array[i];
				bool flag = string.Compare(fieldInfo.Name, name, StringComparison.OrdinalIgnoreCase) == 0;
				FieldInfo result;
				if (flag)
				{
					result = fieldInfo;
				}
				else
				{
					bool flag2 = this.MatchName(fieldInfo, name);
					if (!flag2)
					{
						i++;
						continue;
					}
					result = fieldInfo;
				}
				return result;
			}
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005858 File Offset: 0x00003A58
		protected virtual object GetOptionValue(MemberInfo field)
		{
			object[] array = (field != null) ? field.GetCustomAttributes(typeof(ArgAttribute), true) : null;
			foreach (object value in array)
			{
				Console.WriteLine(value);
			}
			bool flag = array.Length != 0;
			object result;
			if (flag)
			{
				ArgAttribute argAttribute = (ArgAttribute)array[0];
				result = argAttribute.Value;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000058C4 File Offset: 0x00003AC4
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
				bool flag = memberField != null;
				if (flag)
				{
					object obj2 = this.GetOptionValue(memberField);
					bool flag2 = obj2 == null;
					if (flag2)
					{
						bool flag3 = memberField.FieldType == typeof(bool);
						if (flag3)
						{
							obj2 = true;
						}
						else
						{
							bool flag4 = memberField.FieldType == typeof(string);
							if (flag4)
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
								bool flag5 = text3 == null || text3.Length == 0;
								if (flag5)
								{
									return false;
								}
								return true;
							}
							else
							{
								bool isEnum = memberField.FieldType.IsEnum;
								if (isEnum)
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
						}
					}
					memberField.SetValue(this, Convert.ChangeType(obj2, memberField.FieldType, CultureInfo.InvariantCulture));
					return true;
				}
				PropertyInfo memberProperty = this.GetMemberProperty(name);
				bool flag6 = memberProperty != null;
				if (flag6)
				{
					object obj5 = this.GetOptionValue(memberProperty);
					bool flag7 = obj5 == null;
					if (flag7)
					{
						bool flag8 = memberProperty.PropertyType == typeof(bool);
						if (flag8)
						{
							obj5 = true;
						}
						else
						{
							bool flag9 = memberProperty.PropertyType == typeof(string);
							if (flag9)
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
								bool flag10 = text4 == null || text4.Length == 0;
								if (flag10)
								{
									return false;
								}
								return true;
							}
							else
							{
								bool isEnum2 = memberProperty.PropertyType.IsEnum;
								if (isEnum2)
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

		// Token: 0x060000E6 RID: 230 RVA: 0x00005B98 File Offset: 0x00003D98
		protected virtual void SplitOptionAndValue(ref string opt, ref object val)
		{
			int num = -1;
			bool flag = opt != null;
			if (flag)
			{
				num = opt.IndexOfAny(new char[]
				{
					':',
					'='
				});
			}
			bool flag2 = num < 1;
			if (!flag2)
			{
				val = opt.Substring(num + 1);
				opt = opt.Substring(0, num);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005BED File Offset: 0x00003DED
		public virtual void Help()
		{
			Console.WriteLine(this.GetHelpText());
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005BFC File Offset: 0x00003DFC
		public virtual string GetHelpText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type type = base.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			char c = '-';
			foreach (FieldInfo fieldInfo in fields)
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(ArgAttribute), true);
				bool flag = customAttributes.Length != 0;
				if (flag)
				{
					ArgAttribute argAttribute = (ArgAttribute)customAttributes[0];
					bool flag2 = argAttribute.Description != null;
					if (flag2)
					{
						string text = "";
						bool flag3 = argAttribute.Value == null;
						if (flag3)
						{
							bool flag4 = fieldInfo.FieldType == typeof(int);
							if (flag4)
							{
								text = "[Integer]";
							}
							else
							{
								bool flag5 = fieldInfo.FieldType == typeof(float);
								if (flag5)
								{
									text = "[Float]";
								}
								else
								{
									bool flag6 = fieldInfo.FieldType == typeof(string);
									if (flag6)
									{
										text = "[String]";
									}
									else
									{
										bool flag7 = fieldInfo.FieldType == typeof(bool);
										if (flag7)
										{
											text = "[Boolean]";
										}
									}
								}
							}
						}
						stringBuilder.AppendFormat("{0}{1,-20}\n\t{2}", c, fieldInfo.Name + text, argAttribute.Description);
						bool flag8 = argAttribute.Name != null;
						if (flag8)
						{
							stringBuilder.AppendFormat(" (Name format: {0}{1}{2})", c, argAttribute.Name, text);
						}
						stringBuilder.Append(Environment.NewLine);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005DA4 File Offset: 0x00003FA4
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00005DAC File Offset: 0x00003FAC
		protected ArrayList Args { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005DB5 File Offset: 0x00003FB5
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00005DBD File Offset: 0x00003FBD
		protected bool IsInvalid { get; set; } = false;

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005DC6 File Offset: 0x00003FC6
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005DCE File Offset: 0x00003FCE
		public int Count { get; set; }

		// Token: 0x040001BC RID: 444
		private ArrayList mInvalidArgs = new ArrayList();
	}
}
