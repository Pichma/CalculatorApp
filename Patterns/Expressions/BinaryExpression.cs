using System;
using System.Text;

namespace CalculatorApp.Patterns.Expressions
{
    public class BinaryExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;
        private readonly Func<int, int, int> _operation;
        private readonly string _symbol;

        public BinaryExpression(IExpression left, IExpression right,
                                Func<int, int, int> op, string symbol)
        {
            _left = left;
            _right = right;
            _operation = op;
            _symbol = symbol;
        }

        public int Interpret()
        {
            return _operation(_left.Interpret(), _right.Interpret());
        }

        public string Display(int indent)
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string(' ', indent) + "Операция: " + _symbol);
            sb.AppendLine(_left.Display(indent + 2));
            sb.Append(_right.Display(indent + 2));
            return sb.ToString();
        }

        public override string ToString()
        {
            return "(" + _left + " " + _symbol + " " + _right + ")";
        }
    }
}