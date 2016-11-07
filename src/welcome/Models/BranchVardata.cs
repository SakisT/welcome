using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class BranchVardata
    {
        [ForeignKey("Branch")]
        public Guid BranchVardataID { get; set; }

        [StringLength(11)]
        public string SMSSignature { get; set; }

        [StringLength(8)]
        public string LicenseSerialNumber { get; set; }

        [StringLength(30)]
        public string AFM { get; set; }

        [StringLength(30)]
        public string DOY { get; set; }

        [StringLength(100)]
        public string Job { get; set; }

        [StringLength(30)]
        public string Address_Country { get; set; }

        [StringLength(50)]
        public string Address_City { get; set; }

        [StringLength(10), DataType(DataType.PostalCode)]
        public string Address_PostCode { get; set; }

        [StringLength(100)]
        public string Address_AddressLine1 { get; set; }

        [StringLength(100)]
        public string Address_AddressLine2 { get; set; }

        [StringLength(30)]
        public string Contact_Phone1 { get; set; }
        [StringLength(30)]
        public string Contact_Phone2 { get; set; }
        [StringLength(30)]
        public string Contact_Mobile { get; set; }
        [StringLength(30)]
        public string Contact_Fax { get; set; }
        [StringLength(50)]
        public string Contact_email { get; set; }
        [DataType(DataType.Url), StringLength(100)]
        public string Contact_WebSite { get; set; }

        public bool OnlyReservations { get; set; }

        private const string DEFAULT_WHITE_COLOR = "#ffffff";
        private string _Color = DEFAULT_WHITE_COLOR;

        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_WHITE_COLOR)]
        public string Color { get { return _Color; } set { _Color = value; } }

        public bool IsozygioPerYear { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
