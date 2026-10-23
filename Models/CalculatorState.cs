using CalculatorApp.Patterns.Expressions;

namespace CalculatorApp.Models
{
    public class CalculatorState
    {
        public string Input { get; set; }
        public IExpression Tree { get; set; }
        public int? Result { get; set; }
    }
}