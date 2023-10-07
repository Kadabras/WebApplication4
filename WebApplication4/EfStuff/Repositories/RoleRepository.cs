using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.EfStuff.DbModel;

namespace WebApplication4.EfStuff.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
