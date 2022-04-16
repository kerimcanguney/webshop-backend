using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_API.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public bool Requested_Reset { get; set; }
        public string? Reset_Code { get; set; }
        public DateTime? Reset_Time { get; set; }

        public int Account_Id { get; set; }
        public Account Account { get; set; }
    }
}
