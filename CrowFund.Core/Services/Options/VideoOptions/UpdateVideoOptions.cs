using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace CrowFund.Core.Services.Options.VideoOptions
{
    public class UpdateVideoOptions
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int? VideoId { get; set; } 
    }
}
