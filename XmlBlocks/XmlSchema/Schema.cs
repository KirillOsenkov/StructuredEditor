using System.Xml.Schema;
using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using System.Text;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.Xml
{
	public class Schema
	{
		public static XmlSchemaElement FindPath(string path, XmlSchemaElement element)
		{
			if (string.IsNullOrEmpty(path))
			{
				return element;
			}

			int separator = path.IndexOf('/');
			if (separator == -1 || separator >= path.Length - 1)
			{
				return FindAlternative(element, path);
			}
			else
			{
				string head = path.Substring(0, separator);
				string tail = path.Substring(separator + 1);
				XmlSchemaElement found = FindAlternative(element, head);
				if (found == null)
				{
					return null;
				}
				return FindPath(tail, found);
			}
		}

		public static string GetPath(Block block)
		{
			List<NodeBlock> nodes = new List<NodeBlock>(
				block.FindAllContainingBlocks<NodeBlock>());
			nodes.Reverse();
			
			StringBuilder s = new StringBuilder();
			
			foreach (NodeBlock n in nodes)
			{
				if (n.NameBlock.Text == "html")
				{
					continue;
				}
				if (s.Length > 0)
				{
					s.Append("/");
				}
				s.Append(n.NameBlock.Text);
			}

			return s.ToString();
		}

		public static XmlSchemaElement FindAlternative(XmlSchemaElement el, string elementName)
		{
			foreach (XmlSchemaElement found in GetAlternatives(el))
			{
				if (GetName(found) == elementName)
				{
					return found;
				}
			}
			return null;
		}

		public static IEnumerable<XmlSchemaElement> GetAlternatives(XmlSchemaElement element)
		{
			XmlSchemaComplexType t = element.ElementSchemaType as XmlSchemaComplexType;
			if (t == null)
			{
				yield break;
			}

			foreach (XmlSchemaElement e in GetAlternativesFromParticle(GetParticle(t)))
			{
				yield return e;
			}
		}

		private static XmlSchemaParticle GetParticle(XmlSchemaComplexType t)
		{
			return t.Particle ?? t.ContentTypeParticle;
		}

		public static string GetName(XmlSchemaElement e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				return e.Name;
			}
			else if (e.QualifiedName != null && !e.QualifiedName.IsEmpty)
			{
				return e.QualifiedName.Name;
			}
			else if (e.RefName != null && !e.RefName.IsEmpty)
			{
				return e.RefName.Name;
			}
			return "";
		}

		public static IEnumerable<string> GetPossibleTags(XmlSchemaElement element)
		{
			foreach (XmlSchemaElement e in GetAlternatives(element))
			{
				if (e != null)
				{
					yield return GetName(e);
				}
			}
		}

		public static IEnumerable<XmlSchemaElement> GetAlternativesFromParticle(XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaElement)
			{
				yield return particle as XmlSchemaElement;
				yield break;
			}

			XmlSchemaParticle result;

			XmlSchemaSequence seq = particle as XmlSchemaSequence;
			if (seq != null)
			{
				foreach (XmlSchemaObject o in seq.Items)
				{
					result = o as XmlSchemaParticle;
					if (result != null)
					{
						foreach (XmlSchemaElement el in GetAlternativesFromParticle(result))
						{
							yield return el;
						}
					}
				}
			}

			XmlSchemaChoice ch = particle as XmlSchemaChoice;
			if (ch != null)
			{
				foreach (XmlSchemaObject a in ch.Items)
				{
					result = a as XmlSchemaParticle;
					if (result != null)
					{
						foreach (XmlSchemaElement el2 in GetAlternativesFromParticle(result))
						{
							yield return el2;
						}
					}
				}
			}
		}
	}
}