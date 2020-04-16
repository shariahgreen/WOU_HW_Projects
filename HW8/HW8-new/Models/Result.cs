namespace HW8_new.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Result
    {
        public int ID { get; set; }

        public int RaceID { get; set; }

        public int MeetID { get; set; }

        public float RaceTime { get; set; }

        public int AthleteID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Meet Meet { get; set; }

        public virtual Race Race { get; set; }
    }
}
