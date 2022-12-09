using AspnetRunBasics.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<CatalogModel> GetCatalog(string id);
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<CatalogModel> CreateCatalog(CatalogModel model);
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    }
}
