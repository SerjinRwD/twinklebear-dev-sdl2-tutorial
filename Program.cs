using System;
using System.Collections.Generic;
using twinklebear_dev_sdl2_tutorial.Lessons;

namespace twinklebear_dev_sdl2_tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            ILesson lesson;
            Dictionary<char, Type> lessons = new Dictionary<char, Type>
            {
                { '1', typeof(Lessons.Lesson1.Lesson) },
                { '2', typeof(Lessons.Lesson2.Lesson) },
                { '3', typeof(Lessons.Lesson3.Lesson) },
                { '4', typeof(Lessons.Lesson4.Lesson) },
                { '5', typeof(Lessons.Lesson5.Lesson) },
                { '6', typeof(Lessons.Lesson6.Lesson) },
            };

            Console.WriteLine("Type lesson number (or 'q' to exit):");
            var info = Console.ReadKey();
            Console.WriteLine();

            if(info.KeyChar == 'q')
            {
                Console.WriteLine("Bye!");
            }
            else if(lessons.ContainsKey(info.KeyChar))
            {
                Console.WriteLine($"Starting lesson '{info.KeyChar}'...");

                lesson = (ILesson)Activator.CreateInstance(lessons[info.KeyChar]);
                lesson.Execute();
            }
            else
            {
                Console.WriteLine("Unrecognized command.");
            }
        }
    }
}
