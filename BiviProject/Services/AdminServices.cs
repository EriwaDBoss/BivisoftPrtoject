using BiviProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiviProject.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly ApplicationDbContext _context;

        public AdminServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool DeleteAUser(int id)
        {
            var category = _context.Category.Find(id);
            if (category != null)
            {
                category.Softdelete = true;

                _context.Update(category);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
