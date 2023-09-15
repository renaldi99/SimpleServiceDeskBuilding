namespace SimpleServiceDeskBuilding.DTOs
{
    public class UserReqDto
    {
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string identity_employee { get; set; }
    }

    public class UserReqIdDto
    {
        public string user_id { get; set; }
    }
}
