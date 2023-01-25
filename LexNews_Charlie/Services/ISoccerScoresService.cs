using LexNews_Charlie.Models.ViewModels;

namespace LexNews_Charlie.Services
{
    public interface ISoccerScoresService
    {
        Task<DisplayScoresVM> GetScores(string sport);
    }
}
