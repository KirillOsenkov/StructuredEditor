using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class MethodOrPropertyAccessor : CodeBlock
	{
		#region ctors
		
		public MethodOrPropertyAccessor()
			: base()
		{
			Init();
		}
		
		public MethodOrPropertyAccessor(VContainerBlock vMembers)
			: base(vMembers)
		{
			Init();
		}

		private void Init()
		{
			this.MyUniversalControl.OpenCurlyHasNegativeLowerMargin = false;
		}
				
		#endregion

		#region VMembers

		public override VContainerBlock VMembers
		{
			get
			{
				return base.VMembers;
			}
			set
			{
				base.VMembers = value;
				VMembers.AddAcceptableBlockTypes<IMethodLevel>();
			}
		}

		#endregion
		
		#region API

		public ClassBlock ParentClass
		{
			get
			{
				return this.ParentParent as ClassBlock;
			}
		}

		public StructBlock ParentStruct
		{
			get
			{
				return this.ParentParent as StructBlock;
			}
		}

		public virtual ClassOrStructBlock ParentClassOrStruct
		{
			get
			{
				return this.ParentParent as ClassOrStructBlock;
			}
		}

//		/// <summary>
//		/// Fully qualified path for this method, 
//		/// including parent classes and all namespaces,
//		/// but not including the method name itself.
//		/// </summary>
//		/// <returns>All namespaces and classes, 
//		/// where this method is declared, separated by a dot.</returns>
//		/// <example>"GuiLabs.Editor.CSharp.MethodBlock"</example>
//		public string GetFullPath()
//		{
//			System.Text.StringBuilder s = new System.Text.StringBuilder();
//			ContainerBlock parent = this.Parent;
//			while (parent != null && parent != parent.Parent)
//			{
//				if (parent is ClassOrStructBlock)
//				{
//					s.Insert(0, (parent as ClassOrStructBlock).Name +
//						((s.Length == 0) ? "" : "."));
//				}
//				if (parent is NamespaceBlock)
//				{
//					s.Insert(0, (parent as NamespaceBlock).Name +
//						((s.Length == 0) ? "" : "."));
//				}
//				parent = parent.Parent;
//			}
//			return s.ToString();
//		}

		#endregion
	}
}
