namespace WasteManagmentFacility.Models
{
    public class FileUpload
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        // Additional file-related properties

        // Relationships
        public Guid? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public Guid? FacilityId { get; set; }
        public Facility Facility { get; set; }

        // Add more relationships as needed
    }
}
