
using BiviProject.ConstantEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiviProject.Models
{
    public class Category
    {
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
