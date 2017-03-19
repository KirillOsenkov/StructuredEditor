using System;
using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.Sample
{
	public class TestSelectionLabelBlock : TextSelectionBlock
	{
		public TestSelectionLabelBlock(string initialtext)
			: base(initialtext)
		{
		}

		public TestSelectionLabelBlock() : base("")
		{
		}

		protected void FillItems()
		{
			AddTextItem("Monday");
			AddTextItem("Tuesday");
			AddTextItem("Wednesday");
			AddTextItem("Thursday");
			AddTextItem("Friday");
			AddTextItem("Saturday");
			AddTextItem("Sunday");
		}
	}
}
