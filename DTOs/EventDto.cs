namespace GameOfThronesAPI.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }

        public List<string> Characters { get; set; } = new();
        public List<string> Houses { get; set; } = new();
        public string? Stronghold { get; set; }
    }
}
