using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.Blocks
{
	public class LineBlock : HContainerBlock
	{
		public LineBlock() : base()
		{
			this.MyListControl.RedirectHitTestToNearestChild = true;
		}

		#region OnEvents

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			base.OnKeyPress(sender, e);
			if (char.IsLetterOrDigit(e.KeyChar) && !e.Handled)
			{
				Block b = this.FindFirstFocusableChild();
				if (b != null && b.MyControl != null)
				{
					using (Redrawer r = new Redrawer(this.Root))
					{
						b.SetCursorToTheEnd();
						b.MyControl.OnKeyPress(e);
					}
					e.Handled = true;
				}
			}
		}

		protected override void Children_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			if (e.KeyCode == System.Windows.Forms.Keys.Home)
			{
				nextFocusable = this.FindFirstFocusableBlock();
				if (nextFocusable != null)
				{
					nextFocusable.SetCursorToTheBeginning();
					e.Handled = true;
				}
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.End)
			{
				nextFocusable = this.FindLastFocusableChild();
				if (nextFocusable != null)
				{
					nextFocusable.SetCursorToTheEnd();
					e.Handled = true;
				}
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				Block next = this.Next;
				if (next != null)
				{
					nextFocusable = next.FindFirstFocusableBlock();
					if (nextFocusable == null)
					{
						nextFocusable = nextFocusable.FindNextFocusableBlock();
					}
					if (nextFocusable != null)
					{
						nextFocusable.SetFocus();
						e.Handled = true;
					}
				}
			}

			if (!e.Handled)
			{
				base.Children_KeyDown(block, e);
			}
		}

		#endregion
	}
}
