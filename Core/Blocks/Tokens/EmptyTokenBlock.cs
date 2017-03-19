using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.Blocks
{
	public class EmptyTokenBlock : TokenCompletionBlock
	{
		public EmptyTokenBlock(CompletionFunctionality completion)
			: base(completion)
		{
			this.ShouldRecordActions = false;
			this.Completion.VisibleChanged += Completion_VisibleChanged;
		}

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
			if (!isCompletionListNowVisible && !this.ActionManager.InATransaction)
			{
				// then just delete us
				this.RemoveFocus(MoveFocusDirection.SelectPrev, false);
				foreach (Block b in GetBlocksToDelete())
				{
					this.Parent.Children.Delete(b);
				}
				// this.Delete();
			}
		}
	}
}
