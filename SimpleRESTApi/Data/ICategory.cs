using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface ICategory
    {
        //crud
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}