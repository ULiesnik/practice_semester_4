using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pract_sem4_t3
{
    class Transaction
    {
        private string name;
        private string id;
        private string cardNumber;
        private string cvc;
        private int month;
        private int year;
        private DateTime tDate;
        private double amount;

        public Transaction() { }

        public Transaction(string _data)
        {
            string[] _d = Confirm.str_for_transaction(_data);
            Name = _d[0];
            Id = _d[1];
            CardNumber = _d[2];
            Cvc = _d[3];
            Month = Confirm.read_int(_d[4], "Month");
            Year = Confirm.read_int(_d[5], "Year");
            TDate = Confirm.read_date(_d[6], "Date");
            Amount = Confirm.read_double(_d[7], "Amount");
        }

        public override string ToString()
        {
            return $"{name} {id} {cardNumber} {cvc} {month} {year} {tDate.ToShortDateString()} {amount}";
        }

        public void ShowInfo()
        {
            string line = $"\n{name} (id {id}):\n Card {cardNumber} with cvc {cvc}, valid until {month}.{year}.\n";
            line += $" Sent {amount}. Date: {tDate.ToShortDateString()}.";
            Console.WriteLine(line);
        }

        public string Name
        {
            get { return name; }
            set
            {
                while (!Confirm.name_condition(value))
                {
                    Console.WriteLine("\nName data is invalid! Please enter new value!");
                    Console.Write("New name: ");
                    value = Console.ReadLine();
                }
                name = value;

            }
        }

        public string Id
        {
            get { return id; }
            set
            {
                while (!Confirm.id_condition(value))
                {
                    Console.WriteLine("\nId data is invalid! Please enter new value!");
                    Console.Write("New id: ");
                    value = Console.ReadLine();
                }
                id = value;
            }
        }

        public string CardNumber
        {
            get { return cardNumber; }
            set
            {
                while (!Confirm.card_condition(value))
                {
                    Console.WriteLine("\nCard number data is invalid! Please enter new value!");
                    Console.Write("New number: ");
                    value = Console.ReadLine();
                }
                cardNumber = value;
            }
        }

        public string Cvc
        {
            get { return cvc; }
            set
            {
                while (!Confirm.cvc_condition(value))
                {
                    Console.WriteLine("\nCvc data is invalid! Please enter new value!");
                    Console.Write("New cvc: ");
                    value = Console.ReadLine();
                }
                cvc = value;
            }
        }

        public int Month
        {
            get { return month; }
            set
            {
                while (!Confirm.month_condition(value))
                {
                    Console.WriteLine("\nMonth data is invalid! Please enter new value!");
                    Console.Write("New month until card is valid: ");
                    value = Confirm.read_int(Console.ReadLine(), "Month");
                }
                month = value;
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                while (!Confirm.year_condition(value))
                {
                    Console.WriteLine("\nYear data is invalid! Please enter new value!");
                    Console.Write("New year until card is valid: ");
                    value = Confirm.read_int(Console.ReadLine(), "Year");
                }
                year = value;
            }
        }

        public DateTime TDate
        {
            get { return tDate; }
            set
            {
                while (!Confirm.date_condition(value, month, year))
                {
                    Console.WriteLine("\nDate is invalid! Please enter new value!");
                    Console.Write("New date: ");
                    value = Confirm.read_date(Console.ReadLine(), "Date");
                }
                tDate = value;
            }
        }

        public double Amount
        {
            get { return amount; }
            set
            {
                while (!Confirm.amount_condition(value))
                {
                    Console.WriteLine("\nAmount data is invalid! Please enter new value!");
                    Console.Write("New amount: ");
                    value = Confirm.read_double(Console.ReadLine(), "Amount");
                }
                amount = value;
            }
        }

        public bool IsBiggerThen(Transaction other, string field)
        {
            switch (field)
            {
                case "name":
                    return String.Compare(name, other.Name) == 1;
                case "id":
                    return String.Compare(id, other.Id) == 1;
                case "card":
                    return String.Compare(cardNumber, other.cardNumber) == 1;
                case "cvc":
                    return String.Compare(cvc, other.Cvc) == 1;
                case "month":
                    return month > other.Month;
                case "year":
                    return year > other.Year;
                case "date":
                    return DateTime.Compare(tDate, other.TDate) > 0;
                case "amount":
                    return amount > other.Amount;
            }
            return false;
        }


    }


    class Employee
    {
        private string name;
        private double salary;
        private DateTime hiringDate;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        public DateTime HiringDate
        {
            get { return hiringDate; }
            set { hiringDate = value; }
        }
        public Employee()
        {
        }
        public Employee(string n, double s, DateTime d)
        {
            Name = n;
            Salary = s;
            HiringDate = d;
        }
        public int Experience()
        {
            DateTime now = DateTime.Now;
            return now.Year - hiringDate.Year;
        }
        public virtual void ShowInfo()
        {
            Console.WriteLine($"{name} has {Experience()} years of experience.");
        }
        public override string ToString()
        {
            return $"{name} {salary} {hiringDate}";
        }
    }
}
