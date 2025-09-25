namespace GameOfThronesAPI.DTOs
{
    public class StrongholdDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Localizacao { get; set; } = string.Empty;
        public string? Descricao { get; set; }

        public string? Casa { get; set; }

        // Lista com nomes de personagens que vivem lá
        public List<string> Residents { get; set; } = new();
    }
}
