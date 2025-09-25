namespace GameOfThronesAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int? HouseId { get; set; }
        public House? House { get; set; }
        public int? NovaCasaId { get; set; }
        public House? NovaCasa { get; set; }
        public string? Titulo { get; set; }
        public string Status { get; set; } = "Vivo";
        public string? Descricao { get; set; }
        public string Sexo { get; set; } = "Masculino";
        public int? FortalezaId { get; set; }
        public Stronghold? Fortaleza { get; set; }
    }
}
