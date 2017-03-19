using System.Collections.Generic;
using GuiLabs.Editor.UI;
using GuiLabs.Utils.Collections;
using System;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	partial class LanguageService
	{
		private static string[] mEmbeddedTypes = new string[]
		{
			"bool",
			"byte",
			"char",
			"decimal",
			"double",
			"float",
			"int",
			"long",
			"object",
			"sbyte",
			"short",
			"string",
			"uint",
			"ulong",
			"ushort"
		};

		public string[] EmbeddedTypes
		{
			get
			{
				return mEmbeddedTypes;
			}
		}

		public IEnumerable<TextPictureInfo> GetTypeList(
			ClassOrStructBlock callingClass)
		{
			ListSet<TextPictureInfo> results = new ListSet<TextPictureInfo>();
			FillTypeList(callingClass, results);
			return results;
		}

		public virtual void FillTypeList(
			ClassOrStructBlock callingClass,
			IFillable<TextPictureInfo> typeListToFill)
		{
			foreach (string s in EmbeddedTypes)
			{
				TextPictureInfo result = new TextPictureInfo(s);
				if (s == "object" || s == "string")
				{
					result.Picture = Icons.TypeClass;
				}
				else
				{
					result.Picture = Icons.TypeStruct;
				}
				typeListToFill.Add(result);
			}

			CodeUnitBlock codeUnit = callingClass.Root as CodeUnitBlock;
			if (codeUnit != null && codeUnit.UsingSection != null)
			{
				foreach (UsingDirective u in codeUnit.UsingDirectives)
				{
					CacheUsing(u);
					IList<Type> types = null;
                    Types.TryGetValue(u.Text, out types);
					if (types != null)
					{
						AddTypes(types, typeListToFill);
					}
				}
			}
		}

		public void AddTypes(IList<Type> types, IFillable<TextPictureInfo> results)
		{
			foreach (Type t in types)
			{
				TextPictureInfo typeInfo = new TextPictureInfo(
					t.Name, Icons.GetIconForType(t));
				results.Add(typeInfo);
			}
		}

		public void FillTypeItems(
			ClassOrStructBlock callingClass,
			ICompletionListBuilder items)
		{
			ListSet<TextPictureInfo> result = new ListSet<TextPictureInfo>();
			FillTypeList(callingClass, result);
			foreach (TextPictureInfo t in result)
			{
				items.AddText(t.Text, t.Picture);
			}
		}

		Dictionary<string, IList<Type>> Types = new Dictionary<string, IList<Type>>();

		public virtual void CacheUsing(UsingDirective u)
		{
			string ns = u != null ? u.Text : "";
			if (string.IsNullOrEmpty(u.Text) || Types.ContainsKey(u.Text))
			{
				return;
			}

			List<Type> types = Reflector.FindTypesInNamespace(u.Text);
			Types.Add(u.Text, types);
		}
	}
}
