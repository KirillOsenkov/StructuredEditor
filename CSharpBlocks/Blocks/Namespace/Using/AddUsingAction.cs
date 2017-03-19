using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp.Actions
{
	public class AddUsingAction : RootBlockAction
	{
		#region ctors

		public AddUsingAction(CodeUnitBlock root)
			: base(root)
		{
			CodeUnit = root;
		}
		
		public AddUsingAction(CodeUnitBlock root, IEnumerable<UsingDirective> usings)
			: base(root)
		{
			CodeUnit = root;
			PrepareBlocks(usings);
		}
		
		public AddUsingAction(CodeUnitBlock root, params UsingDirective[] usings)
			: base(root)
		{
			CodeUnit = root;
			PrepareBlocks((IEnumerable<UsingDirective>)usings);
		}

		public AddUsingAction(CodeUnitBlock root, IEnumerable<string> usings)
			: base(root)
		{
			CodeUnit = root;
			PrepareBlocks(usings);
		}
		
		public AddUsingAction(CodeUnitBlock root, params string[] usings)
			: base(root)
		{
			CodeUnit = root;
			PrepareBlocks((IEnumerable<string>)usings);
		}

		#endregion
		
		CodeUnitBlock CodeUnit;

		#region blocks to add

		private LinkedList<UsingDirective> list = new LinkedList<UsingDirective>();

		public void PrepareBlocks(params UsingDirective[] blocks)
		{
			PrepareBlocks((IEnumerable<UsingDirective>) blocks);
		}

		public void PrepareBlocks(IEnumerable<UsingDirective> blocks)
		{
			foreach (UsingDirective block in blocks)
			{
				list.AddLast(block);
			}
		}

		public void PrepareBlocks(params string[] blocks)
		{
			PrepareBlocks((IEnumerable<string>) blocks);
		}

		public void PrepareBlocks(IEnumerable<string> blocks)
		{
			foreach (string block in blocks)
			{
				list.AddLast(new UsingDirective(block));
			}
		}

		#endregion

		#region Execute

		protected override void ExecuteCore()
		{
			CodeUnit.EnsureUsingBlockExists();
			UsingBlock usingSection = CodeUnit.UsingSection;
			foreach(UsingDirective dir in list)
			{
				Add(usingSection, dir);
			}
		}

		private void Add(UsingBlock usingSection, UsingDirective usingEntry)
		{
			if (usingSection.VMembers.Children.Count == 1
				&& usingSection.FirstUsing != null
				&& usingSection.FirstUsing.Text == "")
			{
				usingSection.VMembers.Children.Replace(usingSection.FirstUsing, usingEntry);
			}
			else
			{
				usingSection.VMembers.Children.Add(usingEntry);
			}
		}

		#endregion

		#region UnExecute

		protected override void UnExecuteCore()
		{
			RootControl root = Root.MyRootControl;
			UsingBlock usingSection = CodeUnit.UsingSection;

			foreach (UsingDirective b in this.list)
			{
				if (root.IsFocusInsideControl(b.MyControl))
				{
					b.RemoveFocus();
				}
				usingSection.VMembers.Children.Delete(b);
			}
			
			if (usingSection.VMembers.Children.Count == 0)
			{
				if (root.IsFocusInsideControl(usingSection.MyControl))
				{
					usingSection.RemoveFocus();
				}
				CodeUnit.Children.Delete((Block)usingSection);
			}
		}

		#endregion
	}
}
