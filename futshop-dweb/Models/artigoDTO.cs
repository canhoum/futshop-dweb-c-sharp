namespace futshop_dweb.Models
{
    public class ArtigoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tamanho { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public int CategoriaFK { get; set; }
    }
}