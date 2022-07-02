namespace Business.DTOs.Scores
{
    public class BaseScoresDto
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public int Frame { get; set; }
        public int RollIndex { get; set; }
        public bool IsSpare { get; set; }
        public bool IsStrike { get; set; }
        public int PinsKnockedDown { get; set; }
        public int Score { get; set; }
    }
}
