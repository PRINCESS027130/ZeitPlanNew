
    using System;
    using System.Collections.Generic;
   
    public partial class TBL_TIMETABLE
    {
      
        public int TIMETABLE_ID { get; set; }

        public int TEACHER_FID { get; set; }

        public int CLASS_FID { get; set; }

        public int SLOT_FID { get; set; }

        public int ROOM_FID { get; set; }

        public int COURSE_ASSIGN_FID { get; set; }

        public int TBL_CLASSFID { get; set; }

        public int TBL_COURSE_ASSIGNFID { get; set; }

        public int TBL_ROOMFID { get; set; }

        public int TBL_SLOTFID { get; set; }

        public int TBL_TEACHERFID { get; set; }
    }

