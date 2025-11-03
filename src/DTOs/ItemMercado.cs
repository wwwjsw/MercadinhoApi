namespace MercadinhoApi.DTOs
{
    public class CriarItemMercadoDto
    {
        public string Nome { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public class ItemMercadoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}