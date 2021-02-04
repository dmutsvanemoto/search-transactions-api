using System.Threading.Tasks;
using Transactions.API.Data.Models;

namespace Transactions.API.Data.Repositories
{
    public interface IMerchantDescriptionRepository
    {
        Task<MerchantDescription> FindBestMatchAsync(string query);
        Task CreateAsync(MerchantDescription merchantDescription);
    }
}