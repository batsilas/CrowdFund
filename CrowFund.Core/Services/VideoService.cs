using CrowFund.Core.Data;
using CrowFund.Core.Models;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ProjectOptions;
using CrowFund.Core.Services.Options.VideoOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CrowFund.Core.Services
{
    public class VideoService : IVideoService
    {
        private CrowFundDbContext context_;
        private IProjectService projectService_;

        public VideoService(CrowFundDbContext context, IProjectService projectService)
        {
            context_ = context;
            projectService_ = projectService;
        }

        public Video CreateVideo(CreateVideoOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var project = projectService_.Search(new SearchProjectOptions
            {
                ProjectId = options.ProjectId
            }).SingleOrDefault();

            if (project == null)
            {
                return null;
            }

            var video = new Video()
            {
                Name = options.Name,
                Path = options.Path,

            };

            project.Videos.Add(video);
            context_.Update(project);
            context_.Add(video);

            if (context_.SaveChanges() > 0)
            {
                return video;
            }

            return null;

        }

        public Video UpdateVideo(UpdateVideoOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var queryVideo = context_
                    .Set<Video>()
                    .AsQueryable()
                    .Where(v => v.ID == options.VideoId);

            if (queryVideo != null)
            {
                var video = new Video()
                {
                    Name = options.Name,
                    Path = options.Path
                };

                return video;
            }

            return null;
        }
    }
}
