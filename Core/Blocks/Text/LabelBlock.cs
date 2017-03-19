using GuiLabs.Canvas.Controls;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	public class LabelBlock : Block, IHasText, ITextProvider
	{
		#region ctors

		public LabelBlock(string text)
			: base()
		{
			MyLabel = new Label(text);
		}

		public LabelBlock(ITextProvider bindToText)
			: base()
		{
			MyLabel = new Label(bindToText);
		}

		public LabelBlock()
			: this("")
		{
		}

		#endregion

		#region Events

		public event TextChangedEventHandler TextChanged;
		protected void RaiseTextChanged(string oldText, string newText)
		{
			if (TextChanged != null)
			{
				TextChanged(this, oldText, newText);
			}
		}

		#endregion

		#region Control

		private Label mMyLabel;
		public virtual Label MyLabel
		{
			get { return mMyLabel; }
			set
			{
				if (mMyLabel != null)
				{
					UnSubscribeControl();
				}
				mMyLabel = value;
				if (mMyLabel != null)
				{
					SubscribeControl();
				}
			}
		}

		protected override void SubscribeControl()
		{
			base.SubscribeControl();
			mMyLabel.TextChanged += OnTextChanged;
		}

		protected virtual void OnTextChanged(ITextProvider sender, string oldText, string newText)
		{
			RaiseTextChanged(oldText, newText);
		}

		protected override void UnSubscribeControl()
		{
			base.UnSubscribeControl();
			mMyLabel.TextChanged += OnTextChanged;
		}

		public override Control MyControl
		{
			get { return MyLabel; }
		}

		#endregion

		#region Text

		public virtual string Text
		{
			get
			{
				return MyLabel.Text;
			}
			set
			{
				MyLabel.Text = value;
			}
		}

		public override string ToString()
		{
			return MyLabel.Text;
		}

		#endregion
	}
}
