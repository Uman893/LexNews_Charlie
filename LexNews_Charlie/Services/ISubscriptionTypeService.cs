using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.Services
{
    public interface ISubscriptionTypeService
    {
        void CreateSubscriptionType(SubscriptionType subscriptionType);
        SubscriptionType FetchSubscriptionType(int? id);
        void EditSubscriptionType(SubscriptionType subscriptionType);
        SubscriptionType DetailsSubscriptionType(int id);
        List<SubscriptionType> GetSubscriptionTypeList();
        SubscriptionType DeleteSubscriptionType(int? id);
        string DeleteConfirmedSubscriptionType(int id);
        SubscriptionType GetOneSubscriptionTypebyId(int id);
    }
}
