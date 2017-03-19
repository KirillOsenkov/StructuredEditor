using GuiLabs.Editor.Blocks;
using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	public class ControlStructureBlock : 
		CodeBlock, 
		IControlStructure
	{
		#region ctor

		public ControlStructureBlock(string name)
			: base()
		{
			Keyword = new KeywordLabel(name);
            MyUniversalControl.Box.Margins.Left = -1;
            MyUniversalControl.Box.Margins.Top = 1;
			this.MyUniversalControl.OpenCurlyHasNegativeLowerMargin = false;
			this.HMembers.Add(Keyword);
			this.VMembers.Add(new StatementLine());
		}

		#endregion

		public KeywordLabel Keyword { get; set; }

		#region VMembers

		protected override VContainerBlock CreateVMembers()
		{
			return new BlockStatementBlock();
		}

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

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Return && !e.Control)
			{
				this.PrependBlocks(new StatementLine());
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Insert
				|| (e.KeyCode == System.Windows.Forms.Keys.Return && e.Control))
			{
				this.AppendBlocks(new StatementLine());
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion

		#region Delete

		public override void Delete()
		{
			if (this.Next == null && this.Prev == null)
			{
				this.Replace(new StatementLine());
			}
			else
			{
				base.Delete();
			}
		}

		#endregion

		#region Memento

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			StatementLine firstStatement = this.VMembers.Children.Head as StatementLine;
			if (this.VMembers.Children.Count == 1
				&& firstStatement != null && firstStatement.Text == "")
			{
				this.VMembers.Children.Delete(firstStatement);
			}
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child);
			}
		}

		#endregion

		#region SetDefaultFocus

		public override void SetDefaultFocus()
		{
			Block toFocus = this.HMembers.FindFirstFocusableBlock();
			if (toFocus != null)
			{
				toFocus.SetFocus();
			}
			else
			{
				toFocus = this.VMembers.FindFirstFocusableBlock();
				if (toFocus != null)
				{
					toFocus.SetFocus();
				}
				else
				{
					base.SetDefaultFocus();
				}
			}
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "ControlStructureBlock";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			Help.PressEnterToInsertBefore,
			Help.PressCtrlEnterToInsertAfter
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				yield return string.Format(Help.PressTabOrRightArrowToEditName, Keyword.Text);

				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
			}
		}
		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}

		#endregion
	}
}
