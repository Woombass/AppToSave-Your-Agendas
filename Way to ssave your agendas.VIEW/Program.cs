using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Way_to_save_your_agendas.Controller;

namespace Way_to_ssave_your_agendas.VIEW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Приложение запущено!");
            Console.ResetColor();
            var aimController = new AimsController();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Выберите желаемое действие. Чтобы это сделать нажмите необходимую клавишу.");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("S - вывести список всех целей.");
                Console.WriteLine("A - добавить новую цель.");
                Console.WriteLine("N - просмотр подцелей конкретной цели.");
                Console.WriteLine("G - добавление подцелей к цели.");

                var ansKey = Console.ReadKey();

                switch (ansKey.Key)
                {
                    case ConsoleKey.S: 
                        Console.Clear();
                        ShowAllAims(aimController);
                        break;
                    case ConsoleKey.A:
                        Console.Clear();
                        AddAim(aimController);
                        break;
                    case ConsoleKey.N:
                        Console.Clear();
                        ShowNotes(aimController);
                        break;
                    case ConsoleKey.G:
                        Console.Clear();
                        AddNote(aimController);
                        break;


                }
            }



            Console.ReadKey();
        }

        private static void AddNote(AimsController aimsController)
        {
            while (true)
            {


                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Режим добавления подцелей.");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Выберите цель для добавления подцели.");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var aim in aimsController.Aims)
                {
                    Console.WriteLine(aim);
                }

                Console.ForegroundColor = ConsoleColor.White;
                var ans = int.Parse(Console.ReadLine());
                aimsController.Choose(ans);
                if (aimsController.CurrentAim == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введён несуществующий ID!");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    var notes = aimsController.GetAimsNotes();
                    if (notes == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("У данной цели ещё нет подцелей.");
                        Console.ResetColor();

                    }
                    else
                    {
                        foreach (var note in notes)
                        {
                            Console.WriteLine(note);
                        }
                    }

                    AddingNewNote:
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Для добавления подцели введите задачу подцели.");
                        Console.ForegroundColor = ConsoleColor.White;
                        var text = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Введите дату предполагаемого выполнения задачи.");
                        Console.ForegroundColor = ConsoleColor.White;
                        var date = DateTime.Parse(Console.ReadLine());
                        aimsController.AddNote(text, date);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Задача добавлена. Желаете добавить ещё одну задачу? Y/N");
                        var ans2 = Console.ReadKey();

                        if (ans2.Key == ConsoleKey.N)
                        {
                            Console.Clear();
                            return;
                            break;
                        }
                        Console.Clear();
                        goto AddingNewNote;

                    }

                }
            }
        }

        private static void ShowNotes(AimsController aimsController)
        {
            while (true)
            {


                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Режим просмотра подцелей.");
                Console.WriteLine("Выберите цель, введя её ID.");
                foreach (var item in aimsController.Aims)
                {
                    Console.WriteLine(item);
                }

                var ans = int.Parse(Console.ReadLine());
                aimsController.Choose(ans);
                if (aimsController.CurrentAim == null)
                {
                    Console.WriteLine("Введён несуществующий ID!");
                    continue;
                }
                else
                {
                    var notes = aimsController.GetAimsNotes();
                    if (notes == null)
                    {
                        Console.WriteLine("У данной цели ещё нет подцелей.");
                    }
                    else
                    {
                        foreach (var note in notes)
                        {
                            Console.WriteLine(note);
                        }
                    }
                }

                Console.WriteLine("Желаете просмотреть другие цели? Y/N");
                var ans2 = Console.ReadKey();

                if (ans2.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    break;

                }
                Console.Clear();

            }
        }
        private static void AddAim(AimsController aimsController)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Режим добавления целей.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Введите название цели: ");
                var aimName = Console.ReadLine();
                Console.WriteLine("\nВведите дату предполагаемого выполнения цели: ");
                var aimTime = DateTime.Parse(Console.ReadLine());

                aimsController.CreateAim(aimName, aimTime);

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("Желате добавить ещё одну цель? Y/N");

                var ansKey = Console.ReadKey();

                if (ansKey.Key == ConsoleKey.N)
                {
                    break;
                }
            }
            Console.ResetColor();
            Console.Clear();

        }
        private static void ShowAllAims(AimsController aimsController)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            if (aimsController.Aims.Any() == false)
            {
                Console.WriteLine("<пусто>");
            }
            else
            {
                foreach (var aim in aimsController.Aims)
                {
                    Console.WriteLine(aim);
                }
            }

            Console.ResetColor();

            Console.WriteLine("\nНажмите любую клавишу для возврата на стартовый экран.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
