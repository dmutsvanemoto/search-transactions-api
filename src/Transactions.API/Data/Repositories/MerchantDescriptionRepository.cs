using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Transactions.API.Data.Models;

namespace Transactions.API.Data.Repositories
{
    public class MerchantDescriptionRepository : IMerchantDescriptionRepository
    {
        private readonly IElasticClient _elasticClient;

        public MerchantDescriptionRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<MerchantDescription> FindBestMatchAsync(string description)
        {
            var response = await _elasticClient.SearchAsync<MerchantDescription>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Description).Query(description))));

            if (response.IsValid)
            {
                return response.Hits.OrderByDescending(x => x.Score).FirstOrDefault()?.Source;
            }
            
            throw new InvalidOperationException("Invalid Read Query");
        }

        public async Task CreateAsync(MerchantDescription merchantDescription)
        {
            var response = await _elasticClient.IndexDocumentAsync(merchantDescription);

            if (!response.IsValid)
            {
                throw new InvalidOperationException("Invalid Create Query");

            }
        }
    }
}