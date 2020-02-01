using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ILeilaoService
    {
        IEnumerable<Leilao> ObterTodos();
        Leilao ObterPorId(int id);
        Leilao Criar(Leilao leilao);
        void Atualizar(Leilao leilao);
        void Deletar(int id);
    }

    public class LeilaoService : ILeilaoService
    {
        private DataContext _context;

        public LeilaoService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Leilao> ObterTodos()
        {
            return _context.Leiloes;
        }

        public Leilao ObterPorId(int id)
        {
            return _context.Leiloes.Find(id);
        }

        public Leilao Criar(Leilao leilao)
        {
            _context.Leiloes.Add(leilao);
            _context.SaveChanges();

            return leilao;
        }

        public void Atualizar(Leilao leilaoParam)
        {
            var leilao = _context.Leiloes.Find(leilaoParam.Id);

            if (leilao == null)
                throw new AppException("Leilão não encontrado");

            leilao.Nome = leilaoParam.Nome ?? leilao.Nome;
            leilao.ValorInicial = leilaoParam.ValorInicial ?? leilao.ValorInicial;


            _context.Leiloes.Update(leilao);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var leilao = _context.Leiloes.Find(id);
            if (leilao != null)
            {
                _context.Leiloes.Remove(leilao);
                _context.SaveChanges();
            }
        }
    }
}