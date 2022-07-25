using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeitPlan.Models
{
    class Teacher
    {
        [PrimaryKey,AutoIncrement]
        public int Teacher_id { get; set; }
        public string Teacher_Name { get; set; }
        public string Teacher_Phone { get; set; }
        public string Teacher_Email { get; set; }
        public string Teacher_Password { get; set; }
        public string Teacher_Address { get; set; }
        public string Teacher_image { get; set; }

    }
}
