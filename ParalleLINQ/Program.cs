using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.NetworkInformation;

namespace ParalleLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var list= Enumerable.Range(0,100000);
            var primeNumber = list.AsParallel().Where(isPrime);

            Console.WriteLine("Prime number count " + primeNumber.Count());

            List<string> website= new List<string>();
            website.Add("www.google.com");
            website.Add("www.youtube.com");

           List<PingReply> pingReplies = new List<PingReply>();
           pingReplies= website.AsParallel().WithDegreeOfParallelism(website.Count).Select(pingSite).ToList();   // degreee of parallelism
           foreach (var item in pingReplies)
           {
               Console.WriteLine(item.Address + "" + item.Status);
           }
           //Console.ReadLine();

           var numbers= Enumerable.Range(200,1000000);
           var query=numbers.AsParallel().Where(x=>x%25==0);
           ConcurrentBag<int> concurrentBag= new ConcurrentBag<int>();
           query.ForAll(x=>concurrentBag.Add(x));
           Console.WriteLine(query.Count());
           Console.WriteLine(concurrentBag.Count);

        }

        private static PingReply pingSite(string website)
        {
            Ping ping = new Ping();
            return ping.Send(website);
        }

        public static bool isPrime(int x){
                if(x==1){
                    return false;
                }
                if(x==2){
                    return true;
                }
                if(x%2==0){
                    return false;
                }
                var boundry= (int)Math.Floor(Math.Sqrt(x));
                for (int i = 3; i <=boundry; i++)                {
                    if(x%i==0){
                        return false;
                    }
                }

                return true;
        }
    }
}
