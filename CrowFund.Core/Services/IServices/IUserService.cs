using CrowFund.Core.Models;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ApplicationUserOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowFund.Core.Services.IServices
{
    public interface IUserService
    {
        ApplicationUser Create(
            CreateUserOptions createUserOptions);
        IQueryable<ApplicationUser> Search(
            SearchUserOptions searchUserOptions);
        ApplicationUser GetById(int id);
        ApplicationUser GetById(string Id);

        IEnumerable<ApplicationUser> GetAll();
        void Delete(ApplicationUser user);
        void Delete(int id);
        void Add(ApplicationUser user);
        bool Update(UpdateUserOptions options);
    }
}
