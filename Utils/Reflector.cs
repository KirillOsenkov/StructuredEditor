using System;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace GuiLabs.Utils
{
	public class Reflector
	{
		public static Assembly[] GetLoadedAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		public static List<Type> FindTypesInNamespace(string ns)
		{
			List<Type> result = new List<Type>();
			foreach (Assembly a in GetLoadedAssemblies())
			{
				foreach (Type t in a.GetExportedTypes())
				{
					if (t.Namespace == ns)
					{
						result.Add(t);
					}
				}
			}
			return result;
		}

		public static List<Type> FindTypesInNamespace(Assembly searchAssembly, string ns)
		{
			List<Type> result = new List<Type>();
			foreach (Type t in searchAssembly.GetExportedTypes())
			{
				if (t.Namespace == ns)
				{
					result.Add(t);
				}
			}
			return result;
		}

		public static ReadOnlyCollection<TypeAttributeInfo<AttributeType>> FindTypesWithAttribute<AttributeType>(IEnumerable<Assembly> assembliesToSearch)
			where AttributeType : Attribute
		{
			List<TypeAttributeInfo<AttributeType>> result = new List<TypeAttributeInfo<AttributeType>>();
			Type attributeType = typeof(AttributeType);

			foreach (Assembly ass in assembliesToSearch)
			{
				if (!Attribute.IsDefined(ass, typeof(AttributeType)))
				{
					continue;
				}
				foreach (Type t in ass.GetTypes())
				{
					AttributeType foundAttr = GetAttribute<AttributeType>(t);
					if (foundAttr != null)
					{
						TypeAttributeInfo<AttributeType> found = new TypeAttributeInfo<AttributeType>();
						found.TypeInfo = t;
						found.AttrInfo = foundAttr;
						result.Add(found);
					}
				}
			}

			return result.AsReadOnly();
		}

		public static AttributeType GetAttribute<AttributeType>(Type typeToInspect)
			where AttributeType : Attribute
		{
			object[] attrs = typeToInspect.GetCustomAttributes(typeof(AttributeType), false);
			if (attrs != null && attrs.Length > 0)
			{
				return attrs[0] as AttributeType;
			}
			return null;
		}
	}

	public class TypeAttributeInfo<AttributeType>
		where AttributeType : System.Attribute
	{
		public Type TypeInfo;
		public AttributeType AttrInfo;
	}
}
