using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    class Record
    {
        private Transaction item;
        private string userName;
        private string status;
        private string message;

        public Record() { }

        public Record(string transaction_data, string name, string stat, string mess = "No message here")
        {
            Item = new Transaction(transaction_data);
            UserName = name;
            Status = stat;
            Message = mess;
        }

        public void Approve( string mess = "No message here")
        {
            Status = "Approved";
            Message = mess;
        }

        public void Reject(string mess = "No message here")
        {
            Status = "Rejected";
            Message = mess;

        }

        public void Decide()
        {
            Console.WriteLine("Choose whether you want to approve, reject or pass this draft");
            string[] ops = { "approve", "reject", "pass" };
            switch (Confirm.choise_input(ops))
            {
                case "approve":
                    Console.WriteLine("Message:");
                    this.Approve(Console.ReadLine());
                    break;
                case "reject":
                    Console.WriteLine("Message:");
                    this.Reject(Console.ReadLine());
                    break;
                case "pass":
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Item.ToString()}\n{UserName}\n{Status}\n{Message}";
        }

        public void ShowInfo()
        {
            Item.ShowInfo();
            Console.WriteLine($"Transaction added by {UserName} - status is {Status}\n{Message}\n");
            Console.WriteLine("------------------------------------------------------------------------\n");
        }

        public Transaction Item
        {
            get { return item; }
            set { item = value; }
        }

        public string UserName
        {
            get { return userName; }
            set
            {
                while (!Confirm.full_name_condition(value))
                {
                    Console.WriteLine("\nUser name data is invalid! Please enter new value!");
                    Console.Write("New name: ");
                    value = Console.ReadLine();
                }
                userName = value;
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                while (!Confirm.status_condition(value))
                {
                    Console.WriteLine("\nStatus data is invalid! Please enter new value!");
                    Console.Write("New status: ");
                    value = Console.ReadLine();
                }
                status = value;
            }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
