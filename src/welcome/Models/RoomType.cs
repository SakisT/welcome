using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class RoomType
    {
        private const string DEFAULT_WHITE_COLOR = "#ffffff";
        private string _Color = DEFAULT_WHITE_COLOR;
        public RoomType()
        {
            this.Color = DEFAULT_WHITE_COLOR;
        }
        public Guid id { get; set; }

        [StringLength(20), Required]
        public string Name { get; set; }

        [StringLength(10), Required]
        public string Abbreviation { get; set; }
        private bool _IsActive = true;
        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }

        [Required, Range(minimum: 1d, maximum: 20d)]
        public int SuggestedPax { get; set; }

        [Required, Range(minimum: 1d, maximum: 99d)]
        public int Grade { get; set; }

        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_WHITE_COLOR)]
        public string Color { get { return _Color; } set { _Color = value; } }

        public int DisplayOrder { get; set; }

        private bool _IncludeInOnlineAvailabilities = true;
        public bool IncludeInOnlineAvailabilities { get { return _IncludeInOnlineAvailabilities; } set { _IncludeInOnlineAvailabilities = value; } }

        public Guid HotelID { get; set; }

        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        public virtual ICollection<RoomTypeOnLineMapping> RoomTypeOnLineMappings { get; set; }

        [InverseProperty("RoomType")]
        public virtual ICollection<Room> Rooms { get; set; }

    }
}
