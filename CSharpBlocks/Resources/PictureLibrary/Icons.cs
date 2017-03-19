using System.Drawing;
using System;

namespace GuiLabs.Editor.CSharp
{
	public static class Icons
	{
		public static Image GetIconForType(ClassType type, string modifier)
		{
			switch (type)
			{
				case ClassType.Class:
					return GetIconForClass(modifier);
				case ClassType.Enum:
					return GetIconForEnum(modifier);
				case ClassType.Interface:
					return GetIconForInterface(modifier);
				case ClassType.Struct:
					return GetIconForStruct(modifier);
				case ClassType.Delegate:
					return GetIconForDelegate(modifier);
				case ClassType.Module:
				default:
					return GetIconForClass(modifier);
			}
		}

		public static Image GetIconForType(Type reflectionType)
		{
			if (reflectionType.IsClass)
			{
				return Icons.TypeClass;
			}
			else if (reflectionType.IsValueType)
			{
				return Icons.TypeStruct;
			}
			else if (reflectionType.IsInterface)
			{
				return Icons.TypeInterface;
			}
			else if (reflectionType.IsEnum)
			{
				return Icons.TypeEnum;
			}
			else
			{
				return Icons.TypeDelegate;
			}
		}

		public static Image MethodPrivate = Resources.MethodPrivate;
		public static Image MethodProtected = Resources.MethodProtected;
		public static Image MethodInternal = Resources.MethodInternal;
		public static Image MethodPublic = Resources.MethodPublic;

		public static Image GetIconForMethod(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return MethodPrivate;
				case "internal":
					return MethodInternal;
				case "public":
					return MethodPublic;
				default:
					return MethodProtected;
			}
		}

		public static Image PropertyPrivate = Resources.PropertyPrivate;
		public static Image PropertyProtected = Resources.PropertyProtected;
		public static Image PropertyInternal = Resources.PropertyInternal;
		public static Image PropertyPublic = Resources.PropertyPublic;

		public static Image GetIconForProperty(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return PropertyPrivate;
				case "internal":
					return PropertyInternal;
				case "public":
					return PropertyPublic;
				default:
					return PropertyProtected;
			}
		}

		public static Image FieldPrivate = Resources.FieldPrivate;
		public static Image FieldProtected = Resources.FieldProtected;
		public static Image FieldInternal = Resources.FieldInternal;
		public static Image FieldPublic = Resources.FieldPublic;

		public static Image GetIconForField(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return FieldPrivate;
				case "internal":
					return FieldInternal;
				case "public":
					return FieldPublic;
				default:
					return FieldProtected;
			}
		}

		public static Image EventPrivate = Resources.EventPrivate;
		public static Image EventProtected = Resources.EventProtected;
		public static Image EventInternal = Resources.EventInternal;
		public static Image EventPublic = Resources.EventPublic;

		public static Image GetIconForEvent(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return EventPrivate;
				case "internal":
					return EventInternal;
				case "public":
					return EventPublic;
				default:
					return EventProtected;
			}
		}
		
		public static Image TypeClassPrivate = Resources.TypeClassPrivate;
		public static Image TypeClassProtected = Resources.TypeClassProtected;
		public static Image TypeClassInternal = Resources.TypeClassInternal;
		public static Image TypeClass = Resources.TypeClass;

		public static Image GetIconForClass(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return TypeClassPrivate;
				case "internal":
					return TypeClassInternal;
				case "public":
					return TypeClass;
				default:
					return TypeClassProtected;
			}
		}

		public static Image TypeStructPrivate = Resources.TypeStructPrivate;
		public static Image TypeStructProtected = Resources.TypeStructProtected;
		public static Image TypeStructInternal = Resources.TypeStructInternal;
		public static Image TypeStruct = Resources.TypeStruct;

		public static Image GetIconForStruct(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return TypeStructPrivate;
				case "internal":
					return TypeStructInternal;
				case "public":
					return TypeStruct;
				default:
					return TypeStructProtected;
			}
		}

		public static Image TypeInterfacePrivate = Resources.TypeInterfacePrivate;
		public static Image TypeInterfaceProtected = Resources.TypeInterfaceProtected;
		public static Image TypeInterfaceInternal = Resources.TypeInterfaceInternal;
		public static Image TypeInterface = Resources.TypeInterface;

		public static Image GetIconForInterface(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return TypeInterfacePrivate;
				case "internal":
					return TypeInterfaceInternal;
				case "public":
					return TypeInterface;
				default:
					return TypeInterfaceProtected;
			}
		}

		public static Image TypeEnumPrivate = Resources.TypeEnumPrivate;
		public static Image TypeEnumProtected = Resources.TypeEnumProtected;
		public static Image TypeEnumInternal = Resources.TypeEnumInternal;
		public static Image TypeEnum = Resources.TypeEnum;

		public static Image GetIconForEnum(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return TypeEnumPrivate;
				case "internal":
					return TypeEnumInternal;
				case "public":
					return TypeEnum;
				default:
					return TypeEnumProtected;
			}
		}

		public static Image TypeDelegatePrivate = Resources.TypeDelegatePrivate;
		public static Image TypeDelegateProtected = Resources.TypeDelegateProtected;
		public static Image TypeDelegateInternal = Resources.TypeDelegateInternal;
		public static Image TypeDelegate = Resources.TypeDelegate;

		public static Image GetIconForDelegate(string modifier)
		{
			switch (modifier)
			{
				case "private":
					return TypeDelegatePrivate;
				case "internal":
					return TypeDelegateInternal;
				case "public":
					return TypeDelegate;
				default:
					return TypeDelegateProtected;
			}
		}

		public static Image Variable = Resources.Variable;
		public static Image Parameter = Resources.Parameter;
		public static Image CodeSnippet = Resources.CodeSnippet;
		public static Image Keyword = Resources.Keyword;
		public static Image Namespace = Resources.Namespace;
	}
}
