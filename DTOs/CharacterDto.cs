namespace GameOfThronesAPI.DTOs
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Titulo { get; set; }
        public string Status { get; set; } = "Vivo";
        public string Sexo { get; set; } = "Masculino";
        public string? Descricao { get; set; }
        public string? Casa { get; set; }
        public string? NovaCasa { get; set; }
        public string? Fortaleza { get; set; }
    }
}
