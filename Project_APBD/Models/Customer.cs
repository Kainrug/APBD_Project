using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_APBD.Models;

[Table("Customer")]
public abstract class Customer
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("Address")]
    public string Address { get; set; }
    
    [Column("Email")]
    public string Email { get; set; }
    
    [Column("PhoneNumber")]
    [MaxLength(9)]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
    public string PhoneNumber { get; set; }
    
    [Column("IsDeleted")]
    public bool IsDeleted { get; set; }
    
    public IEnumerable<Contract> Contracts { get; set; }
    

}