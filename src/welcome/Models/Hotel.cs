using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Hotel
    {
        [Key, Required]
        public Guid HotelID { get; set; }

        [StringLength(80), Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime HotelDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [StringLength(100)]
        public string Dealer { get; set; }

        private bool _IsPayngSupport = true;
        public bool IsPayingSupport { get { return _IsPayngSupport; } set { _IsPayngSupport = value; } }

        public Guid HotelGroupID { get; set; }

        [ForeignKey("HotelGroupID")]
        public virtual HotelGroup HotelGroup { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
        public virtual HotelVardataInvoice VardataInvoice { get; set; }

        public virtual HotelVardataPlan VardataPlan { get; set; }

        public virtual ICollection<RoomType> RoomTypes { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Board> Boards { get; set; }

        public virtual ICollection<Agent> Agents { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        [InverseProperty("Hotel")]
        public virtual ICollection<Pricelist> Pricelists { get; set; }
    }
}
