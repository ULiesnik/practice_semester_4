using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_sem_4
{
    class Menu
    {

        public static string main_choise()
        {
            string[] ops = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string condition = "\nChoose: ";
            condition += "\n1 - to show current colllection;";
            condition += "\n2 - to add new transaction;";
            condition += "\n3 - to remove transaction;";
            condition += "\n4 - to remove also in file;";
            condition += "\n5 - to edit transaction data;";
            condition += "\n6 - to edit also in file;";
            condition += "\n7 - to sort;";
            condition += "\n8 - to find transaction;";
            condition += "\n9 - to read the file;";
            condition += "\n0 - to exit.";
            Console.WriteLine(condition);
            return Confirm.choise_input(ops);
        }

        public static string field_choise()
        {
            string[] ops = new string[8] { "name", "id", "card", "cvc", "month", "year", "date", "amount" };
            Console.WriteLine("Choose the field for sorting from options below: ");
            foreach(string f in ops)
            {
                Console.Write($"-- {f} --");
            }
            Console.WriteLine("");
            return Confirm.choise_input(ops);
        }

        public static void main_func(Collection c, string choise)
        {
            switch (choise)
            {
                case "1":
                    Options.show(c);
                    break;
                case "2":
                    Options.add_in_file(c);
                    break;
                case "3":
                    Options.remove(c);
                    break;
                case "4":
                    Options.remove_from_file(c);
                    break;
                case "5":
                    Options.edit(c);
                    break;
                case "6":
                    Options.edit_in_file(c);
                    break;
                case "7":
                    Options.sort(c);
                    break;
                case "8":
                    Options.search(c);
                    break;
                case "9":
                    Options.read(c);
                    break;
            }
        }
    }
}
