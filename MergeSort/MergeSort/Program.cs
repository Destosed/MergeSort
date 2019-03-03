using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MergeSortProj
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        static int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
                return array;
            int middle = array.Length / 2;
            return Merge(MergeSort(array.Take(middle).ToArray()), MergeSort(array.Skip(middle).ToArray()));
        }

        static int[] Merge(int[] arr1, int[] arr2)
        {
            int ptr1 = 0, ptr2 = 0;
            int[] merged = new int[arr1.Length + arr2.Length];

            for (int i = 0; i < merged.Length; ++i)
            {
                if (ptr1 < arr1.Length && ptr2 < arr2.Length)
                    merged[i] = arr1[ptr1] > arr2[ptr2] ? arr2[ptr2++] : arr1[ptr1++];
                else
                    merged[i] = ptr2 < arr2.Length ? arr2[ptr2++] : arr1[ptr1++];
            }
            return merged;
        }

        static SingleLinkedList MergeSort(SingleLinkedList lst)
        {
            var sortedArr = MergeSort(lst.ToIntArray());
            var sortedLst = lst.FillFromArray(sortedArr);
            return sortedLst;
        }

        static void GenerateItems()
        {
            Random rnd = new Random();
            //int setsCount = rnd.Next(50, 100);
            //int itemsCount = rnd.Next(100, 10000);

            int setsCount = 5; //Количество наборов
            int itemsCount = 10; //Количество элементов в наборе

            var itemSetArray = new int[setsCount][];   

            for (int i = 0; i < itemSetArray.Length; i++)
            {
                itemSetArray[i] = new int[itemsCount];

                for (int j = 0; j < itemSetArray[i].Length; j++)
                    itemSetArray[i][j] = rnd.Next(0, 100);
            }
            PrintArrayInFile(itemSetArray);
        }

        static void PrintArrayInConsole(int[] arr)
        {
            foreach (var item in arr)
                Console.Write("{0} ", item);
        }

        static void PrintArrayInConsole(int[][] itemSetArray)
        {
            for (int i = 0; i < itemSetArray.Length; i++)
            {
                for (int j = 0; j < itemSetArray[i].Length; j++)
                    Console.Write("{0} ", itemSetArray[i][j]);

                Console.WriteLine();
            }
        }

        static void PrintArrayInFile(int[][] itemsSets)
        {
            var sb = new StringBuilder();
            File.WriteAllText("RandomItemsSet.txt", string.Empty); //Очищение файла перед использованием

            foreach (var itemSet in itemsSets)
            {
                foreach (var item in itemSet)
                    sb.Append(item.ToString() + " ");
                using (StreamWriter w = File.AppendText("RandomItemsSet.txt"))
                    w.WriteLine(sb.ToString() + " ");
                sb.Clear();
            }
        }
    }
}
