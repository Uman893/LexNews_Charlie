using AutoMapper;
using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using LexNews_Charlie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LexNews_Charlie.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ArticleService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void SaveArticle(DisplayArticleVM newArticle, Uri blobUri)
        {
            Article dbArticle = _mapper.Map<Article>(newArticle);
            dbArticle.Category = _db.Categories.Find(Convert.ToInt32(newArticle.CategoryId));
            dbArticle.ImageLink = blobUri;
            dbArticle.DateStamp = DateTime.Now;
            dbArticle.FileName = newArticle.CategoryId + "/" + newArticle.FileName;

            if (dbArticle != null)
            {
                _db.Add(dbArticle);               
            }
            _db.SaveChanges();
        }
        public void EditAndSaveArticle(DisplayArticleVM newEditedArticle, Uri blobUri)
        {
            Article dbArticle = _db.Articles.Find(newEditedArticle.Id);           
            dbArticle.ImageLink = blobUri;
            dbArticle.DateStamp = DateTime.Now;
            dbArticle.FileName = newEditedArticle.CategoryId + "/" + newEditedArticle.FileName;
            dbArticle.Title = newEditedArticle.Title;
            dbArticle.Category = _db.Categories.Find(Convert.ToInt32(newEditedArticle.CategoryId));
            dbArticle.LinkText = newEditedArticle.LinkText;
            dbArticle.Content = newEditedArticle.Content;                     

            if (dbArticle != null)
            {
                _db.Articles.Update(dbArticle);               
            }
            _db.SaveChanges();
        }       

        public List<Article> GetArticles(int id)
        {
            return _db.Articles.Where(a => a.CategoryId == id).ToList();
        }   
      
        public Article FetchArticle(int? id)
        {
            if (id != null)
            {
                var article = _db.Articles.Find(id);
                return article;
            }
            else
                return null;
        }       
       
        public Article DetailsArticle(int id)
        {
            if (id != 0)
            {
                var article = _db.Articles.FirstOrDefault(a => a.Id == id);
                return article;
            }
            else
                return null;
        }
        public List<Article> GetArticlesList()
        {
            List<Article> articles = new List<Article>();
            List<Article> list = _db.Articles.ToList();
            if(list != null)
            {
                foreach (var item in list)
                {
                    articles.Add(item);
                }
            }
            return articles;
        }
        public Article DeleteArticle(int? id)
        {
            if (id == null || _db.Articles == null)
            {
                return null;
            }
            var article = FetchArticle(id);
            if (article == null)
            {
                return null;
            }
            return article;
        }
        public string DeleteConfirmedArticle(int id)
        {
            if (_db.Articles == null)
            {
                return null;
            }
            var article = FetchArticle(id);
            if (article != null)
            {
                _db.Articles.Remove(article);
                _db.SaveChanges();
                return "";
            }
            return "";
        }
        public Article GetOneArticlebyId(int? id)
        {
            Article oneArticleById = _db.Articles.FirstOrDefault(x => x.Id == id);
            return oneArticleById;
        }

    }
}
