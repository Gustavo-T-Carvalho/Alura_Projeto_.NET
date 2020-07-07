using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
   
    public class BuscaDeProdutosViewModel
    {
        public BuscaDeProdutosViewModel(IList<Produto> produtos, string busca)
        {
            Produtos = produtos;
            Busca = busca;
        }
        public IList<Produto> Produtos { get; }

        public string Busca { get; set; }
    }
}
