using GuiLabs.Utils.Commands;
namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox : Control, ITextControl
	{
		#region ctors

		public TextBox()
			: base()
		{
			Init();
		}

		public TextBox(string defaultText)
			: base()
		{
			this.Text = defaultText;
			Init();
		}

		private void Init()
		{
			Focusable = true;
			Layout();
			KeyBrake.DefaultDelay = 0;
			this.Stretch = StretchMode.Horizontal;
			InitMenu();
		}

		#endregion

		#region Menu

		protected virtual void InitMenu()
		{
			if (Menu == null)
			{
				Menu = new CommandList();
				Menu.Add(new CutCommand(this));
				Menu.Add(new CopyCommand(this));
				Menu.Add(new PasteCommand(this));
			}
		}

		private CommandList mMenu;
		public CommandList Menu
		{
			get
			{
				return mMenu;
			}
			set
			{
				mMenu = value;
			}
		}

		#endregion

		#region Completion

		public override void DisplayCompletionList()
		{
			if (this.Root != null)
			{
				Rect r = Bounds.Clone();
				r.Left += TextSize.X;
				r.Width -= TextSize.X;
				this.Root.DisplayCompletionList(r);
			}
		}

		#endregion
	}
}
