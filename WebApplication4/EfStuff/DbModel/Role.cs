namespace WebApplication4.EfStuff.DbModel
{
    public class Role : BaseModel
    {
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";
        public const string User = "User";
        public const string Support = "Support";

        public string Name { get; set; }
        public virtual List<User> RoleUsers { get; set; }
    }
}