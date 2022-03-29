namespace RPG.Core
{
    public interface IPredicateEvaluator
    {
        public bool? Evaluate(string methodName, string[] parameters);
    }
}