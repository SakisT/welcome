using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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

        public  List<Guid> HotelIDs;
        public  List<Guid> BranchIDs;

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

        public Guid[] GetActiveHotels()
        {
            Guid[] returnvalue = new Guid[] { Guid.Empty };
            var sessionvalue = _accessor.HttpContext.Session.GetString("ActiveHotels");
            if (sessionvalue != null)
            {
                returnvalue = sessionvalue.Split(new[] { ',' }).Select(r=>Guid.Parse(r)).ToArray();
            }
            else
            {
                if (HotelIDs.Count() != 0)
                {

                    returnvalue = new Guid[] { HotelIDs.FirstOrDefault() };
                }
                _accessor.HttpContext.Session.SetString("ActiveHotels", string.Join(",", returnvalue));
            }
            return returnvalue;
        }

        public Guid[] GetActiveBranches()
        {
            Guid[] returnvalue = new Guid[] { Guid.Empty };
            var sessionvalue = _accessor.HttpContext.Session.GetString("ActiveBranches");
            if (sessionvalue != null)
            {
                returnvalue = sessionvalue.Split(new[] { ',' }).Select(r => Guid.Parse(r)).ToArray();
            }
            else
            {
                if (HotelIDs.Count() != 0)
                {

                    returnvalue = new Guid[] { HotelIDs.FirstOrDefault() };
                }
                _accessor.HttpContext.Session.SetString("ActiveBranches", string.Join(",", returnvalue));
            }
            return returnvalue;
        }

        public void SetActiveHotels(Guid[] guids)
        {
            _accessor.HttpContext.Session.SetString("ActiveHotels", string.Join(",", guids));
        }

        public void SetActiveBranches(Guid[] guids)
        {
            _accessor.HttpContext.Session.SetString("ActiveBranches", string.Join(",", guids));
        }
    }
}
