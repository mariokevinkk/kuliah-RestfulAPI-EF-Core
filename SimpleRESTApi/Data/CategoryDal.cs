using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryDal : ICategory
    {
        private List<Category> _categories = new List<Category>();
        public CategoryDal()
        {
 _categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "ASP.NET Core" },
            new Category { CategoryId = 2, CategoryName = "ASP.NET MVC" },
            new Category { CategoryId = 3, CategoryName = "ASP.NET Web API" },
            new Category { CategoryId = 4, CategoryName = "Blazor" },
            new Category { CategoryId = 5, CategoryName = "Xamarin" },
            new Category { CategoryId = 6, CategoryName = "Azure" }
            
        };
        }
        
          public Category GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if(category == null)
            {
                throw new Exception("Could not found");
            }
            return category;
        }
         public IEnumerable<Category> GetCategories()
        {
            return _categories;
        }
       
        public Category AddCategory(Category category)
        {
            _categories.Add(category);
            return category;
        }

        public void DeleteCategory(int categoryId)
        {
             var category = GetCategoryById(categoryId);
            if(category != null){
                _categories.Remove(category);
            }
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.CategoryId);
            if(existingCategory != null){
                existingCategory.CategoryName = category.CategoryName;
            }
            return existingCategory;
        }
    }
}