using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BiviProject.Logic.DropdownOrch;

namespace BiviProject.Logic
{
   public interface InterOfDropdownOrch
    {
       List<TemporaryModel> GetAllProductCategoryEnum();
    }
}
