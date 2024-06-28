using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_APBD.Models;

[Table("RefreshToken")]
public class RefreshToken
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    [Column("User")]
    public int UserId { get; set; }

    [Column("Token")]
    public string Token { get; set; }

    [Column("ExpiryDate")]
    public DateTime? ExpiryDate { get; set; }

    
    public User User { get; set; }
}