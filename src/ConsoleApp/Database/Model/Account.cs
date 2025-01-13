using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp.Database.Model;

[Table("Accounts")]
public class Account
{
    [Key]
    public int Id { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = null!;
    
    public bool IsSelected { get; set; }
}