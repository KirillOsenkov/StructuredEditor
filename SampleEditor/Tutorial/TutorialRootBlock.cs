using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Events;
using System.Drawing;
using System;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Editor.Actions;
using GuiLabs.Utils;
using GuiLabs.Canvas.Controls;

namespace GuiLabs.Editor.Sample
{
	public class TutorialRootBlock : RootBlock
	{
		public TutorialRootBlock() : base()
		{
			FirstBlock = new TutorialEmptyBlock();
			this.Children.Add(FirstBlock);
		}

		private TutorialEmptyBlock mFirstBlock;
		public TutorialEmptyBlock FirstBlock
		{
			get
			{
				return mFirstBlock;
			}
			set
			{
				mFirstBlock = value;
			}
		}
	}

	public class TutorialRootBlock2 : RootBlock
	{
		public TutorialRootBlock2() : base()
		{
			HContainerBlock row = new HContainerBlock();
			row.Add(captionCelsius, value, captionFahrenheit, result);
			this.Add(row);

			value.TextChanged += value_TextChanged;
		}

		private LabelBlock captionCelsius = new LabelBlock("Celsius: ");
		private TextBoxBlock value = new TextBoxBlock("20");
		private LabelBlock captionFahrenheit = new LabelBlock(" Fahrenheit: ");
		private LabelBlock result = new LabelBlock();

		private void value_TextChanged(ITextProvider sender, string oldText, string newText)
		{
			double celsius = 0;
			double.TryParse(value.Text, out celsius);
			double fahrenheit = celsius * 9 / 5 + 32;
			result.Text = fahrenheit.ToString();
		}
	}

	public class TutorialRootBlock3 : RootBlock
	{
		public TutorialRootBlock3() : base()
		{
			HContainerBlock con = new HContainerBlock();
			this.Add(con);

			TextBoxBlock text = new TextBoxBlock();
			con.Add(text);
			con.Add("// here the TextBox ends");

			text.MyTextBox.MinWidth = 40;
			
		}
	}

	public class TutorialRootBlock4 : RootBlock
	{
		public TutorialRootBlock4()
			: base()
		{
			HContainerBlock con = new HContainerBlock();
			this.Add(con);

			button = new ButtonBlock("Hide the text");
			text = new TextBoxBlock();
			con.Add(button);
			con.Add(text);

			button.Pushed += button_Pushed;
		}

		void button_Pushed()
		{
			text.MyControl.Visible = !text.MyControl.Visible;
			button.MyButton.Text = "Text.MyControl.Visible = "
				+ text.MyControl.Visible.ToString();
		}

		TextBoxBlock text;
		ButtonBlock button;
	}

	public class TutorialRootBlock5 : RootBlock
	{
		public TutorialRootBlock5()
			: base()
		{
			label = new LabelBlock("Click me");
			mouseCoords = new LabelBlock();
			this.Add(label);
			this.Add(mouseCoords);

			label.MyControl.MouseDown += label_MouseDown;
			this.MyControl.MouseMove += delegate(MouseWithKeysEventArgs MouseInfo)
			{
				mouseCoords.Text = MouseInfo.X.ToString() + "; " + MouseInfo.Y;
			};
			
			this.MyControl.Style.FillStyleInfo.Mode = FillMode.HorizontalGradient;
		}

		LabelBlock label;
		LabelBlock mouseCoords;

		void label_MouseDown(MouseWithKeysEventArgs MouseInfo)
		{
			this.MyControl.Style.FillStyleInfo.FillColor = GetRandomColor();
			this.MyControl.Style.FillStyleInfo.GradientColor = GetRandomColor();
			this.MyControl.Redraw();
			label.Text = "Thank you!";
		}

		Random rnd = new Random();
		Color GetRandomColor()
		{
			return Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
		}
	}

	public class TutorialRootBlock6 : RootBlock
	{
		public TutorialRootBlock6()
			: base()
		{
			n1 = new TutorialUniversalBlock();
			c1 = new TutorialUniversalBlock();
			c2 = new TutorialUniversalBlock();
			TutorialUniversalBlock c3 = new TutorialUniversalBlock();
			n1.VMembers.Children.Add(c1, c2, c3);
			this.Add(n1);

			ButtonBlock moveButton = new ButtonBlock("Move!!!");
			moveButton.Pushed += moveButton_Pushed;
			this.Add(moveButton);
		}

		TutorialUniversalBlock n1;
		TutorialUniversalBlock c1;
		TutorialUniversalBlock c2;

		void moveButton_Pushed()
		{
			c1.MoveAfterBlock(n1);
		}
	}

	public class TutorialRootBlock7 : RootBlock
	{
		public TutorialRootBlock7()
			: base()
		{
			this.Add(status);
			status.MyControl.Box.Margins.Bottom = 40;
			TutorialUniversalBlock n1 = new TutorialUniversalBlock();
			TutorialUniversalBlock c1 = new TutorialUniversalBlock();
			TutorialUniversalBlock c2 = new TutorialUniversalBlock();
			TutorialUniversalBlock c3 = new TutorialUniversalBlock();
			n1.VMembers.Children.Add(c1, c2, c3);
			this.Add(n1);

			this.ActiveBlockChanged += TutorialRootBlock7_ActiveBlockChanged;
		}

		void TutorialRootBlock7_ActiveBlockChanged(Block oldActive, Block newActive)
		{
			if (oldActive == null || newActive == null)
			{
				return;
			}
			status.Text = oldActive.GetType().Name + " => " + newActive.GetType().Name;
		}

		LabelBlock status = new LabelBlock();
	}
}
