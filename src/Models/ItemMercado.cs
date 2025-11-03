namespace MercadinhoApi.Models
{
    public class ItemMercado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }
    }
}