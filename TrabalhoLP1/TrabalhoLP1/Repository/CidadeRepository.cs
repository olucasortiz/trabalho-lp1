using TrabalhoLP1.Entidades;

namespace TrabalhoLP1.Repository
{
    public class CidadeRepository
    {
        private readonly MySqlDbContext _db;

        public CidadeRepository(MySqlDbContext db)=>
            _db = db;

        public bool Criar(Cidade cidade)
        {

        }
    }
}
