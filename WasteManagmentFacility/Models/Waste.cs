using WasteManagmentFacility.Models;

public class Waste
{
    public Guid Id { get; set; }
    //public string Type { get; set; }
    public float Quantity { get; set; }
    public bool? IsPlastic { get; set; }
    public bool? IsMetal { get; set; }
    public bool? IsPaper { get; set; }
    public bool? IsWood { get; set; }
    public bool? IsOrganic { get; set; }

    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
}
