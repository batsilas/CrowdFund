using CrowFund.Core.StaticData;
using System;
using System.Collections.Generic;
using System.Text;


namespace CrowFund.Core.Services.Options.ProjectOptions
{
    public class SearchProjectOptions
    {

        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? DateCreatedFrom { get; set; }
        public DateTimeOffset? DateCreatedTo { get; set; }
        public string StringToFind { get; set; }

        public decimal? TargetFund { get; set; }
        public decimal? CurrentFund { get; set; }
        public DateTimeOffset? DateCreated { get; set; }

        public Category? category { get; set; }



    }
}
