using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModel;

namespace WebStore.Components
{
    //[ViewComponent(Name = "BrandsView")]
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        //public async Task<IViewComponentResult> InvokeAsync() => View();
        public IViewComponentResult Invoke() => View(GetBrands());

        public BrandsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        private IEnumerable<BrandViewModel> GetBrands() => _productData.GetBrands()
            .OrderBy(b => b.Name)
            .Select(b => new BrandViewModel
            {
                Name = b.Name,
                Id = b.Id
            });
    }
}
