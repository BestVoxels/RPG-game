namespace RPG.Utils.Core
{
    public interface IPredicateEvaluator
    {
        public bool? Evaluate(PredicateName methodName, string[] parameters);
    }
}