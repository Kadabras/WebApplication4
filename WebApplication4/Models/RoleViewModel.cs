namespace WebApplication4.Models
{
    public class RoleViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<string> Users { get; set; }
    }
}