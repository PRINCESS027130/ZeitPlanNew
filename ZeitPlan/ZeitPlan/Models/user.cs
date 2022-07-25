using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeitPlan.Models
{
   public class user
    {
        [PrimaryKey,AutoIncrement]
        public int Userid { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
