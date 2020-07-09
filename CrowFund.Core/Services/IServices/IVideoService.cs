using CrowFund.Core.Models;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.VideoOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Services.IServices
{
    public interface IVideoService
    {
        Video CreateVideo(
            CreateVideoOptions options);

        Video UpdateVideo(
            UpdateVideoOptions options);
    }
}
