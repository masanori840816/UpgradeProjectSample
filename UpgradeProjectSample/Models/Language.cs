using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpgradeProjectSample.Models
{
    [Table("language")]
    public class Language
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        public string Name { get; set; } = "";

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
