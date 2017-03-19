using GuiLabs.Editor.Blocks;
using System.Collections.Generic;
using GuiLabs.Utils;
using GuiLabs.Editor.UI.Commands;
using System.Windows.Forms;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("do")]
	public class DoWhileBlock : VContainerBlock, IControlStructure, ICSharpBlock
	{
		#region ctor

		public DoWhileBlock()
			: base()
		{
			DoPart = new DoBlock();
			WhilePart = new HContainerBlock();
			WhileKeyword = new KeywordLabel("while");
			Condition = new ExpressionBlock();
			Condition.MyControl.KeyDown += delegate(object sender, KeyEventArgs e) 
			{
				if (e.KeyCode == Keys.Return)
				{
					this.OnKeyDown(sender, e);
					e.Handled = true;
				}
			};
			WhilePart.Add(WhileKeyword, Condition);
			Condition.Context = CompletionContext.BooleanExpression;
			Condition.MyControl.Box.Margins.Left = ShapeStyle.DefaultFontSize;
			Condition.MyControl.Box.SetMouseSensitivityToMargins();

			MyControl.Focusable = true;
			DoPart.MyControl.Focusable = false;
			Menu = DeleteCommand.CreateDeleteMenu(this);
			DoPart.Menu = null;
			this.CanMoveUpDown = true;
			this.Draggable = true;

			this.MyControl.Style = DoPart.MyControl.Style;
			this.MyControl.SelectedStyle = DoPart.MyControl.SelectedStyle;
			this.MyControl.ShouldDrawBackground = false;

			this.Add(DoPart, WhilePart);
		}

		#endregion

		public DoBlock DoPart { get; set; }
		public HContainerBlock WhilePart { get; set; }
		public KeywordLabel WhileKeyword { get; set; }
		public ExpressionBlock Condition { get; set; }

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			if (IsMoveUpDown(e))
			{
				e.Handled = true;
				return;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Space)
			{
				DoPart.MyUniversalControl.OnKeyDown(e);
				e.Handled = true;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Return && !e.Control)
			{
				this.AppendBlocks(new StatementLine());
				e.Handled = true;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Insert
				|| (e.KeyCode == System.Windows.Forms.Keys.Return && e.Control))
			{
				this.PrependBlocks(new StatementLine());
				e.Handled = true;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Right
				|| e.KeyCode == System.Windows.Forms.Keys.End)
			{
				nextFocusable = this.FindFirstFocusableChild();
				if (nextFocusable == null)
				{
					nextFocusable = this.FindNextFocusableBlockInChain();
				}
				nextFocusable.SetCursorToTheBeginning();
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

		#region Focus

		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			return DoPart.DefaultFocusableControl();
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Condition.Text = storage["condition"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["condition"] = Condition.Text;
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			StatementLine firstStatement = DoPart.VMembers.Children.Head as StatementLine;
			if (DoPart.VMembers.Children.Count == 1
				&& firstStatement != null && firstStatement.Text == "")
			{
				DoPart.VMembers.Children.Delete(firstStatement);
			}
			foreach (Block child in restoredChildren)
			{
				DoPart.VMembers.Children.Add(child);
			}
		}

		public override IEnumerable<Block> GetChildrenToSerialize()
		{
			return DoPart.GetChildrenToSerialize();
		}

		#endregion

		#region AcceptVisitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}

	public class DoBlock : ControlStructureBlock
	{
		#region ctor

		public DoBlock()
			: base("do")
		{
			this.Draggable = false;
		}

		#endregion
	}
}
