namespace GuiLabs.Utils.Commands
{
	public class Command : ICommand
	{
		#region ctor

		public Command(string text)
		{
			Text = text;
		}

		#endregion

		public virtual void Click()
		{
		}

		#region Picture

		private System.Drawing.Image mPicture;
		public virtual System.Drawing.Image Picture
		{
			get
			{
				return mPicture;
			}
			set
			{
				mPicture = value;
			}
		}

		#endregion

		#region Text

		private string mText;
		public string Text
		{
			get
			{
				return mText;
			}
			set
			{
				mText = value;
			}
		}

		public override string ToString()
		{
			return Text;
		}

		#endregion
	}
}
