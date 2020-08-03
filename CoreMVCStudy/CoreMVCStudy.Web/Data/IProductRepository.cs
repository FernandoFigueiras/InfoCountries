using CoreMVCStudy.Web.Data.Entities;
using System.Linq;

namespace CoreMVCStudy.Web.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();
    }
}
