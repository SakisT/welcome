using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class RoomTypeOnLineMapping
    {
        [Key, Required]
        public Guid id { get; set; }

        [Required, StringLength(10)]
        public string OnLineID { get; set; }

        public Guid RoomTypeID { get; set; }
        [ForeignKey("RoomTypeID")]
        public virtual RoomType RoomType { get; set; }
    }
}
