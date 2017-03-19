using System.Collections.Generic;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public static class ClassNavigator
	{
		#region Navigation

		/// <summary>
		/// Fully qualified path for this method, 
		/// including parent classes and all namespaces,
		/// but not including the method name itself.
		/// </summary>
		/// <returns>All namespaces and classes, 
		/// where this method is declared, separated by a dot.</returns>
		/// <example>"GuiLabs.Editor.CSharp.MethodBlock"</example>
		public static string GetFullPath(Block block)
		{
			System.Text.StringBuilder s = new System.Text.StringBuilder();
			ContainerBlock parent = block.Parent;
			while (parent != null && parent != parent.Parent)
			{
				if (parent is ClassOrStructBlock)
				{
					s.Insert(0, (parent as ClassOrStructBlock).Name +
						((s.Length == 0) ? "" : "."));
				}
				if (parent is NamespaceBlock)
				{
					s.Insert(0, (parent as NamespaceBlock).Name +
						((s.Length == 0) ? "" : "."));
				}
				parent = parent.Parent;
			}
			return s.ToString();
		}

		public static MethodBlock FindMain(CodeUnitBlock root)
		{
			IEnumerable<MethodBlock> results = FindMethods(root, "Main");
			IEnumerator<MethodBlock> enumerator = results.GetEnumerator();
			if (enumerator != null && enumerator.MoveNext())
			{
				return enumerator.Current;
			}
			return null;
		}

		public static IEnumerable<MethodBlock> FindMethods(
			CodeUnitBlock root,
			string methodName)
		{
			foreach (MethodBlock method in root.FindChildrenRecursive<MethodBlock>())
			{
				if (method.Name == methodName)
				{
					yield return method;
				}
			}
		}

		#endregion

		#region FindContainer

		public static ClassOrStructBlock FindContainingClassOrStruct(Block block)
		{
			return FindFirstContainingBlock<ClassOrStructBlock>(block);
		}

		public static IEnumerable<ClassOrStructBlock> FindContainingClassOrStructs(Block block)
		{
			return FindAllContainingBlocks<ClassOrStructBlock>(block);
		}

		public static MethodOrPropertyAccessor FindContainingMethodOrPropertyAccessor(Block block)
		{
			return FindFirstContainingBlock<MethodOrPropertyAccessor>(block);
		}

		public static MethodBlock FindContainingMethod(Block block)
		{
			return FindFirstContainingBlock<MethodBlock>(block);
		}
		
//		public static CodeBlock FindContainingMember(Block block)
//		{
//			MethodBlock method = FindContainingMethod(block);
//			if (method != null)
//			{
//				return method;
//			}
//			
//			PropertyAccessorBlock accessor = FindContainingPropertyAccessor(block);
//			if (accessor != null)
//			{
//				return accessor.ParentProperty;
//			}
//			
//			return null;
//		}

		public static ContainerBlock FindContainingControlStructure(Block current)
		{
			ContainerBlock result = FindFirstContainingBlock<ControlStructureBlock>(current);
			if (result == null)
			{
				return null;
			}
			if (result is DoBlock)
			{
				result = result.Parent as ContainerBlock;
			}
			return result;
		}

		public static PropertyAccessorBlock FindContainingPropertyAccessor(Block current)
		{
			return FindFirstContainingBlock<PropertyAccessorBlock>(current);
		}

		public static ContainerBlock FindContainingMember(Block block)
		{
			ContainerBlock current = block.Parent;
			while (current != null
				&& !(current is MethodBlock)
				&& !(current is PropertyBlock))
			{
				current = current.Parent;
			}
			return current;
		}

		public static T FindFirstContainingBlock<T>(Block block)
			where T : class
		{
			ContainerBlock current = block.Parent;
			while (current != null && !(current is T))
			{
				current = current.Parent;
			}
			return current as T;
		}

		public static IEnumerable<T> FindAllContainingBlocks<T>(Block block)
			where T : class
		{
			return block.FindAllContainingBlocks<T>();
		}

		#endregion

		#region Variables

		public static Variable FindAccessibleVariable(string name, Block point)
		{
			foreach (Variable v in FindAccessibleVariables(point))
			{
				if (v.Name == name)
				{
					return v;
				}
			}
			return null;
		}

		public static List<Variable> FindAccessibleVariables(Block point)
		{
			List<Variable> foundVariables = new List<Variable>();
			if (point.Prev != null)
			{
				point = point.Prev;
			}
			else
			{
				point = point.Parent;
			}
			AddAccessibleVariablesStartingFrom(foundVariables, point);
			return foundVariables;
		}

		private static void AddAccessibleVariablesStartingFrom(
			IList<Variable> vars,
			Block point)
		{
			while (!(point == null
				   || point is MethodBlock
				   || point is PropertyAccessorBlock))
			{
				while (point != null)
				{
					AddAccessibleVariableDeclaration(vars, point);
					// jump out when we reached the first statement
					if (point.Prev == null)
					{
						break;
					}
					point = point.Prev;
				}
				// jump to the parent statement and continue search from there
				point = point.Parent;
			}
		}

		private static void AddAccessibleVariableDeclaration(
			IList<Variable> vars,
			Block node)
		{
			StatementLine varDecl = node as StatementLine;
			if (varDecl != null && varDecl.LocalVariableDeclaration != null)
			{
				foreach (Variable var in varDecl.LocalVariableDeclaration)
				{
					vars.Add(var);
				}
			}
			
			ForeachBlock fe = node as ForeachBlock;
			if (fe != null)
			{
				vars.Add(fe.IterationVariable);
			}
			
			ForBlock fb = node as ForBlock;
			if (fb != null)
			{
				AddAccessibleVariableDeclaration(vars, fb.ForInitializer);
			}
		}

		#endregion

		#region Parameters

		public static Parameter FindParameter(string name, Block point)
		{
			ParameterList list = FindParameters(point);
			if (list != null)
			{
				return list[name];
			}
			return null;
		}

		public static ParameterList FindParameters(Block point)
		{
			MethodBlock method = FindContainingMethod(point);
			if (method != null)
			{
				return method.Parameters.Parameters;
			}

			PropertySetBlock propAccessor = FindContainingPropertyAccessor(point) as PropertySetBlock;
			if (propAccessor != null)
			{
				return propAccessor.Parameters;
			}

			return null;
		}

		#endregion
	}
}
