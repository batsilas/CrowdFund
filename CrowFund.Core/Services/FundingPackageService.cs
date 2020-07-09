using System;
using System.Linq;
using CrowFund.Core.Data;
using CrowFund.Core.Models;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.FundingPackageOptions;
using CrowFund.Core.Services.Options.ProjectOptions;

namespace CrowFund.Core.Services
{
    public class FundingPackageService : IFundingPackageService
    {
        private CrowFundDbContext context_;
        private IProjectService projectService_;

        public FundingPackageService(CrowFundDbContext context, IProjectService projectService)
        {
            context_ = context;
            projectService_ = projectService;
        }

        public void Add(FundingPackage fundingPackage)
        {
            context_.Add(fundingPackage);
            context_.SaveChanges();
        }

        public FundingPackage CreateFundingPackage(CreateFundingPackageOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var project = projectService_.Search(new SearchProjectOptions
            {
                ProjectId = options.ProjectId
            }).SingleOrDefault();

            if (project == null)
            {
                return null;
            }

            var fp = new FundingPackage()
            {
                Name = options.Name,
                Description = options.Description,
                Price = options.Price,

            };

            project.FundingPackages.Add(fp);
            context_.Update(project);
            context_.Add(fp);

            if (context_.SaveChanges() > 0)
            {
                return fp;
            }
            return null;
        }

        public FundingPackage GetFundingPackageById(int fundingpackageid)
        {
            var fundingpackage = SearchFundingPackage(new SearchFundingPackageOptions
            {
               FundingPackageId  = fundingpackageid
            }).SingleOrDefault();

            return fundingpackage;
        }

        public IQueryable<FundingPackage> SearchFundingPackage(SearchFundingPackageOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<FundingPackage>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(fp => fp.Name == options.Name);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(fp => fp.Name == options.Name);
            }

            if (options.FundingPackageId != null)
            {
                query = query.Where(fp => fp.FundingPackageId == options.FundingPackageId.Value);
            }

            if (options.PriceFrom != null)
            {
                query = query.Where(fp => fp.Price >= options.PriceFrom);
            }

            if (options.PriceTo != null)
            {
                query = query.Where(fp => fp.Price <= options.PriceTo);
            }
            query = query.Take(500);

            return query;
        }

        public FundingPackage UpdateFundingPackage(UpdateFundingPackageOptions options,int fundingpackageid)
        {
            if (options == null)
            {
                return null;
            }

            var fundingpackage = SearchFundingPackage(new SearchFundingPackageOptions
            {
                FundingPackageId = fundingpackageid
            }).SingleOrDefault();

            if (fundingpackage != null)
            {
                fundingpackage.Name = options.Name;
                fundingpackage.Description = options.Description;
                fundingpackage.Price = options.Price;

            }

            if (context_.SaveChanges() >= 0)
            {
                return fundingpackage;
            }


            return null;
        }
    }
}
