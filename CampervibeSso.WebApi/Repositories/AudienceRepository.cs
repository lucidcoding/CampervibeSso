using CampervibeSso.WebApi.Data;
using CampervibeSso.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CampervibeSso.WebApi.Repositories
{
    public class AudienceRepository : IDisposable
    {
        private AuthContext _ctx;

        public AudienceRepository()
        {
            _ctx = new AuthContext();
        }

        public void Add(Audience audience)
        {
            _ctx.Audiences.Add(audience);
        }

        public Audience Find(string clientId)
        {
            return _ctx.Audiences.Find(clientId);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}