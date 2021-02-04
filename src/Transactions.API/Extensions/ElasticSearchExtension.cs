using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Transactions.API.Data.Models;

namespace Transactions.API.Extensions
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var settings = new ConnectionSettings(new Uri(url))
                    .DefaultMappingFor<MerchantDescription>(descriptionMapping => descriptionMapping
                        .IndexName("descriptions")
                        .IdProperty(description => description.Description)

                    )
                ;
            var client = new ElasticClient(settings);
            var response = client.Indices.Create("descriptions", creator => creator
                .Map<MerchantDescription>(description => description
                    .AutoMap()
                )
            );
            // maybe check response to be safe...
            services.AddSingleton<IElasticClient>(client);
        }

        public static IList<MerchantDescription> Descriptions = new List<MerchantDescription>
        {
            new MerchantDescription("sainsbury's sprmrkts lt london", "Sainburys"),
            new MerchantDescription("uber help.uber.com", "Uber"),
            new MerchantDescription("uber eats j5jgo help.uber.com", "Uber Eats"),
            new MerchantDescription("netflix.com amsterdam ", "Netflix"),
            new MerchantDescription("amazon eu sarl amazon.co.uk/", "Amazon"),
            new MerchantDescription("netflix.com 866-716-0414", "Netflix"),
            new MerchantDescription("uber eats 6p7n7 help.uber.com", "Uber Eats"),
            new MerchantDescription("google *google g.co/helppay#", "Google"),
            new MerchantDescription("amazon prime amzn.co.uk/pm", "Amazon Prime"),
            new MerchantDescription("dvla vehicle tax ", "DVLA"),
            new MerchantDescription("dvla vehicle tax - vis", "DVLA"),
            new MerchantDescription("direct debit payment to dvla-i2den", "DVLA"),
            new MerchantDescription("dvla-ln99abc", "DVLA"),
            new MerchantDescription("sky digital 13524686324522", "Sky Digital"),
            new MerchantDescription("direct debit sky digital 83741234567852 ddr", "Sky Digital"),
            new MerchantDescription("sky subscription - sky subscription 38195672157 gb", "Sky"),
        };

        public static void LoadData(this IServiceProvider services)
        {
            var elasticClient = services.GetRequiredService<IElasticClient>();
            foreach (var merchantDescription in Descriptions)
            {
                var response = elasticClient.IndexDocument(merchantDescription);
            }
        }
    }
}
