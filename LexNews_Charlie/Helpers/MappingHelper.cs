using AutoMapper;
using LexNews_Charlie.Models;
using LexNews_Charlie.Models.ViewModels;

namespace LexNews_Charlie.Helpers
{
    public class MappingHelper : Profile
    {

        public MappingHelper()
        {
            CreateMap<DisplayArticleVM, Article>();
            CreateMap<Article, DisplayArticleVM>();
        }
    }
}
