//using System.IO;
//using System.Xml;
//using GuiLabs.Editor.Blocks;

//namespace GuiLabs.Editor.Xml
//{
//    public class XMLVisitor : IVisitor
//    {
//        private XmlTextWriter writer;

//        public XMLVisitor(StreamWriter Writer)
//        {
//            writer = new XmlTextWriter(Writer);
//            writer.Formatting = Formatting.Indented;
//        }

//        // ========================================================

//        void VisitMyChildren(ContainerBlock Block)
//        {
//            foreach (IXmlBlock child in Block.FindChildren<IXmlBlock>())
//            {
//                child.AcceptVisitor(this);
//            }
//        }

//        public void Visit(XMLRootBlock Block)
//        {
//            writer.WriteStartDocument();
//            VisitMyChildren(Block);
//            writer.WriteEndDocument();
//        }
//    }
//}