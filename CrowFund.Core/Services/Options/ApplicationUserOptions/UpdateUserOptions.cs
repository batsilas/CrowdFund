using CrowFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace CrowFund.Core.Services.Options.ApplicationUserOptions
{
    public class UpdateUserOptions
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
