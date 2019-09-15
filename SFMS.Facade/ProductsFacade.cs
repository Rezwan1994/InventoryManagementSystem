using IMSRepository;
using SFMS.Entity;
using System;
using System.Collections.Generic;

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
        public List<Product> GetProductsByKey(string key,string ExistEquipment,Guid Warehouse)
        {
            return productRepository.GetProductsByKey(key,ExistEquipment, Warehouse);
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
