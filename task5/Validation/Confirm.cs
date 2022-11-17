using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PracticeAPIAuthSem4.Models;

namespace PracticeAPIAuthSem4.Validation
{
    class Confirm
    {
        public static bool password_condition(string pw, out string m)
        {
            if (Regex.IsMatch(pw, @"[a-z]+") && Regex.IsMatch(pw, @"[A-Z]+") && Regex.IsMatch(pw, @".{8,}"))
            {
                m = "";
                return true;
            }
            m = "Password must contain at least 1 upper and 1 lower letter and consist of at least 8 symbols.";
            return false;
        }
        public static bool email_condition(string email, out string m)
        {
            if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                m = "";
                return true;
            }
            m = "Wrong email format.";
            return false;
        }
        public static bool name_condition(string name, out string m)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z]+$") && name.Length > 2 && name.Length < 40)
            {
                m = "";
                return true;
            }
            m = "Name must contain only letters and have at least 3 symbols.";
            return false;
        }
        public static bool user_name_condition(string name, out string m)
        {
            if (name.Length > 2 && name.Length < 40)
            {
                m = "";
                return true;
            }
            m = "Name must consist of at least 3, but no more then 40 symbols.";
            return false;
        }

        public static bool id_condition(string id, out string m)
        {
            if (id.Length == 10 && Regex.IsMatch(id, @"^[0-9]+$"))
            {
                m = "";
                return true;
            }
            m = "Id must consist of 10 digital symbols.";
            return false;
        }

        public static bool card_condition(string card, out string m)
        {
            if (card.Length == 16 && Regex.IsMatch(card, @"^[0-9]+$"))
            {
                m = "";
                return true;
            }
            m = "Must consist of 16 digits.";
            return false;
        }

        public static bool cvc_condition(string cvc, out string m)
        {
            if (cvc.Length > 2 && cvc.Length < 5 && Regex.IsMatch(cvc, @"^[0-9]+$"))
            {
                m = "";
                return true;
            }
            m = "Cvc must consist of 3 or 4 digits.";
            return false;
        }

        public static bool day_condition(int day, int month, out string m)
        {
            if (day <= max_day_num(month) && day > 0)
            {
                m = "";
                return true;
            }
            m = "Day number is invalid.";
            return false;
        }

        public static bool month_condition(int month, out string m)
        {
            if (month < 13 && month > 0)
            {
                m = "";
                return true;
            }
            m = "There is no such month number.";
            return false;
        }

        public static bool year_condition(int year, out string m)
        {
            if (year > 1950 && year < 2025)
            {
                m = "";
                return true;
            }
            m = "Year is invalid.";
            return false;
        }

        public static bool amount_condition(int amount, out string m)
        {
            if (amount > 0)
            {
                m = "";
                return true;
            }
            m = "Amount must be positive.";
            return false;
        }

        public static bool date_condition(DateTime d, int month, int year, out string m)
        {
            string m1,m2,m3;
            m=m1=m2=m3= "";
            if (day_condition(d.Day, d.Month,out m1) && month_condition(d.Month, out m2) && year_condition(d.Year,out m3))
            {
                if ((d.Year < year) || (d.Year == year && d.Month <= month))
                {
                    return true;
                }
                m = "Transaction date must be before the date until the card is valid. ";
            }
            m += m1 + " ";
            m += m2 + " ";
            m += m3;
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

        public static bool userIsValid(User u, out JsonResult j)
        {
            Dictionary<string, string> message = new Dictionary<string, string>();
            bool res = true;
            string mess;
            if (!name_condition(u.FirstName, out mess))
            {
                message.Add("FirstName", mess);
                res = false;
            }
            if (!name_condition(u.LastName, out mess))
            {
                message.Add("LastName", mess);
                res = false;
            }
            if (!user_name_condition(u.UserName, out mess))
            {
                message.Add("UserName", mess);
                res = false;
            }
            if (!email_condition(u.Email, out mess))
            {
                message.Add("Email", mess);
                res = false;
            }
            if (!password_condition(u.Password, out mess))
            {
                message.Add("Password", mess);
                res = false;
            }
            j = new JsonResult(message);
            return res;
        }

        public static bool transactionIsValid(Transaction tr, out JsonResult j)
        {
            Dictionary<string, string> message = new Dictionary<string, string>();
            bool res = true;
            string mess;
            if (!name_condition(tr.Name,out mess))
            {
                message.Add("Name", mess);
                res = false;
            }
            if (!id_condition(tr.Id, out mess))
            {
                message.Add("Id", mess);
                res = false;
            }
            if (!card_condition(tr.CardNumber, out mess))
            {
                message.Add("CardNumber", mess);
                res = false;
            }
            if (!cvc_condition(tr.Cvc, out mess))
            {
                message.Add("Cvc", mess);
                res = false;
            }
            if (!month_condition(tr.Month, out mess))
            {
                message.Add("Month", mess);
                res = false;
            }
            if (!year_condition(tr.Year, out mess))
            {
                message.Add("Year", mess);
                res = false;
            }
            if (!date_condition(tr.Date, tr.Month, tr.Year, out mess))
            {
                message.Add("Date", mess);
                res = false;
            }
            if (!amount_condition(tr.Amount, out mess))
            {
                message.Add("Amount", mess);
                res = false;
            }
            j = new JsonResult(message);
            return res;
        }
    }
}
