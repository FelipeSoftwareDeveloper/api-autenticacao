namespace Blank.Infraestructure.Data.Utils
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacao paginacao)
        => queryable.Skip((paginacao.Pagina - 1) * paginacao.QuantidadePorPagina)
                    .Take(paginacao.QuantidadePorPagina);
    }

    public record class Paginacao
    {
        public bool Paginar { get; set; }
        public int Pagina { get; set; } = 1;
        public int QuantidadePorPagina { get; set; }
    }
}
