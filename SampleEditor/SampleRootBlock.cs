using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Sample
{
	class SampleRootBlock : RootBlock
	{
		#region AlignmentTest

		private ButtonBlock Alignment;
		private ButtonBlock OrientationButton;

		private LayoutTestBlock layoutTest;
		private ILinearLayout layout;

		private UniversalBlock AlignmentTestBlock;

		private void InitAlignment()
		{
			AlignmentTestBlock = new UniversalBlock();
			AlignmentTestBlock.HMembers.Children.Add("Layout test");

			// configuration buttons
			Alignment = new ButtonBlock("Alignment: LeftOrTopEdge");
			OrientationButton = new ButtonBlock("Orientation: Vertical");
			Alignment.MyButton.Pushed += Alignment_Pushed;
			OrientationButton.MyButton.Pushed += Orientation_Pushed;

			// ===============================================
			// init layout and set preferred properties
			// ===============================================

			layoutTest = new LayoutTestBlock();
			layout = layoutTest.LinearLayoutStrategy;

			layout.Orientation = OrientationType.Vertical;
			layout.Alignment = AlignmentType.LeftOrTopEdge;
			layout.XSpacing = 24;
			layout.YSpacing = 24;
			layout.WrapMaxSize = 950;
			layoutTest.MyControl.Box.Padding.SetLeftAndRight(16);
			layoutTest.MyControl.Box.Padding.SetTopAndBottom(32);

			layoutTest.MyControl.Layout();

			// ===============================================

			AlignmentTestBlock.VMembers.Children.Add(Alignment);
			AlignmentTestBlock.VMembers.Children.Add(OrientationButton);
			AlignmentTestBlock.VMembers.Children.Add(layoutTest);

			this.Children.Add(AlignmentTestBlock);
		}

		#region Orientation

		void Orientation_Pushed(Button itemChanged)
		{
			switch (layout.Orientation)
			{
				case OrientationType.Horizontal:
					layout.Orientation = OrientationType.Vertical;
					break;
				case OrientationType.Vertical:
					layout.Orientation = OrientationType.Horizontal;
					break;
				default:
					break;
			}

			UpdateLayoutTest();
		}

		#endregion

		#region Alignment

		void Alignment_Pushed(Button itemChanged)
		{
			switch (layout.Alignment)
			{
				case AlignmentType.LeftOrTopEdge:
					layout.Alignment = AlignmentType.Center;
					break;
				case AlignmentType.Center:
					layout.Alignment = AlignmentType.RightOrBottomEdge;
					break;
				case AlignmentType.RightOrBottomEdge:
					layout.Alignment = AlignmentType.LeftOrTopEdge;
					break;
				case AlignmentType.Justify:
					layout.Alignment = AlignmentType.LeftOrTopEdge;
					break;
				default:
					break;
			}

			UpdateLayoutTest();
		}

		#endregion

		#region UpdateLayout

		void UpdateLayoutConfigButtons()
		{
			Alignment.MyButton.Text = "Alignment: " + layout.Alignment.ToString();
			OrientationButton.MyButton.Text = "Orientation: " + layout.Orientation.ToString();
		}

		void UpdateLayoutTest()
		{
			UpdateLayoutConfigButtons();
			layoutTest.MyControl.Layout();
			layoutTest.MyControl.Redraw();
		}

		#endregion

		#endregion

		#region ButtonsTest

		private void InitButtons()
		{
			UniversalBlock ButtonsTest = new UniversalBlock();
			HContainerBlock h = new HContainerBlock();

			ButtonsTest.HMembers.Add("Buttons");
			ButtonsTest.VMembers.Add(h);

			h.Add("Buttons: 1. text: ");
			h.Add(new ButtonBlock("caption"));
			h.Add(" 2. picture: ");
			h.Add(new ButtonBlock(PictureLibrary.Instance.Collapsed));
			h.Add(" 3. picture and text: ");
			h.Add(new ButtonBlock(PictureLibrary.Instance.Delete, "imagebutton"));
			h.Add(" 4. text and picture: ");
			h.Add(new ButtonBlock("imagebutton", PictureLibrary.Instance.Plus));

			ButtonBlock b2 = new ButtonBlock(PictureLibrary.Instance.Delete2, "under pic");
			b2.MyButton.LayoutStrategy = LayoutFactory.VerticalIndented;
			ButtonsTest.VMembers.Children.Add(b2);

			this.Children.Add(ButtonsTest);
		}

		#endregion

		#region TextLine

		private void InitTextLine()
		{
			UniversalBlock TextLineTest = new UniversalBlock();
			this.Children.Add(TextLineTest);

			TextLineTest.HMembers.Children.Add("labels, texts, buttons");
			HContainerBlock line = new HContainerBlock();
			TextLineTest.VMembers.Children.Add(line);

			line.Children.Add(new TextBoxBlock());

			ButtonBlock b = new ButtonBlock("focusable");
			b.MyControl.Focusable = true;
			line.Children.Add(b);

			line.Children.Add("bla");
			line.Children.Add(new TestSelectionLabelBlock("Monday"));

			TextSelectionBlock fromArray = new TextSelectionBlock(
				new string[] { 
					"Montag", 
					"Dienstag", 
					"Mittwoch", 
					"Donnerstag", 
					"Freitag", 
					"Samstag", 
					"Sonntag" });
			line.Children.Add(fromArray);

			TextSelectionBlock fromEnum = new TextSelectionBlock(
				System.Windows.Forms.Keys.A);
			line.Children.Add(fromEnum);

			line.Children.Add(new TextBoxBlock());
		}

		#endregion

		#region EmptyBlockTest

		//private void InitEmptyBlock()
		//{
		//    SampleEmptyBlock first = new SampleEmptyBlock();
		//    this.Children.Add(first);
		//    first.SetFocus(true);
		//}

		#endregion

		#region LayoutDockTest

		private void InitLayoutDock()
		{
			this.Children.Add(new TextBoxBlock());
			this.Children.Add(new TextBoxBlock());
		}

		#endregion

		#region Multiline text test

		private UniversalBlock MultilineContainer;

		private void InitMultiline()
		{
			MultilineContainer = new UniversalBlock();
			MultilineContainer.HMembers.Children.Add("multiline test");
			this.Children.Add(MultilineContainer);

			MultilineContainer.VMembers.Children.Add(new MagicTextLine());
		}

		#endregion

		#region TextBoxBlockWithCompletion test

		public void InitTextBoxBlockWithCompletion()
		{
			UniversalBlock u = new UniversalBlock();
			u.HMembers.Children.Add("Text completion");
			HContainerBlock h = new HContainerBlock();
			u.VMembers.Children.Add(h);
			TextBoxBlockWithCompletion t = new TextBoxBlockWithCompletion();
			h.Children.Add("Text: ");
			h.Children.Add(t);
			//t.AddTextItem("Monday");
			//t.AddTextItem("Tuesday");
			//t.AddTextItem("Wednesday");
			//t.AddTextItem("Thursday");
			//t.AddTextItem("Friday");
			//t.AddTextItem("Saturday");
			//t.AddTextItem("Sunday");
			//t.AddTextItem("Hello");
			//t.AddTextItem("World");

			System.Type typ = typeof(System.Drawing.Color);
			foreach (System.Reflection.PropertyInfo member in typ.GetProperties())
			{
				t.AddTextItem(member.Name);
			}

			t.AddTextItem("=");
			t.AddTextItem("dad");
			t.AddTextItem("Color");

			this.Children.Add(u);
		}

		#endregion

		public SampleRootBlock()
		{
			InitAlignment();
			InitButtons();
			InitTextLine();
			InitMultiline();
			InitLayoutDock();
			InitTextBoxBlockWithCompletion();

			//SampleEmptyBlock first = new SampleEmptyBlock();
			//this.Children.Add(first);
			//first.SetFocus(true);
		}
	}
}
