using GuiLabs.Utils.Delegates;

namespace GuiLabs.Utils.Commands
{
	public class DelegateCommand : Command
	{
		public DelegateCommand(string text, EmptyHandler func)
			: base(text)
		{
			this.Function = func;
		}

		private EmptyHandler Function;

		public override void Click()
		{
			if (Function != null)
			{
				Function();
			}
		}
	}
}
