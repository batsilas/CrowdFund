using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Services.Options.PhotoOptions
{
    public class UpdatePhotoOptions
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int? PhotoId { get; set; }
    }
}
