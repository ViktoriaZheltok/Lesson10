using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson10
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = "";

            Client client = new Client(type);

            for (int i = 0; i < 10; i++)
            {
                if (i < 7)
                {
                    type = "adult";
                }
                else if (i < 9)
                {
                    type = "pensioner";
                }
                else
                {
                    type = "invalid";
                }
            }

            Console.ReadLine();
        }
    }

    class Client
    {
        public string Type { get; set; }
        static Semaphore sem = new Semaphore(2, 2);
        Thread myThread;

        public Client(string type)
        {
            myThread = new Thread(Ask);
            myThread.Name = $"Client {type}";
            if (type == "adult")
            {
                myThread.Priority = ThreadPriority.Normal;
            }
            else if (type == "pensioner" || type == "invalid")
            {
                myThread.Priority = ThreadPriority.Highest;
            }
            myThread.Start();
        }

        public void Ask()
        {

            sem.WaitOne();

            Console.WriteLine($"{Thread.CurrentThread.Name} takes a queue");
            Thread.Sleep(2000);

            Console.WriteLine($"{Thread.CurrentThread.Name} asks a question");
            Thread.Sleep(1000);

            Console.WriteLine($"{Thread.CurrentThread.Name} leaves the queue");

            sem.Release();

            Thread.Sleep(1000);


        }
    }
}
