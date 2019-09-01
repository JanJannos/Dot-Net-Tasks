using System;
using System.Threading.Tasks;

namespace TasksPractice
{

    // Creating and testing tasks

    internal class Program
    {
        public static void Write(char c)
        {

            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object oo)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(oo);
            }
        }

        public static int TextLength(object oo)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} is processing object {oo}");
            return oo.ToString().Length;
        }

        static void Main1(string[] args)
        {
            //Task.Factory.StartNew(() => Write('.'));
            //var t = new Task(() => Write('?')); // 
            //t.Start();
            //Write('-');
            //Console.WriteLine("Hello World!");

            //Task t = new Task(Write , "hello");
            //t.Start();
            //Task.Factory.StartNew(Write, 123);

            string text1 = "texting", text2 = "this";
            var task1 = new Task<int>(TextLength , text1);
            task1.Start();
            Func<object, int> textLength = TextLength;
            Task<int> task2 = Task.Factory.StartNew<int>(textLength, text2); 

            // waiting for both tasks 

            Console.WriteLine($"The length of '{text1}' is {task1.Result}");
            Console.WriteLine($"The length of '{text2}' is {task2.Result}");


            Console.WriteLine("Main Program Done !");
            Console.ReadKey();
        }
    }
}
