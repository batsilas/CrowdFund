using CrowFund.Core.StaticData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrowFund.Core.Models
{
    public class Project
    {

        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Category Category { get; set; }

        public DateTime DateCreated { get; set; }
        public List<Photo> Photos {get;set;}
        public List<Video> Videos { get; set; }
        public List<Status> Status { get; set; }
        public List<FundingPackage> FundingPackages { get; set; }
        public ApplicationUser UserCreator { get; set; }
        public decimal TargetFund { get; set; }
        public decimal CurrentFund { get; set; }
        public List<ApplicationUser> UserBackers { get; set; }
        public int FundsReceivedCounter { get; set; }


        public Project()
        {
            Photos = new List<Photo>();
            Videos = new List<Video>();
            Status = new List<Status>();
            FundingPackages = new List<FundingPackage>();
            UserBackers = new List<ApplicationUser>();
            DateCreated = DateTime.Now;
            FundsReceivedCounter = 0;
        }

        public static void UploadImage(Project project, string webRootPath, IFormFileCollection files)
        {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, @"images\projects");
            var extension = Path.GetExtension(files[0].FileName);

            if (project.ImageUrl != null)
            {
                //this is an edit and we need to remove older image
                var imagePath = Path.Combine(webRootPath, project.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(filesStreams);
            }

            project.ImageUrl = @"\images\projects\" + fileName + extension;
        }

    }
}
