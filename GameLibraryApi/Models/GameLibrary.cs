namespace GameLibraryApi.Models
{
    public class GameLibrary
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Developer { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
