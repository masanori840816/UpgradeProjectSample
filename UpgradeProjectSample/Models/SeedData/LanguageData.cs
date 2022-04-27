namespace UpgradeProjectSample.Models.SeedData
{
    public static class LanguageData
    {
        public static Language GetEnglish()
        {
            return new Language { Id = 1, Name = "English"};
        }
        public static Language GetJapanese()
        {
            return new Language { Id = 2, Name = "Japanese" };
        }
        public static Language[] GetAll()
        {
            return new []
            {
                GetEnglish(),
                GetJapanese(),
            };
        }
    }
}
