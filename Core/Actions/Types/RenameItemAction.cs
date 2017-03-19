using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Undo;

namespace GuiLabs.Editor.Actions
{
	//public class RenameItemAction : RootBlockAction
	//{
	//    #region ctor

	//    public RenameItemAction(RootBlock root, IHasText textContainer, string oldText)
	//        : base(root)
	//    {
	//        Root = root;
	//        TextContainer = textContainer;
	//        OldString = oldText;
	//    }

	//    #endregion

	//    private IHasText mTextContainer;
	//    public IHasText TextContainer
	//    {
	//        get
	//        {
	//            return mTextContainer;
	//        }
	//        set
	//        {
	//            mTextContainer = value;
	//        }
	//    }

	//    private string OldString;
	//    private string NewString;
	//    private bool UndoAlreadyHappened = false;

	//    protected override void ExecuteCore()
	//    {
	//        if (UndoAlreadyHappened)
	//        {
	//            TextContainer.SetText(NewString, ActionOptions.NoRedrawNoUndo);
	//        }
	//    }

	//    protected override void UnExecuteCore()
	//    {
	//        UndoAlreadyHappened = true;
	//        NewString = TextContainer.Text;
	//        TextContainer.SetText(OldString, ActionOptions.NoRedrawNoUndo);
	//    }

	//    public override bool TryToMerge(IAction FollowingAction)
	//    {
	//        if (!FollowingAction.AllowToMergeWithPrevious)
	//        {
	//            return false;
	//        }

	//        RenameItemAction Follower = FollowingAction as RenameItemAction;
	//        if (Follower != null && Follower.TextContainer == this.TextContainer)
	//        {
	//            return true;
	//        }
	//        else
	//        {
	//            return false;
	//        }
	//    }
	//}
}
