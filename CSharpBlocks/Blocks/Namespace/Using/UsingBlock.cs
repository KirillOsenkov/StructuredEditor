using System.Collections;
using System.Collections.Generic;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("using")]
	public class UsingBlock : 
		ListBlock, 
		ICSharpBlock, 
		IEnumerable<UsingDirective>,
		INamespaceLevel
	{
		#region ctor

		public UsingBlock()
		{
            keyword.MyControl.Box.Margins.Top = 3;
            this.MyUniversalControl.Box.Margins.Left = -1;
            this.MyUniversalControl.Box.Margins.Top = -4;
            MyUniversalControl.HideCurlies = true;
			this.HMembers.Add(keyword);
			Add("");
			this.Draggable = false;
			this.CanPrependBlocks = false;
			this.CanMoveUpDown = false;
            MyUniversalControl.Layout();
        }

		#endregion

		#region API

		#region Add

		public void Add(UsingDirective usingEntry)
		{
			if (this.VMembers.Children.Count == 1
				&& FirstUsing != null
				&& FirstUsing.Text == "")
			{
				if (this.Root != null)
				{
					FirstUsing.Replace(usingEntry);
				}
				else
				{
					this.VMembers.Children.Replace(FirstUsing, usingEntry);
				}
			}
			else
			{
				this.VMembers.Add(usingEntry);
			}
		}

		public new void Add(string usingText)
		{
			Add(new UsingDirective(usingText));
		}

		#endregion

		#region Exists

		public bool Exists(string usingDirective)
		{
			foreach (UsingDirective ud in this.VMembers.Children)
			{
				if (ud.Text == usingDirective)
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		public UsingDirective FirstUsing
		{
			get
			{
				return this.VMembers.Children.Head as UsingDirective;
			}
		}

		public IEnumerator<UsingDirective> GetEnumerator()
		{
			foreach (UsingDirective u in this.VMembers.FindChildren<UsingDirective>())
			{
				yield return u;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.End
				&& this.FirstUsing != null)
			{
				this.FirstUsing.SetFocus();
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
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
				this.VMembers.AddAcceptableBlockTypes<UsingDirective>();
			}
		}

		#endregion
		
		#region Label

		private KeywordLabel keyword = new KeywordLabel("using");

		#endregion

		#region Memento

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			if (this.VMembers.Children.Count == 1 
				&& FirstUsing != null
				&& string.IsNullOrEmpty(FirstUsing.Text))
			{
				this.VMembers.Children.Delete(FirstUsing);
			}
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child);
			}
		}

		#endregion

		#region DefaultFocusableControl

		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			if (FirstUsing != null)
			{
				return FirstUsing.MyControl;
			}
			else
			{
				return this.MyControl;
			}
		}

		#endregion

		#region GetBlocksToDelete

		public override IEnumerable<Block> GetBlocksToDelete()
		{
			yield return this;
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "UsingBlock";
		}

		#endregion

		#region AcceptVisitor

		public override void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
