using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class HotelGroup
    {
        [Key, Required]
        public Guid id { get; set; }

        [StringLength(80), Required]
        public string Name { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
