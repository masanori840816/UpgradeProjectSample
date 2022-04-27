using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UpgradeProjectSample.Models
{
    [Table("book")]
    public class Book
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; } = "";
        [Required]
        [Column("author_id")]
        public int AuthorId { get; set; }
        [Required]
        [Column("language_id")]
        public int LanguageId { get; set; }
        private DateTime? purchaseDate;
        [JsonIgnore]
        [Column("purchase_date", TypeName = "date")]
        public DateTime? PurchaseDate
        {
            get { return this.purchaseDate; }
            set { this.purchaseDate = value; }
        }

        [Column("price", TypeName = "money")]
        public decimal? Price { get; set; }
        [Required]
        [Column("last_update_date", TypeName = "timestamp with time zone")]
        public DateTime LastUpdateDate { get; set; }
        public Author Author { get; set; } = new Author();
        public Language Language { get; set; } = new Language();

        public static Book Create(Author author, Book value)
        {
            return new Book
            {
                Name = value.Name,
                AuthorId = author.Id,
                LanguageId = value.LanguageId,
                PurchaseDate = value.PurchaseDate,
                Price = value.Price,
                LastUpdateDate = DateTime.Now.ToUniversalTime(),
            };
        }
    }
}
