using System;
using twinklebear_dev_sdl2_tutorial.Lessons;

namespace twinklebear_dev_sdl2_tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            ILesson lesson;

            Console.Write("Lesson number: ");
            var info = Console.ReadKey();

            switch(info.KeyChar)
            {
                case '1':
                    lesson = new Lessons.Lesson1.Lesson();
                    break;

                case '2':
                    lesson = new Lessons.Lesson2.Lesson();
                    break;

                default:
                    throw new NotImplementedException();
            }

            lesson.Execute();
        }
    }
}
