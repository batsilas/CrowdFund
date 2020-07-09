using CrowFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace CrowFund.Core.Services.Options.ProjectOptions
{
    public class CreateProjectOptions
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Status> Status { get; set; }
        public IEnumerable<FundingPackage> FundingPackages { get; set; }

        
        
        public decimal TargetFund { get; set; }
        public decimal CurrentFund { get; set; }

    }
}
