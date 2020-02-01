using System;

namespace WebApi.Entities
{
    public class Leilao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Responsavel { get; set; }
        public long Abertura { get; set; }
        public long Finalizacao { get; set; }
        public int IndicUsado { get; set; }
        public decimal? ValorInicial { get; set; }
    }
}