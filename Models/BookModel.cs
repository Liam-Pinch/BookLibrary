
namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int YearOfPublication { get; set; }

        public string Genre { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string OriginalLanguage { get; set; } = string.Empty;

        public string AvailableLanguages { get; set; } = string.Empty;

    }
}