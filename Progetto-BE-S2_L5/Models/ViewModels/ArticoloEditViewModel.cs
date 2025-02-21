namespace Progetto_BE_S2_L5.Models.ViewModels
{
    public class ArticoloEditViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public List<string>? Immagini { get; set; } = new List<string>();
    }
}
