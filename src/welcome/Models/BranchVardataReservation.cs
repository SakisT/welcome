using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class BranchVardataReservation
    {
        [ForeignKey("Branch")]
        public Guid BranchVardataReservationId { get; set; }

        public int UsualStay { get; set; }

        public int UsualNationalityID { get; set; }

        [ForeignKey("UsualNationalityID")]
        public Nationality UsualNationality { get; set; }

        private string _UsualPricelistCode = "99";
        [StringLength(20)]
        public string UsualPricelistCode { get { return _UsualPricelistCode; } set { _UsualPricelistCode = value; } }

        [StringLength(40), DataType(DataType.EmailAddress)]
        public string emailOnNonShowOrCancellation { get; set; }

        public bool ResetResAAEveryYear { get; set; }

        public bool? UseOfCreditCardPreAuthorization { get; set; }

        public bool AcceptPastDatesForDeposit { get; set; }

        public virtual Branch Branch { get; set; }

    }
}
