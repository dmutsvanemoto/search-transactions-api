using System.Threading.Tasks;
using Transactions.API.Data.Models;
using Transactions.API.Data.Repositories;

namespace Transactions.API.Business
{
    public class SearchService : ISearchService
    {
        private readonly IMerchantDescriptionRepository _repository;

        public SearchService(IMerchantDescriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetMerchantNameByDescriptionAsync(string description)
        {
            var merchantDescription = await _repository.FindBestMatchAsync(description);

            return merchantDescription?.Merchant;
        }

        public async Task CreateMerchantDescriptionAsync(MerchantDescription merchantDescription)
        {
            var merchant = string.IsNullOrWhiteSpace(merchantDescription.Merchant) ? "unknown" : merchantDescription.Merchant;
            var create = new MerchantDescription(merchantDescription.Description, merchant);

            await _repository.CreateAsync(create);
        }
    }
}