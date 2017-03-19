using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	public class MemberNameBlock : NameBlock
	{
		#region ctors

		public MemberNameBlock()
			: base()
		{
			Init();
		}

		public MemberNameBlock(int minWidth)
			: base(minWidth)
		{
		}

		public MemberNameBlock(string defaultText)
			: base(defaultText)
		{
			Init();
		}

		private void Init()
		{
			const int c = ShapeStyle.DefaultFontSize;
			this.MyTextBox.MinWidth = 16;
			this.MyControl.Box.Margins.Left = c;
			this.MyControl.Box.MouseSensitivityArea.Left = c;
			this.MyControl.Layout();
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "MemberNameBlock";
		}

		#endregion
	}
}
