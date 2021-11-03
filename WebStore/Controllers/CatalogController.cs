using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModel;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index(int? brandId, int? sectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId
            };

            var products = _productData.GetProducts(filter);


            var viewModel = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products
                    .OrderBy(p=>p.Order)
                    .Select(p=>new ProductViewModel
                    {
                        Name = p.Name,
                        Id=p.Id,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    })

            };

            return View(viewModel);
        }
    }
}
