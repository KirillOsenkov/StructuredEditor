using GuiLabs.Canvas.DrawStyle;
using System.Drawing;

namespace GuiLabs.Editor.Xml
{
	public class XmlStyleFactory : StyleFactory
	{
		public XmlStyleFactory()
		{
			#region Colors

			Color lightGray = Color.FromArgb(250, 250, 250);
			Color niceGray = Color.FromArgb(246, 243, 243);
			Color classBack = lightGray;
			Color methodBack = lightGray;
			Color memberBack = Color.GhostWhite;
			Color namespaceBack = lightGray;
			Color propertyBack = lightGray;
			Color grayFrame = Color.Black;// Color.Gainsboro;
			Color deselectedFrame = Color.Transparent; // Color.Gainsboro;
			//Color controlStructure = Color.White;
			//Color controlStructure = Color.FromArgb(250, 250, 250);
			Color controlStructure = lightGray;
			Color typeName = Color.FromArgb(43, 145, 175);

			#endregion

			AddStyle(StyleNames.Transparent,
				Color.Transparent,
				Color.Transparent);
			AddAlias(StyleNames.TransparentSel, StyleNames.Transparent);

			#region Types

			AddStyle(
				StyleNames.TypeName,
				Color.Transparent,
				Color.Transparent,
				typeName//, "Consolas", 11, FontStyle.Regular
				);

			AddAlias(
				StyleNames.TypeNameSel,
				StyleNames.TypeName);

			AddStyle("ClassBlock",
				//Color.Lavender,
				deselectedFrame,
				classBack);

			AddStyle("ClassBlock_selected",
				grayFrame,
				classBack);

			AddAliasAndSelected("StructBlock", "ClassBlock");
			AddAliasAndSelected("InterfaceBlock", "ClassBlock");
			AddAliasAndSelected("EnumBlock", "ClassBlock");

			#endregion

			#region Namespace

			AddStyle("NodeBlock",
				//Color.PeachPuff, 
				deselectedFrame,
				namespaceBack);

			AddStyle("NodeBlock_selected",
				grayFrame,
				namespaceBack);

			AddStyle("UsingBlock",
				deselectedFrame,
				lightGray);

			AddStyle("UsingBlock_selected",
				grayFrame,
				lightGray);

			#endregion

			#region Members

			AddStyle("MethodBlock",
				//Color.PaleGreen, 
				deselectedFrame,
				methodBack);

			AddStyle("MethodBlock_selected",
				grayFrame,
				methodBack);

			AddStyle("FieldBlock",
				Color.Transparent,
				Color.Transparent);

			AddStyle("FieldBlock_selected",
				grayFrame,
				memberBack);

			AddStyle("PropertyBlock",
				//Color.PaleGreen, 
				deselectedFrame,
				propertyBack);

			AddStyle("PropertyBlock_selected",
				grayFrame,
				propertyBack);

			AddStyle("PropertyAccessorBlock",
				//Color.PaleGreen, 
				deselectedFrame,
				propertyBack);

			AddStyle("PropertyAccessorBlock_selected",
				grayFrame,
				propertyBack);

			AddAliasAndSelected("InterfaceAccessorsBlock", "FieldBlock");

			#endregion

			AddAliasAndSelected("DelegateBlock", "FieldBlock");

			#region Method

			AddStyle("ControlStructureBlock",
				//Color.PaleGreen, 
				deselectedFrame,
				controlStructure);

			AddStyle("ControlStructureBlock_selected",
				grayFrame,
				controlStructure);

			#endregion

			#region Text

			//ShapeStyle s = new ShapeStyle();
			//s.Name = "GuiLabs.Editor.CSharp.ModifierLabelBlock_selected";
			//s.FontStyleInfo = 
			//    GuiLabs.Canvas.Renderer.RendererSingleton.StyleFactory.ProduceNewFontStyleInfo(
			//    "Lucida Console",
			//    14,
			//    FontStyle.Regular);
			//s.LineColor = Color.BlueViolet;
			//Add(s);

			AddStyle("TextBoxBlock",
				Color.Transparent,
				Color.Transparent);

			AddStyle("TextBoxBlock_selected",
				Color.Transparent,
				Color.Transparent);

			AddStyle("MemberNameBlock",
				Color.Transparent,
				Color.Transparent,
				Color.Black,
				"Courier New",
				10,
				FontStyle.Bold);

			AddStyle("MemberNameBlock_selected",
				Color.PaleGoldenrod,
				Color.LightYellow,
				Color.Black,
				"Courier New",
				10,
				FontStyle.Bold);

			AddStyle("ButtonBlock",
				Color.DarkRed,
				Color.Linen,
				Color.PeachPuff,
				FillMode.VerticalGradient);

			AddStyle("ButtonBlock_selected",
				Color.DarkRed,
				Color.Linen,
				Color.SandyBrown,
				FillMode.VerticalGradient);

			AddStyle("EmptyBlock",
				Color.Transparent,
				Color.Transparent);

			AddStyle("EmptyBlock_selected",
				Color.Transparent,
				Color.Transparent);

			AddStyle("EmptyBlock_validated",
				Color.OrangeRed,
				Color.FloralWhite);

			AddStyle("LabelBlock",
				Color.Transparent,
				Color.Transparent);

			AddStyle("LabelBlock_selected",
				Color.Transparent,
				Color.Transparent);

			AddStyle("SpaceBlock",
				Color.Transparent,
				Color.Transparent);

			AddStyle("SpaceBlock_selected",
				Color.DarkBlue,
				Color.LightCyan);

			AddStyle("TextLine",
				Color.Transparent,
				Color.Transparent);

			AddStyle("TextLine_selected",
				Color.LightGray,
				Color.LightYellow);
			//Color.Transparent,
			//Color.Transparent);

			AddStyle("StatementLine",
				Color.Transparent,
				Color.Transparent);

			AddStyle("StatementLine_selected",
				Color.LightGray,
				Color.LightYellow);

			AddStyle("KeywordLabel",
				Color.Transparent,
				Color.Transparent,
				Color.Blue);

			AddStyle("KeywordLabel_selected",
				Color.Blue,
				Color.LightCyan,
				Color.Blue);

			AddStyle("OneWordStatement",
				Color.Transparent,
				Color.Transparent);

			AddStyle("OneWordStatement_selected",
				Color.Gray,
				Color.Yellow);

			AddAliasAndSelected("ExpressionBlock", "TextLine");

			#endregion

			#region Modifiers

			AddAliasAndSelected("ModifierSelectionBlock", "KeywordLabel");
			AddAliasAndSelected("ModifierSeparatorBlock", "KeywordLabel");

			AddStyle("TypeSelection",
				Color.Transparent,
				Color.Transparent,
				typeName);
			AddStyle("TypeSelection_selected",
				Color.Blue,
				Color.LightCyan,
				typeName);

			AddAliasAndSelected("InterfacePropertyAccessor", "ModifierSelectionBlock");

			#endregion
		}
	}

	public static class StyleNames
	{
		public static string sel = "_selected";
		public static string val = "_validated";

		public static string Transparent = "Transparent";
		public static string TransparentSel = Transparent + sel;

		public static string TypeName = "TypeName";
		public static string TypeNameSel = TypeName + sel;
	}
}