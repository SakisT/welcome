using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class HotelVardataInvoice
    {
        [ForeignKey("Hotel")]
        public Guid HotelVardataInvoiceId { get; set; }
        public virtual Hotel Hotel { get; set; }

        private int _InvoiceCopies = 1;
        public int InvoiceCopies { get { return _InvoiceCopies; } set { _InvoiceCopies = value; } }

        private Invoice.InvoiceView _DefaultView = Invoice.InvoiceView.Detailed;
        public Invoice.InvoiceView DefaultView { get { return _DefaultView; } set { _DefaultView = value; } }

        private string _ArrangementDescription = "Διαμονή / Arrangement";
        [StringLength(50)]
        public string ArrangementDescription { get { return _ArrangementDescription; } set { _ArrangementDescription = value; } }

        [DataType(DataType.MultilineText)]
        public string InvoiceEmailBody { get; set; }

        public bool IncludePaxInInvoice { get; set; }

        public bool ResetInvoiceNumbers { get; set; }

        public Invoice.PaymentMethod DefaultPaymentMethod { get; set; }

        public bool ObligatoryAddressAtInvoices { get; set; }

        [StringLength(40), DataType(DataType.EmailAddress)]
        public string SendInvoiceToHotelMailAsCC { get; set; }


    }
}
