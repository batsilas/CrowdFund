using CrowFund.Core.Data;
using CrowFund.Core.Models;
using CrowFund.Core.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Services
{
    public class UserBackerService : IUserBackerService
    {
        private CrowFundDbContext context_;

        public UserBackerService(CrowFundDbContext context)
        {
            context_ = context;
        }

        public void Add(UserBacker userBacker)
        {
            context_.Add(userBacker);
            context_.SaveChanges();
        }
    }
}
