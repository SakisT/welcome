using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Deposit
    {
        [Key, Required]
        public Guid DepositID { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime HotelDate { get; set; }
        public decimal Euro { get; set; }
        [StringLength(25), DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [StringLength(25)]
        public string CardHolder { get; set; }
        [StringLength(10)]
        public string CCV { get; set; }
        public bool IsPreAuthorization { get; set; }
        [Range(minimum: 1, maximum: 12)]
        public int Expiration_Month { get; set; }
        [Range(minimum: 1900, maximum: 2100)]
        public int Expiration_Year { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public DateTime DepositTimeStamp { get; set; }

        public Guid UserId { get; set; }

        public Guid? ReservationID { get; set; }
        [ForeignKey("ReservationID")]
        public virtual Reservation Reservation { get; set; }

        public Guid? StayRoomID { get; set; }
        [ForeignKey("StayRoomID")]
        public virtual StayRoom StayRoom { get; set; }

        public Guid? CreditCardOrBankID { get; set; }
        [ForeignKey("CreditCardOrBankID")]
        public virtual Agent CreditCardOrBank { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
