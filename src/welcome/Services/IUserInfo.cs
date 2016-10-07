using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace welcome.Services
{
 public   interface IUserInfo
    {
       List< Guid> HotelGuids { get; set; }
       List<Guid> BranchGuids { get; set; }

        void Refresh();
    }
}
