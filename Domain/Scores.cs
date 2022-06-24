namespace Domain
{
    public class Scores
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public int Frame { get; set; }
        public int PinsKnockedDown { get; set; }
    }
}
