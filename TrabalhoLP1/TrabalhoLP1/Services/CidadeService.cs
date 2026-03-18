
using TrabalhoLP1.Entidades;
using TrabalhoLP1.Repository;

namespace TrabalhoLP1.Services
{
    public class CidadeService
    {
        private readonly CidadeRepository _cidadeRepository;
        public CidadeService(CidadeRepository cidadeRepository)=>
            _cidadeRepository = cidadeRepository;

        public bool Criar(Cidade cidade)
        {
            return _cidadeRepository.Criar(cidade);
        }
    }
}
