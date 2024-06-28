using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_APBD.Enums;

namespace Project_APBD.Models;

[Table(("User"))]
public class User
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("Email")]
    [MaxLength(50)]
    public string Email { get; set; }
    
    [Column("Login")]
    [MaxLength(50)]
    public string Login { get; set; }
    
    [Column("Password")]
    [MaxLength(50)]
    public string Password { get; set; }
    
    [Column("Salt")]
    public string Salt { get; set; }
    
    [Column("RefreshToken")]
    public string RefreshToken { get; set; }
    
    [Column("RefreshTokenExp")]
    public DateTime? RefreshTokenExp { get; set; }
    
    [Column("Role")]
    public UserRoles UserRoles { get; set; }
    
}