using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db)
        {
            _db = db;
        }
        public IEnumerable<Section> GetSections() => _db.Sections;

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (filter?.SectionId is { } sectionId)  //filter?.SectionId != null
                query = query.Where(p => p.SectionId == sectionId);
            if (filter?.BrandId is { } brandId)
                query = query.Where(p => p.BrandId == brandId);
            return query;
        }
    }
}
