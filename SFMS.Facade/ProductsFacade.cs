using IMSRepository;
using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class ProductsFacade: Facade<Product>
    {
        ProductRepository productRepository = null;
        public ProductsFacade(DataContext dataContext) : base(dataContext)
        {
            productRepository = new ProductRepository(dataContext);
        }
        public ProductsModel GetProducts(ProductsFilter filter)
        {
            return productRepository.GetProducts(filter);
        }

        public PWMsModel GetPWM(PWMsFilter filter)
        {
            return productRepository.GetPWM(filter);
        }

        public List<Product> GetAllProductsbyQuery(string query)
        {
            return productRepository.GetAllProductsbyQuery(query);
        }
    }
}
