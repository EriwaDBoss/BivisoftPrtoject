
using BiviProject.ConstantEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BiviProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Softdelete { get; set; }
        public int Image { get; set; }
        public string Discription { get; set; }
        public ProductCategoryEnum EnumCategory { get; set; }
        public int Quatity { get; set; }
        public int Price { get; set; }
        public string Keywords { get; set; }
        public DateTime DateCreated { get; set; }


        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
