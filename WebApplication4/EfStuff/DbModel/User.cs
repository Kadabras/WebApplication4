using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static System.Net.Mime.MediaTypeNames;
using System.Security;

namespace WebApplication4.EfStuff.DbModel
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public virtual List<Role> Roles { get; set; }
    }

}
