using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_API.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool Email_Verified { get; set; }
        public IpAdress IpAdress { get; set; }
        public Transaction Transaction { get; set; }

        public Password Password { get; set; }

    }
}
