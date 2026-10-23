namespace CalculatorApp.Patterns.Expressions
{
    public interface IExpression
    {
        int Interpret();
        string Display(int indent);
    }
}