namespace CrowFund.Core.Models
{
    public class UserProject
    {
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

    }
}
