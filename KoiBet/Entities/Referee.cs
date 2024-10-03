using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KoiBet.Entities
{
    public class Referee
    {
        public string koi_id {  get; set; }

        public string koi_name { get; set; }

        public string koi_variety { get; set; }

        public string koi_size { get; set; }

        public string koi_age { get; set; }

        //public string koi_type { get; set; }

        [ForeignKey(nameof(user_id))]
        [JsonIgnore]
        public virtual Users user_id { get; set; }


    }
}
