namespace CalculatorApp.Patterns.Expressions
{
    public class NumberExpression : IExpression
    {
        private readonly int _value;

        public NumberExpression(int value)
        {
            _value = value;
        }

        public int Interpret()
        {
            return _value;
        }

        public string Display(int indent)
        {
            return new string(' ', indent) + "Число: " + _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}