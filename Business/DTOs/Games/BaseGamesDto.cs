namespace Business.DTOs.Games
{
    public class BaseGamesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsEnded { get; set; }
    }
}
