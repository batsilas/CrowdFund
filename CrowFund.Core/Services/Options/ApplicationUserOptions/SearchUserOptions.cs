using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Services.Options.ApplicationUserOptions
{
    public class SearchUserOptions
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirthFrom { get; set; }
        public DateTime DateOfBirthTo { get; set; }
    }
}
