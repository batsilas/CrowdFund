using CrowFund.Core.Models;
using System.Collections.Generic;

namespace CrowFund.Core.ViewModels
{
    public class TrendingViewModel
    {
        public IEnumerable<Project> GetTrendsByFunders { get; set; }
        public IEnumerable<Project> GetTrendsByFunds { get; set; }
        public IEnumerable<Project> GetTrendsByCategory { get; set; }

        public TrendingViewModel()
        {
            GetTrendsByFunders = new List<Project> ();
            GetTrendsByFunds = new List<Project> ();
            GetTrendsByCategory = new List<Project>();
        }
    }
}
