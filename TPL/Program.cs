using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static void Main(string[] args)
        {   
            Stopwatch  stopWatch = new Stopwatch();
             Console.WriteLine("when to not use thread");
           for (int i = 0; i < 10; i++)
           {
               //Console.WriteLine(i);
           }
            Console.WriteLine(stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
            stopWatch.Start();

            Parallel.For(0,10,i=>{
                 //Console.WriteLine(i);
            });
            Console.WriteLine(stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();


            ////Parallel Option and cancellations
            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken=cancellationToken.Token;
            parallelOptions.MaxDegreeOfParallelism= System.Environment.ProcessorCount;

            var list = Enumerable.Range(0,10000000).ToArray();
            Console.WriteLine("press X for Cancelling thread");

            Task.Factory.StartNew(()=>{ 
                if(Console.ReadKey().KeyChar=='x'){

                    cancellationToken.Cancel();
                }

            });

            long Total=0;

            try
            {
                Parallel.For<long>(0,list.Length,parallelOptions,()=>0,(count,parallelLoopState,subtotal)=>{
                        parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                        subtotal+=list[count];  //0+1+2+..9=45
                        return subtotal;
                },
                (x)=>{
                    Console.WriteLine(Interlocked.Add(ref Total,x));
                });
                
            }
            catch (System.OperationCanceledException)
            {
                
                throw;
            }
            Console.WriteLine("final total " + Total);
        }

    }
}
