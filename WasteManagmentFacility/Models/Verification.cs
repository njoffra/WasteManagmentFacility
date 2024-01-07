using System.ComponentModel.DataAnnotations;

public class Verification
{
    [Key]
    public Guid VerificationId { get; set; }
    public bool IsPlastic { get; set; }
    public bool IsMetal { get; set; }
    public bool IsPaper { get; set; }
    public bool IsWood { get; set; }
    public bool IsOrganic { get; set; }
}
