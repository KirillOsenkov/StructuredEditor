using System.Drawing;
using System.Windows.Forms;
using GuiLabs.Utils;
namespace GuiLabs.Editor.UI
{
	public class CompletionListItem
	{
		public CompletionListItem(string text)
		{
			Text = text;
			Param.CheckNonEmptyString(text, "text");
		}

		public virtual void Click(CompletionFunctionality hostItemList)
		{

		}

		public ItemClickReason Reason;

		public virtual bool ShouldShow(CompletionFunctionality hostItemList)
		{
			return true;
		}

		#region Text

		private string mText;
		public string Text
		{
			get
			{
				return mText;
			}
			set
			{
				mText = value;
			}
		}

		public override string ToString()
		{
			return mText;
		}

		#endregion

		#region Picture

		private Image mPicture;
		public virtual Image Picture
		{
			get
			{
				return mPicture;
			}
			set
			{
				mPicture = value;
			}
		}

		#endregion

		private bool mVisible;
		public bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				mVisible = value;
			}
		}
	}

	public enum ItemClickSource
	{
		MouseClick,
		KeyDown,
		KeyPress
	}

	public struct ItemClickReason
	{
		public ItemClickReason(Keys keycode)
		{
			Type = ItemClickSource.KeyDown;
			KeyCode = keycode;
			KeyChar = ' ';
		}

		public ItemClickReason(char c)
		{
			Type = ItemClickSource.KeyPress;
			KeyChar = c;
			KeyCode = Keys.None;
		}

		public char KeyChar;
		public Keys KeyCode;
		public ItemClickSource Type;
	}
}
