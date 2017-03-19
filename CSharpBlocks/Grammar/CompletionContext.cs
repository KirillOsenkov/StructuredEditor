namespace GuiLabs.Editor.CSharp
{
	public class CompletionContext
	{
		public static CompletionContext MethodParameters = new CompletionContext();
		public static CompletionContext Statement = new CompletionContext();
		public static CompletionContext ForInitializer = new CompletionContext();
		public static CompletionContext ForCondition = new CompletionContext();
		public static CompletionContext ForIncrementStep = new CompletionContext();
		public static CompletionContext ForeachType = new CompletionContext();
		public static CompletionContext ForeachCollection = new CompletionContext();
		public static CompletionContext BooleanExpression = new CompletionContext();
		public static CompletionContext ObjectExpression = new CompletionContext();
	}
}
