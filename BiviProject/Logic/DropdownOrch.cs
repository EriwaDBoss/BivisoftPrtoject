using BiviProject.ConstantEnums;

using System;
using System.Collections.Generic;
using System.Linq;


namespace BiviProject.Logic
{
    public class DropdownOrch : InterOfDropdownOrch
    {
        public List<TemporaryModel> GetAllProductCategoryEnum()
        {
            var ProductCategoryEnum = new TemporaryModel()
            {
                Id = 0,
                Name = "---- Select Product ----"
            };

            var dataEnums = Enum.GetValues(typeof(ProductCategoryEnum)).Cast<ProductCategoryEnum>()
                .Select(tempoModel => new TemporaryModel()
                  { Id = (int)tempoModel, Name = tempoModel.ToString() })
                 .OrderBy(x => x.Id).ToList();

            dataEnums.Insert(0, ProductCategoryEnum);
            return dataEnums;
        }

        public class TemporaryModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


    }
}
