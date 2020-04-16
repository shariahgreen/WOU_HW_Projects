namespace CS460_Final.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using CS460_Final.Models;

    public partial class ManagementContext : DbContext
    {
        public ManagementContext()
            : base("name=ManagementContext_Azure")
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<RSVP> RSVPs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.RSVPs)
                .WithRequired(e => e.Event1)
                .HasForeignKey(e => e.Event);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.Coordinator);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RSVPs)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
