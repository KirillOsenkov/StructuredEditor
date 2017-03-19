using System.Drawing;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Editor.CSharp
{
	public class CSharpStyleFactory : StyleFactory
	{
		public CSharpStyleFactory()
		{
			#region Colors

			Color lightGray = Color.FromArgb(250, 250, 250);
			lightGray = Color.FromArgb(230, 230, 230);
			//lightGray = Color.FromArgb(255, 255, 230);
			Color niceGray = Color.FromArgb(246, 243, 243);
			Color classBack = lightGray;
			Color methodBack = lightGray;
			Color memberBack = Color.GhostWhite;
			Color namespaceBack = lightGray;
			Color propertyBack = lightGray;
			Color grayFrame = Color.Black;// Color.Gainsboro;
			Color deselectedFrame = Color.Transparent;
			//deselectedFrame = lightGray;
			//deselectedFrame = Color.Gainsboro;
			//Color controlStructure = Color.White;
			//Color controlStructure = Color.FromArgb(250, 250, 250);
			Color controlStructure = lightGray;
			Color typeName = Color.FromArgb(43, 145, 175);

			#endregion

            Color secondGradient = Color.White;

            Color namespaceGradient = Color.FromArgb(255, 220, 255);
            IFillStyleInfo namespaceBackground = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                namespaceGradient, secondGradient, FillMode.HorizontalGradient);
            IFillStyleInfo namespaceBackgroundSelected = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                namespaceGradient, secondGradient, FillMode.HorizontalGradient);

            Color typeGradient = Color.FromArgb(230, 230, 255);
            IFillStyleInfo typeBackground = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                typeGradient, secondGradient, FillMode.HorizontalGradient);
			IFillStyleInfo typeBackgroundSelected = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                typeGradient, secondGradient, FillMode.HorizontalGradient);

            Color memberGradient = Color.FromArgb(230, 255, 200);
            IFillStyleInfo memberBackground = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                memberGradient, secondGradient, FillMode.HorizontalGradient);
            IFillStyleInfo memberBackgroundSelected = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                memberGradient, secondGradient, FillMode.HorizontalGradient);

            Color fieldGradient = Color.FromArgb(230, 235, 255);
            IFillStyleInfo fieldBackground = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                fieldGradient, secondGradient, FillMode.HorizontalGradient);
            IFillStyleInfo fieldBackgroundSelected = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                fieldGradient, secondGradient, FillMode.HorizontalGradient);

            Color controlStructureGradient = Color.FromArgb(255, 240, 200);
            IFillStyleInfo controlStructureBackground = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                controlStructureGradient, secondGradient, FillMode.HorizontalGradient);
            IFillStyleInfo controlStructureBackgroundSelected = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
                controlStructureGradient, secondGradient, FillMode.HorizontalGradient);

			AddStyle(StyleNames.Transparent,
				Color.Transparent,
				Color.Transparent);
			AddAlias(StyleNames.TransparentSel, StyleNames.Transparent);
			AddAliasAndSelected("Control", StyleNames.Transparent);

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
				deselectedFrame,
				typeBackground);

			AddStyle("ClassBlock_selected",
				grayFrame,
				typeBackgroundSelected);

			AddAliasAndSelected("StructBlock", "ClassBlock");
			AddAliasAndSelected("InterfaceBlock", "ClassBlock");
			AddAliasAndSelected("EnumBlock", "ClassBlock");

			#endregion

			#region Namespace

            AddStyle("NamespaceBlock",
                deselectedFrame,
                namespaceBackground);

            AddStyle("NamespaceBlock_selected",
                grayFrame,
                namespaceBackgroundSelected);

			AddAliasAndSelected("UsingBlock", "NamespaceBlock");

			#endregion

			#region Members

            AddStyle("MethodBlock",
                deselectedFrame,
                memberBackground);

            AddStyle("MethodBlock_selected",
                grayFrame,
                memberBackgroundSelected);

			AddStyle("FieldBlock",
				Color.Transparent,
                Color.Transparent);

			AddStyle("FieldBlock_selected",
				grayFrame,
                fieldBackgroundSelected);

            AddAliasAndSelected("PropertyBlock", "MethodBlock");
            AddAliasAndSelected("PropertyAccessorBlock", "MethodBlock");
			AddAliasAndSelected("InterfaceAccessorsBlock", "FieldBlock");

			#endregion

			AddAliasAndSelected("DelegateBlock", "FieldBlock");

			#region Method

			AddStyle("ControlStructureBlock",
				deselectedFrame,
				controlStructureBackground);

			AddStyle("ControlStructureBlock_selected",
				grayFrame,
				controlStructureBackgroundSelected);

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

			AddStyle("MemberNameBlock",
				Color.Transparent,
				Color.Transparent,
				Color.Black,
				FontStyle.Bold);

			AddStyle("MemberNameBlock_selected",
				Color.PaleGoldenrod,
				Color.LightYellow,
				Color.Black,
				FontStyle.Bold);

            AddStyle("CommentBlock",
                //Color.MintCream,
                //Color.MintCream,
                Color.Transparent,
                Color.Transparent,
                Color.Transparent,
                FillMode.HorizontalGradient);

            AddStyle("CommentBlock_selected",
                Color.Green,
                Color.MintCream,
                secondGradient,
                FillMode.HorizontalGradient);

            AddStyle("CommentLine",
                Color.Transparent,
                Color.Transparent,
                Color.Green, 
                "Arial", 8, FontStyle.Regular);

            AddStyle("CommentLine_selected",
                Color.Transparent,
                Color.Transparent,
                Color.Green,
                "Arial", 8, FontStyle.Regular);

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

			AddStyle("SpaceBlock",
				Color.Transparent,
				Color.Transparent);

			AddStyle("SpaceBlock_selected",
				Color.DarkBlue,
				Color.LightCyan);

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

            AddStyle("AttributeBlock",
                Color.Yellow,
                Color.LightYellow);

            AddStyle("AttributeBlock_selected",
                Color.DarkKhaki,
                Color.Yellow);

			AddStyle("OneWordStatement",
				Color.Transparent,
				Color.Transparent,
                Color.Blue);

			AddStyle("OneWordStatement_selected",
				Color.Gray,
				Color.Yellow,
                Color.Black);

			AddStyle("TextBox",
				Color.Transparent,
				Color.Transparent);

			AddStyle("TextBox_selected",
				Color.Transparent,
				Color.Transparent);

			AddAliasAndSelected("Label", "TextBox");
			AddAliasAndSelected("TextLine", "TextBox");
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
}
