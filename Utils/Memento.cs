// Copyright (C) Kirill Osenkov, www.osenkov.com
// Feel free to use and reuse in any projects.
// Please do not remove this comment.

using System;
using System.Collections.Generic;
using System.Drawing; // needed for System.Drawing.Color
using System.IO;
using System.Xml;

namespace GuiLabs.Utils
{
	public interface ISupportMemento
	{
		Memento CreateSnapshot();
	}

	/// <summary>
	/// Each Memento object corresponds to a single XML node in memory.
	/// It can be serialized and deserialized into XML.
	/// * NodeType corresponds to XML node tag name.
	/// * Attributes corresponds to the XML node attributes
	/// * Children corresponds to subnodes
	/// </summary>
	public class Memento
	{
		#region ctors

		public Memento()
		{
		}

		public Memento(XmlNode node)
			: this()
		{
			ReadFromXmlNode(node);
		}

		#endregion

		#region this

		public string this[string attributeName]
		{
			get
			{
				return Get<string>(attributeName);
			}
			set
			{
				Attributes[attributeName] = value;
			}
		}

		#endregion

		#region NodeType

		private string mNodeType;
		/// <summary>
		/// Tag name in the resulting XML
		/// </summary>
		public string NodeType
		{
			get
			{
				return mNodeType;
			}
			set
			{
				mNodeType = value;
			}
		}

		#endregion

		#region Attributes

		private Dictionary<string, object> mAttributes = new Dictionary<string, object>(0);
		/// <summary>
		/// Attributes in the resulting XML node
		/// </summary>
		internal IDictionary<string, object> Attributes
		{
			get
			{
				return mAttributes;
			}
		}

		#region Get / Put

		public T Get<T>(string attributeName)
		{
			object val;
			this.Attributes.TryGetValue(attributeName, out val);
			T result = default(T);

			if (typeof(T).IsEnum)
			{
				result = (T)Enum.Parse(typeof(T), val as string, true);
				return result;
			}
			try
			{
				result = (T)val;
			}
			catch (Exception)
			{
			}
			return result;
		}

		public void Put(string attributeName, Color colorValue)
		{
			if (colorValue != Color.Transparent)
			{
				this[attributeName] = colorValue.ToArgb().ToString();
			}
		}

		public void Put(string attributeName, int intValue)
		{
			if (intValue != 0)
			{
				this[attributeName] = intValue.ToString();
			}
		}

		public void Put(string attributeName, bool boolValue)
		{
			if (boolValue)
			{
				this[attributeName] = boolValue.ToString();
			}
		}

		public int GetInt(string attributeName)
		{
			string str = this[attributeName];
			if (string.IsNullOrEmpty(str))
			{
				return 0;
			}
			int val = 0;
			int.TryParse(this[attributeName], out val);
			return val;
		}

		public bool GetBool(string attributeName)
		{
			bool b = false;
			string str = this[attributeName];
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			bool.TryParse(str, out b);
			return b;
		}

		private static readonly int ColorTransparentArgb = Color.Transparent.ToArgb();
		public Color GetColor(string attributeName)
		{
			string str = this[attributeName];
			if (string.IsNullOrEmpty(str) || str == "-1")
			{
				return Color.Transparent;
			}
			int val = ColorTransparentArgb;
			int.TryParse(str, out val);
			return Color.FromArgb(val);
		}

		#endregion

		#endregion

		#region Children

		public void Add(Memento child)
		{
			if (child != null)
			{
				Children.Add(child);
			}
		}

		public void Add(ISupportMemento objectToSerialize)
		{
			if (objectToSerialize != null)
			{
				Add(objectToSerialize.CreateSnapshot());
			}
		}

		public Memento FindChild(string name)
		{
			foreach (Memento child in this.Children)
			{
				if (child.NodeType == name)
				{
					return child;
				}
			}
			return null;
		}

		private List<Memento> mChildren = new List<Memento>(0);
		public ICollection<Memento> Children
		{
			get
			{
				return mChildren;
			}
		}

		#endregion

		#region Factory methods

		public static Memento ReadFromString(string contents)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(new StringReader(contents));
			Memento result = new Memento(doc.DocumentElement);
			return result;
		}

		public static Memento ReadFromFile(string fileName)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);
			Memento result = new Memento();
			result.ReadFromXmlNode(doc.DocumentElement);
			return result;
		}

		#endregion

		#region Read

		public void ReadFromXmlNode(XmlNode node)
		{
			this.NodeType = node.Name;
			if (node.Attributes != null)
			{
				foreach (XmlAttribute attr in node.Attributes)
				{
					Attributes.Add(attr.Name, attr.Value);
				}
			}
			if (node.ChildNodes != null)
			{
				foreach (XmlNode child in node.ChildNodes)
				{
					Children.Add(new Memento(child));
				}
			}
		}

		#endregion

		#region Write

		public void SaveToFile(string fileName)
		{
			using (StreamWriter w = new StreamWriter(fileName))
			{
				using (XmlTextWriter writer = new XmlTextWriter(w))
				{
					writer.Formatting = Formatting.Indented;
					writer.WriteStartDocument();
					WriteToXml(writer);
					writer.WriteEndDocument();
				}
			}
		}

		public void WriteToXml(XmlTextWriter writer)
		{
			writer.WriteStartElement(NodeType);
			foreach (KeyValuePair<string, object> pair in Attributes)
			{
				writer.WriteAttributeString(pair.Key, pair.Value.ToString());
			}
			foreach (Memento child in this.Children)
			{
				child.WriteToXml(writer);
			}
			writer.WriteEndElement();
		}

		public void Write(TextWriter writer)
		{
			using (XmlTextWriter x = new XmlTextWriter(writer))
			{
				x.Formatting = Formatting.Indented;
				WriteToXml(x);
			}
		}

		public string WriteToString()
		{
			using (StringWriter sw = new StringWriter())
			{
				Write(sw);
				return sw.ToString();
			}
		}

		#endregion
	}
}
