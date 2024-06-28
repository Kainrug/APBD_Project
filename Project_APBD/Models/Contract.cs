using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_APBD.Models;

[Table("Contract")]
public class Contract
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
        
    [ForeignKey("Software")]
    [Column("SoftwareId")]
    public int SoftwareId { get; set; }
        
    [ForeignKey("Customer")]
    [Column("CustomerId")]
    public int CustomerId { get; set; }
        
    [Column("StartDate")]
    public DateTime StartDate { get; set; }
        
    [Column("EndDate")]
    public DateTime EndDate { get; set; }
        
    [Column("Price")]
    public decimal Price { get; set; }
    
        
    [Column("SupportYears")]
    [Range(1,4)]
    public int SupportYears { get; set; }
    
    [Column("discount_id")]
    [ForeignKey("Discount")]
    public int? DiscountId { get; set; }
    
    [Column("software_version")]
    public string SoftwareVersion { get; set; }
    
    [Column("is_signed")]
    public bool IsSigned { get; set; }
    
    
    public Customer Customer { get; set; }
    
    public Software Software { get; set; }
    
    public Discount? Discount { get; set; }

    public IEnumerable<Payment> Payments { get; set; }
    
}