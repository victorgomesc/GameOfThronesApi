namespace GameOfThronesAPI.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Lema { get; set; } = string.Empty;

        public int? StrongholdId { get; set; }
        public Stronghold? Stronghold { get; set; }

        public List<Character> Characters { get; set; } = new();

        // 🔑 Relação de vassalos (self reference)
        public List<House> Vassalos { get; set; } = new();
        public int? OverlordHouseId { get; set; }
        public House? OverlordHouse { get; set; }
    }
}