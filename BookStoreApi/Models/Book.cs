namespace BookStoreApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int PublishedYear { get; set; }
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }

    }
}
