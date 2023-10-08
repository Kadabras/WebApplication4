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
