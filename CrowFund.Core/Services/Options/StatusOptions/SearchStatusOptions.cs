using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Services.Options.StatusOptions
{
    public class SearchStatusOptions
    {
        public int? StatusId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreatedFrom { get; set; }
        public DateTimeOffset DateCreatedTo { get; set; }
    }
}
