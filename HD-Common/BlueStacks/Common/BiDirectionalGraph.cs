using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlueStacks.Common
{
	// Token: 0x020000BA RID: 186
	public class BiDirectionalGraph<T>
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00004A4F File Offset: 0x00002C4F
		public ObservableCollection<BiDirectionalVertex<T>> Vertices { get; }

		// Token: 0x0600049F RID: 1183 RVA: 0x00004A57 File Offset: 0x00002C57
		public BiDirectionalGraph(ObservableCollection<BiDirectionalVertex<T>> initialNodes = null)
		{
			this.Vertices = (initialNodes ?? new ObservableCollection<BiDirectionalVertex<T>>());
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00004A6F File Offset: 0x00002C6F
		public void AddVertex(BiDirectionalVertex<T> vertex)
		{
			if (vertex != null && !this.Vertices.Contains(vertex))
			{
				this.Vertices.Add(vertex);
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00004A8E File Offset: 0x00002C8E
		public void AddParentChild(BiDirectionalVertex<T> parent, BiDirectionalVertex<T> child)
		{
			if (parent != null && child != null)
			{
				this.AddVertex(parent);
				this.AddVertex(child);
				this.AddParentChildRelation(parent, child);
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00004AAC File Offset: 0x00002CAC
		public void RemoveVertex(BiDirectionalVertex<T> vertex)
		{
			if (vertex != null && this.Vertices.Contains(vertex))
			{
				this.DeLinkMacro(vertex);
				this.Vertices.Remove(vertex);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00018044 File Offset: 0x00016244
		public void DeLinkMacroChild(BiDirectionalVertex<T> recording)
		{
			if (recording != null)
			{
				foreach (BiDirectionalVertex<T> biDirectionalVertex in recording.Childs)
				{
					biDirectionalVertex.RemoveParent(recording);
				}
				recording.Childs.Clear();
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000180A4 File Offset: 0x000162A4
		public void DeLinkMacroParent(BiDirectionalVertex<T> recording)
		{
			if (recording != null)
			{
				foreach (BiDirectionalVertex<T> biDirectionalVertex in recording.Parents)
				{
					biDirectionalVertex.RemoveChild(recording);
				}
				recording.Parents.Clear();
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00004AD3 File Offset: 0x00002CD3
		public void DeLinkMacro(BiDirectionalVertex<T> recording)
		{
			this.DeLinkMacroChild(recording);
			this.DeLinkMacroParent(recording);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00004AE3 File Offset: 0x00002CE3
		private void AddParentChildRelation(BiDirectionalVertex<T> parent, BiDirectionalVertex<T> child)
		{
			if (parent != null && child != null)
			{
				if (!child.Parents.Contains(parent))
				{
					child.AddParent(parent);
				}
				if (!parent.Childs.Contains(child))
				{
					parent.AddChild(child);
				}
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00018104 File Offset: 0x00016304
		private bool ChildExist(BiDirectionalVertex<T> root, BiDirectionalVertex<T> searchVertex)
		{
			if (!root.IsVisited)
			{
				root.IsVisited = true;
				return root.Equals(searchVertex) || root.Childs.Any((BiDirectionalVertex<T> child) => this.ChildExist(child, searchVertex));
			}
			return false;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00004B15 File Offset: 0x00002D15
		public bool DoesParentExist(BiDirectionalVertex<T> root, BiDirectionalVertex<T> searchVertex)
		{
			if (root == null)
			{
				return false;
			}
			this.UnVisitAllVertices();
			return this.ParentExist(root, searchVertex);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00018160 File Offset: 0x00016360
		private bool ParentExist(BiDirectionalVertex<T> root, BiDirectionalVertex<T> searchVertex)
		{
			if (!root.IsVisited)
			{
				root.IsVisited = true;
				return root.Equals(searchVertex) || root.Parents.Any((BiDirectionalVertex<T> parent) => this.ParentExist(parent, searchVertex));
			}
			return false;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000181BC File Offset: 0x000163BC
		private void UnVisitAllVertices()
		{
			foreach (BiDirectionalVertex<T> biDirectionalVertex in this.Vertices)
			{
				biDirectionalVertex.IsVisited = false;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00018208 File Offset: 0x00016408
		public List<BiDirectionalVertex<T>> GetAllChilds(BiDirectionalVertex<T> vertex)
		{
			BiDirectionalGraph<T>.<>c__DisplayClass15_0 CS$<>8__locals1;
			CS$<>8__locals1.dependents = new List<BiDirectionalVertex<T>>();
			if (vertex != null)
			{
				BiDirectionalGraph<T>.<GetAllChilds>g__GetChilds|15_0(vertex, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.dependents;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00018234 File Offset: 0x00016434
		public List<MacroRecording> GetAllLeaves(MacroRecording macro)
		{
			BiDirectionalGraph<T>.<>c__DisplayClass16_0 CS$<>8__locals1;
			CS$<>8__locals1.leaves = new List<MacroRecording>();
			if (macro != null)
			{
				BiDirectionalGraph<T>.<GetAllLeaves>g__IterateForLeaves|16_0(macro, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.leaves;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00018260 File Offset: 0x00016460
		[CompilerGenerated]
		internal static void <GetAllChilds>g__GetChilds|15_0(BiDirectionalVertex<T> node, ref BiDirectionalGraph<T>.<>c__DisplayClass15_0 A_1)
		{
			foreach (BiDirectionalVertex<T> biDirectionalVertex in node.Childs)
			{
				if (!A_1.dependents.Contains(biDirectionalVertex))
				{
					A_1.dependents.Add(biDirectionalVertex);
					if (biDirectionalVertex.Childs.Count > 0)
					{
						BiDirectionalGraph<T>.<GetAllChilds>g__GetChilds|15_0(biDirectionalVertex, ref A_1);
					}
				}
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000182DC File Offset: 0x000164DC
		[CompilerGenerated]
		internal static void <GetAllLeaves>g__IterateForLeaves|16_0(MacroRecording recording, ref BiDirectionalGraph<T>.<>c__DisplayClass16_0 A_1)
		{
			foreach (BiDirectionalVertex<MacroRecording> biDirectionalVertex in recording.Childs)
			{
				MacroRecording macroRecording = (MacroRecording)biDirectionalVertex;
				if (macroRecording.Childs.Count == 0 && !A_1.leaves.Contains(macroRecording))
				{
					A_1.leaves.Add(macroRecording);
				}
				else
				{
					BiDirectionalGraph<T>.<GetAllLeaves>g__IterateForLeaves|16_0(macroRecording, ref A_1);
				}
			}
		}
	}
}
