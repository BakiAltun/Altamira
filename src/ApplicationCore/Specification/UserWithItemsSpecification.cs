using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Vimo.ApplicationCore.Entities.UserAggregate;

namespace Vimo.ApplicationCore.Specifications
{
    public class UserWithItemsSpecification : Specification<User>
    {
        public UserWithItemsSpecification()
        {
            Query
            .Include(x => x.Addresses);

            Query
            .Include(x => x.Companies);
        }
        public UserWithItemsSpecification(int id)
        { 
            Query
            .Include(x => x.Addresses);

            Query
            .Include(x => x.Companies);

            Query
            .Where(x => x.Id == id);
        }


    }


}