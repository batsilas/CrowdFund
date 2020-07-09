using System;
using System.Collections.Generic;
using System.Linq;
using CrowFund.Core.Models;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ProjectOptions;
using CrowFund.Core.StaticData;

namespace CrowFund.Core.Services.IServices
{
    public interface IProjectService
    {
        Project Create(CreateProjectOptions options);
        IQueryable<Project> Search(SearchProjectOptions options);
        IQueryable<Project> Search(string stringToFind);
        bool Update(UpdateProjectOptions options);
        bool Update(Project project);
        Project GetById(int id);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetTrendsByFunds();
        IEnumerable<Project> GetTrendsByFunders();
        IEnumerable<Project> GetTrendsByCategory(Category category);
        void Delete(Project project);
        void Delete(int id);
        void Add(Project project);
        bool FundProject(FundProjectOptions fundProjectOptions);

    }
}
