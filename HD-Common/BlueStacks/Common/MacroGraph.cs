using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x02000160 RID: 352
	public sealed class MacroGraph
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002FA58 File Offset: 0x0002DC58
		public static BiDirectionalGraph<MacroRecording> Instance
		{
			get
			{
				if (MacroGraph.mInstance == null)
				{
					object obj = MacroGraph.lockObj;
					lock (obj)
					{
						if (MacroGraph.mInstance == null)
						{
							MacroGraph.mInstance = new BiDirectionalGraph<MacroRecording>(null);
							MacroGraph.CreateMacroGraphInstance();
						}
					}
				}
				return MacroGraph.mInstance;
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0000C82E File Offset: 0x0000AA2E
		public static void ReCreateMacroGraphInstance()
		{
			if (MacroGraph.mInstance == null)
			{
				MacroGraph.mInstance = new BiDirectionalGraph<MacroRecording>(null);
			}
			MacroGraph.mInstance.Vertices.Clear();
			MacroGraph.CreateMacroGraphInstance();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
		private static void CreateMacroGraphInstance()
		{
			if (Directory.Exists(RegistryStrings.MacroRecordingsFolderPath))
			{
				foreach (string path in Directory.GetFiles(RegistryStrings.MacroRecordingsFolderPath))
				{
					string path2 = Path.Combine(RegistryStrings.MacroRecordingsFolderPath, path);
					if (File.Exists(path2))
					{
						try
						{
							MacroRecording macroRecording = JsonConvert.DeserializeObject<MacroRecording>(File.ReadAllText(path2), Utils.GetSerializerSettings());
							if (macroRecording != null && !string.IsNullOrEmpty(macroRecording.Name) && !string.IsNullOrEmpty(macroRecording.TimeCreated))
							{
								if (macroRecording.Events == null)
								{
									ObservableCollection<MergedMacroConfiguration> mergedMacroConfigurations = macroRecording.MergedMacroConfigurations;
									if (mergedMacroConfigurations == null || mergedMacroConfigurations.Count <= 0)
									{
										goto IL_98;
									}
								}
								MacroGraph.mInstance.AddVertex(macroRecording);
							}
							IL_98:;
						}
						catch
						{
							Logger.Error("Unable to deserialize userscript.");
						}
					}
				}
				MacroGraph.DrawMacroGraph();
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002FB88 File Offset: 0x0002DD88
		private static void DrawMacroGraph()
		{
			foreach (BiDirectionalVertex<MacroRecording> biDirectionalVertex in MacroGraph.mInstance.Vertices)
			{
				MacroGraph.LinkMacroChilds(biDirectionalVertex as MacroRecording);
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002FBDC File Offset: 0x0002DDDC
		public static void LinkMacroChilds(MacroRecording macro)
		{
			if (((macro != null) ? macro.MergedMacroConfigurations : null) != null)
			{
				using (IEnumerator<string> enumerator = macro.MergedMacroConfigurations.SelectMany((MergedMacroConfiguration macro_) => macro_.MacrosToRun).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string dependentMacro = enumerator.Current;
						MacroGraph.mInstance.AddParentChild(macro, (from MacroRecording macro_ in MacroGraph.mInstance.Vertices
						where string.Equals(macro_.Name, dependentMacro, StringComparison.InvariantCultureIgnoreCase)
						select macro_).FirstOrDefault<MacroRecording>());
					}
				}
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002FC94 File Offset: 0x0002DE94
		public static bool CheckIfDependentMacrosAreAvailable(MacroRecording macro)
		{
			if (macro2 == null)
			{
				return false;
			}
			if (macro2.RecordingType == RecordingTypes.SingleRecording)
			{
				return true;
			}
			if (macro2.MergedMacroConfigurations.SelectMany((MergedMacroConfiguration macro) => macro.MacrosToRun).Distinct<string>().Count<string>() != macro2.Childs.Count)
			{
				return false;
			}
			return macro2.Childs.Cast<MacroRecording>().All((MacroRecording childMacro) => MacroGraph.CheckIfDependentMacrosAreAvailable(childMacro));
		}

		// Token: 0x04000639 RID: 1593
		private static BiDirectionalGraph<MacroRecording> mInstance = null;

		// Token: 0x0400063A RID: 1594
		private static readonly object lockObj = new object();
	}
}
