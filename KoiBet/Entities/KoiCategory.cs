using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiBet.Entities
{
    public class KoiCategory
    {
        [Key]
        public string category_id { get; set; }

        public string category_name { get; set; }

        [ForeignKey("standard_id")]
        public virtual KoiStandard koi_standard { get; set; }
    }
}
