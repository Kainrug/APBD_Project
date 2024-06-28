using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_APBD.Models;

[Table("Software")]
public class Software
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
        
    [Column("Name")]
    public string Name { get; set; }
        
    [Column("Description")]
    public string Description { get; set; }
        
    [Column("CurrentVersion")]
    public string CurrentVersion { get; set; }
        
    [Column("Category")]
    public string Category { get; set; }
        
    [Column("IsSubscriptionAvailable")]
    public bool IsSubscriptionAvailable { get; set; }
        
    [Column("IsOneTimePurchaseAvailable")]
    public bool IsOneTimePurchaseAvailable { get; set; }

    public IEnumerable<Contract> Contracts { get; set; }
    
}