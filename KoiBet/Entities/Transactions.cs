using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KoiBet.Entities
{
    public class Transactions
    {
        [Key]
        public string transactions_id { get; set; }

        [Required]
        public string amount { get; set; }
        public string messages { get; set; }

        public DateTime transactions_time { get; set; }

        public DateTime transactions_created { get; set;}

        [ForeignKey(nameof(user_id))]
        [JsonIgnore]
        public virtual Users user_id { get; set; }
    }
}
