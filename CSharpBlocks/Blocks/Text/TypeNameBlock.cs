using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	public class TypeNameBlock : NameBlock
	{
		#region ctors

		public TypeNameBlock()
			: base()
		{
			Init();
		}

		public TypeNameBlock(int minWidth)
			: base(minWidth)
		{
			Init();
		}

		private void Init()
		{
			this.MyTextBox.Box.Padding.SetLeftAndRight(ShapeStyle.DefaultFontSize);
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return StyleNames.TypeName;
		}

		#endregion
	}
}
