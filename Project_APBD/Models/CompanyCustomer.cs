using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_APBD.Models;

[Table("CompanyCustomer")]
public class CompanyCustomer : Customer
{
    [Column("CompanyName")]
    public string CompanyName { get; set; }
    
    [Column("Krs")]
    [MaxLength(10)]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "KRS number must be exactly 10 digits.")]
    public string KrsNumber { get; set; }
}