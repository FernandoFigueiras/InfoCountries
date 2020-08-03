using CoreMVCStudy.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCStudy.Web.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Name);
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<bool> SaveAllASync()
        {
            return await _context.SaveChangesAsync() > 0;//Caso hajam gravacoes a fazer ele retorna maior que 0
            //Pode haver mais do que uma alteracao em pool, poque o contexto de dados fica em memoria ate gravar
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
