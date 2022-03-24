using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_sem_4
{
    class Options
    {

        public static void add_in_file(Collection c)
        {
            c.member_input();
            string f = Confirm.file_name_read();
            c.rewrite_to_file(f);
        }

        public static void remove(Collection c)
        {
            Console.Write("Enter the value to search for:");
            string val = Console.ReadLine();
            c.remove_where(val);
        }

        public static void remove_from_file(Collection c)
        {
            Console.Write("Enter the value to search for:");
            string val = Console.ReadLine();
            c.remove_where(val);
            string f = Confirm.file_name_read();
            c.rewrite_to_file(f);
        }

        public static void edit(Collection c)
        {
            Console.Write("Enter the value to search for:");
            string val = Console.ReadLine();
            c.edit_where(val);
        }

        public static void edit_in_file(Collection c)
        {
            Console.Write("Enter the value to search for:");
            string val = Console.ReadLine();
            c.edit_where(val);
            string f = Confirm.file_name_read();
            c.rewrite_to_file(f);
        }

        public static void sort(Collection c)
        {
            string field = Menu.field_choise();
            c.sort(field);
            c.ShowInfo();
        }

        public static void search(Collection c)
        {
            Console.Write("Enter the value to search for:");
            string val = Console.ReadLine();
            Collection found = c.search(val);
            found.ShowInfo();
        }

        public static void read(Collection c)
        {
            string f = Confirm.file_name_read();
            c.read_file(f);
        }

        public static void show(Collection c)
        {
            c.ShowInfo();
        }
    }
}
