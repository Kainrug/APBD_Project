using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_APBD.Models;

[Table("IndividualCustomer")]
public class IndividualCustomer : Customer
{
    [Column("FirstName")]
    public string FirstName { get; set; }
    
    [Column("LastName")]
    public string LastName { get; set; }
    
    [Column("Pesel")]
    [MaxLength(11)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL must be exactly 11 digits.")]
    public string Pesel { get; set; }
    
}