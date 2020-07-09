using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowFund.Core.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public Photo()
        {
            DateCreated = DateTime.Now;
        }
    }
}
