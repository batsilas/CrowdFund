using System;
using System.Collections.Generic;
using System.Linq;
using CrowFund.Core.Data;
using CrowFund.Core.ExtraMethods;
using CrowFund.Core.Models;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ProjectOptions;
using CrowFund.Core.StaticData;
using Microsoft.EntityFrameworkCore;

namespace CrowFund.Core.Services
{
    public class ProjectService : IProjectService
    {

        private CrowFundDbContext context_;

        public ProjectService(CrowFundDbContext context)
        {
            context_ = context;
        }

        public Project Create(CreateProjectOptions options)
        {
            if (options == null)
            {
                return null;
            }
            var project = new Project()
            {
                Title = options.Title,
                Description = options.Description,
                TargetFund = options.TargetFund,
                CurrentFund = options.CurrentFund
            };
            context_.Add(project);
            if (context_.SaveChanges() > 0)
            {
                return project;
            }
            return null;
        }

        public void Delete(Project project)
        {
            context_.Remove(project);
        }
        public void Add(Project project)
        {
            context_.Add(project);
        }

        public void Delete(int id)
        {
            var project = Search(new SearchProjectOptions
            {
                ProjectId = id
            }).SingleOrDefault();

            context_.Remove(project);
        }

        public IEnumerable<Project> GetAll()
        {
            var projects = context_.Set<Project>()
                .Include(u => u.UserCreator)
                .Include(fd => fd.FundingPackages).ToList();


            projects.ForEach(p => p.Description = ExtraMethod.ConvertToRawHtml(p.Description));

            return projects;

        }

        public IEnumerable<Project> GetTrendsByFunds()
        {
            var projects = context_.Set<Project>()
                .Include(u => u.UserCreator)
                .OrderByDescending(p => p.CurrentFund)
                .ToList();

            projects.ForEach(p => p.Description = ExtraMethod.ConvertToRawHtml(p.Description));

            return projects;
        }

        public IEnumerable<Project> GetTrendsByFunders()
        {
            var projects = context_.Set<Project>()
                .Include(u => u.UserBackers)
                .OrderByDescending(p => p.FundsReceivedCounter)
                .ToList();

            projects.ForEach(p => p.Description = ExtraMethod.ConvertToRawHtml(p.Description));

            return projects;
        }

        public IEnumerable<Project> GetTrendsByCategory(Category category)
        {
            var projects = context_.Set<Project>()
                .Include(u => u.UserBackers)
                .Where(p => p.Category == category)
                .OrderByDescending(p => p.CurrentFund)
                .ToList();

            projects.ForEach(p => p.Description = ExtraMethod.ConvertToRawHtml(p.Description));

            return projects;
        }

            public Project GetById(int projectid)
        {
            var project = Search(new SearchProjectOptions
            {
                ProjectId = projectid
            }).SingleOrDefault();

            return project;
        }

        public IQueryable<Project> Search(string stringToFind) {
            if (stringToFind.Equals(null))
            {
                return null;
            }

            var query = context_
                .Set<Project>()
                .Include(u => u.UserCreator)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(stringToFind)) { 
                query = query.Where(p => p.Title.ToLower().Contains(stringToFind.ToLower()) ||
                                                       p.Description.ToLower().Contains(stringToFind.ToLower()));

            }
            query = query.Take(500);

            return query;
        }

        public IQueryable<Project> Search(SearchProjectOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = GetAll()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                query = query.Where(p => p.Title == options.Title);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(p => p.Description == options.Description);
            }

            if (options.ProjectId != null)
            {
                query = query.Where(p => p.ProjectId == options.ProjectId);
            }

            if (options.DateCreatedFrom != null)
            {
                query = query.Where(p => p.DateCreated >= options.DateCreatedFrom);
            }

            if (options.DateCreatedTo != null)
            {
                query = query.Where(p => p.DateCreated <= options.DateCreatedTo);
            }

            if (options.TargetFund != null)
            {
                query = query.Where(p => p.TargetFund >= options.TargetFund.Value);
            }

            if (options.CurrentFund != null)
            {
                query = query.Where(p => p.CurrentFund >= options.CurrentFund.Value);
            }
            if (!string.IsNullOrWhiteSpace(options.StringToFind))
            {
                query = query.Where(p => p.Title.ToLower().Contains(options.StringToFind.ToLower()) ||
                                         p.Description.ToLower().Contains(options.StringToFind.ToLower()));

            }
            if (options.category != null)
            {
                query = query.Where(p => p.Category == options.category);
            }
            query = query.Take(500);

            return query;
        }


        public bool Update(
            UpdateProjectOptions options)
        {
            if (options == null)
            {
                return false;
            }

            var project = Search(
                new SearchProjectOptions()
                {
                    ProjectId = options.ProjectId
                }).SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                project.Title = options.Title;
            }
            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                project.Description = options.Description;
            }
            project.DateCreated = DateTime.Now;
            //if (options.Photos != null) {
            //    project.Photos = options.Photos;
            //}
            //if (options.Videos != null)
            //{
            //    project.Videos = options.Videos;
            //}
            if (options.Status != null)
            {
                project.Status = options.Status;
            }
            if (options.FundingPackages != null)
            {
                project.FundingPackages = options.FundingPackages;
            }
            if (options.TargetFund != null)
            {
                project.TargetFund = options.TargetFund.Value;
            }
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;


        }

        public bool Update(Project project)
        {
            if (project == null)
            {
                return false;
            }

            var projectDb = Search(
                new SearchProjectOptions()
                {
                    ProjectId = project.ProjectId
                }).SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(project.Title))
            {
                projectDb.Title = project.Title;
            }
            if (!string.IsNullOrWhiteSpace(project.Description))
            {
                projectDb.Description = project.Description;
            }
            projectDb.DateCreated = DateTime.Now;
            //if (options.Photos != null) {
            //    project.Photos = options.Photos;
            //}
            //if (options.Videos != null)
            //{
            //    project.Videos = options.Videos;
            //}
            if (project.Status != null)
            {
                projectDb.Status = project.Status;
            }
            if (project.FundingPackages != null)
            {
                projectDb.FundingPackages = project.FundingPackages;
            }
            if (project.TargetFund != null)
            {
                projectDb.TargetFund = project.TargetFund;
            }
            if (project.Category != null)
            {
                projectDb.Category = project.Category;
            }
            if (project.ImageUrl != null)
            {
                projectDb.ImageUrl = project.ImageUrl;
            }
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;


        }

        public bool FundProject(FundProjectOptions options)
        {
            if (options == null || options.ProjectId == null || options.FundingPackageId == null)
            {
                return false;
            }

            var project = Search(new SearchProjectOptions
            {
                ProjectId = options.ProjectId

            }).SingleOrDefault();

            if (project == null)
            {
                return false;
            }

            var fundpack = project.FundingPackages.Where(fp => fp.FundingPackageId == options.FundingPackageId).SingleOrDefault();
            if (fundpack == null)
            {
                return false;
            }
            //project.TargetFund -= fundpack.Price;
            project.CurrentFund += fundpack.Price;

            project.FundsReceivedCounter += 1;

            if (context_.SaveChanges() > 0)
            {
                return true;
            }


            return false;
        }



    }
}
