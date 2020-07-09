using CrowFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace CrowFund.Core.Services.Options.ProjectOptions
{
    public class UpdateProjectOptions
    {
        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public List<Status> Status { get; set; }
        public List<FundingPackage> FundingPackages { get; set; }
        public decimal? TargetFund { get; set; }

    }
}
