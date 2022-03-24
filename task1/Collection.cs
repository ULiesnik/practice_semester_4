using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_sem_4
{
    class Collection
    {
        private Transaction[] arr;

        public Collection()
        {
            arr = new Transaction[0];
        }

        public Transaction[] Arr
        {
            get { return arr; }
        }

        public void add(Transaction t)
        {
            if(arr == null)
            {
                arr = new Transaction[] { t };
            }
            else
            {
                Transaction[] res = new Transaction[arr.Length + 1];
                arr.CopyTo(res, 0);
                res[arr.Length] = t;
                arr = res;
            }
        }

        public void remove(int index)
        {
            for(int i = index; i < arr.Length-1; i++)
            {
                arr[i] = arr[i + 1];
            }
            Array.Resize(ref arr, arr.Length - 1);
        }

        public void read_file(string file_name)
        {
            string[] array = File.ReadAllLines(file_name);
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"Line {i}: ");
                this.add(new Transaction(array[i]));
                Console.WriteLine("added successfully.");
            }
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

        public void member_input()
        {
            Console.WriteLine("\nEnter data in format below (white spaces between elements):");
            Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
            this.add(new Transaction(Console.ReadLine()));
            Console.WriteLine("added successfully.");
        }

        public bool is_found(string val, out int[] indexes)
        {
            indexes = new int[arr.Length];
            bool found = false;
            int i = 0;
            for(int index=0;index<arr.Length;index++)
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

        public Collection search(string val)
        {
            Collection result = new Collection();
            int[] indexes;
            if(this.is_found(val, out indexes))
            {
                foreach(int i in indexes)
                {
                    result.add(arr[i]);
                }
            }
            else
            {
                Console.WriteLine("Any transaction found.");
            }
            return result;
        }

        public void remove_where(string val)
        {
            int[] indexes;
            if (this.is_found(val, out indexes))
            {
                Console.WriteLine("\nTransactions removed:");
                foreach (int i in indexes)
                {
                    arr[i].ShowInfo();
                    Console.WriteLine("--------------------------------------------------------------------");
                    this.remove(i);
                }
            }
            else
            {
                Console.WriteLine("Any transaction found.");
            }
        }

        public void edit_where(string val)
        {
            int[] indexes;
            if (this.is_found(val, out indexes))
            {
                foreach (int i in indexes)
                {
                    Console.WriteLine("\nTransaction found:");
                    arr[i].ShowInfo();
                    Console.WriteLine("Enter new data in format below (white spaces between elements):");
                    Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
                    arr[i] = new Transaction(Console.ReadLine());
                    Console.WriteLine("changed successfully.");
                }
            }
            else
            {
                Console.WriteLine("Any transaction found.");
            }
        }

        public void sort(string field) 
        {
            Transaction temp;
            for(int i = 0; i < arr.Length; i++)
            {
                for(int j = 0; j < arr.Length; j++)
                {
                    if (arr[j].IsBiggerThen(arr[i], field))
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine("\nTransactions are:");
            foreach(var t in arr)
            {
                t.ShowInfo();
                Console.WriteLine("--------------------------------------------------------------------------");
            }
        }
    }
}
