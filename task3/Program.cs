using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_sem4_t3
{
    class Program
    {
        static void Main(string[] args)
        {

            Company c = new Company("../../Users.txt", "../../Records.txt");
            Menu.mainMenu(c);
            /*
     Data for passwords
STAFF:
Tony Stark ironman@gmail.com hvmjlJvv 
STAFF:
Tim Drake robin@gmail.com KohvbjnPh
ADMIN:
Sherlock Holmes sociopath@gmail.com 9uhutgjjYvh
STAFF:
Uliana Liesnik liana@gmail.com uIrka1924
STAFF:
Astrid Lindgren writer@yachoo.com tcjOghvcn
             */
        }
    }
}
