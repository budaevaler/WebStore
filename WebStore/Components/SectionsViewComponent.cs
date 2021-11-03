using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModel;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public SectionsViewComponent(IProductData productData)
        {
            _productData = productData;
        }
        public IViewComponentResult Invoke()
        {
            var sections = _productData.GetSections();
            var parentSections = sections.Where(s => s.ParentId is null);
            var parentSectionsViews = parentSections
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                }).ToList();

            foreach (var parentSection in parentSectionsViews)
            {
                var children = sections.Where(s => s.Id == parentSection.Id);
                foreach (var child in children)
                {
                    parentSection.ChildSections.Add(new SectionViewModel
                    {
                        Id=child.Id,
                        Name = child.Name,
                        Order = child.Order,
                        Parent = parentSection
                    });
                    parentSection.ChildSections.Sort((a,b)=>
                        Comparer<int>.Default.Compare(a.Order, b.Order));
                }
            }

            parentSectionsViews.Sort((a,b)=>Comparer<int>.Default.Compare(a.Order,b.Order));

            return View();
        }
    }
}
