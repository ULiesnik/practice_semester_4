using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PracticeAPISem4.Models;

namespace PracticeAPISem4
{
    class Validation
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

        public static bool amount_condition(int amount)
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

        public static bool isValid(Transaction tr, out JsonResult j) 
        {
            string message = "";
            bool res = true;
            if (!name_condition(tr.Name))
            {
                message += "Name is invalid. ";
                res = false;
            }
            if (!id_condition(tr.Id))
            {
                message += "Id is invalid. ";
                res = false;
            }
            if (!card_condition(tr.CardNumber))
            {
                message += "CardNumber is invalid. ";
                res = false;
            }
            if (!cvc_condition(tr.Cvc))
            {
                message += "Cvc is invalid. ";
                res = false;
            }
            if (!month_condition(tr.Month))
            {
                message += "Month is invalid. ";
                res = false;
            }
            if (!year_condition(tr.Year))
            {
                message += "Year is invalid. ";
                res = false;
            }
            if (!date_condition(tr.Date,tr.Month,tr.Year))
            {
                message += "Date is invalid. ";
                res = false;
            }
            if (!amount_condition(tr.Amount))
            {
                message += "Amount is invalid. ";
                res = false;
            }
            j = new JsonResult(message);
            return res;
        }
    }
}
