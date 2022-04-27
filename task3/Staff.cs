using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    class Staff : User
    {
        private double salary;
        private DateTime firstDayInCompany;

        public Staff() : base() { }
        public Staff(string data)
        {
            string[] d = Confirm.str_for_staff(data);
            FirstName = d[0];
            LastName = d[1];
            Email = d[2];
            Password = d[3];
            Salary = Confirm.read_double(d[4], "Salary");
            FirstDayInCompany = Confirm.read_date(d[5], "First day in company");
            role = "STAFF";
        }
        public Staff(string[] d)
        {
            FirstName = d[0];
            LastName = d[1];
            Email = d[2];
            password = d[3];
            Salary = Confirm.read_double(d[4], "Salary");
            FirstDayInCompany = Confirm.read_date(d[5], "First day in company");
            role = "STAFF";
        }
        public override void addTransaction(Company c)
        {
            Console.WriteLine("\nEnter data in format below (white spaces between elements):");
            Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
            Record draft = new Record(Console.ReadLine(), $"{FirstName} {LastName}", "Draft");
            using (StreamWriter sw = File.AppendText(c.Records))
            {
                sw.WriteLine(draft.ToString());
            }
            Console.WriteLine("added successfully.");
        }

        public void ShowMyDrafts(Company c, string stat = null)
        {
            GenericCollection<Record> drafts = Confirm.records_read(c.Records);
            for (int i = 0; i < drafts.Length(); i++)
            {
                if ((stat == null || drafts[i].Status == stat) && drafts[i].UserName == $"{FirstName} {LastName}")
                {
                    drafts[i].ShowInfo();
                }
            }
        }

        public void ShowAllApprovedDrafts(Company c)
        {
            GenericCollection<Record> drafts = Confirm.records_read(c.Records);
            for (int i = 0; i < drafts.Length(); i++)
            {
                if (drafts[i].Status == "Approved")
                {
                    drafts[i].ShowInfo();
                }
            }
        }

        public void RemoveDraft(Company c)
        {
            GenericCollection<Record> drafts = Confirm.records_read(c.Records);
            Console.WriteLine("Enter the value you are searching for:");
            int[] indexes;
            if(drafts.is_found(Console.ReadLine(),out indexes))
            {
                foreach(int i in indexes)
                {
                    if( drafts[i].Status == "Draft")
                    {
                        drafts[i].ShowInfo();
                        drafts.remove(i);
                    }
                }
            }
            drafts.rewrite_to_file(c.Records);
        }

        public void EditDraft(Company c)
        {
            GenericCollection<Record> drafts = Confirm.records_read(c.Records);
            Console.WriteLine("Enter the value you are searching for:");
            int[] indexes;
            if (drafts.is_found(Console.ReadLine(), out indexes))
            {
                foreach (int i in indexes)
                {
                    if (drafts[i].Status == "Draft")
                    {
                        drafts[i].ShowInfo();
                        Console.WriteLine("\nEnter data in format below (white spaces between elements):");
                        Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
                        drafts[i] = new Record(Console.ReadLine(), $"{FirstName} {LastName}", "Draft");
                    }
                }
            }
            drafts.rewrite_to_file(c.Records);
        }

        public void ResendDraft(Company c)
        {
            GenericCollection<Record> drafts = Confirm.records_read(c.Records);
            Console.WriteLine("Enter the value you are searching for:");
            int[] indexes;
            if (drafts.is_found(Console.ReadLine(), out indexes))
            {
                foreach (int i in indexes)
                {
                    if (drafts[i].Status == "Rejected" && drafts[i].UserName == $"{FirstName} {LastName}")
                    {
                        drafts[i].ShowInfo();
                        Console.WriteLine("\nEnter data in format below (white spaces between elements):");
                        Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
                        drafts[i] = new Record(Console.ReadLine(), $"{FirstName} {LastName}", "Draft", "Resended");
                    }
                }
            }
            drafts.rewrite_to_file(c.Records);
        }

        public override string ToString()
        {
            return base.ToString() + $" {Salary} {FirstDayInCompany.ToShortDateString()}";
        }

        public double Salary
        {
            get { return salary; }
            set
            {
                while (!Confirm.amount_condition(value))
                {
                    Console.WriteLine("\nSalary data is invalid! Please enter new value!");
                    Console.Write("New salary: ");
                    value = Confirm.read_double(Console.ReadLine(), "Salary");
                }
                salary = value;
            }
        }

        public DateTime FirstDayInCompany
        {
            get { return firstDayInCompany; }
            set
            {
                while (!Confirm.date_condition(value, DateTime.Now.Month, DateTime.Now.Year))
                {
                    Console.WriteLine("\nFirst day date is invalid! Please enter new value!");
                    Console.Write("New date: ");
                    value = Confirm.read_date(Console.ReadLine(), "First day in company");
                }
                firstDayInCompany = value;
            }
        }
    }
}
