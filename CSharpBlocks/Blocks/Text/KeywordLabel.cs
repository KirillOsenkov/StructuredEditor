using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class KeywordLabel : LabelBlock
	{
		public KeywordLabel() : base()
		{
		}

		public KeywordLabel(string text) : base(text)
		{
		}

		#region Style

		protected override string StyleName()
		{
			return "KeywordLabel";
		}

		#endregion
		
	}
}
