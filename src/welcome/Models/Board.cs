using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Board
    {
        public Guid BoardID { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }
        [Required, StringLength(5)]
        public string Abbrevation { get; set; }
        public int DisplayOrder { get; set; }

        public Guid HotelID { get; set; }
        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<BoardPart> BoardParts { get; set; }
    }
}
