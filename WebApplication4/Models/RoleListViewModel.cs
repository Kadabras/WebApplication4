namespace WebApplication4.Models
{
    public class RoleListViewModel
    {
        public PaggerViewModel<UserViewModel> PaggerViewModel { get; set; }
        public bool IsDescending { get; set; }
        public string LastSort { get; set; }
        public string LastFilter { get; set; }
        public string LastString { get; set; }
    }
}