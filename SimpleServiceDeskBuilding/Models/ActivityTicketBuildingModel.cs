namespace SimpleServiceDeskBuilding.Models
{
    public class ActivityTicketBuildingModel
    {
        public Guid id { get; set; }
        public Guid id_ticket { get; set; }
        public Guid id_user_done { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string file_attachment { get; set; }
        public DateTime last_created_at { get; set; }
        public DateTime last_updated_at { get; set; }
        public DateTime last_finished_at { get; set; }
        public bool is_closed { get; set; } = false; // default 
    }
}