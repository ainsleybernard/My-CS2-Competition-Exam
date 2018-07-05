namespace TC.SCBS.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TCSCBSContext : DbContext
    {
        public TCSCBSContext()
            : base("name=TCSCBSContext")
        {
            //existing database initilization not required.
            Database.SetInitializer<TCSCBSContext>(null);

        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Center> Centers { get; set; }
        public virtual DbSet<CenterType> CenterTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.Client_Full_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Center>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Center>()
                .Property(e => e.Street_Address)
                .IsUnicode(false);

            modelBuilder.Entity<CenterType>()
                .Property(e => e.Value)
                .IsUnicode(false);
        }
    }
}
