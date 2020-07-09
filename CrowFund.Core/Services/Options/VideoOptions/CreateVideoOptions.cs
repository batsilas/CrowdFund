using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Services.Options.VideoOptions
{
    public class CreateVideoOptions
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
