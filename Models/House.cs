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
        public List<House> Vassalos { get; set; } = new List<House>();
        public int? OverlordHouseId { get; set; }
        public House? OverlordHouse { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>();
    }
}
