using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IEnumerable<Product> query = TestData.Products;
            if (filter?.SectionId is {} sectionId)  //filter?.SectionId != null
                query = query.Where(p => p.SectionId == sectionId);
            if (filter?.BrandId is {} brandId)
                query = query.Where(p => p.BrandId == brandId);
            return query;
        }
    }
}
