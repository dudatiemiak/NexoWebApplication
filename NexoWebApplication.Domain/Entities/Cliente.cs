namespace NexoWebApplication.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ICollection<DescricaoCliente> Descricoes { get; set; }
    }
}