using System;
using CalculatorApp.Models;
using CalculatorApp.Patterns.Expressions;

namespace CalculatorApp.Patterns.Commands
{
    public class CalcCommand : ICommand
    {
        private readonly CalculatorState _state;
        private readonly CalculatorState _backup;
        private readonly string _input;
        private readonly Parser _parser;

        public CalcCommand(CalculatorState state, string input)
        {
            _state = state;
            _backup = new CalculatorState
            {
                Input = state.Input,
                Tree = state.Tree,
                Result = state.Result
            };
            _input = input;
            _parser = new Parser();
        }

        public void Execute()
        {
            try
            {
                _state.Input = _input;
                _state.Tree = _parser.Parse(_input);
                _state.Result = _state.Tree.Interpret();
                Console.WriteLine("[Успех] Выражение вычислено: " + _input + " = " + _state.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Ошибка] " + ex.Message);
                _state.Input = null;
                _state.Tree = null;
                _state.Result = null;
            }
        }

        public void Undo()
        {
            _state.Input = _backup.Input;
            _state.Tree = _backup.Tree;
            _state.Result = _backup.Result;
            Console.WriteLine("[Отмена] Состояние возвращено назад.");
        }
    }
}