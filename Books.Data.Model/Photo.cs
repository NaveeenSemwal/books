using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Data.Model
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}