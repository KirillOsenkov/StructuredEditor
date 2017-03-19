using System.Text;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		#region Text

		public string Text
		{
			get
			{
				return DynamicString.ToString();
			}
			set
			{
				if (value == null)
				{
					return;
				}
				SetText(value);
				//if (value == Text)
				//{
				//    return;
				//}

				//// change buffer
				//string oldText = Text;
				//DynamicString.Remove(0, DynamicString.Length);
				//DynamicString.Append(value);
				
				//// change caret
				//SetCaretPosition(0);

				//// layout
				//Layout();
			}
		}

		private StringBuilder DynamicString = new StringBuilder();

		public int TextLength
		{
			get
			{
				return DynamicString.Length;
			}
		}

		#endregion
	}
}