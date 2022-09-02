using BiviProject.ConstantEnums;
using BiviProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiviProject.ViewModel
{
    public class CategoryViewModel
    {
        public virtual List<Category> Categories { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Softdelete { get; set; }
        public int Image { get; set; }
        public string Discription { get; set; }
        public ProductCategoryEnum EnumCategory { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
    }

}
