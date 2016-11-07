using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class Nationality
    {
        [Key, Required]
        public int NationalityID { get; set; }

        [StringLength(3)]
        public string Abbrevation { get; set; }

        [StringLength(50)]
        public string EnglishName { get; set; }

        [StringLength(50)]
        public string GreekName { get; set; }

        //public virtual ICollection<Customer> Customers { get; set; }
    }
}
