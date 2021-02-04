using System.Threading.Tasks;
using Transactions.API.Data.Models;

namespace Transactions.API.Business
{
    public interface ISearchService
    {
        Task<string> GetMerchantNameByDescriptionAsync(string description);
        Task CreateMerchantDescriptionAsync(MerchantDescription merchantDescription);
    }
}
