using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200005D RID: 93
	public static class ConfigConverter
	{
		// Token: 0x06000226 RID: 550 RVA: 0x00012264 File Offset: 0x00010464
		public static bool Convert(string oldConfigPath, string newConfigPath, string newVersion, bool isBuiltIn, bool useCustomName)
		{
			bool result;
			try
			{
				JObject jobject = JObject.Parse(File.ReadAllText(oldConfigPath));
				string text = (oldConfigPath != null) ? oldConfigPath.Split(new char[]
				{
					'\\'
				}).Last<string>() : null;
				text.Remove(text.Length - 4, 4);
				int? configVersion = ConfigConverter.GetConfigVersion(jobject);
				int num = 13;
				if (configVersion.GetValueOrDefault() <= num & configVersion != null)
				{
					JObject jobject2 = ConfigConverter.Convert(jobject, newVersion, isBuiltIn, useCustomName);
					if (jobject2 != null)
					{
						File.WriteAllText(newConfigPath, jobject2.ToString());
						return true;
					}
				}
				else
				{
					configVersion = ConfigConverter.GetConfigVersion(jobject);
					num = 16;
					if ((configVersion.GetValueOrDefault() < num & configVersion != null) && Utils.CheckIfImagesArrayPresentInCfg(jobject))
					{
						JObject jobject3 = jobject;
						foreach (JToken jtoken in ((IEnumerable<JToken>)jobject["ControlSchemes"]))
						{
							JObject jobject4 = (JObject)jtoken;
							jobject4["Images"] = ConfigConverter.ConvertImagesArrayForPV16(jobject4);
						}
						jobject3["MetaData"]["Comment"] = string.Format(CultureInfo.InvariantCulture, "Generated automatically from ver {0}", new object[]
						{
							(int)jobject3["MetaData"]["ParserVersion"]
						});
						jobject3["MetaData"]["ParserVersion"] = 16;
						if (jobject3 != null)
						{
							File.WriteAllText(newConfigPath, jobject3.ToString());
							return true;
						}
					}
				}
				result = false;
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error while parsing config file {0}", new object[]
				{
					oldConfigPath
				}), new object[]
				{
					ex.Message
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00012460 File Offset: 0x00010660
		public static JObject Convert(JObject oldConfigJson, string newVersion, bool isBuiltIn, bool useCustomName)
		{
			if (useCustomName)
			{
				ConfigConverter.DEFAULT_PROFILE_NAME = "Custom";
			}
			List<string> list = new List<string>();
			JArray jarray = ((oldConfigJson != null) ? oldConfigJson["Primitives"] : null) as JArray;
			if (jarray != null)
			{
				foreach (JToken jtoken in jarray)
				{
					JArray jarray2 = jtoken["Tags"] as JArray;
					if (jarray2 != null)
					{
						foreach (JToken jtoken2 in jarray2)
						{
							list.Add(jtoken2.ToString());
						}
					}
				}
			}
			list = list.Distinct<string>().ToList<string>();
			string text = string.Empty;
			JArray jarray3 = new JArray();
			if (!list.Any<string>())
			{
				jarray3.Add(new JObject
				{
					{
						"Name",
						ConfigConverter.DEFAULT_PROFILE_NAME
					},
					{
						"BuiltIn",
						isBuiltIn
					},
					{
						"Selected",
						true
					},
					{
						"IsBookMarked",
						false
					},
					{
						"KeyboardLayout",
						oldConfigJson["MetaData"]["KeyboardLayout"]
					},
					{
						"GameControls",
						new JArray()
					},
					{
						"Images",
						new JArray()
					}
				});
			}
			else
			{
				if (oldConfigJson["Schemes"] != null)
				{
					JArray jarray4 = oldConfigJson["Schemes"] as JArray;
					if (jarray4 != null && jarray4.Any<JToken>())
					{
						JToken jtoken3 = (from scheme in oldConfigJson["Schemes"]
						where bool.Parse(scheme["Selected"].ToString())
						select scheme).FirstOrDefault<JToken>();
						if (jtoken3 != null && jtoken3["Tag"] != null)
						{
							text = jtoken3["Tag"].ToString();
						}
					}
				}
				foreach (string text2 in list)
				{
					jarray3.Add(new JObject
					{
						{
							"Name",
							text2.ToString(CultureInfo.InvariantCulture)
						},
						{
							"BuiltIn",
							isBuiltIn
						},
						{
							"Selected",
							string.Equals(text, text2.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
						},
						{
							"IsBookMarked",
							false
						},
						{
							"KeyboardLayout",
							oldConfigJson["MetaData"]["KeyboardLayout"]
						},
						{
							"GameControls",
							new JArray()
						},
						{
							"Images",
							new JArray()
						}
					});
				}
				if (string.IsNullOrEmpty(text))
				{
					jarray3[0]["Selected"] = true;
				}
			}
			foreach (JToken jtoken4 in ((IEnumerable<JToken>)oldConfigJson["Primitives"]))
			{
				JObject jobject = jtoken4.DeepClone() as JObject;
				if (jobject != null)
				{
					List<string> tags = new List<string>();
					if (jobject["Tags"] != null)
					{
						jobject["Tags"].ToList<JToken>().ForEach(delegate(JToken x)
						{
							tags.Add(x.ToString());
						});
						jobject["Tags"].Parent.Remove();
					}
					ConfigConverter.ConvertComboSequences(jobject);
					ConfigConverter.UpdateTiltAndStatePrimitives(jobject);
					if (!tags.Any<string>())
					{
						ConfigConverter.AddPrimitiveToGameControls(jarray3, jobject);
					}
					else
					{
						ConfigConverter.AddPrimitiveToGameControls(from scheme in jarray3.ToList<JToken>()
						where tags.Contains(scheme["Name"].ToString())
						select scheme, jobject);
					}
				}
			}
			if (!string.IsNullOrEmpty(text) && !list.Contains(text))
			{
				jarray3.Add(new JObject
				{
					{
						"Name",
						text
					},
					{
						"BuiltIn",
						isBuiltIn
					},
					{
						"Selected",
						true
					},
					{
						"IsBookMarked",
						false
					},
					{
						"KeyboardLayout",
						oldConfigJson["MetaData"]["KeyboardLayout"]
					},
					{
						"GameControls",
						new JArray()
					},
					{
						"Images",
						new JArray()
					}
				});
			}
			return new JObject
			{
				{
					"MetaData",
					ConfigConverter.GetMetadata(oldConfigJson["MetaData"], newVersion)
				},
				{
					"ControlSchemes",
					jarray3
				},
				{
					"Strings",
					oldConfigJson["Strings"].DeepClone()
				}
			};
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0001295C File Offset: 0x00010B5C
		public static int? GetConfigVersion(string config)
		{
			int? result;
			try
			{
				result = ConfigConverter.GetConfigVersion(JObject.Parse(File.ReadAllText(config)));
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error while parsing config file {0}", new object[]
				{
					config
				}), new object[]
				{
					ex.Message
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000129C8 File Offset: 0x00010BC8
		public static int? GetConfigVersion(JObject configJson)
		{
			int value;
			if (configJson != null && configJson["MetaData"] != null && configJson["MetaData"]["ParserVersion"] != null && int.TryParse(configJson["MetaData"]["ParserVersion"].ToString(), out value))
			{
				return new int?(value);
			}
			return null;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00012A30 File Offset: 0x00010C30
		private static void AddPrimitiveToGameControls(IEnumerable<JToken> controlSchemes, JToken primitiveCopy)
		{
			controlSchemes.ToList<JToken>().ForEach(delegate(JToken scheme)
			{
				JArray jarray = scheme["GameControls"] as JArray;
				if (jarray != null)
				{
					jarray.Add(primitiveCopy);
				}
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00012A64 File Offset: 0x00010C64
		private static JObject GetMetadata(JToken oldMetadata, string newVersion)
		{
			return new JObject
			{
				{
					"ParserVersion",
					newVersion
				},
				{
					"Comment",
					string.Format(CultureInfo.InvariantCulture, "Generated automatically from ver {0}", new object[]
					{
						oldMetadata["ParserVersion"]
					})
				}
			};
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00012ABC File Offset: 0x00010CBC
		private static void ConvertComboSequences(JObject primitive)
		{
			if (primitive["Type"] != null && string.Equals(primitive["Type"].ToString(), "Combo", StringComparison.OrdinalIgnoreCase))
			{
				primitive.Add("X", ConfigConverter.GetLocationForPoint(ConfigConverter.mScriptCol, 8));
				primitive.Add("Y", ConfigConverter.GetLocationForPoint(ConfigConverter.mScriptRow, 8));
				ConfigConverter.mScriptCol++;
				if (ConfigConverter.mScriptCol == 8)
				{
					ConfigConverter.mScriptRow++;
					ConfigConverter.mScriptRow %= 8;
				}
				ConfigConverter.mScriptCol %= 8;
				primitive["Type"] = "Script";
				primitive["$type"] = "Script, Bluestacks";
				primitive["IsVisibleInOverlay"] = true;
				primitive["ShowOnOverlay"] = true;
				primitive.Add("Comment", primitive["Description"]);
				primitive["Description"].Parent.Remove();
				if (primitive["Events"] != null)
				{
					JArray jarray = primitive["Events"] as JArray;
					if (jarray != null)
					{
						JArray jarray2 = new JArray();
						int num = 0;
						foreach (JToken jtoken in jarray)
						{
							int num2;
							if (int.TryParse(jtoken["Timestamp"].ToString(), out num2))
							{
								int num3 = num2 - num;
								jarray2.Add(string.Format(CultureInfo.InvariantCulture, "wait {0}", new object[]
								{
									num3
								}));
								ConfigConverter.ComboEventType comboEventType;
								if (jtoken["EventType"] != null && EnumHelper.TryParse<ConfigConverter.ComboEventType>(jtoken["EventType"].ToString(), out comboEventType))
								{
									switch (comboEventType)
									{
									case ConfigConverter.ComboEventType.MouseDown:
										jarray2.Add(string.Format(CultureInfo.InvariantCulture, "mouseDown {0} {1}", new object[]
										{
											jtoken["X"].ToString(),
											jtoken["Y"].ToString()
										}));
										break;
									case ConfigConverter.ComboEventType.MouseUp:
										jarray2.Add(string.Format(CultureInfo.InvariantCulture, "mouseUp {0} {1}", new object[]
										{
											jtoken["X"].ToString(),
											jtoken["Y"].ToString()
										}));
										break;
									case ConfigConverter.ComboEventType.MouseMove:
										jarray2.Add(string.Format(CultureInfo.InvariantCulture, "mouseMove {0} {1}", new object[]
										{
											jtoken["X"].ToString(),
											jtoken["Y"].ToString()
										}));
										break;
									case ConfigConverter.ComboEventType.MouseWheel:
										jarray2.Add(string.Format(CultureInfo.InvariantCulture, "mouseWheel {0} {1} {2}", new object[]
										{
											jtoken["X"].ToString(),
											jtoken["Y"].ToString(),
											jtoken["Delta"].ToString()
										}));
										break;
									case ConfigConverter.ComboEventType.KeyDown:
										jarray2.Add(string.Format(CultureInfo.InvariantCulture, "keyDown {0}", new object[]
										{
											jtoken["KeyName"].ToString()
										}));
										break;
									case ConfigConverter.ComboEventType.KeyUp:
										jarray2.Add(string.Format(CultureInfo.InvariantCulture, "keyUp {0}", new object[]
										{
											jtoken["KeyName"].ToString()
										}));
										break;
									case ConfigConverter.ComboEventType.IME:
									{
										string[] array = jtoken["Msg"].ToString().Split(new char[]
										{
											' '
										});
										string text = array[1].Split(new char[]
										{
											'='
										})[1];
										if (!string.Equals(text, "0", StringComparison.OrdinalIgnoreCase))
										{
											jarray2.Add(string.Format(CultureInfo.InvariantCulture, "text backspace {0}", new object[]
											{
												text
											}));
										}
										if (!string.IsNullOrEmpty(array[0].Split(new char[]
										{
											'_'
										})[1]))
										{
											jarray2.Add(string.Format(CultureInfo.InvariantCulture, "text {0}", new object[]
											{
												array[0].Split(new char[]
												{
													'_'
												})[1]
											}));
										}
										break;
									}
									}
								}
							}
							num = num2;
						}
						primitive.Add("Commands", jarray2);
						primitive["Events"].Parent.Remove();
					}
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00012F9C File Offset: 0x0001119C
		private static void UpdateTiltAndStatePrimitives(JObject primitive)
		{
			if ((primitive["Type"] != null && string.Equals(primitive["Type"].ToString(), "State", StringComparison.OrdinalIgnoreCase)) || string.Equals(primitive["Type"].ToString(), "Tilt", StringComparison.OrdinalIgnoreCase))
			{
				primitive.Add("X", ConfigConverter.GetLocationForPoint(ConfigConverter.mTiltCol, 2));
				primitive.Add("Y", ConfigConverter.GetLocationForPoint(ConfigConverter.mTiltRow, 2));
				ConfigConverter.mTiltCol++;
				if (ConfigConverter.mTiltCol == 2)
				{
					ConfigConverter.mTiltRow++;
					ConfigConverter.mTiltRow %= 2;
				}
				ConfigConverter.mTiltCol %= 2;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00013060 File Offset: 0x00011260
		private static int GetLocationForPoint(int _location, int _maxCol)
		{
			int num = 100 / _maxCol;
			return num / 2 + _location * num;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001307C File Offset: 0x0001127C
		public static JArray ConvertImagesArrayForPV16(JObject scheme)
		{
			JArray jarray = new JArray();
			if (scheme != null && scheme["Images"] != null && ((JArray)scheme["Images"]).Count > 0)
			{
				foreach (JToken jtoken in ((IEnumerable<JToken>)scheme["Images"]))
				{
					JObject jobject = new JObject();
					jobject.Add("ImageId", jtoken["ImageId"]);
					jobject.Add("ImageType", ConfigConverter.sImagesVersion);
					if (jtoken["ImageType"] != null)
					{
						jobject.Add("TextureCRC", jtoken["TextureCRC"]);
						jobject.Add("TextureIndex", jtoken["TextureIndex"]);
						jobject.Add("TextureCoord", jtoken["TextureCoord"]);
						jobject.Add("VerticalIndex", jtoken["VerticalIndex"]);
						jobject.Add("VertexRect", "VertexRect");
					}
					else
					{
						JObject jobject2 = jobject;
						string propertyName = "TextureCRC";
						JToken jtoken2 = jtoken["Texture"];
						jobject2.Add(propertyName, (jtoken2 != null) ? jtoken2["CRC"] : null);
						JObject jobject3 = jobject;
						string propertyName2 = "TextureIndex";
						JToken jtoken3 = jtoken["VarState"];
						JToken value;
						if (jtoken3 == null)
						{
							value = null;
						}
						else
						{
							JToken jtoken4 = jtoken3[0];
							if (jtoken4 == null)
							{
								value = null;
							}
							else
							{
								JToken jtoken5 = jtoken4[0];
								value = ((jtoken5 != null) ? jtoken5["Index"] : null);
							}
						}
						jobject3.Add(propertyName2, value);
						JObject jobject4 = jobject;
						string propertyName3 = "TextureCoord";
						JToken jtoken6 = jtoken["VarState"];
						JToken value2;
						if (jtoken6 == null)
						{
							value2 = null;
						}
						else
						{
							JToken jtoken7 = jtoken6[0];
							if (jtoken7 == null)
							{
								value2 = null;
							}
							else
							{
								JToken jtoken8 = jtoken7[0];
								value2 = ((jtoken8 != null) ? jtoken8["Buffer"] : null);
							}
						}
						jobject4.Add(propertyName3, value2);
						jobject.Add("VerticalIndex", 0);
						jobject.Add("VertexRect", new JArray());
					}
					jarray.Add(jobject);
				}
			}
			return jarray;
		}

		// Token: 0x040000DA RID: 218
		private const int THRESHOLD_VERSION = 13;

		// Token: 0x040000DB RID: 219
		public const int IMAGES_VERSION1_PARSER_VERSION = 16;

		// Token: 0x040000DC RID: 220
		public const string METADATA = "MetaData";

		// Token: 0x040000DD RID: 221
		public const string PARSER_VERSION = "ParserVersion";

		// Token: 0x040000DE RID: 222
		public const string COMMENT = "Comment";

		// Token: 0x040000DF RID: 223
		public const string COMMENT_VALUE = "Generated automatically from ver {0}";

		// Token: 0x040000E0 RID: 224
		private const string PRIMITIVES = "Primitives";

		// Token: 0x040000E1 RID: 225
		private const string SCHEMES = "Schemes";

		// Token: 0x040000E2 RID: 226
		private const string TAGS = "Tags";

		// Token: 0x040000E3 RID: 227
		private const string TAG = "Tag";

		// Token: 0x040000E4 RID: 228
		public const string CONTROL_SCHEMES = "ControlSchemes";

		// Token: 0x040000E5 RID: 229
		private const string NAME = "Name";

		// Token: 0x040000E6 RID: 230
		private const string BUILT_IN = "BuiltIn";

		// Token: 0x040000E7 RID: 231
		private const string SELECTED = "Selected";

		// Token: 0x040000E8 RID: 232
		private const string IS_BOOKMARKED = "IsBookMarked";

		// Token: 0x040000E9 RID: 233
		private const string KEYBOARD_LAYOUT = "KeyboardLayout";

		// Token: 0x040000EA RID: 234
		public const string IMAGES = "Images";

		// Token: 0x040000EB RID: 235
		private const string GAME_CONTROLS = "GameControls";

		// Token: 0x040000EC RID: 236
		private const string STRINGS = "Strings";

		// Token: 0x040000ED RID: 237
		private const string TYPE = "Type";

		// Token: 0x040000EE RID: 238
		private const string TYPE_ = "$type";

		// Token: 0x040000EF RID: 239
		private const string COMBO = "Combo";

		// Token: 0x040000F0 RID: 240
		private const string STATE = "State";

		// Token: 0x040000F1 RID: 241
		private const string TILT = "Tilt";

		// Token: 0x040000F2 RID: 242
		private const string SCRIPT = "Script";

		// Token: 0x040000F3 RID: 243
		private const string SCRIPT_ = "Script, Bluestacks";

		// Token: 0x040000F4 RID: 244
		private const string DESCRIPTION = "Description";

		// Token: 0x040000F5 RID: 245
		private const string X = "X";

		// Token: 0x040000F6 RID: 246
		private const string Y = "Y";

		// Token: 0x040000F7 RID: 247
		private const string EVENTS = "Events";

		// Token: 0x040000F8 RID: 248
		private const string EVENT_TYPE = "EventType";

		// Token: 0x040000F9 RID: 249
		private const string TIMESTAMP = "Timestamp";

		// Token: 0x040000FA RID: 250
		private const string KEY_NAME = "KeyName";

		// Token: 0x040000FB RID: 251
		private const string MSG = "Msg";

		// Token: 0x040000FC RID: 252
		private const string DELTA = "Delta";

		// Token: 0x040000FD RID: 253
		private const string COMMANDS = "Commands";

		// Token: 0x040000FE RID: 254
		public const string OVERLAY = "IsVisibleInOverlay";

		// Token: 0x040000FF RID: 255
		public const string SHOW_OVERLAY = "ShowOnOverlay";

		// Token: 0x04000100 RID: 256
		private const int mMaxRowCol = 8;

		// Token: 0x04000101 RID: 257
		private static string DEFAULT_PROFILE_NAME = "Default";

		// Token: 0x04000102 RID: 258
		private static int mScriptRow = 0;

		// Token: 0x04000103 RID: 259
		private static int mScriptCol = 0;

		// Token: 0x04000104 RID: 260
		private const int mTiltMaxRowCol = 2;

		// Token: 0x04000105 RID: 261
		private static int mTiltCol = 0;

		// Token: 0x04000106 RID: 262
		private static int mTiltRow = 0;

		// Token: 0x04000107 RID: 263
		internal static string sImagesVersion = "Version 1";

		// Token: 0x0200005E RID: 94
		private enum ComboEventType
		{
			// Token: 0x04000109 RID: 265
			None,
			// Token: 0x0400010A RID: 266
			MouseDown,
			// Token: 0x0400010B RID: 267
			MouseUp,
			// Token: 0x0400010C RID: 268
			MouseMove,
			// Token: 0x0400010D RID: 269
			MouseWheel,
			// Token: 0x0400010E RID: 270
			KeyDown,
			// Token: 0x0400010F RID: 271
			KeyUp,
			// Token: 0x04000110 RID: 272
			IME
		}
	}
}
