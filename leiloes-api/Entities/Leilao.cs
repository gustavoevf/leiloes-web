using System;

namespace WebApi.Entities
{
    public class Leilao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Resposavel { get; set; }
        public DateTime? Abertura { get; set; }
        public DateTime? Finalizacao { get; set; }
        public string IndicUsado { get; set; }
        public decimal? ValorInicial { get; set; }
    }
}