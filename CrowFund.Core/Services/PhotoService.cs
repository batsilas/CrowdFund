using CrowFund.Core.Data;
using CrowFund.Core.Models;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.PhotoOptions;
using CrowFund.Core.Services.Options.ProjectOptions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CrowFund.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private CrowFundDbContext context_;
        private IProjectService projectService_;

        public PhotoService(CrowFundDbContext context, IProjectService projectService)
        {
            context_ = context;
            projectService_ = projectService;
        }

        public Photo CreatePhoto(CreatePhotoOptions options)
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

            var photo = new Photo()
            {
                Name = options.Name,
                Path = options.Path,

            };

            project.Photos.Add(photo);
            context_.Update(project);
            context_.Add(photo);
            
            if (context_.SaveChanges() > 0)
            {
                return photo;
            }
            
            return null;

        }

        public Photo UpdatePhoto(UpdatePhotoOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var queryPhoto = context_
                    .Set<Photo>()
                    .AsQueryable()
                    .Where(p => p.ID == options.PhotoId);

            if ( queryPhoto != null)
            {
                var photo = new Photo()
                {
                    Name = options.Name,
                    Path = options.Path
                };

                return photo;
            }

            return null;
        }
    }
}
