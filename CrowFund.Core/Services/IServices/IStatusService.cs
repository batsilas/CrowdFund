using CrowFund.Core.Models;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.StatusOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowFund.Core.Services.IServices
{
    public interface IStatusService
    {
        Status CreateStatus(CreateStatusOptions options);
        IQueryable<Status> SearchStatus(SearchStatusOptions options);
        Status UpdateStatus(UpdateStatusOptions options, int id);
        Status GetStatusById(int id);
    }
}
