using System.ComponentModel.DataAnnotations;

namespace UpgradeProjectSample.Books.Dto
{
    public class SearchedBook
    {
        [Key]
        public int BookId { get; set; }
        public int LanguageId { get; set; }
        public string BookName { get; set; } = "";
        public string AuthorName { get; set; } = "";
    }
}
