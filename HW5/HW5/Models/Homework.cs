using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW5.Models
{
    public class Homework
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Title"),Required]
        [StringLength(64)]
        public string Title { get; set; }

        [DisplayName("Due Date"), Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DueDate { get; set; }

        [DisplayName("Due Time"), Required]
        public TimeSpan DueTime { get; set; }

        [DisplayName("Priority"), Required]
        public int HWPriority { get; set; }

        [DisplayName("Department"), Required]
        [StringLength(64)]
        public string Department { get; set; }

        [DisplayName("Course"), Required]
        public int Course { get; set; }

        [DisplayName("Notes"), Required]
        [StringLength(128)]
        public string Notes { get; set; }
    }
}