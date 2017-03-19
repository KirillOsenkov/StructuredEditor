using System.Collections.Generic;
using Node = GuiLabs.Utils.Collections.TreeNode<string>;
using System.Reflection;
using System;
using GuiLabs.Utils;
using System.Linq;

namespace GuiLabs.Editor.CSharp
{
	public class ReflectionNamespaceManager
	{
		#region ctor

		public ReflectionNamespaceManager()
		{
			// 95 ms on my machine
			AddAllAssembliesFromCurrentAppDomain();
		}

		#endregion

		#region Filling data structure

		public void AddNamespacesFromAssembly(Assembly a)
		{
			foreach (Type t in a.GetExportedTypes())
			{
				AddNamespace(t.Namespace);
			}
		}

		public void AddAllAssembliesFromCurrentAppDomain()
		{
			foreach (Assembly a in Reflector.GetLoadedAssemblies())
			{
				AddNamespacesFromAssembly(a);
                AddAttributesFromAssembly(a);
			}
		}

        public void AddAttributesFromAssembly(Assembly a)
        {
            foreach (var attributeType in a.GetExportedTypes()
                .Where(t => typeof(Attribute).IsAssignableFrom(t)
                && !t.IsAbstract))
            {
                AttributeTypes.Add(attributeType);
            }
        }

        public List<Type> AttributeTypes = new List<Type>();

		#endregion

		#region Data structure

		Node Root = new Node();

		public void AddNamespace(string ns)
		{
			string[] parts = ns.Split('.');
			AddNamespace(parts, 0, Root);
		}

		private void AddNamespace(string[] parts, int index, Node node)
		{
			if (parts == null || index >= parts.Length)
			{
				return;
			}

			string head = parts[index];

			Node current = node.FindChild(head);
			if (current == null)
			{
				current = node.AddChild(head);
			}

			index++;

			AddNamespace(parts, index, current);
		}

		public IEnumerable<string> GetNamespaces(string ns)
		{
			string[] parts = ns.Split('.');
			Node found;

			if (string.IsNullOrEmpty(ns))
			{
				found = Root;
			}
			else
			{
				found = FindNamespace(parts, 0, Root);
			}
			if (found != null)
			{
				return found.GetValues();
			}
			return Strings.EmptyArray;
		}

		private Node FindNamespace(string[] parts, int index, Node node)
		{
			while (node != null)
			{
				if (index >= parts.Length)
				{
					return node;
				}
				node = node.FindChild(parts[index++]);
			}

			return null;
		}

		#endregion
	}
}
