using System;
using CalculatorApp.Patterns.Expressions;

namespace CalculatorApp.Patterns
{
    public class Parser
    {
        private string _input;
        private int _pos;

        public IExpression Parse(string input)
        {
            _input = input.Replace(" ", "");
            _pos = 0;
            return ParseExpr();
        }

        private IExpression ParseExpr()
        {
            var result = ParseTerm();
            while (_pos < _input.Length && (_input[_pos] == '+' || _input[_pos] == '-'))
            {
                char op = _input[_pos++];
                var right = ParseTerm();
                result = new BinaryExpression(result, right,
                    op == '+' ? (Func<int, int, int>)((a, b) => a + b)
                              : (a, b) => a - b, op.ToString());
            }
            return result;
        }

        private IExpression ParseTerm()
        {
            var result = ParseFactor();
            while (_pos < _input.Length && (_input[_pos] == '*' || _input[_pos] == '/'))
            {
                char op = _input[_pos++];
                var right = ParseFactor();
                result = new BinaryExpression(result, right,
                    op == '*' ? (Func<int, int, int>)((a, b) => a * b)
                              : (a, b) => a / b, op.ToString());
            }
            return result;
        }

        private IExpression ParseFactor()
        {
            if (_pos < _input.Length && _input[_pos] == '(')
            {
                _pos++;
                var result = ParseExpr();
                if (_pos < _input.Length && _input[_pos] == ')') _pos++;
                return result;
            }

            int start = _pos;
            while (_pos < _input.Length && char.IsDigit(_input[_pos])) _pos++;
            if (start == _pos) throw new Exception("Ожидалось число");
            return new NumberExpression(int.Parse(_input.Substring(start, _pos - start)));
        }
    }
}