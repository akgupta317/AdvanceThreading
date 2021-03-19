using System;
using System.Threading;
using System.Threading.Tasks;

namespace Signal_CoundDown
{
    class Program
    {   

        private static CountdownEvent countdownEvent = new CountdownEvent(6);
        static void Main(string[] args)
        {   
            // six thread and 6 as a count down .it will continue to wait until you can thread  as a given  number of time
            Task.Factory.StartNew(DoSomething);
            Task.Factory.StartNew(DoSomething);
            Task.Factory.StartNew(DoSomething);
            Task.Factory.StartNew(DoSomething);
            Task.Factory.StartNew(DoSomething);
            Task.Factory.StartNew(DoSomething);
            

            countdownEvent.Wait();
            Console.WriteLine("singnal has been called");
        }

        private static void DoSomething()
        {   Thread.Sleep(250);
            Console.WriteLine(Task.CurrentId + "is callled Singnal");
            countdownEvent.Signal();
        }
    }
}
