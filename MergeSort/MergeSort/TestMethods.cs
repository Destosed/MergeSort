using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MergeSortProj
{
    public static class TestMethods
    {
        public static int[] GenerateOneSet(int itemsCount)
        {
            var rnd = new Random();
            var set = new int[itemsCount];

            for (int i = 0; i < set.Length; i++)
                set[i] = rnd.Next(100);

            return set;
        }

        public static void PrintSetToFile(int[] set)
        {
            var sb = new StringBuilder();
            foreach (var item in set)
                sb.Append(item + " ");
            File.WriteAllText("RandomItemsSet.txt", sb.ToString());
        }

        public static int[] ReadOneSet()
        {
            var set = File.ReadAllLines("RandomItemsSet.txt")[0].Split().ToArray();
            return Program.ConvertStringArrToIntArr(set);
        }
        
    }
}
