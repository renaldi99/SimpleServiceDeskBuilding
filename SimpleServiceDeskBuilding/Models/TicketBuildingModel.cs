namespace SimpleServiceDeskBuilding.Models
{
    public class TicketBuildingModel
    {
        public Guid id { get; set; }
        public Guid id_user_created { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string file_attachment { get; set; }
    }
}
