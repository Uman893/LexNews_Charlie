using LexNews_Charlie.Models;
using LexNews_Charlie.Models.ViewModels;

namespace LexNews_Charlie.Services
{
    public interface IArticleService
    {
           
        List<Article> GetArticles(int id);
        void SaveArticle(DisplayArticleVM article, Uri blobUri); 
        Article FetchArticle(int? id);    

        Article DetailsArticle(int id);
        List<Article> GetArticlesList();

        Article DeleteArticle(int? id);
        string DeleteConfirmedArticle(int id);
        Article GetOneArticlebyId(int? id);
        void EditAndSaveArticle(DisplayArticleVM newEditedArticle, Uri blobUri);
    }
}
