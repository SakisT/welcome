using Microsoft.EntityFrameworkCore;
using welcome.Models;

namespace welcome.Data
{
    public class WelcomeContext: DbContext
    {
        public WelcomeContext(DbContextOptions<WelcomeContext> options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        #region  R e s e r v a t i o n s 

        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Preference> Preferences { get; set; }

        public DbSet<ReservationReference> ReservationReferences { get; set; }

        public DbSet<StayRoom> StayRooms { get; set; }
        public DbSet<StayPerson> StayPersons { get; set; }
        public DbSet<VaryingStay> VaryingStays { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        #endregion

        #region   P r i c e l i s t s  
        public DbSet<Pricelist> Pricelists { get; set; }
        #endregion

        #region  A g e n t s 
        public DbSet<AgentVardata> AgentsVardatas { get; set; }
        public DbSet<Agent> Agents { get; set; }
        #endregion

        #region  D e p a r t m e n t s   A n d   B o a r d s 
        public DbSet<BoardPart> BoardParts { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Department> Departments { get; set; }
        #endregion

        #region   R o o m s   A n d   R o o m T y p e s 
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomTypeOnLineMapping> RoomTypeOnLineMappings { get; set; }
        #endregion

        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        #region   H o t e l   S e t t i n g s 
        public DbSet<HotelGroup> HotelGroups { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelVardataInvoice> HotelVardataInvoices { get; set; }
        public DbSet<HotelVardataPlan> HotelVardataPlans { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchVardata> BranchVardatas { get; set; }
        public DbSet<BranchVardataReservation> BranchVardataReservations { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Initialize a many-to-many relation between Reservation as Preferences (each reservation has many preferences and each preference belongs to many reservations
            modelBuilder.Entity<ReservationReference>().HasKey(t => new { t.ReservationID, t.PreferenceID });
            modelBuilder.Entity<ReservationReference>().HasOne(pt => pt.Reservation).WithMany(p => p.ReservationReferences).HasForeignKey(pt => pt.ReservationID);
            modelBuilder.Entity<ReservationReference>().HasOne(pt => pt.Preference).WithMany(t => t.ReservationReferences).HasForeignKey(pt => pt.PreferenceID);
            modelBuilder.Entity<BoardPart>().HasOne(r => r.Department).WithMany(r => r.BoardParts).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<BillDetail>().HasOne(r => r.Department).WithMany(r => r.BillDetails).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<Agent>().HasMany(r => r.CardOrBankBills).WithOne(r => r.CreditCardOrBank).IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<Agent>().HasMany(r => r.CardOrBankBills).WithOne(r => r.CreditCardOrBank).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<Agent>().HasMany(r => r.AgentBills).WithOne(r => r.Agent).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<Bill>().HasOne(r => r.StayRoom).WithMany(r => r.Bills).IsRequired(required: false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<Bill>().HasOne(r => r.StayPerson).WithMany(r => r.Bills).IsRequired(required: false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<Bill>().HasOne(r => r.Room).WithMany().IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<Bill>().HasOne(r => r.Pricelist).WithMany().IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<Invoice>().HasMany(r => r.BillRecords).WithOne(r => r.Invoice).IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);
            modelBuilder.Entity<InvoiceDetail>().HasMany(r=>r.Invoices).WithOne(r => r.InvoiceDetail).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<AgentVardata>().HasOne(r => r.InvoiceDetail).WithOne().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);
            modelBuilder.Entity<Deposit>().HasMany(r => r.Bills).WithOne(r => r.Deposit).IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<Deposit>().HasOne(r => r.Reservation).WithMany(r => r.Deposits).IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);
            modelBuilder.Entity<Deposit>().HasOne(r => r.StayRoom).WithMany(r => r.Deposits).IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);
            modelBuilder.Entity<Reservation>().HasMany(r => r.StayRooms).WithOne(r => r.Reservation).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            
            modelBuilder.Entity<StayRoom>().HasOne(r => r.Pricelist).WithMany().IsRequired().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<StayRoom>().HasOne(r => r.Board).WithMany().IsRequired().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<StayRoom>().HasOne(r => r.ChargeRoomType).WithMany().IsRequired().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<StayRoom>().HasOne(r => r.Room).WithMany().IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<StayRoom>().HasOne(r => r.Agent).WithMany().IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);
            //modelBuilder.Entity<Agent>().HasMany(r => r.StayRooms).WithOne(r => r.Agent).IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);

            modelBuilder.Entity<RoomType>().HasMany(r => r.Rooms).WithOne(r => r.RoomType).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<VaryingStay>().HasOne(r => r.ChargeRoomType).WithMany().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            modelBuilder.Entity<Pricelist>().HasOne(r => r.Hotel).WithMany(r => r.Pricelists).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
