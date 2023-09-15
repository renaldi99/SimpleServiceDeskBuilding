namespace SimpleServiceDeskBuilding.Models
{
    public class UserModel
    {
        public Guid id { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string identity_employee { get; set; }
        public string is_active { get; set; }
        public string role { get; set; }
        public string refresh_token { get; set; }
    }
}
