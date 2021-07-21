using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Vimo.ApplicationCore.Entities.UserAggregate;

namespace Vimo.ApplicationCore.Specifications
{
    public class UserSpecification : Specification<User>
    {
        public UserSpecification()
        {

        }
        public UserSpecification(string username, string password)
        {
           
            Query.Where(x => x.Username == username && x.Password == password);
        }


    }


}