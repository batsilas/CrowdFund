using System;
namespace CrowFund.Core.Services.Options.FundingPackageOptions
{
    public class SearchFundingPackageOptions
    {
        public int? FundingPackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

    }
}
