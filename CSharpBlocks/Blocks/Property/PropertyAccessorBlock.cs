using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;
using System.Collections.Generic;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	public class PropertyAccessorBlock : MethodOrPropertyAccessor, ICSharpBlock
	{
		#region ctor

		public PropertyAccessorBlock(string label)
			: base()
		{
			Init(label);
			this.Draggable = false;
			this.VMembers.Children.Add(new StatementLine());
		}

		public PropertyAccessorBlock(string label, BlockStatementBlock statementBlock)
			: base()
		{
			Init(label);
			this.VMembers = statementBlock;
		}

		private void Init(string label)
		{
			this.CanMoveUpDown = false;
			Keyword = new KeywordLabel(label);
			Keyword.MyControl.Box.Margins.SetAll(2);
			this.HMembers.Children.Add(Keyword);
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				using (Redrawer r = new Redrawer(this.Root))
				{
					ToggleOrientation();
				}
				return;
			}
			base.OnKeyDown(sender, e);
		}

		#endregion

		#region Orientation

		private bool isVertical = true;
		private int indent = 0;
		protected void ToggleOrientation()
		{
			if (isVertical)
			{
				this.MyUniversalControl.HCompartment.Remove(this.MyUniversalControl.MyCollapseButton);
				this.MyUniversalControl.Add(this.MyUniversalControl.MyCollapseButton);
				this.MyUniversalControl.LinearLayoutStrategy.Orientation = GuiLabs.Canvas.Controls.OrientationType.Horizontal;
				indent = this.MyUniversalControl.VMembers.Box.Margins.Left;
				this.MyUniversalControl.VMembers.Box.Margins.Left = ShapeStyle.DefaultFontSize;
			}
			else
			{
				this.MyUniversalControl.Remove(this.MyUniversalControl.MyCollapseButton);
				this.MyUniversalControl.HCompartment.Add(this.MyUniversalControl.MyCollapseButton);
				this.MyUniversalControl.LinearLayoutStrategy.Orientation = GuiLabs.Canvas.Controls.OrientationType.Vertical;
				this.MyUniversalControl.VMembers.Box.Margins.Left = indent;
			}
			this.MyUniversalControl.Layout();
			isVertical = !isVertical;
			this.DisplayContextHelp();
		}

		#endregion

		#region Keyword

		private LabelBlock mKeyword;
		public LabelBlock Keyword
		{
			get
			{
				return mKeyword;
			}
			set
			{
				mKeyword = value;
			}
		}

		#endregion

		#region Parent

		public PropertyBlock ParentProperty
		{
			get
			{
				return ParentParent as PropertyBlock;
			}
		}

		public override ClassOrStructBlock ParentClassOrStruct
		{
			get
			{
				return this.ParentProperty.ParentClassOrStruct;
			}
		}

		#endregion

		#region Memento

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			StatementLine firstStatement = this.VMembers.Children.Head as StatementLine;
			if (this.VMembers.Children.Count == 1
				&& firstStatement != null)
			{
				this.VMembers.Children.Delete(firstStatement);
			}
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child);
			}
		}

		#endregion

		#region Delete

		public override void Delete()
		{
			PropertyBlock parent = this.ParentProperty;
			if (parent != null)
			{
				if (this is PropertyGetBlock)
				{
					parent.GetAccessor = null;
				}
				else if (this is PropertySetBlock)
				{
					parent.SetAccessor = null;
				}
			}
		}

		public override System.Collections.Generic.IEnumerable<Block> GetBlocksToDelete()
		{
			yield return this;
		}

		#endregion
	
		#region Style

		protected override string StyleName()
		{
			return "PropertyAccessorBlock";
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
