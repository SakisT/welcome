using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{

    public class InvoiceDetail
    {

        [Key, Required]
        public Guid id { get; set; }
        [StringLength(120)]
        public string InvoiceStructure_InvoiceName { get; set; }

        [StringLength(150)]
        public string InvoiceStructure_Job { get; set; }

        [StringLength(20)]
        public string InvoiceStructure_VATNumber { get; set; }

        [StringLength(80)]
        public string InvoiceStructure_TaxDepartment { get; set; }

        [StringLength(30)]
        public string InvoiceStructure_Address_Country { get; set; }

        [StringLength(50)]
        public string InvoiceStructure_Address_City { get; set; }

        [StringLength(10), DataType(DataType.PostalCode)]
        public string InvoiceStructure_Address_PostCode { get; set; }

        [StringLength(100)]
        public string InvoiceStructure_Address_AddressLine1 { get; set; }

        [StringLength(100)]
        public string InvoiceStructure_Address_AddressLine2 { get; set; }

        public Guid? HotelID { get; set; }
        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}