using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GuiLabs.Editor.Blocks
{
	/// <summary>
	/// A textblock with drop-down completion list,
	/// which is used to create new blocks.
	/// </summary>
	/// <remarks>
	/// After user selects something from the completion list,
	/// new block items are inserted after this EmptyBlock.
	/// </remarks>
	/// <example>
	/// To create a new empty block, which can create 
	/// for example, namespaces and classes,
	/// see EmptyNamespaceBlock.
	/// You just create a class which inherits from EmptyBlock
	/// and override the virtual FillItems() method.
	/// </example>
	public abstract class EmptyBlock : TextBoxBlockWithCompletion, ISeparatorBlock
	{
		#region ctor

		public EmptyBlock()
			: base()
		{
			this.ShouldRecordActions = false;
		}

		#endregion

		#region Completion

		protected override void OnCompletionVisibleChanged(bool isCompletionListNowVisible)
		{
			base.OnCompletionVisibleChanged(isCompletionListNowVisible);
			if (!isCompletionListNowVisible)
			{
				MyTextBox.Text = "";
			}
		}

		#endregion

		#region Control

		protected override void SubscribeControl()
		{
			base.SubscribeControl();
			MyTextBox.CharFilters.Add(FilterInvalidPrefixChars);
			MyTextBox.ShouldShowPopupMenu = false;
		}

		protected override void UnSubscribeControl()
		{
			base.UnSubscribeControl();
			MyTextBox.CharFilters.Remove(FilterInvalidPrefixChars);
		}

		private bool FilterInvalidPrefixChars(char c)
		{
			if (MyTextBox.Text.Length > 0)
			{
				return true;
			}
			return Completion.CanShowForPrefix(c.ToString());
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((e.KeyCode == System.Windows.Forms.Keys.Return
				|| e.KeyCode == System.Windows.Forms.Keys.Apps
				|| e.KeyCode == System.Windows.Forms.Keys.Tab)
				&& !e.Control
				&& !e.Shift
				&& !e.Alt)
			{
				Completion.ShowCompletionList(this);
				e.Handled = true;
			}

			if (e.KeyCode == Keys.Return 
				&& e.Control)
			{
				CtrlEnter();
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected void CtrlEnter()
		{
			if (!CanCtrlEnter())
			{
				return;
			}
			
			TextBoxBlock next = ParentParent.Next as TextBoxBlock;
			if (next == null)
			{
				return;
			}

			next.SetFocus();
		}

		protected bool CanCtrlEnter()
		{
			return this.Prev != null
				&& this.Next == null
				&& this.ParentParent != null
				&& this.ParentParent.Next is ISeparatorBlock;
		}

		//protected override void OnDoubleClick(GuiLabs.Canvas.Events.MouseWithKeysEventArgs MouseInfo)
		//{
		//    Completion.ShowCompletionList(this);
		//}

		//protected override void OnMouseDown(GuiLabs.Canvas.Events.MouseWithKeysEventArgs MouseInfo)
		//{
		//    base.OnMouseDown(MouseInfo);
		//    if (!Completion.Visible && MouseInfo.IsRightButtonPressed)
		//    {
		//        Completion.ShowCompletionList(this);
		//    }
		//}

		protected override void OnKeyDownBack(System.Windows.Forms.KeyEventArgs e)
		{
			if (this.MyTextBox.CaretIsAtBeginning)
			{
				Block prevBlock = this.FindPrevFocusableBlockInChain();
				if (prevBlock != null)
				{
					prevBlock.SetCursorToTheEnd();
					e.Handled = true;
				}
			}
			if (!e.Handled)
			{
				base.OnKeyDownBack(e);
			}
		}

		//protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		//{
		//    if (this.MyControl.IsFocused && !char.IsControl(e.KeyChar))
		//    {
		//        Completion.ShowCompletionList(this);
		//    }
		//}

		//protected override void OnTextHasChanged(ITextProvider changedControl, string oldText, string newText)
		//{
		//    base.OnTextHasChanged(changedControl, oldText, newText);
		//    if (Text.Length > 0)
		//    {
		//        Completion.ShowCompletionList(this, Text);
		//    }
		//    else
		//    {
		//        Completion.HideCompletionList();
		//    }
		//}

		protected override void OnMouseUpRight(GuiLabs.Canvas.Events.MouseWithKeysEventArgs e)
		{
			using (Redrawer r = new Redrawer(Root))
			{
				SetFocus();
				Completion.ShowCompletionList(this);
			}
			e.Handled = true;
		}

		#endregion

		#region Clipboard

		public override void Cut()
		{
			
		}

		public override void Copy()
		{
			
		}

		public override void Paste()
		{
			PasteCore();
		}

		public override void PasteBlocks(IEnumerable<Block> blocksToPaste)
		{
			List<Block> toPaste = new List<Block>(blocksToPaste);
			LinearContainerBlock l = this.Parent as LinearContainerBlock;
			if (l != null && l.HasSeparators)
			{
				toPaste.Add(l.CreateSeparator());
			}
			base.PasteBlocks(toPaste);
		}

		#endregion

		#region Memento

		public override Memento CreateSnapshot()
		{
			return null;
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "EmptyBlock";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
				if (MyTextBox.CaretIsAtBeginning && FindPrevFocusableBlockInChain() != null)
				{
					yield return "Press [Backspace] to move to the previous block.";
				}
				if (CanCtrlEnter())
				{
					yield return HelpBase.PressCtrlEnterToJumpOut;
				}
				if (!Completion.Visible)
				{
					foreach (string pageDownUp in HelpBase.PressPageDownAndOrUp(this))
					{
						yield return pageDownUp;
					}
				}
			}
		}
		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}
		protected override IEnumerable<string> GetCompletionHelp()
		{
			if (Completion.Visible)
			{
				yield return HelpBase.CommitCompletion;
				yield return HelpBase.CancelCompletion;
			}
			else
			{
				yield return HelpBase.StartTypingForCompletion;
				yield return HelpBase.PopupCompletion;
			}
		}

		#endregion
	}
}
