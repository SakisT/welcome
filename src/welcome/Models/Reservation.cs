using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Reservation
    {
        [Key, Required]
        public Guid ReservationID { get; set; }
        public int AA { get; set; }
        [Required, StringLength(50), Display(Name ="Guest")]
        public string GuestOrGroup { get; set; }

        public decimal? AskPrePay { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:d/M/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime? AskPrePayDate { get; set; }
        [StringLength(100)]
        public string AskPrePayRemarks { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Required]
        public Guid HotelID { get; set; }

        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        public virtual ICollection<StayRoom> StayRooms { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }

        public virtual ICollection<ReservationReference> ReservationReferences { get; set; }

    }

    public enum ReservationStatus
    {
        Reservation = 1,
        Arrived = 2,
        Departed = 3,
        Canceled = 4,
        NonShow = 5,
        Option = 6,
        Offer = 7
    }
}
