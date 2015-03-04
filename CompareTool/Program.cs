using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please input the path of fileA: ");
            string patha = Console.ReadLine();

            Console.WriteLine("please input the path of fileB: ");
            string pathb = Console.ReadLine();

            List<int> linesA = File.ReadAllLines(patha).Select(int.Parse).ToList();
            List<int> linesB = File.ReadAllLines(pathb).Select(int.Parse).ToList();

            if (linesA.Count != linesB.Count)
            {
                Console.WriteLine("读出的数字数量不同! ");
            }
            else
            {
                Console.WriteLine("读取完毕, 共{0}数字, 开始比较! ", linesA.Count);
                bool same = true;
                for (int i = 0; i < linesA.Count; i++)
                {
                    if (linesA[i] != linesB[i])
                    {
                        same = false;
                        Console.WriteLine("在第{0}次出现不同: {1} vs {2}", i, linesA[i], linesB[i]);
                    }
                }
                if (same)
                {
                    Console.WriteLine("完全相同! ");
                }
            }
            Console.WriteLine("按回车退出");
            Console.ReadLine();
        }
    }
}
