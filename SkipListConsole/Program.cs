using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkipListLib;

namespace SkipListConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 10000;
            int[] array = new int[n];

            Random randNum = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                bool flag = true;
                while (flag)
                {
                    int randInt = randNum.Next(0, 3 * n);
                    if (!array.Contains(randInt))
                    {
                        array[i] = randInt;
                        flag = false;
                    }
                }
            }
            SkipList<int, int> skiplist = new SkipList<int, int>();

            foreach (var t in array)
            {
                skiplist.Add(t, 0);
            }
            for (int i = 0; i < array.Length; i++)
            {
                skiplist.Remove(array[i]);
            }

            skiplist = new SkipList<int, int>();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (var t in array)
            {
                skiplist.Add(t, 0);
            }
            for (int i = 5000; i < 7000; i++)
            {
                skiplist.Remove(array[i]);
            }
            for (int i = 0; i < 5000; i++)
            {
                if (!skiplist.Contains(array[i])) Console.WriteLine("No Find");
            }
            for (int i = 7000; i < 10000; i++)
            {
                if (!skiplist.Contains(array[i])) Console.WriteLine("No Find");
            }
            stopWatch.Stop();
            Console.WriteLine("SkipList: {0}", stopWatch.ElapsedMilliseconds);

            SortedList<int, int> sortedlist = new SortedList<int, int>();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var t in array)
            {
                sortedlist.Add(t, 0);
            }
            for (int i = 5000; i < 7000; i++)
            {
                sortedlist.Remove(array[i]);
            }
            for (int i = 0; i < 5000; i++)
            {
                if (!sortedlist.ContainsKey(array[i])) Console.WriteLine("No Find");
            }
            for (int i = 7000; i < 10000; i++)
            {
                if (!sortedlist.ContainsKey(array[i])) Console.WriteLine("No Find");
            }
            watch.Stop();
            Console.WriteLine("SortedList: {0}", watch.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}
