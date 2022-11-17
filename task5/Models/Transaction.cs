using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPIAuthSem4.Models
{
    public class Transaction
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
    }
}
