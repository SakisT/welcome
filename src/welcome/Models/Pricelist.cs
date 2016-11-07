using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Pricelist
    {
        [Key, Required]
        public Guid PricelistID { get; set; }
        [Required, StringLength(10)]
        public string Code { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public Guid HotelID { get; set; }
        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

    }
}
