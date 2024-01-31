namespace WasteManagmentFacility.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public float Capacity { get; set; }
        public bool Available { get; set; }
        public float CurrentOccupancy { get; set; }

        ////Foreign key for the facility associated with the vehicle
        //public Guid FacilityId { get; set; }

        //// Navigation property for the facility associated with the vehicle
        //public Facility Facility { get; set; }
    }
}
