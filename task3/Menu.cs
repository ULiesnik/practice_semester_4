using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    class Menu
    {
        public static string sign_choise()
        {
            string[] ops = new string[4] { "1", "2", "3", "0" };
            string condition = "\nChoose: ";
            condition += "\n1 - to sign in;";
            condition += "\n2 - to sign up;";
            condition += "\n3 - to log out;";
            condition += "\n0 - to exit.";
            Console.WriteLine(condition);
            return Confirm.choise_input(ops);
        }
        public static string admin_choise()
        {
            string[] ops = new string[5] { "1", "2", "3","4","0" };
            string condition = "\nChoose: ";
            condition += "\n1 - to review all drafts;";
            condition += "\n2 - to review all approved records;";
            condition += "\n3 - to review all rejected records;";
            condition += "\n4 - to add new record here;";
            condition += "\n0 - to exit.";
            Console.WriteLine(condition);
            return Confirm.choise_input(ops);
        }
        public static string staff_choise()
        {
            string[] ops = new string[7] { "1","2","3","4","5","6","0" };
            string condition = "\nChoose: ";
            condition += "\n1 - to show my records;";
            condition += "\n2 - to show all approved records;";
            condition += "\n3 - to add new draft;";
            condition += "\n4 - to remove draft;";
            condition += "\n5 - to edit draft;";
            condition += "\n6 - to resend rejected draft;";
            condition += "\n0 - to exit.";
            Console.WriteLine(condition);
            return Confirm.choise_input(ops);
        }
        public static string status_choise()
        {
            string[] ops = new string[4] { "1", "2", "3", "4"};
            string condition = "\nChoose: ";
            condition += "\n1 - to show only approved;";
            condition += "\n2 - to show only rejected;";
            condition += "\n3 - to show only drafts;";
            condition += "\n4 - to show all.";
            Console.WriteLine(condition);
            return Confirm.choise_input(ops);
        }

        public static string SignFunc(Company c)
        {
            string choice = sign_choise();
            switch (choice)
            {
                case "1":
                    c.SignIn();
                    break;
                case "2":
                    c.SignUp();
                    break;
                case "3":
                    c.logout();
                    break;
                case "0":
                    break;
            }
            return choice;
        }
        public static string AdminFunc(Company c, Admin user)
        {
            string choice = admin_choise();
            switch (choice)
            {
                case "1":
                    user.DraftsReview(c);
                    break;
                case "2":
                    user.DraftsReview(c, "Approved");
                    break;
                case "3":
                    user.DraftsReview(c, "Rejected");
                    break;
                case "4":
                    user.addTransaction(c);
                    break;
                case "0":
                    break;
            }
            return choice;
        }

        public static string StaffFunc(Company c, Staff user)
        {
            string choice = staff_choise();
            switch (choice)
            {
                case "1":
                    switch (status_choise())
                    {
                        case "1":
                            user.ShowMyDrafts(c, "Approved");
                            break;
                        case "2":
                            user.ShowMyDrafts(c, "Rejected");
                            break;
                        case "3":
                            user.ShowMyDrafts(c, "Draft");
                            break;
                        case "4":
                            user.ShowMyDrafts(c);
                            break;
                    }
                    break;
                case "2":
                    user.ShowAllApprovedDrafts(c);
                    break;
                case "3":
                    user.addTransaction(c);
                    break;
                case "4":
                    user.RemoveDraft(c);
                    break;
                case "5":
                    user.EditDraft(c);
                    break;
                case "6":
                    user.ResendDraft(c);
                    break;
                case "0":
                    break;
            }
            return choice;
        }
        public static void mainMenu(Company c)
        {
            
            while (SignFunc(c) != "0")
            {
                string[] u = c.CurrentUser.Split('\n');
                string choice = "";
                switch (u[0])
                {
                    case "ADMIN:":
                        while(choice != "0")
                        {
                            choice = AdminFunc(c, new Admin(u[1]));
                        }
                        break;
                    case "STAFF:":
                        while (choice != "0")
                        {
                            choice = StaffFunc(c, new Staff(u[1]));
                        }
                        break;
                }
            }
        }

    }
}
