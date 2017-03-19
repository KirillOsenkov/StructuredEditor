using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;
using GuiLabs.Undo;

namespace GuiLabs.Editor.Actions
{
	public abstract class RootBlockAction : AbstractAction
	{
		protected RootBlockAction(RootBlock root)
		{
			Param.CheckNotNull(root, "root");
			Root = root;
		}

		public RootBlock Root { get; set; }

		private bool mShouldRedrawWhenDone = true;
		public bool ShouldRedrawWhenDone
		{
			get
			{
				return mShouldRedrawWhenDone;
			}
			set
			{
				mShouldRedrawWhenDone = value;
			}
		}

		#region Focus

		private Block mBlockToFocus;
		public Block BlockToFocus
		{
			get
			{
				return mBlockToFocus;
			}
			set
			{
				mBlockToFocus = value;
			}
		}

		private Block mBlockToFocusAfterUndo;
		public Block BlockToFocusAfterUndo
		{
			get
			{
				return mBlockToFocusAfterUndo;
			}
			set
			{
				mBlockToFocusAfterUndo = value;
			}
		}

		public bool BlockToFocusIsValid
		{
			get
			{
				return BlockToFocus != null
					&& BlockToFocus.Root != null
					&& BlockToFocus.Root == this.Root;
			}
		}

		public bool BlockToFocusAfterUndoIsValid
		{
			get
			{
				return BlockToFocusAfterUndo != null
					&& BlockToFocusAfterUndo.Root != null
					&& BlockToFocusAfterUndo.Root == this.Root;
			}
		}

		#endregion

		public override sealed void Execute()
		{
			if (!CanExecute())
			{
				return;
			}
			using (new Redrawer(Root, ShouldRedrawWhenDone))
			{
				ExecuteCore();
				ExecuteCount++;
				if (BlockToFocusIsValid)
				{
					BlockToFocus.SetDefaultFocus();
				}
			}
		}

		public override sealed void UnExecute()
		{
			if (!CanUnExecute())
			{
				return;
			}
			using (new Redrawer(Root, ShouldRedrawWhenDone))
			{
				UnExecuteCore();
				ExecuteCount--;
				if (BlockToFocusAfterUndoIsValid)
				{
					BlockToFocusAfterUndo.SetDefaultFocus();
				}
			}
		}
	}
}
