namespace Progetto_BE_S2_L5.Models
{
    public class Articolo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public List<string>? Immagini { get; set; }

    }
}
