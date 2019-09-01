using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksPractice.Tasks_JAN
{
    internal class Task2_CancelingTasks
    {
        // First part of the lesson
        static void Main1(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            // Register to an event when the user clicked something (First way , there's another below {marked XXXX})
            token.Register(() =>
            {
                Console.WriteLine("Cancellation Has Been Requested!");

            });


            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if (token.IsCancellationRequested)
                    //{
                    //    // one way : 
                    //    //  break;

                    //    // another way
                    //    throw new OperationCanceledException();
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"{i++}\t");
                    //}

                    // a third way is combining the two above ...
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);

            t.Start();


            // {XXXX} Another way to know when the user clicked something 
            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released , cancellation has been requested ...");
            });


            Console.ReadKey();
            cts.Cancel();


            Console.WriteLine("Main program is Done!");
            Console.ReadKey();
        }


        // Second part of the lesson
        static void Main(string[] args)
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(planned.Token,
                preventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                    Thread.Sleep(100);
                }
            }, paranoid.Token);

            Console.ReadKey();
            emergency.Cancel();


            Console.WriteLine("Main program is Done!");
            Console.ReadKey();
        }
    }
}
