namespace WasteManagmentFacility.Models
{
    public class Facility
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Capacity { get; set; }
        public float CurrentOccupancy { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Vehicle> Vehicles { get; set; } 
    }
}
