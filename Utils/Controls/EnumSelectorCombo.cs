using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace GuiLabs.Utils
{

	public class EnumSelectorCombo<TEnum> : EnumSelectorComboBox
	{
		public EnumSelectorCombo() : base(typeof(TEnum)) { }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TEnum Value
		{
			get
			{
				return (TEnum)Enum.Parse(
					typeof(TEnum),
					this.SelectedItem.ToString());
			}
			set
			{
				this.SelectedItem = value.ToString();
			}
		}
	}

	public partial class EnumSelectorComboBox : ComboBox
	{
		public EnumSelectorComboBox() : base() { }

		public EnumSelectorComboBox(Type enumeration)
			: this()
		{
			this.Enumeration = enumeration;
		}

		private Type mEnumeration;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type Enumeration
		{
			get
			{
				return mEnumeration;
			}
			set
			{
				if (value != null && value != mEnumeration)
				{
					mEnumeration = value;
					FillItems();
				}
			}
		}

		private void FillItems()
		{
			this.Items.Clear();
			this.Items.AddRange(Enum.GetNames(Enumeration));
			this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		}
	}
}
