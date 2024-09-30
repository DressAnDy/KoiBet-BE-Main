using KoiBet.Entities;
using System.ComponentModel.DataAnnotations;

namespace KoiBet.Entites
{
    public class Roles
    {
        [Key]
        public string role_id {  get; set; }

        public string role_name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
