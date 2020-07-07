using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public async Task<IList<Produto>> GetProdutos()
        {

            return await dbSet.Include(c => c.categoria).ToListAsync();

        }
        

        public async Task<IList<Produto>> GetProdutos(string busca)
        {
            IQueryable<Produto> listaPesquisa = dbSet.Include(p => p.categoria);

            if (!string.IsNullOrWhiteSpace(busca))
            {

               listaPesquisa = listaPesquisa.Where(c => c.Nome.Contains(busca) || c.categoria.Nome.Contains(busca));

            }
            return await listaPesquisa.ToListAsync();

        }

        //Método que salva os livros a partir da lista de livros obtidas no arquivo json
        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {

                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    var categoria = await categoriaRepository.SaveCategoria(livro.Categoria);

                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
