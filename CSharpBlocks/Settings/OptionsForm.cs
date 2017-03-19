using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using UniversalControlDesign = GuiLabs.Canvas.Controls.UniversalControl.UniversalControlDesign;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.UI;
using GuiLabs.Canvas.DrawStyle;
using System.Collections;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.CSharp
{
	public partial class OptionsForm : Form
	{
		#region ctor

		public OptionsForm(CodeUnitBlock codeUnit)
		{
			InitializeComponent();

			CodeUnit = codeUnit;

			DesignOfUniversalControlCombo.SelectedValueChanged += DesignOfUniversalControlCombo_SelectedValueChanged;
            DesignOfUniversalControlCombo.Value = Settings.Current.DesignOfUniversalControl;

			CurlyCombo.SelectedItem = Settings.Current.CurliesInUniversalControl.ToString();
			CurlyCombo.SelectedValueChanged += CurlyCombo_SelectedValueChanged;

			FontSizeBox.Value = Settings.Current.FontSize;

			StylesProperties.Fill(StyleFactory.Instance);
			StylesProperties.Changed += StylesProperties_Changed;
		}

		void StylesProperties_Changed(object sender, EventArgs e)
		{
			CodeUnit.Redraw();
		}

		#endregion

		private CodeUnitBlock CodeUnit;

		#region Dialog management

		private void OptionsForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Return)
			{
				Close();
			}
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		public static void ShowOptions(CodeUnitBlock codeUnit)
		{
			(new OptionsForm(codeUnit)).ShowDialog();
		}

		#endregion

		#region OnValueChanged

		void CurlyCombo_SelectedValueChanged(object sender, EventArgs e)
		{
			UniversalControl.TypeOfCurlies value = CurlyCombo.Value;
			Settings.Current.CurliesInUniversalControl = value;
			using (new Redrawer(CodeUnit))
			{
				foreach (UniversalBlock b in CodeUnit.FindChildrenRecursive<UniversalBlock>())
				{
					b.MyUniversalControl.CurlyType = value;
					b.MyUniversalControl.UpdateOffsetCurlies();
				}
			}
		}

		void DesignOfUniversalControlCombo_SelectedValueChanged(object sender, EventArgs e)
		{
			Settings.Current.DesignOfUniversalControl = DesignOfUniversalControlCombo.Value;
			using (new Redrawer(CodeUnit))
			{
				foreach (UniversalBlock b in CodeUnit.FindChildrenRecursive<UniversalBlock>())
				{
					b.MyUniversalControl.UpdateOffsetCurlies();
				}
			}
		}

		private void FontSizeBox_ValueChanged(object sender, EventArgs e)
		{
			int size = (int)FontSizeBox.Value;
			Settings.Current.FontSize = size;

			foreach (IShapeStyle s in StyleFactory.Instance)
			{
				if (s.FontSize != size)
				{
					s.FontSize = size;
				}
			}

			CodeUnit.MyControl.LayoutAll();
			CodeUnit.Redraw();
		}

		#endregion

		private static string FilterStylesheets = "Block stylesheets (*.xml)|*.xml";

		private void WriteToFileButton_Click(object sender, EventArgs e)
		{
			string fileName = Common.GetSaveFileName(FilterStylesheets);
			if (!string.IsNullOrEmpty(fileName))
			{
				StyleFactory.Instance.SaveToFile(fileName);
			}
		}

		private void LoadFromFileButton_Click(object sender, EventArgs e)
		{
			string fileName = Common.GetOpenFileName(FilterStylesheets);
			if (string.IsNullOrEmpty(fileName))
			{
				return;
			}
			StyleFactory styles = StyleFactory.LoadFromFile(fileName);
			StyleFactory.Instance = styles;
			StylesProperties.Fill(StyleFactory.Instance);
			CodeUnit.ReloadAllStyles();
			CodeUnit.Redraw();
		}

		#region Old

		//void FilterCombo_SelectedValueChanged(object sender, EventArgs e)
		//{
		//    Settings.Current.BlockFilter = FilterCombo.Value;
		//}

		//public void FillUndoBufferList(IEnumerable<IAction> actionList)
		//{
		//    ListboxActions.Items.Clear();
		//    foreach (IAction CurrentAction in actionList)
		//    {
		//        ListboxActions.Items.Add(CurrentAction);
		//    }
		//}

		#endregion

	}

	public class UniversalControlDesignCombo : EnumSelectorCombo<UniversalControlDesign>
	{

	}

	public class CurlyTypeCombo : EnumSelectorCombo<UniversalControl.TypeOfCurlies>
	{

	}
}