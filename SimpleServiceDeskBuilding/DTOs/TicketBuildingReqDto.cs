namespace SimpleServiceDeskBuilding.DTOs
{
    public class TicketBuildingReqDto
    {
        public string id_user_created { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public IFormFile file { get; set; }
    }

    public class TicketBuildingByIdDto
    {
        public string ticket_id { get; set; }
    }
}
