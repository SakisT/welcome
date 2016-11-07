using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class VaryingStay
    {
        [Key, Required]
        public Guid VaryingStayID { get; set; }

        [DataType(DataType.Date)]
        public DateTime HotelDate { get; set; }
        public decimal? RoomPrice { get; set; }
        public bool IsAgentCharge { get; set; }

        public Guid StayRoomID { get; set; }
        [ForeignKey("StayRoomID")]
        public virtual StayRoom StayRoom { get; set; }

        public Guid BoardID { get; set; }
        [ForeignKey("BoardID")]
        public Board Board { get; set; }

        public Guid PricelistID { get; set; }
        [ForeignKey("PricelistID")]
        public virtual Pricelist PriceList { get; set; }

        public Guid ChargeRoomTypeID { get; set; }
        [ForeignKey("ChargeRoomTypeID")]
        public virtual RoomType ChargeRoomType { get; set; }

        public virtual ICollection<Supplement> Supplements { get; set; }
    }
}
