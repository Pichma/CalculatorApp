using System;
using System.Collections.Generic;
using CalculatorApp.Models;
using CalculatorApp.Patterns.Commands;

namespace CalculatorApp
{
    class Program
    {
        static void Main()
        {
            var state = new CalculatorState();
            var history = new Stack<ICommand>();

            Console.WriteLine("========================================");
            Console.WriteLine("  Калькулятор выражений с отменой");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("  calc <выражение> - вычислить выражение");
            Console.WriteLine("  tree             - показать дерево выражения");
            Console.WriteLine("  undo             - отменить последнюю операцию");
            Console.WriteLine("  clear            - очистить состояние");
            Console.WriteLine("  exit             - выйти из программы");
            Console.WriteLine();
            Console.WriteLine("Пример: calc ((2+3)*4-5)");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");

            while (true)
            {
                Console.Write("\nВведите команду> ");
                string input = Console.ReadLine();
                
                if (input == null)
                {
                    break;
                }
                
                input = input.Trim();

                if (input == "exit")
                {
                    Console.WriteLine();
                    Console.WriteLine("Завершение работы программы...");
                    Console.WriteLine("Нажмите любую клавишу для выхода.");
                    Console.ReadKey();
                    break;
                }
                else if (input.StartsWith("calc "))
                {
                    string expr = input.Substring(5);
                    var cmd = new CalcCommand(state, expr);
                    cmd.Execute();
                    history.Push(cmd);
                }
                else if (input == "tree")
                {
                    if (state.Tree != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("--- Дерево выражения ---");
                        Console.WriteLine(state.Tree.Display(0));
                        Console.WriteLine("------------------------");
                    }
                    else
                    {
                        Console.WriteLine("Дерево выражений пусто. Сначала выполните calc.");
                    }
                }
                else if (input == "undo")
                {
                    if (history.Count > 0)
                    {
                        history.Pop().Undo();
                    }
                    else
                    {
                        Console.WriteLine("[Отмена] Нечего отменять.");
                    }
                }
                else if (input == "clear")
                {
                    state.Input = null;
                    state.Tree = null;
                    state.Result = null;
                    history.Clear();
                    Console.WriteLine("[Очистка] Калькулятор сброшен.");
                }
                else
                {
                    Console.WriteLine("Неизвестная команда. Введите 'exit' для выхода.");
                }
            }
        }
    }
}