using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Models
{
    public class FundingPackage
    {
        public int FundingPackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public FundingPackage()
        {
            DateCreated = DateTime.Now;
        }

    }
}
