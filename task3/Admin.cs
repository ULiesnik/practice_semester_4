using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    class Admin : User
    {
        public Admin() : base() { }
        public Admin(string data)
        {
            string[] d = Confirm.str_for_admin(data);
            FirstName = d[0];
            LastName = d[1];
            Email = d[2];
            Password = d[3];
            role = "ADMIN";
        }

        public Admin(string[] d)
        {
            FirstName = d[0];
            LastName = d[1];
            Email = d[2];
            password = d[3];
            role = "ADMIN";
        }

        public override void addTransaction(Company c)
        {
            Console.WriteLine("\nEnter data in format below (white spaces between elements):");
            Console.WriteLine("Name Id CardNumber Cvc MonthUntilCardIsValid YearUntilValid Date Amount");
            Record draft = new Record(Console.ReadLine(), $"{FirstName} {LastName}", "Approved");
            using (StreamWriter sw = File.AppendText(c.Records))
            {
                sw.WriteLine(draft.ToString());
            }
            Console.WriteLine("added successfully.");
        }

        public void DraftsReview(Company c, string stat = "Draft")
        {
            GenericCollection<Record> drafts = Confirm.records_read(c.Records);
            for (int i=0; i < drafts.Length(); i++)
            {
                if(drafts[i].Status == stat)
                {
                    drafts[i].ShowInfo();
                    if(stat == "Draft")
                    {
                        drafts[i].Decide();
                    }
                }
            }
            drafts.rewrite_to_file(c.Records);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}