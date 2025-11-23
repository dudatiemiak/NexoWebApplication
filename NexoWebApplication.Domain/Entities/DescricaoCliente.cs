namespace NexoWebApplication.Domain.Entities
{
    public class DescricaoCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string AreaEstudo { get; set; }
        public string OcupacaoAtual { get; set; }
        public int AnosExperiencia { get; set; }
        public int Satisfacao { get; set; }
        public int AdocaoTecnologia { get; set; }
        public bool InteresseMudar { get; set; }
        public Cliente? Cliente { get; set; }
    }
}