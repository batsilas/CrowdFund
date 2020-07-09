using System;
namespace CrowFund.Core.Services.Options.FundingPackageOptions
{
    public class CreateFundingPackageOptions
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
