using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.Blocks
{
	public class TemporaryCompletionBlock : TextBoxBlockWithCompletion
	{
		#region ctor

		public TemporaryCompletionBlock(CompletionFunctionality completion)
			: base(completion)
		{
			this.MyTextBox.MinWidth = 1;
			this.ShouldRecordActions = false;
			this.Completion.VisibleChanged += Completion_VisibleChanged;
		}

		#endregion

		#region Completion

		void Completion_VisibleChanged(bool isCompletionListNowVisible)
		{
			// this is very important:
			// we only react when we are the block currently showing completion
			if (Completion.HostBlock != this)
			{
				return;
			}

			// if the completion list has just disappeared for whatever reason
			// AND we're not in a transaction (we're not deleting ourselves already)
			if (!isCompletionListNowVisible && !this.ActionManager.ActionIsExecuting)
			{
				// then just delete us
				this.RemoveFocus(MoveFocusDirection.SelectNextInChain);
				this.Parent.Children.Delete(GetBlocksToDelete());
			}
		}

		#endregion
	}
}
