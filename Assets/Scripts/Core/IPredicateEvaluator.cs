namespace RPG.Core
{
    public interface IPredicateEvaluator
    {
        public bool? Evaluate(string predicate, string[] parameters);
    }
}