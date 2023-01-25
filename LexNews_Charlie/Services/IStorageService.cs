using LexNews_Charlie.Models;
using LexNews_Charlie.Models.Entities;
using LexNews_Charlie.Models.ViewModels;

namespace LexNews_Charlie.Services
{
    public interface IStorageService
    {
        Uri UploadBlob(string pathfile);
        void AddSpotPriceToTable();
        List<SpotPriceEntity> GetEntities(string partitionKey);
        void DeleteEntity(string rowKey);
        void UpdateEntity(string rowKey);
        Uri GetBlob(string blobName, string containerName);
        void DeleteBlob(Article newArticle);
    }
}
