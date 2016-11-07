using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class StayPerson
    {
        [Key, Required]
        public Guid StayPersonID { get; set; }

        private Symbol _PaxSymbol = Symbol.X;
        public Symbol PaxSymbol { get { return _PaxSymbol; } set { _PaxSymbol = value; } }
        public enum Symbol
        {
            X,
            C,
            C2,
            I
        }

        [DataType(DataType.DateTime)]
        public DateTime Arrival { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ActualDeparture { get; set; }
        public int Order { get; set; }
        private bool _IsFree = false;
        public bool IsFree { get { return _IsFree; } set { _IsFree = value; } }

        public Guid StayRoomID { get; set; }
        [ForeignKey("StayRoomID")]
        public virtual StayRoom StayRoom { get; set; }

        [ForeignKey("StayPersonID")]
        public virtual ICollection<Bill> Bills { get; set; }

        public Guid CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }


    }
}
