using System.Collections.Generic;
using System.Drawing;

namespace GuiLabs.Canvas.DrawStyle
{
	using GuiLabs.Canvas.Renderer;
	using StyleDic = Dictionary<string, IShapeStyle>;
using System.Xml;
	using GuiLabs.Utils;

	/// <summary>
	/// Globally visible singleton collection of all Styles for all Layouts.
	/// </summary>
	public class StyleFactory : IStyleFactory
	{
		#region AddStyle

		public void AddStyle(string name, IShapeStyle existingStyleToAdd)
		{
			//existingStyleToAdd.Name = name;
			Add(name, existingStyleToAdd);
		}

		public void AddStyle(
			string name,
			Color fillColor)
		{
			Add(name,
				new ShapeStyle(fillColor));
		}

		public void AddStyle(
			string name,
			Color borderColor,
			Color fillColor)
		{
			Add(name,
				new ShapeStyle(borderColor, fillColor));
		}

		public void AddStyle(
			string name,
			Color borderColor,
			IFillStyleInfo fill)
		{
			Add(name,
				new ShapeStyle(borderColor, fill));
		}

		public void AddStyle(
			string name,
			Color borderColor,
			Color fillColor,
			Color fontColor)
		{
			ShapeStyle s = new ShapeStyle();
			s.FontStyleInfo.ForeColor = fontColor;
			s.LineColor = borderColor;
			s.FillColor = fillColor;
			Add(name, s);
		}

		public void AddStyle(
			string name,
			Color borderColor,
			Color fillColor,
			Color fontColor,
			string fontName)
		{
			AddStyle(
				name,
				borderColor,
				fillColor,
				fontColor,
				fontName,
				ShapeStyle.DefaultFontSize,
				FontStyle.Regular);
		}

		public void AddStyle(
			string name,
			Color borderColor,
			Color fillColor,
			Color fontColor,
			int fontSize)
		{
			AddStyle(
				name,
				borderColor,
				fillColor,
				fontColor,
				ShapeStyle.DefaultFont,
				fontSize,
				FontStyle.Regular);
		}

		public void AddStyle(
			string name,
			Color borderColor,
			Color fillColor,
			Color fontColor,
			FontStyle fontStyle)
		{
			AddStyle(
				name,
				borderColor,
				fillColor,
				fontColor,
				ShapeStyle.DefaultFont,
				ShapeStyle.DefaultFontSize,
				fontStyle);
		}

		public void AddStyle(
			string name,
			Color borderColor,
			Color fillColor,
			Color fontColor,
			string fontName,
			int fontSize,
			FontStyle fontStyle)
		{
			ShapeStyle s = new ShapeStyle();
			s.LineColor = borderColor;
			s.FillColor = fillColor;
			s.FontStyleInfo = RendererSingleton.StyleFactory.ProduceNewFontStyleInfo(fontName, fontSize, fontStyle);
			s.FontStyleInfo.ForeColor = fontColor;
			Add(name, s);
		}

		public void AddStyle(
			string name,
			Color borderColor,
			Color fillColor,
			Color gradientColor,
			FillMode gradientMode)
		{
			Add(name,
				new ShapeStyle(
				borderColor,
				fillColor,
				gradientColor,
				gradientMode));
		}

		#endregion

		#region Singleton Instances

		private static StyleFactory mInstance;
		public static StyleFactory Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new StyleFactory();
				}
				return mInstance;
			}
			set
			{
				mInstance = value;
			}
		}

		#endregion

		#region Hash

		/// <summary>
		/// (String -> ShapeStyle) hashtable. 
		/// Not visible from outside.
		/// </summary>
		private StyleDic mStyleList = new StyleDic();
		private StyleDic StyleList
		{
			get
			{
				return mStyleList;
			}
		}

		public void Clear()
		{
			StyleList.Clear();
		}

		#endregion

		/// <param name="shapeType">Typically Layout.StyleName</param>
		/// <returns>ShapeStyle with the specified name, if it exists, null otherwise.
		/// </returns>
		public IShapeStyle GetStyle(string shapeType)
		{
			// It's OK when there are no styles, GetStyle will just return null.
			/*if (!IsInit)
			{
				Init();
			}
			*/
			IShapeStyle result = null;
			StyleList.TryGetValue(shapeType, out result);
			return result;
		}

		#region AddAlias

		public void AddAliasAndSelected(string name, string existingStyleName)
		{
			AddAlias(name, existingStyleName);
			AddAlias(name + "_selected", existingStyleName + "_selected");
		}

		public void AddAlias(string name, string existingStyleName)
		{
			IShapeStyle existing = GetStyle(existingStyleName);
			if (existing != null)
			{
				Add(name, existing);
			}
		}

		#endregion

		#region Add

		public void Add(string name, IShapeStyle existingStyle)
		{
			if (existingStyle == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(existingStyle.Name))
			{
				existingStyle.Name = name;
			}
			StyleList.Add(name, existingStyle);
		}

		#endregion

		#region Enumerator

		public IEnumerator<IShapeStyle> GetEnumerator()
		{
			return StyleList.Values.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region SaveToFile

		public void SaveToFile(string fileName)
		{
			CreateSnapshot().SaveToFile(fileName);
		}

		public Memento CreateSnapshot()
		{
			Memento result = new Memento();
			result.NodeType = "ShapeStyles";
			foreach (KeyValuePair<string, IShapeStyle> pair in StyleList)
			{
				Memento styleSnapshot = pair.Value.CreateSnapshot();
				styleSnapshot["name"] = pair.Key;
				result.Add(styleSnapshot);
			}
			return result;
		}

		public static StyleFactory LoadFromFile(string fileName)
		{
			StyleFactory result = new StyleFactory();
			Memento styles = Memento.ReadFromFile(fileName);
			foreach (Memento s in styles.Children)
			{
				ShapeStyle newStyle = ShapeStyle.CreateFromMemento(s);
				result.Add(newStyle.Name, newStyle);
			}
			return result;
		}

		#endregion
	}
}
