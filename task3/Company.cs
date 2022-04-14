using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    class Company
    {
        private string users;
        private string drafts;
        private string currentUser;

        public Company() { }
        public Company(string users_file, string records_file)
        {
            Users = Confirm.file_name_read(users_file);
            Records = Confirm.file_name_read(records_file);
            CurrentUser = null;
        }

        public void SignUp()
        {
            Console.WriteLine("\nEnter your data in format below (white spaces between elements):");
            Console.WriteLine("FirstName LastName Email Password Salary FirstDayInCompany");
            Staff you = new Staff(Console.ReadLine());
            using (StreamWriter sw = File.AppendText(this.Users))
            {
                sw.WriteLine(you.ToString());
            }
            CurrentUser = you.ToString();
        }

        public void SignIn()
        {
            User[] u = Confirm.users_read(this.Users);
            bool signed = false;
            while (!signed)
            {
                Console.WriteLine("\nEnter your data in format below to sign in(white spaces between elements):");
                Console.WriteLine("Email Password");
                string data = Console.ReadLine();
                for (int i = 0; i < u.Length; i++)
                {
                    if (u[i].IsMe(data))
                    {
                        CurrentUser = u[i].ToString();
                        signed = true;
                        break;
                    }
                }
            }
        }
        public void logout()
        {
            CurrentUser = null;
        }

        public string Users
        {
            get { return users; }
            set { users = value; }
        }

        public string Records
        {
            get { return drafts; }
            set { drafts = value; }
        }

        public string CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
    }
}
