using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using SkipListLib;
using System.Linq;

namespace SkipLIstTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Add()
        {
            SkipList<int, int> skiplist = new SkipList<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                skiplist.Add(i, i);
            }
            Assert.AreEqual(skiplist.Count, n);

        }
        [TestMethod]
        public void AddingAndComparing()
        {
            SkipList<int, int> skiplist = new SkipList<int, int>();
            SortedList<int, int> sortedlist = new SortedList<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                skiplist.Add(i, i);
            }
            for (int i = 0; i < n; i++)
            {
                sortedlist.Add(i, i);
            }
            Assert.AreEqual(skiplist.Count, n);
            bool flag = true;
            foreach (var item in skiplist)
            {
                if(item.Value != sortedlist[item.Key])
                {
                    flag = false;
                    break;
                }
            }
            Assert.IsTrue(flag);
        }
        [TestMethod]
        public void RemoveExist()
        {
            SkipList<int, int> skiplist = new SkipList<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                skiplist.Add(i, i);
            }
            Assert.AreEqual(true, skiplist.Remove(n - 1));
            Assert.AreEqual(n - 1, skiplist.Count);
        }
        [TestMethod]
        public void RemoveNoExist()
        {
            SkipList<int, int> skiplist = new SkipList<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                skiplist.Add(i, i);
            }
            Assert.AreEqual(false, skiplist.Remove(n + 1));
        }
        [TestMethod]
        public void AdditionAndRemoveAndComparison()
        {
            SkipList<int, int> skiplist = new SkipList<int, int>();
            SortedList<int, int> sortedlist = new SortedList<int, int>();
            int n = 100;
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

            for (int i = 0; i < array.Length; i++)
            {
                skiplist.Add(array[i], array[i]);
            }
            for (int i = 0; i < array.Length; i++)
            {
                sortedlist.Add(array[i], array[i]);
            }
            for (int i = 0; i < array.Length; i += 10)
            {
                skiplist.Remove(array[i]);
            }
            for (int i = 0; i < array.Length; i += 10)
            {
                sortedlist.Remove(array[i]);
            }
            Assert.AreEqual(sortedlist.Count, skiplist.Count);
            bool flag1 = true;
            foreach (var item in skiplist)
            {
                if (item.Value != sortedlist[item.Key])
                {
                    flag1 = false;
                    break;
                }
            }
            Assert.AreEqual(true, flag1);
        }
        [TestMethod]
        public void Contains()
        {
            SkipList<int, int> skiplist = new SkipList<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                skiplist.Add(i, i);
            }
            Assert.AreEqual(true,skiplist.Contains(n - 1));
        }
        [TestMethod]
        public void NoContains()
        {
            SkipList<int, int> skiplist = new SkipList<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                skiplist.Add(i, i);
            }
            Assert.AreEqual(false, skiplist.Contains(n));
        }
    }
}
