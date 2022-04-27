using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    class GenericCollection<T>
    {
        private T[] arr;

        public GenericCollection()
        {
            arr = new T[0];
        }

        public GenericCollection(T[] data_list)
        {
            foreach (T item in data_list)
            {
                this.add(item);
            }
        }
        public T[] Arr
        {
            get { return arr; }
        }

        public int Length()
        {
            return this.arr.Length;
        }

        public T this[int index]
        {
            get { return arr[index]; }
            set { arr[index] = value; }
        }

        public void add(T item)
        {
            if (arr == null)
            {
                arr = new T[] { item };
            }
            else
            {
                T[] res = new T[arr.Length + 1];
                arr.CopyTo(res, 0);
                res[arr.Length] = item;
                arr = res;
            }
        }

        public void remove(int index)
        {
            for (int i = index; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            Array.Resize(ref arr, arr.Length - 1);
        }

        public void append_to_file(string file_name)
        {
            using (StreamWriter sw = File.AppendText(file_name))
            {
                foreach (var t in arr)
                {
                    sw.WriteLine(t.ToString());
                }
            }
        }

        public void rewrite_to_file(string file_name)
        {
            File.WriteAllText(file_name, String.Empty);
            this.append_to_file(file_name);
        }

        public bool is_found(string val, out int[] indexes)
        {
            indexes = new int[arr.Length];
            bool found = false;
            int i = 0;
            for (int index = 0; index < arr.Length; index++)
            {
                if (arr[index].ToString().Contains(val))
                {
                    found = true;
                    indexes[i] = index;
                    i++;
                }
            }
            Array.Resize(ref indexes, i);
            return found;
        }

        public GenericCollection<T> search(string val)
        {
            GenericCollection<T> result = new GenericCollection<T>();
            int[] indexes;
            if (this.is_found(val, out indexes))
            {
                foreach (int i in indexes)
                {
                    result.add(arr[i]);
                }
            }
            else
            {
                Console.WriteLine("Any item found.");
            }
            return result;
        }
        public void remove_where(string val)
        {
            int[] indexes;
            if (this.is_found(val, out indexes))
            {
                Console.WriteLine("\nItems removed:");
                foreach (int i in indexes)
                {
                    Console.WriteLine(arr[i].ToString());
                    Console.WriteLine("--------------------------------------------------------------------");
                    this.remove(i);
                }
            }
            else
            {
                Console.WriteLine("Any item found.");
            }
        }

        public object GetFieldValue(object item, string field = null)
        {
            if (field == null)
            {
                return item;
            }
            return item.GetType().GetProperty(field).GetValue(item, null);
        }

        public void sort(string field = null)
        {

            T[] sorted = new T[Length()];
            sorted = (from item in this.arr orderby GetFieldValue(item, field) ascending select item).ToArray();
            for (int i = 0; i < Length(); i++)
            {
                arr[i] = sorted[i];
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine("\nItems are:");
            foreach (var item in arr)
            {
                item.ToString();
                Console.WriteLine("--------------------------------------------------------------------------");
            }
        }
    }
}
