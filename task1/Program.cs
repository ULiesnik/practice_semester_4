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
            Collection c = new Collection();
            c.read_file("../../TextFile1.txt");
            c.ShowInfo();
            string choise = Menu.main_choise();
            while (choise != "0")
            {
                Menu.main_func(c, choise);
                choise = Menu.main_choise();
            }

        }
    }
}
