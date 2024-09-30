using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiBet.Entites
{
    public class Transactions
    {
        [Key]
        public string transactions_id { get; set; }

        [Required]
        public string user_id { get; set; }

        [Required]
        public string amount { get; set; }
        public string messages { get; set; }

        public DateTime transactions_time { get; set; }

        public DateTime transactions_created { get; set;}

        //[ForeignKey]   
    }
}
