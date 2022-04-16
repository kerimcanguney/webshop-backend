using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Account_Id { get; set; }
        public string Payment_Type { get; set; }
        public int Amount { get; set; }
        public string Order_Id { get; set; }
        public string Billing_Adress { get; set; }
        public Account Account { get; set; }

    }
}
