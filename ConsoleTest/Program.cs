using System;
using System.Threading.Tasks;
using ETModel;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main");

            var init = new Init();
            Task task = init.Start();
            Task.WaitAll(task);
            Console.WriteLine("Finish");
        }
    }
}
