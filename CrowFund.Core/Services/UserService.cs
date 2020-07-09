using CrowFund.Core.Data;
using CrowFund.Core.Models;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ApplicationUserOptions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowFund.Core.Services
{
    public class UserService : IUserService
    {
        private readonly CrowFundDbContext context_;
        public UserService
            (CrowFundDbContext context)
        {
            context_ = context;
        }

        public ApplicationUser Create(CreateUserOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var user = new ApplicationUser()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                UserName = options.UserName,
                DateOfBirth = options.DateOfBirth

            };

            if (user != null) {
                context_.Add(user);
                context_.SaveChanges();
                return user;
            }
          
            return null;
        }

        public IQueryable<ApplicationUser> Search(SearchUserOptions options) {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<ApplicationUser>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.FirstName))
            {
                query = query.Where(u => u.FirstName == options.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(options.LastName))
            {
                query = query.Where(u => u.LastName == options.LastName);
            }

            if (!string.IsNullOrWhiteSpace(options.UserName))
            {
                query = query.Where(u => u.UserName == options.UserName);
            }



            if (options.DateOfBirthFrom != null)
            {
                query = query.Where(u => u.DateOfBirth >= options.DateOfBirthFrom);
            }

            if (options.DateOfBirthTo != null)
            {
                query = query.Where(u => u.DateOfBirth <= options.DateOfBirthTo);
            }

            query = query.Take(500);

            return query;
        }

        public void Delete(ApplicationUser user)
        {
            context_.Remove(user);
        }
        public void Add(ApplicationUser user)
        {
            context_.Add(user);
        }

        public void Delete(int id)
        {
            var user = Search(new SearchUserOptions
            {
                Id = id
            }).SingleOrDefault();

            context_.Remove(user);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return context_.Set<ApplicationUser>().ToList();
        }

        public ApplicationUser GetById(int id)
        {
            var user = Search(new SearchUserOptions
            {
                Id = id
            }).SingleOrDefault();

            return user;
        }
        public ApplicationUser GetById(string id)
        {
            var user = context_.ApplicationUsers.Find(id);

            return user;
        }
        public bool Update(
                 UpdateUserOptions options)
        {
            if (options == null)
            {
                return false;
            }

            var user = Search(
                new SearchUserOptions()
                {
                    Id = options.UserId
                }).SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(options.FirstName))
            {
                user.FirstName = options.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(options.LastName))
            {
                user.LastName = options.LastName;
            }
            if (!string.IsNullOrWhiteSpace(options.UserName))
            {
                user.UserName = options.UserName;
            }
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;


        }
    }
}
