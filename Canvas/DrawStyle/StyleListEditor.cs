using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Canvas.DrawStyle
{
	public partial class StyleListEditor : UserControl
	{
		public StyleListEditor()
		{
			InitializeComponent();
		}

		public event EventHandler Changed;
		public void RaiseChanged()
		{
			if (Changed != null)
			{
				Changed(this, new EventArgs());
			}
		}

		public void Fill(IStyleFactory factory)
		{
			Set<IShapeStyle> added = new Set<IShapeStyle>();
			ListStyles.BeginUpdate();
			ListStyles.Items.Clear();
			foreach (IShapeStyle style in factory)
			{
				if (!added.Contains(style))
				{
					ListStyles.Items.Add(style);
					added.Add(style);
				}
			}
			ListStyles.EndUpdate();
			PropertiesStyle.SelectedObject = null;
		}

		private void ListStyles_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ListStyles.SelectedItems.Count <= 0)
			{
				PropertiesStyle.SelectedObject = null;
				return;
			}
			object[] selection = new object[ListStyles.SelectedItems.Count];
			ListStyles.SelectedItems.CopyTo(selection, 0);
			PropertiesStyle.SelectedObjects = selection;
		}

		private void PropertiesStyle_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			RaiseChanged();
		}

		private void PropertiesStyle_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
		{
			//RaiseChanged();
		}
	}
}
