using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    abstract class User
    {
        private string firstName;
        private string lastName;
        private string email;
        protected string role;
        protected string password;

        public User() { }

        abstract public void addTransaction(Company c);

        public bool IsMe(string data)
        {
            string[] d = Confirm.str_for_user(data);
            if (d[0] == Email && PasswordHasher.Verify(d[1], Password))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Role}:\n{FirstName} {LastName} {Email} {Password}";
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                while (!Confirm.name_condition(value))
                {
                    Console.WriteLine("\nFirst name data is invalid! Please enter new value!");
                    Console.Write("New name: ");
                    value = Console.ReadLine();
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                while (!Confirm.name_condition(value))
                {
                    Console.WriteLine("\nLast name data is invalid! Please enter new value!");
                    Console.Write("New name: ");
                    value = Console.ReadLine();
                }
                lastName = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                while (!Confirm.email_condition(value))
                {
                    Console.WriteLine("\nEmail data is invalid! Please enter new value!");
                    Console.Write("New email: ");
                    value = Console.ReadLine();
                }
                email = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                while (!Confirm.password_condition(value))
                {
                    Console.WriteLine("\nPassword data is invalid! Please enter new value!");
                    Console.Write("New password: ");
                    value = Console.ReadLine();
                }
                password = PasswordHasher.Hash(value);
            }
        }

        public string Role
        {
            get { return role; }
        }
    }
}
