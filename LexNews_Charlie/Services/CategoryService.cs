using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.Services
{    
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void CreateCategory([Bind("Name")] Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }
        public Category FetchCategory(int? id)
        {
            if (id != null)
            {
                var category = _db.Categories.Find(id);
                return category;
            }
            else
                return null;
        }
        public void EditCategory([Bind("Name")] Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }
        public Category DetailsCategory(int id)
        {
            if (id != 0)
            {
                var category = _db.Categories.FirstOrDefault(a => a.Id == id);
                return category;
            }
            else
                return null;
        }
        public List<Category> GetCategoryList()
        {
            List<Category> category = _db.Categories.ToList();
            return category;
        }
        public Category DeleteCategory(int? id)
        {
            if (id == null || _db.Categories == null)
            {
                return null;
            }
            var category = FetchCategory(id);
            if (category == null)
            {
                return null;
            }
            return category;
        }
        public string DeleteConfirmedCategory(int id)
        {
            if (_db.Categories == null)
            {
                return null;
            }
            var category = FetchCategory(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return "";
            }
            return "";
        }
        public Category GetOneCategoryById(int id)
        {
            return _db.Categories.Find(id);
        }
    }
}
