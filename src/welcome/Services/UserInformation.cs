using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace welcome.Services
{
    public class UserInformation : IUserInfo, IHttpContextAccessor
    {
        private List<Guid> _BranchGuids;
        private List<Guid> _HotelGuids;
        public List<Guid> BranchGuids
        {
            get
            {
               if (_BranchGuids == null)
                {
                    Refresh();
                }
                return _BranchGuids;
            }

            set
            {
                _BranchGuids = value;
            }
        }

        public List<Guid> HotelGuids
        {
            get
            {
                if (_HotelGuids == null)
                {
                    Refresh();
                }
                return _HotelGuids;
            }

            set
            {
                _HotelGuids=value;
            }
        }

        public void Refresh()
        {
            if (HttpContext?.User?.Identity?.IsAuthenticated??false)
            {
                ClaimsIdentity identity = (ClaimsIdentity)HttpContext.User.Identity;
                _BranchGuids= identity.Claims.Where(r => r.Type == "BranchID").Select(r => Guid.Parse(r.Value)).ToList();
                _HotelGuids = identity.Claims.Where(r => r.Type == "HotelID").Select(r => Guid.Parse(r.Value)).ToList();
            }
        }

        public HttpContext HttpContext { get; set; }

       
    }
}
