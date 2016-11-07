using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace welcome.Models
{
    public class Supplement
    {
        [Key, Required]
        public Guid SupplementID { get; set; }
        [StringLength(40)]
        public string InvoiceDescription { get; set; }
        public decimal Euro { get; set; }

        public bool IsAgentCharge { get; set; }

        public Guid VaryingStayID { get; set; }
        [ForeignKey("VaryingStayID")]
        public virtual VaryingStay VaryingStay { get; set; }

        public Guid? BoardID { get; set; }
        [ForeignKey("BoardID")]
        public virtual Board Board { get; set; }

        public Guid? DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }


    }
}
