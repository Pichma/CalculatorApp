namespace CalculatorApp.Patterns.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}