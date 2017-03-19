using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class BaseVisitor : IVisitor
	{
		public virtual void VisitContainer(ContainerBlock block)
		{
			foreach (Block child in block.Children)
			{
				ICSharpBlock visitable = child as ICSharpBlock;
				if (visitable != null)
				{
					visitable.AcceptVisitor(this);
				}
			}
		}

		public virtual void VisitContainerRecursive(ContainerBlock block)
		{
			foreach (Block child in block.Children)
			{
				ICSharpBlock visitable = child as ICSharpBlock;
				if (visitable != null)
				{
					visitable.AcceptVisitor(this);
				}
				else
				{
					ContainerBlock container = child as ContainerBlock;
					if (container != null)
					{
						VisitContainerRecursive(container);
					}
				}
			}
		}

		public virtual void Visit(CodeUnitBlock block)
		{
			
		}

		public virtual void Visit(CodeBlock block)
		{

		}

		public virtual void Visit(NamespaceBlock block)
		{

		}

		public virtual void Visit(UsingBlock block)
		{

		}

		public virtual void Visit(UsingDirective block)
		{

		}

		public virtual void Visit(EmptyNamespaceBlock block)
		{

		}

		public virtual void Visit(ClassBlock block)
		{

		}

		public virtual void Visit(EmptyClassMember block)
		{

		}

		public virtual void Visit(MethodBlock block)
		{

		}

		public virtual void Visit(ParameterListBlock block)
		{

		}

		public virtual void Visit(StructBlock block)
		{
			
		}

		public virtual void Visit(InterfaceBlock block)
		{
			
		}

		public virtual void Visit(EnumBlock block)
		{
			
		}

		public virtual void Visit(EnumValue block)
		{

		}

		public virtual void Visit(DelegateBlock block)
		{
			
		}

		public virtual void Visit(PropertyBlock block)
		{
			
		}

		public virtual void Visit(PropertyAccessorBlock block)
		{
			
		}

		public virtual void Visit(ConstructorBlock block)
		{
			
		}

		public virtual void Visit(FieldBlock block)
		{

		}

		public virtual void Visit(BlockStatementBlock block)
		{

		}

		public virtual void Visit(ForBlock block)
		{
			
		}

		public virtual void Visit(ForeachBlock block)
		{
			
		}

		public virtual void Visit(IfBlock block)
		{
			
		}

		public virtual void Visit(WhileBlock block)
		{
			
		}

		public virtual void Visit(ElseBlock block)
		{
			
		}

		public virtual void Visit(CodeLine block)
		{

		}

		public virtual void Visit(InterfaceMemberDeclarationBlock block)
		{
			
		}

		public virtual void Visit(InterfaceMemberTextBlock block)
		{
			
		}

		public virtual void Visit(InterfaceAccessorsBlock block)
		{
			
		}

		public virtual void Visit(DoWhileBlock block)
		{
			
		}

		public virtual void Visit(UsingStatementBlock block)
		{
			
		}

		public virtual void Visit(TryBlock block)
		{
			
		}

		public virtual void Visit(CatchBlock block)
		{
			
		}

		public virtual void Visit(FinallyBlock block)
		{
			
		}

		public virtual void Visit(BreakStatement block)
		{
			
		}

		public virtual void Visit(ContinueStatement block)
		{
			
		}

		public virtual void Visit(LockBlock block)
		{
			
		}

        public virtual void Visit(CommentBlock block)
        {

        }

        public virtual void Visit(CommentLine block)
        {

        }
	}
}
