using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vimo.ApplicationCore.Entities.UserAggregate
{
    public class UserCompany : UpsertEntity
    {
        public UserCompany()
        {

        }

        public UserCompany(int id)
        {
            Id = id;
        }
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
        
    }
}