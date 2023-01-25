using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.Services
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
        Category FetchCategory(int? id);
        void EditCategory( Category category);
        Category DetailsCategory(int id);
        List<Category> GetCategoryList();
        Category DeleteCategory(int? id);
        string DeleteConfirmedCategory(int id);
        Category GetOneCategoryById(int id);
    }
}
