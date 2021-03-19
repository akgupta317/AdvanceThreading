using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class  CFG {

    private static EventWaitHandle cazton  = new EventWaitHandle(false,EventResetMode.ManualReset);

    public static void Main(){
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);

        Thread.Sleep(1000);
        Console.WriteLine("Press key to release all thread");
        //Console.ReadLine();
        cazton.Set();  // release

        Thread.Sleep(1000); // this time they will not stop since we have already Set them we need to reset them
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);

        Thread.Sleep(1000); // this time they will  stop since we have reset them
        cazton.Reset();
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);
        Task.Factory.StartNew(CallwaitOne);

        Console.ReadLine();
        cazton.Set();  //release all threase again since they are blocked
    }

    private static void CallwaitOne()
    {
       Console.WriteLine(Task.CurrentId + " has called WaitOne");
       cazton.WaitOne();   //blocks if not set
        Console.WriteLine(Task.CurrentId + " Finally Ended");
    }
}