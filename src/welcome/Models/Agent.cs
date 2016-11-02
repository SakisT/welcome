using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace welcome.Models
{
    public class Agent
    {
        [Key, Required]
        public Guid id { get; set; }
        private AgentType _Type = AgentType.Agent;
        public AgentType Type { get { return _Type; } set { _Type = value; } }
        public enum AgentType
        {
            Agent = 1,
            Company = 2,
            CreditCard = 3,
            Individual = 4,
            Internet = 6,
            Bank = 7,
            Result = 8
        }
        [Required, StringLength(15)]
        public string Code { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        [StringLength(15)]
        public string ChannelID { get; set; }
        private bool _IsActive = true;
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }

        [Required]
        public Guid HotelID { get; set; }
        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        public virtual AgentVardata Vardata { get; set; }

        public virtual ICollection<Bill> CardOrBankBills { get; set; }

        public virtual ICollection<Bill> AgentBills { get; set; }

        public virtual ICollection<StayRoom> StayRooms { get; set; }
    }
}
