using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpgradeProjectSample.Models
{
    [Table("language")]
    public record Language
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        
        [Required]
        [Column("name")]
        public string Name { get; init; } = "";

        public List<Book> Books { get; init; } = new List<Book>();
    }
}
