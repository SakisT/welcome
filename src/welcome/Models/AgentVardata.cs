using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class AgentVardata
    {
        [ForeignKey("Agent")]
        public Guid AgentVardataID { get; set; }
        [DataType(DataType.EmailAddress), StringLength(50)]
        public string emailForInvoices { get; set; }

        [StringLength(200)]
        public string PreDefinedInvoiceRemarks { get; set; }

        [StringLength(20)]
        public string AccountCode { get; set; }
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

        [StringLength(255)]
        public string Person { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateOn { get; set; }

        public decimal Commission { get; set; }

        #region Color
        private const string DEFAULT_AGENT_COLOR = "#66b3ff";
        private string _Color = DEFAULT_AGENT_COLOR;
        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_AGENT_COLOR)]
        public string Color { get { return _Color; } set { _Color = value; } }
        #endregion

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual Agent Agent { get; set; }

        public Guid? InvoiceDetailID { get; set; }
        [ForeignKey("InvoiceDetailID")]
        public virtual InvoiceDetail InvoiceDetail { get; set; }



    }
}
