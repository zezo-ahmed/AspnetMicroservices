using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<CatalogModel> GetCatalog(string id);
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    }
}
