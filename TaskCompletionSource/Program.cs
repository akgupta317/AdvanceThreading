using System;
using System.Threading;
using System.Threading.Tasks;


namespace TaskCompletionSource
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskCompletionSource<Person>  taskCompletionSource = new  TaskCompletionSource<Person> ();
            Task<Person>  LazyPerson = taskCompletionSource.Task;

            Task.Factory.StartNew(() =>
            {
                taskCompletionSource.SetResult(new Person{ id=1 , name ="Test"});
            });

            Task.Factory.StartNew(() =>
            {
            //    if(Console.ReadKey().KeyChar=='x'){
            //        Person result = LazyPerson.Result;
            //        Console.WriteLine(result.name);
            //    }
               Person result = LazyPerson.Result;
                Console.WriteLine(result.name);
            });

        }  
    }
}
