using System.Windows.Forms;
using System.Collections.Generic;
using GuiLabs.Utils;
using System.Text;
using System.Drawing;

namespace GuiLabs.Editor.CSharp
{
	public class DynamicHelpControl : RichTextBox
	{
		public DynamicHelpControl()
			: base()
		{
			this.BackColor = System.Drawing.Color.LightYellow;
			this.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "TextContextHelp";
			this.ReadOnly = true;
			this.Size = new System.Drawing.Size(792, 124);
			this.TabIndex = 0;
			this.TabStop = false;
			this.Text = "";
			boldFont = new Font(Font, FontStyle.Bold);
		}

		public void ShowHelp(IEnumerable<string> strings)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string helpLine in strings)
			{
				sb.AppendLine(helpLine);
			}
			ShowHelp(sb.ToString());
		}

		public void ShowHelp(string help)
		{
			API.SetRedraw(this, false);
			this.Text = help;
			ColorizeContextHelp();
			API.SetRedraw(this, true);
			Refresh();
		}

		void ColorizeContextHelp()
		{
			int bracketBalance = 0;
			int startBracket = -1;

			for (int i = 0; i < this.Lines.Length; i++)
			{
				string line = this.Lines[i];
				List<Pair<int>> pairs = new List<Pair<int>>();

				for (int j = 0; j < line.Length; j++)
				{
					if (line[j] == '[')
					{
						if (bracketBalance == 0)
						{
							startBracket = j;
						}
						bracketBalance++;
					}
					else if (line[j] == ']')
					{
						bracketBalance--;
						if (bracketBalance == 0)
						{
							pairs.Add(new Pair<int>(startBracket, j));
						}
					}
				}

				foreach (Pair<int> pair in pairs)
				{
					SelectSubstring(i, pair.First, pair.Second);
					//TextContextHelp.SelectionColor = Color.Blue;
					this.SelectionFont = boldFont;
				}
			}

			this.SelectionLength = 0;
		}

		private Font boldFont;

		void SelectSubstring(
			int lineNumber,
			int startPos,
			int endPos)
		{
			this.SelectionStart =
				this.GetFirstCharIndexFromLine(lineNumber)
				+ startPos;
			this.SelectionLength = endPos - startPos + 1;
		}

	}
}
