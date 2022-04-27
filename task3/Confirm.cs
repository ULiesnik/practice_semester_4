using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Pract_sem4_t3
{
    class Confirm
    {
        public static bool password_condition(string pw)
        {
            if (Regex.IsMatch(pw, @"[a-z]+") && Regex.IsMatch(pw, @"[A-Z]+") && Regex.IsMatch(pw, @".{8,}"))
            {
                return true;
            }
            return false;
        }
        public static bool email_condition(string email)
        {
            if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                return true;
            }
            return false;
        }

        public static bool full_name_condition(string name)
        {
            if (Regex.IsMatch(name, @"^([a-zA-Z]+) ([a-zA-Z]+)$") && name.Length > 5 && name.Length < 80)
            {
                return true;
            }
            return false;
        }

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

        public static bool status_condition(string stat)
        {
            if (stat == "Draft" || stat == "Approved" || stat == "Rejected")
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
            if (m == 2)
            {
                return 29;
            }
            else if (m == 4 || m == 6 || m == 9 || m == 11)
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

        public static User[] users_read(string file)
        {
            string[] array = File.ReadAllLines(file);
            User[] users = new User[array.Length / 2];
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "ADMIN:":
                        string[] d = str_for_admin(array[i + 1]);
                        users[i / 2] = new Admin(d);
                        break;
                    case "STAFF:":
                        string[] d1 = str_for_staff(array[i + 1]);
                        users[i / 2] = new Staff(d1);
                        break;
                }
                i++;
            }
            return users;
        }

        public static GenericCollection<Record> records_read(string file)
        {
            GenericCollection<Record> drafts = new GenericCollection<Record>();
            string[] array = File.ReadAllLines(file);
            for (int i = 0; i < array.Length; i++)
            {
                drafts.add(new Record(array[i], array[i + 1], array[i + 2], array[i + 3]));
                i += 3;
            }
            return drafts;
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

        public static string file_name_read(string file = "")
        {
            while (!File.Exists(file))
            {
                Console.WriteLine("Enter the file name: ");
                file = Console.ReadLine();
            }
            return file;
        }

        public static string[] str_for_transaction(string s)
        {
            string[] _d = s.Split();
            while (_d.Length != 8)
            {
                Console.WriteLine("\nWrong data format. Enter data in format below (white spaces between elements):");
                Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
                s = Console.ReadLine();
                _d = s.Split();
            }
            return _d;
        }

        public static string[] str_for_admin(string s)
        {
            string[] _d = s.Split();
            while (_d.Length != 4)
            {
                Console.WriteLine("\nWrong data format. Enter data in format below (white spaces between elements):");
                Console.WriteLine("FirstName LastName Email Password");
                s = Console.ReadLine();
                _d = s.Split();
            }
            return _d;
        }

        public static string[] str_for_staff(string s)
        {
            string[] _d = s.Split();
            while (_d.Length != 6)
            {
                Console.WriteLine("\nWrong data format. Enter data in format below (white spaces between elements):");
                Console.WriteLine("FirstName LastName Email Password Salary FirstDayInCompany");
                s = Console.ReadLine();
                _d = s.Split();
            }
            return _d;
        }

        public static string[] str_for_user(string s)
        {
            string[] _d = s.Split();
            while (_d.Length != 2)
            {
                Console.WriteLine("\nWrong data format. Enter data in format below (white spaces between elements):");
                Console.WriteLine("Email Password");
                s = Console.ReadLine();
                _d = s.Split();
            }
            return _d;
        }
    }
}
