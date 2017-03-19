using System.IO;
using GuiLabs.Canvas.Events;
using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Controls;
using System.Xml.Schema;
using GuiLabs.Utils;
using System.Diagnostics;
using System.Collections.Generic;

namespace GuiLabs.Editor.Xml
{
	public class XMLRootBlock : RootBlock, IXmlBlock
	{
		public XMLRootBlock()
			: base()
		{
			StyleFactory.Instance = new XmlStyleFactory();
			UniversalControl.Design = UniversalControl.UniversalControlDesign.VerticalLine;
			RootElement.MyControl.Focusable = false;
			this.Add(RootElement);
			ReadSchema();
		}

		public XmlSchemaSet Schema = new XmlSchemaSet();
		public XmlSchemaElement RootSchemaElement;

		void ReadSchema()
		{
			string schemaFile =
				@"XmlSchema\xhtml1-strict.xsd"
				// @"XmlSchema\html.xsd"
				;

			Schema.Add(null, schemaFile);
			Schema.Compile();
			foreach (XmlSchema t in Schema.Schemas())
			{
				foreach (XmlSchemaElement el in t.Elements.Values)
				{
					if (el.Name == "html")
					{
						RootSchemaElement = el;
					}
				}
			}
		}

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.F12)
			{
				OptionsDialog options = new OptionsDialog();
				options.ShouldRedraw += options_ShouldRedraw;
				options.ShowDialog();
				options.ShouldRedraw -= options_ShouldRedraw;
			}
			base.OnKeyDown(sender, e);
		}

		void options_ShouldRedraw()
		{
			this.Redraw();
		}

		#endregion

		#region Visitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion

		//#region Generate XML

		//public void MakeXMLFile(string Filename)
		//{
		//    using (StreamWriter Writer = new StreamWriter(Filename))
		//    {
		//        XMLVisitor visitor = new XMLVisitor(Writer);
		//        AcceptVisitor(visitor);
		//    }
		//}

		//#endregion

		#region RootElement

		private NodeBlock mRootElement = new NodeBlock();
		public NodeBlock RootElement
		{
			get
			{
				return mRootElement;
			}
			set
			{
				mRootElement = value;
			}
		}

		#endregion
	}
}
