namespace E_Library.DTO
{
    public class CreateDTOs
    {
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? FileUrl { get; set; }
        public int Year { get; set; }
    }
}
