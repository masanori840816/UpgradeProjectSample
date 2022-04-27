namespace UpgradeProjectSample.Books
{
    public struct SearchBookCriteria
    {
        public string Name { get; set; }
        public int[] LanguageIds { get; set; }
        public string AuthorName { get; set; }
        public SearchBookCriteria(string name, int[] languageIds, string authorName)
        {
            this.Name = name;
            this.LanguageIds = languageIds;
            this.AuthorName = authorName;
        }
    }
}
