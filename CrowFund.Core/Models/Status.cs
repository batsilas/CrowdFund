using System;

namespace CrowFund.Core.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public Status()
        {
            DateCreated = DateTime.Now;
        }
    }
}
