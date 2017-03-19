using GuiLabs.Canvas.Controls;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	public class NameValueBlock : HContainerBlock
	{
		#region ctors

		public NameValueBlock(string name, string value)
			: base()
		{
			Name = new LabelBlock(name);
			Value = new TextBoxBlock(value);

			this.Add(Name, Value);

			this.MyListControl.Focusable = true;
		}

		public NameValueBlock(string caption)
			: this(caption, "")
		{

		}

		public NameValueBlock()
			: this("name", "value")
		{

		}

		#endregion

		private LabelBlock mName;
		public LabelBlock Name
		{
			get { return mName; }
			set { mName = value; }
		}

		private TextBoxBlock mValue;
		public TextBoxBlock Value
		{
			get { return mValue; }
			set { mValue = value; }
		}

		public override Control DefaultFocusableControl()
		{
			return Value.DefaultFocusableControl();
		}
	}
}
