using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace GuiLabs.Utils
{
	public partial class ObjectViewer : Form
	{
		public ObjectViewer()
		{
			InitializeComponent();
		}

		public static void Show(params object[] objects)
		{
			ObjectViewer viewer = new ObjectViewer();
			viewer.Fill(objects);
			viewer.ShowDialog();
		}

		public void Fill(object o)
		{
			Fill(new object[] { o });
		}

		public void Fill(IEnumerable objects)
		{
			Fill(objects, true);
		}

		public void Fill(IEnumerable objects, bool shouldSaveState)
		{
			if (shouldSaveState)
			{
				SaveState();
			}

			GridProperties.SelectedObject = null;
			ListObjects.BeginUpdate();
			ListObjects.Items.Clear();
			foreach (object o in objects)
			{
				ListObjects.Items.Add(o);
			}
			ListObjects.EndUpdate();

			if (ListObjects.Items.Count > 0)
			{
				ListObjects.SelectedIndex = 0;
			}

			UpdateEnabled();
		}

		private void SaveState()
		{
			List<object> currentList = new List<object>();
			foreach (object o in ListObjects.Items)
			{
				currentList.Add(o);
			}
			history.Push(currentList.ToArray());
		}

		public void ShowProperties(object o)
		{
			GridProperties.SelectedObject = o;
		}

		private void ListObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentObject = ListObjects.SelectedItem;
		}

		private Stack<object[]> history = new Stack<object[]>();

		private object mCurrentObject;
		public object CurrentObject
		{
			get
			{
				return mCurrentObject;
			}
			set
			{
				mCurrentObject = value;
				if (mCurrentObject != null)
				{
					LabelTypeName.Text = CurrentObject.GetType().ToString();
				}
				else
				{
					LabelTypeName.Text = "";
				}
				ShowProperties(CurrentObject);
				FillFields(CurrentObject);
			}
		}

		public void FillFields(object o)
		{
			ListFields.BeginUpdate();
			ListFields.Items.Clear();
			ListFields.DisplayMember = "Name";
			if (o != null)
			{
				Type type = o.GetType();
				while (type != null)
				{
					AddFields(type);
					type = type.BaseType;
				}
			}
			ListFields.EndUpdate();
		}

		private void AddFields(Type type)
		{
			FieldInfo[] fields = type.GetFields(
				System.Reflection.BindingFlags.Instance
				| System.Reflection.BindingFlags.Public
				| System.Reflection.BindingFlags.NonPublic
				);
			foreach (FieldInfo f in fields)
			{
				ListFields.Items.Add(f);
			}
		}

		private void ListFields_MouseClick(object sender, MouseEventArgs e)
		{
			if (CurrentObject != null 
				&& ListFields.SelectedItem != null)
			{
				FieldInfo f = ListFields.SelectedItem as FieldInfo;
				if (f != null)
				{
					try
					{
						object newObj = f.GetValue(CurrentObject);
						if (newObj != null)
						{
							if (newObj is IEnumerable)
							{
								Fill(newObj as IEnumerable);
							}
							else
							{
								Fill(newObj);
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
		}

		private void ButtonBack_Click(object sender, EventArgs e)
		{
			if (history == null || history.Count == 0)
			{
				return;
			}

			object[] objects = history.Pop();
			Fill(objects, false);
		}

		private void UpdateEnabled()
		{
			ButtonBack.Enabled = history != null && history.Count > 1;
		}
	}
}
