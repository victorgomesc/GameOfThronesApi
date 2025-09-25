namespace GameOfThronesAPI.DTOs
{
    public class HouseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Lema { get; set; } = string.Empty;
        public string? Fortaleza { get; set; }
        public List<string> Vassalos { get; set; } = new();
    }
}
