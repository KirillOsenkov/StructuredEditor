using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Editor.Blocks;
using System.Diagnostics;
using GuiLabs.Editor.Actions;

namespace SampleEditor.DiplomSampleCode
{
	public class TestMethodAttribute : Attribute
	{

	}

	public class Tests
	{
		public void Init()
		{
			
		}

		RootBlock Root = new RootBlock();

		public void Run()
		{
			TestSetTextInTransaction();
		}

		[TestMethod]
		public void TestSetTextInTransaction()
		{
			TextBoxBlock name = new TextBoxBlock();
			Root.Add(name);

			name.MyTextBox.MinWidth = 30;

			name.Text = "A"; // takes effect immediately
			Assert(name.Text == "A");

			using (Transaction t = Transaction.Create(Root))
			{
				// takes effect only after 
				// the transaction is committed (disposed)
				name.Text = "B";
				Assert(name.Text == "A");
			}

			// "B", because the transaction was committed
			// after exiting the using statement
			Assert(name.Text == "B");

			name.Delete();
		}

		public void Assert(bool condition)
		{
			Debug.Assert(condition);
		}
	}
}
