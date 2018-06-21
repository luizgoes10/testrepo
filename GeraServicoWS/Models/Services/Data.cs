using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeraServicoWS.Models.Services
{
    public abstract class Data
    {
        public abstract void SaveOwner(Owner owner);
        public abstract List<Owner> GetOwners();
        public abstract Owner GetOwner(string login);
        public abstract void SaveNewApi(Owner owner, params string[] values);
        public abstract Dictionary<string, string> GetTableApi(string owner);
    }
}