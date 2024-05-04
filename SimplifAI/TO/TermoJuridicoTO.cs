namespace SimplifAI.TO
{
    public class TermoJuridicoTO
    {
        public int? IdTermoJuridico { get; set; }
        public string Termo { get; set; }
        public string Definicao { get; set; }

        public int? IdSimplificacao { get; set; }
        public string Simplificacao { get; set; }

        public int? Id { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
    }
}
