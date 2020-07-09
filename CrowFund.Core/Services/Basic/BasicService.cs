using CrowFund.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Services.Basic
{
    public class BasicService : IBasicService
    {
        private readonly CrowFundDbContext _db;

        public BasicService(CrowFundDbContext db)
        {
            _db = db;
        }
        public void Complete()
        {
            _db.SaveChanges();
        }

    }
}
