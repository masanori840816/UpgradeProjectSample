using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpgradeProjectSample.Models
{
    [Table("author")]
    public class Author
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; } = "";

        public List<Book> Books { get; init; } = new List<Book>();

        public static Author Create(string name)
        {
            return new Author
            {
                Name = name,
            };
        }
    }
}
