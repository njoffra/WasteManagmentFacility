namespace WasteManagmentFacility.Models
{
    public class Logging
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
