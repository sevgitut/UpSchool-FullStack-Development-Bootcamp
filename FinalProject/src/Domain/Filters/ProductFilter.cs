using Domain.Entities;

namespace Domain.Filters
{
    public static class ProductFilter
    {
        public static List<Product> FilterBySalePrice(List<Product> products)
        {
            return products.Where(x=>x.SalePrice > 0).ToList();
        }

        public static List<Product> FilterByPrice(List<Product> products)
        {
            return products.Where(x => x.SalePrice == 0).ToList();
        }

        public static List<Product> FilterAll(List<Product> products)
        {
            return products;
        }
    }
}
