using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSortProj
{
    class Elem
    {
        public int Info { get; set; }
        public Elem Next { get; set; }
    }

    class SingleLinkedList
    {
        public Elem First { get; set; }

        public int Length
        {
            get
            {
                int k = 0;
                var el = First;
                while (el != null)
                {
                    k++;
                    el = el.Next;
                }
                return k;
            }
        }

        public void AddFirst(int x)
        {
            var el = new Elem() { Info = x, Next = First };
            First = el;
        }

        public void AddLast(int x)
        {
            var el = First;
            if (el == null)
            {
                AddFirst(x);
                return;
            }
            while (el.Next != null)
                el = el.Next;
            el.Next = new Elem() { Info = x };
        }

        public override string ToString()
        {
            var el = First;
            var sb = new StringBuilder();
            while (el != null)
            {
                sb.Append($"{el.Info} ");
                el = el.Next;
            }
            return sb.ToString();
        }

        public bool IsOrdered()
        {
            var el = First;
            if (el == null || el.Next == null)
                return true;
            while (el.Next != null)
            {
                if (el.Info > el.Next.Info)
                    return false;
                el = el.Next;
            }
            return true;
        }

        public int[] ToIntArray()
        {
            var arr = new int[this.Length];
            var el = First;
            var k = 0;

            while (el != null)
            {
                arr[k] = el.Info;
                k++;
                el = el.Next;
            }
            return arr;
        }

        public SingleLinkedList FillFromArray(int[] arr)
        {
            var lst = new SingleLinkedList();
            var el = First;
            var k = 0;
            while (el != null)
            {
                lst.AddLast(arr[k]);
                k++;
                el = el.Next;
            }
            return lst;
        }
    }
}