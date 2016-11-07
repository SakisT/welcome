using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class BoardPart
    {
        public Guid BoardPartID { get; set; }

        [Range(minimum: 0, maximum: 1)]
        public double ParticipationRate { get; set; }

        public Guid BoardID { get; set; }
        [ForeignKey("BoardID")]
        public virtual Board Board { get; set; }

        public Guid DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

    }
}
