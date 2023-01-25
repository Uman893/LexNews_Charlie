using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using LexNews_Charlie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.Services
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly ApplicationDbContext _db;
        public SubscriptionTypeService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void CreateSubscriptionType([Bind("TypeName,Description,Price")] SubscriptionType subscriptionType)
        {
            _db.SubscriptionType.Add(subscriptionType);
            _db.SaveChanges();
        }
        public SubscriptionType FetchSubscriptionType(int? id)
        {
            if (id != null)
            {
                var subscriptionType = _db.SubscriptionType.Find(id);
                return subscriptionType;
            }
            else
                return null;
        }
        public void EditSubscriptionType([Bind("TypeName,Description,Price")] SubscriptionType subscriptionType)
        {
            _db.SubscriptionType.Update(subscriptionType);
            _db.SaveChanges();
        }
        public SubscriptionType DetailsSubscriptionType(int id)
        {
            if (id != 0)
            {
                var subscriptionType = _db.SubscriptionType.FirstOrDefault(a => a.Id == id);
                return subscriptionType;
            }
            else
                return null;
        }
        public List<SubscriptionType> GetSubscriptionTypeList()
        {
            List<SubscriptionType> subscriptionType = _db.SubscriptionType.ToList();
            return subscriptionType;
        }
        public SubscriptionType DeleteSubscriptionType(int? id)
        {
            if (id == null || _db.SubscriptionType == null)
            {
                return null;
            }
            var subscriptionType = FetchSubscriptionType(id);
            if (subscriptionType == null)
            {
                return null;
            }
            return subscriptionType;
        }
        public string DeleteConfirmedSubscriptionType(int id)
        {
            if (_db.Articles == null)
            {
                return null;
            }
            var subscriptionType = FetchSubscriptionType(id);
            if (subscriptionType != null)
            {
                _db.SubscriptionType.Remove(subscriptionType);
                _db.SaveChanges();
                return "";
            }
            return "";
        }
        public SubscriptionType GetOneSubscriptionTypebyId(int id)
        {
            return _db.SubscriptionType.Find(id);
        }
    }
}

