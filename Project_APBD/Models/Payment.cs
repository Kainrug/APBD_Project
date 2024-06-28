using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_APBD.Enums;

namespace Project_APBD.Models;

[Table("Payment")]
public class Payment
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("ContractId")]
    [ForeignKey("Contract")]
    public int ContractId { get; set; }

    [Column("Amount")]
    public decimal Amount { get; set; }
    
    [Column("Date")]
    public DateTime Date { get; set; }
    
    [Column("PaymentType")]
    public PaymentType PaymentType { get; set; }
    
    public Contract Contract { get; set; }
}