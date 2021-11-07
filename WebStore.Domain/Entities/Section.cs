using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))] //можно не указывать, так как названия ParentId и Parent 
        public Section Parent { get; set; }
        public ICollection<Product> Products { get; set; }
        
    }
}