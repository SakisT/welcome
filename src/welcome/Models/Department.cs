using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Department
    {
        [Key, Required]
        public Guid DepartmentID { get; set; }

        public int DisplayOrder { get; set; }

        [Required, StringLength(35)]
        public string Name { get; set; }

        [Range(minimum: 0d, maximum: 1),DisplayFormat(DataFormatString ="{0:N2}",ApplyFormatInEditMode =true)]
        public decimal VatPercentage { get; set; }
        [Range(minimum: 0d, maximum: 1), DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TaxPrcentage { get; set; }

        private bool _IsActive = true;

        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }

        [Required]
        public Guid HotelID { get; set; }

        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        public virtual ICollection<BoardPart> BoardParts { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }

        //public virtual ICollection<Supplement> Supplements { get; set; }

    }
}
