using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace welcome.Models
{
    public class BillDetail
    {
        [Key,Required]
        public Guid BillDetailID { get; set; }
        public Guid BillID { get; set; }
        [ForeignKey("BillID")]
        public virtual Bill Bill { get; set; }

        public Guid DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

        private double _Percentage = 1d;
        [Range(minimum:0d,maximum:1d)]
        public double Percentage { get { return _Percentage; } set { _Percentage = value; } }
    }
}
