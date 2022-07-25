
    using System;
    using System.Collections.Generic;
    

    public partial class TBL_TEACHER
    {
        
        public int TEACHER_ID { get; set; }

        public string TEACHER_NAME { get; set; }

        public string TEACHER_EMAIL { get; set; }

        public string TEACHER_PASSWORD { get; set; }

        public string TEACHER_PHNO { get; set; }

        public string TEACHER_ADDRESS { get; set; }

        public int DEPARTMENT_FID { get; set; }

        public int TBL_DEPARTMENTFID { get; set; }

       
        public int TBL_TIMETABLEFID { get; set; }
    }
