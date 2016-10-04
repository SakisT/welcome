using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Preference
    {
        [Key, Required]
        public Guid id { get; set; }

        public Guid HotelId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ReservationReference> ReservationReferences { get; set; }
    }

    public class ReservationReference
    {
        public Guid ReservationID { get; set; }
        public virtual Reservation Reservation { get; set; }

        public Guid PreferenceID { get; set; }
        public virtual Preference Preference { get; set; }
    }
}
