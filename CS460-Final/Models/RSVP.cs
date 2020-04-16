namespace CS460_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RSVP
    {
        public int ID { get; set; }

        public int Event { get; set; }

        public int Person { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual Event Event1 { get; set; }

        public virtual Person Person1 { get; set; }
    }
}
