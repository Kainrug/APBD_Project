using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_APBD.Enums;

namespace Project_APBD.Models;

[Table("Discount")]
public class Discount
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
        
    [Column("Name")]
    public string Name { get; set; }
        
    [Column("DiscountType")]
    public DiscountType DiscountType { get; set; }
        
    [Column("Value")]
    [Range(0,100)]
    public double Value { get; set; }
        
    [Column("StartDate")]
    public DateTime StartDate { get; set; }
        
    [Column("EndDate")]
    public DateTime EndDate { get; set; }

    public IEnumerable<Contract> Contracts { get; set; }
    
    
}