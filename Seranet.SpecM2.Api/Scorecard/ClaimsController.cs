﻿using Seranet.SpecM2.Data;
using Seranet.SpecM2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Seranet.SpecM2.Api.Scorecard
{
    public class ClaimsController : BaseApiController
    {
        //Shift this method from here elsewhere
        private Status getStatus(int k)
        {

            Status s = Status.PENDING;
            switch (k)
            {
                case 1:
                    s = Status.APPROVED;
                    break;
                case 2:
                    s = Status.REJECTED;
                    break;
                case 3:
                    s = Status.PENDING;
                    break;

            }

            return s;
        }

        // GET api/claims
        public IEnumerable<Claim> Get()
        {
            return context.Claims;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void post([FromBody] Claim[] claim)
        {

            for (int i = 0; i < claim.Length; i++)
            {
                var claimToAdd = claim[i];
                claimToAdd.GUID = Guid.NewGuid();

                claimToAdd.CreatedTime = DateTime.Now;
                claimToAdd.Practice = context.Practices.FirstOrDefault(t => t.Id == claimToAdd.Practice.Id);
                claimToAdd.Project = context.Projects.FirstOrDefault(t => t.Id == claimToAdd.Project.Id);
                claimToAdd.Status = getStatus(3);
                context.Claims.Add(claimToAdd);
                context.SaveChanges();
            }


        }
    }
}
