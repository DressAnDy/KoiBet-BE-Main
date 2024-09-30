using KoiBet.Entites;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiBet.Entities;

public class Users
{
    [Key]
    public string user_id { get; set; }

    [Required]
    [MaxLength(100)]
    public string user_name { get; set; }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Password)]
    public string pass_word { get; set; }

    [Required]
    [MaxLength(100)]
    public string full_name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string Phone { get; set; }

    [Required]
    public string role_id { get; set; } 

    public decimal Balance { get; set; }

    public Users() { }

    public Users(string user_name, string pass_word, string email)
    {
        this.user_name = user_name;
        this.pass_word = pass_word;
        this.Email = email;
    }

    public virtual Roles Role {  get; set; }    
}
