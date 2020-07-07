using CasaDoCodigo.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {

        }

      
        //Método que busca no BD se já existe uma categoria com o nome e caso não exista adiciona uma nova
        //O método retorna a categoria
        public async Task<Categoria> SaveCategoria(string nomeCategoria)
        {
            var CategoriaDB = dbSet.Where(c => c.Nome == nomeCategoria).SingleOrDefault();
            if (CategoriaDB == null)
            {
               var novaCategoria = new Categoria(nomeCategoria);
               CategoriaDB = dbSet.Add(novaCategoria).Entity;
                
            }
            await contexto.SaveChangesAsync();
            return CategoriaDB;
        }
    }
}

