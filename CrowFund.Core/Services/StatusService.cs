using CrowFund.Core.Data;
using CrowFund.Core.Models;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ProjectOptions;
using CrowFund.Core.Services.Options.StatusOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowFund.Core.Services
{
    public class StatusService : IStatusService
    {
        private CrowFundDbContext context_;
        private IProjectService projectService_;

        public StatusService(CrowFundDbContext context, IProjectService projectService)
        {
            context_ = context;
            projectService_ = projectService;
        }

        public Status CreateStatus(CreateStatusOptions options)
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

            var status = new Status()
            {
                Description = options.Description
               
            };

            project.Status.Add(status);
            context_.Update(project);
            context_.Add(status);

            if (context_.SaveChanges() > 0)
            {
                return status;
            }
            return null;
        }

        public Status GetStatusById(int statusid)
        {
            var status = SearchStatus(new SearchStatusOptions
            {          
                StatusId = statusid
            }).SingleOrDefault();

            return status;
        }

        public IQueryable<Status> SearchStatus(SearchStatusOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<Status>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(st => st.Description == options.Description);
            }

            if (options.StatusId!=null)
            {
                query = query.Where(st => st.StatusId == options.StatusId);
            }

            if (options.DateCreatedFrom != null)
            {
                query = query.Where(st => st.DateCreated >= options.DateCreatedFrom);
            }

            if (options.DateCreatedTo != null)
            {
                query = query.Where(st => st.DateCreated <= options.DateCreatedTo);
            }
            query = query.Take(500);

            return query;
        }

        public Status UpdateStatus(UpdateStatusOptions options, int statusid)
        {
            if (options == null)
            {
                return null;
            }

            var status = SearchStatus(new SearchStatusOptions
            {
                StatusId = statusid
            }).SingleOrDefault();

            if (status != null)
            {
                status.Description = options.Description;
            }

            if (context_.SaveChanges() >= 0)
            {
                return status;
            }

            return null;
        }
    }
}
