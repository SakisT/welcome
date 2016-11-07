using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Branch
    {
        [Key, Required]
        public Guid BranchID { get; set; }

        [StringLength(80), Required]
        public string Name { get; set; }

        [Required]
        public Guid HotelID { get; set; }

        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        public virtual BranchVardata Vardata { get; set; }

        public virtual BranchVardataReservation VardataReservations { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
