
﻿using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LexNews_Charlie.Models;
using LexNews_Charlie.Models.Entities;
using LexNews_Charlie.Models.SpotModels;
using LexNews_Charlie.Models.ViewModels;
using Newtonsoft.Json;

namespace LexNews_Charlie.Services
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly TableServiceClient _tableServiceClient;
        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _blobServiceClient = new BlobServiceClient(_configuration["AzureWebJobsStorage"]);
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobsStorage"]);
        }
        public Uri UploadBlob(string pathfile)
        {
            string containerName = "news-images";
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/articles");
            BlobClient blobClient = containerClient.GetBlobClient(pathfile);
            string FileNameWithPath = Path.Combine(path, pathfile);
            blobClient.Upload(FileNameWithPath, true);

            return blobClient.Uri;
        }
        public void DeleteBlob(Article newArticle)
        {
            string containerName = "news-images";
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName + "/");
            //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/articles" + "/" + categoryId);
            string fullPath = newArticle.FileName;
            BlobClient blobClient = containerClient.GetBlobClient(fullPath);     

             blobClient.DeleteIfExists();
        }
        public Uri GetBlob(string blobName, string containerName)
        {
            //string cotainerName = "news-images-sm";
            BlobContainerClient containerClient = _blobServiceClient
                .GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            return blobClient.Uri;
        }
        public async void AddSpotPriceToTable()
        {
            // add request for data
            HttpClient _spotHttpClient = new HttpClient();
            var request = await _spotHttpClient.GetStringAsync("https://spotfunc.azurewebsites.net/api/SpotPriceRequest?code=vgUdbbCJSApniy7OgY2tfEJuTomMaNzZ-QWTNcMYS8h-AzFuS91H_w==");
            TodaysSpotData todaysData = JsonConvert.DeserializeObject<TodaysSpotData>(request);
            var allData = todaysData.TodaysSpotPrices.SelectMany(a => a.SpotData).ToList();
        
            List<AreaData> areaData = new List<AreaData>();

            var se1Data = allData.Where(d => d.AreaName == "SE1").ToList();
            var se1High = se1Data.Max(p => (Convert.ToDouble(p.Price) / 1000));
            var se1Low = se1Data.Min(p => (Convert.ToDouble(p.Price) / 1000));
            areaData.Add(new AreaData() { Area = "SE1", PriceHigh = se1High, PriceLow = se1Low });

            var se2Data = allData.Where(d => d.AreaName == "SE2").ToList();
            var se2High = se2Data.Max(p => (Convert.ToDouble(p.Price) / 1000));
            var se2Low = se2Data.Min(p => (Convert.ToDouble(p.Price) / 1000));
            areaData.Add(new AreaData() { Area = "SE2", PriceHigh = se2High, PriceLow = se2Low });

            var se3Data = allData.Where(d => d.AreaName == "SE3").ToList();
            var se3High = se3Data.Max(p => (Convert.ToDouble(p.Price) / 1000));
            var se3Low = se3Data.Min(p => (Convert.ToDouble(p.Price) / 1000));
            areaData.Add(new AreaData() { Area = "SE3", PriceHigh = se3High, PriceLow = se3Low });

            var se4Data = allData.Where(d => d.AreaName == "SE4").ToList();
            var se4High = se4Data.Max(p => (Convert.ToDouble(p.Price) / 1000));
            var se4Low = se4Data.Min(p => (Convert.ToDouble(p.Price) / 1000));
            areaData.Add(new AreaData() { Area = "SE4", PriceHigh = se4High, PriceLow = se4Low });

            TableClient tableClient = _tableServiceClient.GetTableClient
                (tableName: "spotpricetable");
            tableClient.CreateIfNotExists();

            foreach (var item in areaData)
            {
                SpotPriceEntity newEntity = new();
                newEntity.AreaName = item.Area;
                newEntity.SpotPriceHigh = item.PriceHigh;
                newEntity.SpotPriceLow = item.PriceLow;
                newEntity.PartitionKey = item.Area;
                newEntity.DateAndTime = DateTime.SpecifyKind(DateTime.Now.Date, DateTimeKind.Utc);
                newEntity.RowKey = item.Area + newEntity.DateAndTime;
                tableClient.AddEntity(newEntity);
            }
        }
       public List<SpotPriceEntity> GetEntities(string partitionKey)
           {
            TableClient tableClient = _tableServiceClient.GetTableClient
                (tableName: "spotpricetable");
            var list = tableClient.Query<SpotPriceEntity>(r => r.PartitionKey == partitionKey).ToList();
            //var high =list.First().SpotPriceHigh;
            return list;
           }
        public void DeleteEntity(string rowKey)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient
                (tableName: "spotpricetable");
            var tableEntity = tableClient.Query<SpotPriceEntity>(e => e.RowKey == rowKey).FirstOrDefault();
            tableClient.DeleteEntity(tableEntity.PartitionKey, tableEntity.RowKey);
        }

        public void UpdateEntity(string rowKey)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient
                (tableName: "spotpricetable");
            var tableEntity = tableClient.Query<SpotPriceEntity>(e => e.RowKey == rowKey).FirstOrDefault();
            tableEntity.SpotPriceHigh = 0.765;
            tableClient.UpdateEntity<SpotPriceEntity>(tableEntity, tableEntity.ETag);
        }



    }
}
