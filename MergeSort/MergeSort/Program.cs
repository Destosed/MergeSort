using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MergeSortProj
{
    class Program
    {
        private static int counter = 0;

        static void Main(string[] args)
        {
            GenerateAndPrintItems(1, 100);
            SortLinesInFile("RandomItemsSet.txt");
            Console.ReadKey();
        }

        static int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
                return array;
            int middle = array.Length / 2;
            var finalSortedArr = Merge(MergeSort(array.Take(middle).ToArray()), MergeSort(array.Skip(middle).ToArray()));
            return finalSortedArr;
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
                counter++;
            }
            return merged;
        }

        static SingleLinkedList MergeSort(SingleLinkedList lst)
        {
            var sortedArr = MergeSort(lst.ToIntArray());
            var sortedLst = lst.FillFromArray(sortedArr);
            return sortedLst;
        }

        static void GenerateAndPrintItems(int setsCount, int itemsCount)
        {
            //setsCount - Количество наборов
            //itemsCount - Количество элементов в наборе

            Random rnd = new Random();
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
                for (int i = 0; i < itemSet.Length; i++)
                {
                    if(i != itemSet.Length - 1)
                        sb.Append(itemSet[i].ToString() + ' ');
                    else
                        sb.Append(itemSet[i].ToString()); //После последнего элемента пробел не ставится
                }
                
                using (StreamWriter w = File.AppendText("RandomItemsSet.txt"))
                    w.WriteLine(sb.ToString());
                sb.Clear();
            }
        }

        static string ConvertIntArrToString(int[] arr)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != arr.Length - 1)
                    sb.Append(arr[i] + " ");
                else
                    sb.Append(arr[i]);
            }
            return sb.ToString();
        }

        static int[] ConvertStringArrToIntArr(string[] arr)
        {
            var intArr = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                intArr[i] = int.Parse(arr[i]);
            return intArr;
        }

        static void SortLinesInFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var sortedLines = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var elems = ConvertStringArrToIntArr(lines[i].Split(' '));
                var firstTime = DateTime.Now.Ticks;                         //Отсчет времени 
                sortedLines[i] = ConvertIntArrToString(MergeSort(elems));
                Console.WriteLine("Время работы в тиках: {0}", DateTime.Now.Ticks - firstTime);  //Конец отсчета (Работает криво, если замерять сразу несколько наборов)
            }
            File.WriteAllLines(path, sortedLines);
            Console.WriteLine("Количество итераций: {0}", counter);
        }
    }
}
