using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	/// <summary>
	/// Base interface to encapsulate an operation,
	/// which can be applied to all elements in the block tree.
	/// </summary>
	/// <example>
	/// This can be any operation, such as e.g. CodeGeneratingVisitor,
	/// XmlSaveVisitor, PrintingVisitor etc.
	/// </example>
	public interface IVisitor
	{
		// Code compile unit
		void Visit(CodeUnitBlock block);

		// Containers
		void Visit(CodeBlock block);

		// Namespace
		void Visit(NamespaceBlock block);
		void Visit(EmptyNamespaceBlock block);
		void Visit(UsingBlock block);
		void Visit(UsingDirective block);

		// Types
		void Visit(ClassBlock block);
		void Visit(StructBlock block);
		void Visit(InterfaceBlock block);
		void Visit(EnumBlock block);
		void Visit(EnumValue block);
		void Visit(DelegateBlock block);
		void Visit(EmptyClassMember block);

		// Members
		void Visit(MethodBlock block);
		void Visit(ConstructorBlock block);
		void Visit(PropertyBlock block);
		void Visit(PropertyAccessorBlock block);
		void Visit(FieldBlock block);

		void Visit(ParameterListBlock block);

		void Visit(InterfaceMemberDeclarationBlock block);
		void Visit(InterfaceMemberTextBlock block);
		void Visit(InterfaceAccessorsBlock block);

		void Visit(BlockStatementBlock block);

		// Control structures
		void Visit(ForBlock block);
		void Visit(ForeachBlock block);
		void Visit(IfBlock block);
		void Visit(WhileBlock block);
		void Visit(DoWhileBlock block);
		void Visit(ElseBlock block);
		void Visit(UsingStatementBlock block);
		void Visit(TryBlock block);
		void Visit(CatchBlock block);
		void Visit(FinallyBlock block);
		void Visit(LockBlock block);

		void Visit(BreakStatement block);
		void Visit(ContinueStatement block);

		void Visit(CodeLine block);
        void Visit(CommentBlock block);
        void Visit(CommentLine block);
	}
}
