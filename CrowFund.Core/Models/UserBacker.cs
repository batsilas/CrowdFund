using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Models
{
    public class UserBacker
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        public string UserId { get; set; }
        public UserBacker()
        {

        }
        public UserBacker(int projectId, string userId)
        {
            ProjectId = projectId;
            UserId = userId;
        }
    }
}
