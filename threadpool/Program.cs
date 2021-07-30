using System;
using System.Threading;
using System.Threading.Tasks;

namespace threadpool
{
    internal class Program
    {
        private static void BackgroundTask(Object stateInfo)
        {
            Console.WriteLine("Hello! I'm a worker from ThreadPool");
            Thread.Sleep(1000);
        }

        private static void BackgroundTaskWithObject(Object stateInfo)
        {
            Person data = (Person)stateInfo;
            Console.WriteLine($"Hi {data.Name} from ThreadPool.");
            Thread.Sleep(1000);
        }

        private static void DoWork(Object stateInfo)
        {
            Console.WriteLine($"[${DateTime.Now}] - Do work: {(int)stateInfo}");
            Thread.Sleep(5);
            Console.WriteLine($"[${DateTime.Now}] - Did work: {(int)stateInfo}");
        }

        private static void DoWork(Object stateInfo, SemaphoreSlim sm)
        {
            Console.WriteLine($"[${DateTime.Now}] - Do work: {(int)stateInfo}");
            Thread.Sleep(5);
            Console.WriteLine($"[${DateTime.Now}] - Did work: {(int)stateInfo}");

            sm.Release();
        }

        private static void Main(string[] args)
        {
            int workers, ports;
            ThreadPool.GetMaxThreads(out workers, out ports);

            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(2, 2);

            //SemaphoreSlim sm = new SemaphoreSlim(2, 2);

            //for (int i = 0; i < 10_000; i++)
            //{
            //    sm.Wait();
            //    int j = i;
            //    var t = new Thread(() => DoWork(j, sm));
            //    t.Start();
            //}

            for (int i = 0; i < 10_000; i++)
                ThreadPool.QueueUserWorkItem(DoWork, i);

            while (ThreadPool.PendingWorkItemCount > 0) ;

            //ThreadPool.QueueUserWorkItem(BackgroundTask);
            //Console.WriteLine("Main thread does some work, then sleeps");
            //Thread.Sleep(500);
            //Console.WriteLine("Main thread exits");
            //Console.ReadKey();

            //Person p = new Person("Lucas", 29, "male");
            //ThreadPool.QueueUserWorkItem(BackgroundTaskWithObject, p);

            //ThreadPool.QueueUserWorkItem(BackgroundTask);
            //Console.WriteLine("Main thread does some work, then sleeps");
            //Thread.Sleep(500);

            //Person p = new Person("Lucas", 29, "male");
            //ThreadPool.QueueUserWorkItem(BackgroundTaskWithObject, p);

            //ThreadPool.GetMaxThreads(out workers, out ports);
            //Console.WriteLine($"Maximum worker threads: {workers}");
            //Console.WriteLine($"Maximum completion port threads: {ports}");

            //ThreadPool.GetAvailableThreads(out workers, out ports);
            //Console.WriteLine($"Available worker threads: {workers}");
            //Console.WriteLine($"Available completion port threads: {ports}");

            //int minWorker, minIOC;
            //ThreadPool.GetMinThreads(out minWorker, out minIOC);
            //ThreadPool.SetMinThreads(4, minIOC);

            //int processCount = Environment.ProcessorCount;
            //Console.WriteLine($"No. of processes available on the system: {processCount}");

            //// Get minimum number of threads
            //ThreadPool.GetMinThreads(out workers, out ports);
            //Console.WriteLine($"Minimum worker threads: {workers} ");
            //Console.WriteLine($"Minimum completion port threads: {ports}");

            Console.ReadKey();
        }

        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Sex { get; set; }

            public Person(string name, int age, string sex)
            {
                this.Name = name;
                this.Age = age;
                this.Sex = sex;
            }
        }
    }
}