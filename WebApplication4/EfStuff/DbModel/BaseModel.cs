using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static System.Net.Mime.MediaTypeNames;
using System.Security;

namespace WebApplication4.EfStuff.DbModel
{
    public class BaseModel
    {
        public long Id { get; set; }
    }
}
