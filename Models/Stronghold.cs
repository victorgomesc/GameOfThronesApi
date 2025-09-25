namespace GameOfThronesAPI.Models
{
    public class Stronghold
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;      
        public string Localizacao { get; set; } = string.Empty; 
        public string? Descricao { get; set; }                  
        public int? HouseId { get; set; }
        public House? House { get; set; }
        public List<Character> Residents { get; set; } = new List<Character>();
    }
}
