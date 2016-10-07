using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace welcome.Services
{
    public class UserAccessInfo
    {
        private readonly IHttpContextAccessor _accessor;

        private readonly List<Guid> HotelIDs;
        private readonly List<Guid> BranchIDs;

        public UserAccessInfo(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
            ClaimsIdentity identity = (ClaimsIdentity)_accessor.HttpContext.User.Identity;
            HotelIDs = (from p in identity.Claims.Where(r => r.Type == "HotelID") let g = Guid.Parse(p.Value) select g).ToList();
            BranchIDs = (from p in identity.Claims.Where(r => r.Type == "BranchID") let g = Guid.Parse(p.Value) select g).ToList();
        }

        public string GetValue()
        {
            return _accessor.HttpContext.Request.Query["value"];
        }
    }
}
