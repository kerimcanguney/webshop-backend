using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_API.Models
{
    public class IpAdress
    {
        public int Id { get; set; }
        public string Ip1 { get; set; }
        public string? Ip2 { get; set; }
        public string? Ip3 { get; set; }
        public string LastUsed { get; set; }
        public int Account_Id { get; set; }
        public Account Account { get; set; }

    }
}
