using System;
using System.Linq;
using CrowFund.Core.Models;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.FundingPackageOptions;

namespace CrowFund.Core.Services.IServices
{
    public interface IFundingPackageService
    {
        FundingPackage CreateFundingPackage(CreateFundingPackageOptions options);
        IQueryable<FundingPackage> SearchFundingPackage(SearchFundingPackageOptions options);
        FundingPackage UpdateFundingPackage(UpdateFundingPackageOptions options,int id);
        FundingPackage GetFundingPackageById(int id);
        void Add(FundingPackage fundingPackage);

    }
}
