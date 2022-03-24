using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Practice_sem_4
{
    class Confirm
    {
        public static bool name_condition(string name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z]+$") && name.Length > 2 && name.Length < 40)
            {
                return true;
            }
            return false;
        }

        public static bool id_condition(string id)
        {
            if (id.Length == 5 && Regex.IsMatch(id, @"^[0-9]+$"))
            {
                return true;
            }
            return false;
        }

        public static bool card_condition(string card)
        {
            if (card.Length == 16 && Regex.IsMatch(card, @"^[0-9]+$"))
            {
                return true;
            }
            return false;
        }

        public static bool cvc_condition(string cvc)
        {
            if (cvc.Length > 2 && cvc.Length < 5 && Regex.IsMatch(cvc, @"^[0-9]+$"))
            {
                return true;
            }
            return false;
        }

        public static bool day_condition(int day, int month)
        {
            if (day <= max_day_num(month) && day > 0)
            {
                return true;
            }
            return false;
        }

        public static bool month_condition(int month)
        {
            if (month < 13 && month > 0)
            {
                return true;
            }
            return false;
        }

        public static bool year_condition(int year)
        {
            if (year > 1950 && year < 2023)
            {
                return true;
            }
            return false;
        }

        public static bool amount_condition(double amount)
        {
            if (amount > 0)
            {
                return true;
            }
            return false;
        }

        public static bool date_condition(DateTime d, int month, int year)
        {
            if (day_condition(d.Day, d.Month) && month_condition(d.Month) && year_condition(d.Year))
            {
                if ((d.Year < year) || (d.Year == year && d.Month <= month))
                {
                    return true;
                }
            }
            return false;
        }

        static int max_day_num(int m)
        {
            if(m == 2)
            {
                return 29;
            }
            else if(m==4 || m==6 || m == 9 || m == 11)
            {
                return 30;
            }
            else
            {
                return 31;
            }
        }

        public static int read_int(string data, string data_name)
        {
            int v = 0;
            bool is_val = false;
            while (!is_val)
            {
                try
                {
                    v = Convert.ToInt32(data);
                    is_val = true;
                }
                catch (FormatException e) 
                {
                    Console.Write($"{data_name} is not int! Try again: ");
                    data = Console.ReadLine();
                }
            }
            return v;
        }

        public static double read_double(string data, string data_name)
        {
            double v = 0;
            bool is_val = false;
            while (!is_val)
            {
                try
                {
                    v = Convert.ToDouble(data);
                    is_val = true;
                }
                catch (FormatException e)
                {
                    Console.Write($"{data_name} is not double! Try again: ");
                    data = Console.ReadLine();
                }
            }
            return v;
        }

        public static DateTime read_date(string data, string data_name)
        {
            DateTime d = new DateTime();
            bool is_val = false;
            while (!is_val)
            {
                try
                {
                    d = Convert.ToDateTime(data);
                    is_val = true;
                }
                catch (FormatException e)
                {
                    Console.Write($"{data_name} is not valid! Try again: ");
                    data = Console.ReadLine();
                }
            }
            return d;
        }

        public static string choise_input(string[] options)
        {
            string choise = "";
            while (!options.Contains(choise))
            {
                Console.WriteLine("Enter one of options above: ");
                choise = Console.ReadLine();
            }
            return choise;
        }
        
        public static string file_name_read()
        {
            string file = "";
            while (!File.Exists(file))
            {
                Console.WriteLine("Enter the file name: ");
                file = Console.ReadLine();
            }
            return file;
        }

        public static string[] str_check(string s)
        {
            string[] _d = s.Split();
            while(_d.Length != 8)
            {
                Console.WriteLine("\nWrong data format. Enter data in format below (white spaces between elements):");
                Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
                s = Console.ReadLine();
                _d = s.Split();
            }
            return _d;
        }
        
    }
}