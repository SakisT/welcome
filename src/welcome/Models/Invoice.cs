using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Invoice
    {
        [Key, Required]
        public Guid InvoiceID { get; set; }
        public InvoiceView ArrangementsView { get; set; }

        public InvoiceView ExtrasView { get; set; }
        public enum InvoiceView
        {
            Detailed = 2,
            PerDay = 4,
            OneRecord = 8,
            PerRoom = 16,
            PerRoomAndEuro = 32
        }

        public enum PaymentMethod
        {
            Cash = 1,
            CreditCard = 2,
            ChargeAgent = 3,
            Bank = 4,
            Check = 5
        }

        //public Guid BranchId { get; set; }

        //[ForeignKey("BranchId")]
        //public Branch Branch { get; set; }

        public Guid InvoiceDetailID { get; set; }
        [ForeignKey("InvoiceDetailID")]
        public virtual InvoiceDetail InvoiceDetail { get; set; }


        public virtual ICollection<Bill> BillRecords { get; set; }
    }
}
