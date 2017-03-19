using System.Windows.Forms;

namespace GuiLabs.Editor.Sample
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			root = new SampleRootBlock();
			viewWindow1.RootBlock = root;
		}

		private SampleRootBlock root;
	}
}