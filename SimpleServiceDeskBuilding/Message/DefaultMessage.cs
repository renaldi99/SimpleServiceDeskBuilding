namespace SimpleServiceDeskBuilding.Message
{
    public class DefaultMessage
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
    }

    public class PayloadMessage
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public dynamic payload { get; set; }
    }
}
