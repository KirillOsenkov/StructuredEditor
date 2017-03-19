using System;

namespace GuiLabs.Utils
{
	public interface ICondition
	{
		bool IsTrue();
	}

	public delegate bool PredicateDelegate();

	public class DelegateCondition : ICondition
	{
		public DelegateCondition(PredicateDelegate predicate)
		{
			Predicate = predicate;
		}

		private PredicateDelegate Predicate;

		public bool IsTrue()
		{
			return Predicate();
		}
	}
}
