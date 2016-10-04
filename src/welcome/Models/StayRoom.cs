using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class StayRoom
    {
        [Key, Required]
        public Guid id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Arrival { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Departure { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ActualDeparture { get; set; }

        public DateTime? ArrivalTimeStamp { get; set; }
        public DateTime? DepartureTimeStamp { get; set; }

        public Guid ArrivalUserId { get; set; }
        public Guid DepartureUserId { get; set; }
        public decimal AgentCommissionPercentage { get; set; }

        private ReservationStatus _Status = ReservationStatus.Reservation;
        public ReservationStatus Status { get { return _Status; } set { _Status = value; } }

        public bool IsFree { get; set; }

        public int Adults { get; set; }
        public int Children { get; set; }
        public int Children2 { get; set; }
        public int Infants { get; set; }
        public int AdultsCharge { get; set; }
        public int ChildrenCharge { get; set; }
        public int Children2Charge { get; set; }
        public int InfantsCharge { get; set; }
        public int Pax { get { return (Adults + Children + Children2 + Infants); } }

        public decimal? RoomPrice { get; set; }

        [StringLength(30)]
        public string Reference { get; set; }

        [StringLength(30)]
        public string ChannelReference { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        private bool _IsLocked = false;
        public bool IsLocked { get { return _IsLocked; } set { _IsLocked = value; } }

        public bool IsAgentCharge { get; set; }

        private const string DEFAULT_STAYROOM_COLOR = "#ffffff";
        private string _Color = DEFAULT_STAYROOM_COLOR;
        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_STAYROOM_COLOR)]
        public string Color { get { return _Color; } set { _Color = value; } }

        public Guid ReservationID { get; set; }

        [ForeignKey("ReservationID")]
        public virtual Reservation Reservation { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }

        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public virtual Agent Agent { get; set; }


        public Guid PricelistID { get; set; }
        [ForeignKey("PricelistID")]
        public virtual Pricelist Pricelist { get; set; }

        public Guid BoardID { get; set; }
        [ForeignKey("BoardID")]
        public virtual Board Board { get; set; }


        public Guid? RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual Room Room { get; set; }

        public Guid ChargeRoomTypeID { get; set; }
        [ForeignKey("ChargeRoomTypeID")]
        public virtual RoomType ChargeRoomType { get; set; }
        
        public virtual ICollection<StayPerson> StayPersons { get; set; }

        [ForeignKey("StayRoomID")]
        public virtual ICollection<Bill> Bills { get; set; }

        [InverseProperty("StayRoom")]
        public virtual ICollection<VaryingStay> VaryingStays { get; set; }

    }
}
