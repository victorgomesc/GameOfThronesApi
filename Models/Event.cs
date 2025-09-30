namespace GameOfThronesAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }

        public List<Character> Characters { get; set; } = new();
        public List<House> Houses { get; set; } = new();
        public int? StrongholdId { get; set; }
        public Stronghold? Stronghold { get; set; }
    }
}
