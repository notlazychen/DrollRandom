using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RandomCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please type the random seed: ");
            uint seed = uint.Parse(Console.ReadLine());

            DrollRandom.DrollRandom r1 = new DrollRandom.DrollRandom(seed);
            //DrollRandom r2 = new DrollRandom(1);

            Console.WriteLine("please type the min value: ");
            int minValue = int.Parse(Console.ReadLine());
            Console.WriteLine("please type the max value: ");
            int maxValue = int.Parse(Console.ReadLine());

            Console.WriteLine("how many times? ");
            int times = int.Parse(Console.ReadLine());


            using (StreamWriter file = new StreamWriter(@"filecs.txt"))
            {
                for (int i = 0; i < times; i++)
                {
                    int x1 = r1.Next(minValue, maxValue);
                    //int x2 = r2.Next(0, 100000000);
                    //Console.WriteLine("r1:{0}, r2:{1}", x1, x2);
                    //if (x1 != x2)
                    //{
                    //    Console.WriteLine("Error---{0}", i);
                    //    Console.ReadLine();
                    //}
                    //Thread.Sleep(500);
                    file.WriteLine(x1);
                }
            }

            Console.WriteLine("over");

            Console.ReadLine();
        }
    }
}
