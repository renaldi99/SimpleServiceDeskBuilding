namespace SimpleServiceDeskBuilding.DTOs
{
    public class ActivityTicketBuildingReqDto
    {
        public string id_activity { get; set; }
        public string id_user_done { get; set; } // PIC Ticket
        public string status { get; set; }
        public string? description { get; set; }
        public IFormFile? file { get; set; }
    }

    public class GetAllActivityTicketBuildingDto
    {
        public Guid id_activity { get; set; }
        public Guid id_ticket { get; set; }
        public Guid pic { get; set; }
        public string status { get; set; }
        public string desc_activity { get; set; }
        public string file_activity { get; set; }
        public DateTime last_created_at { get; set; }
        public DateTime last_updated_at { get; set; }
        public DateTime last_finished_at { get; set; }
        public Guid id_user_created { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string file_attachment { get; set; }
    }
}
