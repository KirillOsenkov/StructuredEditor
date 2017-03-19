using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	public class InterfacePropertyAccessor : HContainerBlock
	{
		public InterfacePropertyAccessor(string initialText)
			: base()
		{
			this.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize / 2);
			this.MyControl.Box.SetMouseSensitivityToMargins();
			this.MyControl.Focusable = true;

			keyword = new FocusableKeyword(initialText);
			this.Add(keyword, new LabelBlock(";"));
		}

		private FocusableKeyword keyword;

		public string Text
		{
			get
			{
				return keyword.Text;
			}
		}

		private class FocusableKeyword : KeywordLabel
		{
			public FocusableKeyword(string initialText)
				: base(initialText)
			{
			}
		}

		#region Style

		protected override string StyleName()
		{
			return "InterfacePropertyAccessor";
		}

		#endregion

	}
}
