using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Customer
    {
        [Key, Required]
        public Guid CustomerID { get; set; }


        public int Folio { get; set; }
        [StringLength(30)]
        public string Person_LastName { get; set; }
        [StringLength(20)]
        public string Person_FirstName { get; set; }

        public string Person_FullName
        {
            get
            {
                return string.Format($"{Person_LastName} {Person_FirstName}").Trim();
            }
        }
        [DataType(DataType.Date)]

        public DateTime? Person_BirthDate { get; set; }

        public int? Person_Age
        {
            get
            {
                if (Person_BirthDate.HasValue)
                {
                    return Convert.ToInt16((DateTime.Today - Person_BirthDate.Value).TotalDays / 365d);
                }
                return null;
            }
        }

        [StringLength(20)]
        public string FathersName { get; set; }
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

        public bool HasAddress
        {
            get
            {
                return (Address_AddressLine1 + Address_AddressLine2 + Address_City + Address_Country + Address_PostCode).Trim() != string.Empty;
            }
        }

        [StringLength(50)]
        public string Job { get; set; }

        [StringLength(40)]
        public string Passport { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        private sex _Sex = sex.Male;
        public sex Sex { get { return _Sex; } set { _Sex = value; } }

        public enum sex
        {
            Male = 1,
            Female = 2
        }
        public bool IsBlackListed { get; set; }

        public Guid HotelID { get; set; }
        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        public int? NationalityID { get; set; }
        [ForeignKey("NationalityID")]
        public virtual Nationality Nationality { get; set; }

        public Guid? InvoiceDetailId { get; set; }
        [ForeignKey("InvoiceDetailId")]
        public virtual InvoiceDetail InvoiceDetail { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<StayPerson> StaysAsPerson { get; set; }
    }
}
