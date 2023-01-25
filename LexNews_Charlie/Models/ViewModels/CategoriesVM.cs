using AutoMapper;

namespace LexNews_Charlie.Models.ViewModels
{
    public class CategoriesVM 
    {
        public List<Category> CategoriesList { get; set; }  

        public List<DisplayArticleVM> ArticlesList { get; set; }

        

        public CategoriesVM()
        {
            CategoriesList = new List<Category>();    
            ArticlesList = new List<DisplayArticleVM>();
            

        }
    }
}
