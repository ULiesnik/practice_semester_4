using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_sem_4
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Collection c = new Collection();
            c.read_file("../../TextFile1.txt");
            c.ShowInfo();
            string choise = Menu.main_choise();
            while (choise != "0")
            {
                Menu.main_func(c, choise);
                choise = Menu.main_choise();
            }*/
            GenericCollection<int> c1 = new GenericCollection<int>();
            Random rand = new Random();
            for(int i = 0; i < 10; i++)
            {
                c1.add(rand.Next(1, 100));
            }
            c1.sort();
            c1.append_to_file("../../TextFile2.txt");

            GenericCollection<string> c2 = new GenericCollection<string>();
            c2.add("This");
            c2.add("are");
            c2.add("my");
            c2.add("string");
            c2.add("values");
            GenericCollection<string> c3 = c2.search("i");
            c3.append_to_file("../../TextFile2.txt");

            GenericCollection<Transaction> c4 = new GenericCollection<Transaction>();
            c4.add(new Transaction("Taras 28769 3464787845659876 234 12 2020 12.12.2018 2900"));
            c4.add(new Transaction("Stephany 45411 4545656756769090 0985 2 2021 28.12.2019 4500"));
            c4.add(new Transaction("Devid 45656 1356456433335555 998 11 2022 09.11.2021 7000"));
            c4.add(new Transaction("Joahn 12342 1243999900008888 3457 12 2022 12.12.2021 2000"));
            c4.remove_where("2020");
            c4.sort("Amount");
            c4.append_to_file("../../TextFile2.txt");

            GenericCollection<DateTime> c6 = new GenericCollection<DateTime>();
            c6.add(new DateTime(2020, 12, 12));
            c6.add(new DateTime(1999, 09, 17));
            c6.add(new DateTime(2017, 11, 15));
            c6.add(new DateTime(2022, 02, 08));
            c6.sort();
            c6.append_to_file("../../TextFile2.txt");
            c6.sort("Month");
            c6.append_to_file("../../TextFile2.txt");
        }
    }
}
