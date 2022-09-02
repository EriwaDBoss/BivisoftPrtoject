using BiviProject.ConstantEnums;
using BiviProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiviProject.ViewModel
{
    public class ProductViewModel
    {
        public virtual List<Product> Products { get; set; }
        public virtual Product Product { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool Softdelete { get; set; }
        public int Image { get; set; }
        public string Discription { get; set; }
        public ProductCategoryEnum EnumCategory { get; set; }
        public int Quatity { get; set; }
        public int Price { get; set; }
        public string Keywords { get; set; }
        public DateTime DateCreated { get; set; }
    }

}
