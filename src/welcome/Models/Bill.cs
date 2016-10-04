using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Bill
    {
        [Key, Required]
        public Guid id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HotelDate { get; set; }
        public DateTime PreInvoiceHotelDate { get; set; }

        public DateTime BillTimeStamp { get; set; }
        public Guid? UserId { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Parastatiko { get; set; }
        [Range(minimum: 0d, maximum: 1)]
        public double VatPercentage { get; set; }

        [Range(minimum: 0d, maximum: 1)]
        public double TaxPercentage { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public decimal Euro { get; set; }

        private BillType _Type = BillType.Debit;
        public BillType Type { get { return _Type; } set { _Type = value; } }
        public enum BillType
        {
            Debit = 1,//Discount is Negative (-100)
            Credit = 2//Cash Or Credit Card or Bank 
        }
        [Range(minimum: 0, maximum: 1)]
        public double? AgentCommission { get; set; }

        public Guid BranchID { get; set; }
        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }

        public Guid? CreditCardOrBankID { get; set; }
        [ForeignKey("CreditCardOrBankID")]
        public virtual Agent CreditCardOrBank { get; set; }

        public Guid? StayPersonID { get; set; }
        [ForeignKey("StayPersonID")]
        public virtual StayPerson StayPerson { get; set; }
        /// <summary>
        /// Αν υπάρχει τιμή και στον Agent και στο StayPersonID πρόκειτε για Internet Agent
        /// </summary>
        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public virtual Agent Agent { get; set; }

        public Guid? StayRoomID { get; set; }
        [ForeignKey("StayRoomID")]
        public virtual StayRoom StayRoom { get; set; }

        public Guid? InvoiceID { get; set; }
        [ForeignKey("InvoiceID")]
        public virtual Invoice Invoice { get; set; }

        public Guid? RoomTypeID { get; set; }
        [ForeignKey("RoomTypeID")]
        public virtual RoomType RoomType { get; set; }

        public Guid? DepositID { get; set; }
        [ForeignKey("DepositID")]
        public virtual Deposit Deposit { get; set; }

        public Guid? PricelistID { get; set; }
        [ForeignKey("PricelistID")]
        public virtual Pricelist Pricelist { get; set; }

        public Guid? RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual Room Room { get; set; }

        public Guid? BoardID { get; set; }
        [ForeignKey("BoardID")]
        public virtual Board Board { get; set; }

    }
}
