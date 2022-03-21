using RestWithASPNET5.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Hypermedia.Utils
{
    //Classe para otimizar as buscas do banco de dados. Se tivermos muitos resultados
    //vamos dividir os números e buscá-los em partes para economizar recursos
    public class PagedSearchVO<T> where T : ISupportsHyperMedia
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirections { get; set; }

        public Dictionary<string, Object> Filters { get; set; }

        public List<T> List { get; set; }

        //Teremos que ter diferentes construtores para cada senário já que é do tipo T
        public PagedSearchVO() { }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }

        // Este está setando o primeiro construtor com parâmetros quando o usuário fizer só estes três argumentos abaixo
        public PagedSearchVO(int currentPage, string sortFields, string sortDirections)
            : this(currentPage, 10, sortFields, sortDirections) { }

        public int GetCurrentPage()
        {
            // Vai retornar CurrentPage se for igual a 0 ele vai retornar a página corrente na 2, se não ele vai na CurrentPage
            //é só pra validar quando manda nulo
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            // vai retornar o PageSize e se for igual a 0 ele vai retornar o tamanho da página como 10, e se
            // não vai retornar o tamanho da página especificado
            //Assim evitamos quebrar a aplicação
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
