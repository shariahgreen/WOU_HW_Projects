using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HW5.Models;

namespace HW5.DAL
{
    public class HomeworkContext : DbContext
    {
        public HomeworkContext() : base("name=HomeworkDB")
        {
            Database.SetInitializer<HomeworkContext>(null);
        }

        public virtual DbSet<Homework> MyHomework { get; set; }
    }
}