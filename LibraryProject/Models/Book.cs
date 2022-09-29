namespace LibraryProject.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int RealiseYears { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsReserved { get; set; } = false;
        public bool IsTaked { get; set; } = false;

    }
}
