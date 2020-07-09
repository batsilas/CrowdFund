using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CrowFund.Core.Data;
using CrowFund.Core.ExtraMethods;
using CrowFund.Core.Models;
using CrowFund.Core.Services;
using CrowFund.Core.Services.Basic;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ProjectOptions;
using CrowFund.Core.StaticData;
using CrowFund.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Crowdfund_Project.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class ProjectController : Controller
    {
        private IProjectService _projectService;
        private IBasicService _basicService;
        private IUserService _userService;
        private IUserBackerService _userBackerService;

        private readonly IWebHostEnvironment _hostEnviroment;


        public ProjectController(IProjectService projectService,IBasicService basicService, IWebHostEnvironment hostEnvironment, IUserService userService, IUserBackerService userBackerService )
        {
            _projectService = projectService;
            _basicService = basicService;
            _hostEnviroment = hostEnvironment;
            _userService = userService;
            _userBackerService = userBackerService;

        }

        public IActionResult Trending(Category category)
        {
            var trendsByFunds = _projectService.GetTrendsByFunds().ToList();
            var trendsByFunders = _projectService.GetTrendsByFunders().ToList();
            var trendsByCategory = _projectService.GetTrendsByCategory(category).ToList();

            if (trendsByFunds == null ) {
                return null;
            }

            if (trendsByFunders == null ) {
                return null;
            }

            trendsByFunds.ForEach(p => p.Description =
                    ExtraMethod.ConvertToRawHtml(p.Description));
            trendsByFunders.ForEach(p => p.Description =
                    ExtraMethod.ConvertToRawHtml(p.Description));
            trendsByCategory.ForEach(p => p.Description =
                    ExtraMethod.ConvertToRawHtml(p.Description));

            var TrendingsViewModel = new TrendingViewModel() {
                GetTrendsByFunders = trendsByFunders,
                GetTrendsByFunds = trendsByFunds,
                GetTrendsByCategory = trendsByCategory

            };

            return View(TrendingsViewModel);
        }

        public IActionResult ShowAll()
        {
            var projects = _projectService.GetAll().ToList();


            return View(projects);
        }
        [Authorize]
        public IActionResult ShowMine()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            var projects = _projectService.GetAll().
                Where(u=>u.UserCreator.Id == claim.Value)
                .ToList();


            return View(projects);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var project = _projectService.GetById(id);
            ExtraMethod.ConvertToRawHtml(project.Description);

            project.FundingPackages.ForEach(p => p.Description = ExtraMethod.ConvertToRawHtml(p.Description));

            return View(project);
        }


        [Authorize]
        public IActionResult Upsert(int? id)
        {

            var project = new Project();

            if (id == null)
            {
                //this is for create
                return View(project);
            }

            // this is for edit
            project = _projectService.GetById(id.GetValueOrDefault());

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

      
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Project project)
        {


            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _hostEnviroment.WebRootPath;

                if (files.Count > 0)
                {
                    Project.UploadImage(project, webRootPath, files);
                }
                else
                {
                    //update when they do not change the image
                    if (project.ProjectId != 0)
                    {
                        var objFromDb = _projectService.GetById(project.ProjectId);
                        project.ImageUrl = objFromDb.ImageUrl;
                    }
                }
                if (project.ProjectId == 0)
                {
                    _projectService.Add(project);

                    project.UserCreator = _userService.GetById(User.GetUsername());

                }
                else
                {
                    _projectService.Update(project);
                }

                _basicService.Complete();

                return RedirectToAction("ShowMine");
            }
            else
            {

                if (project.ProjectId != 0)
                {
                    project = _projectService.GetById(project.ProjectId);
                }
            }
            return View(project);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = _projectService.GetAll().ToList();


            return Json(projects);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return BadRequest();
            }

            return Json(project);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return BadRequest();
            }

            _projectService.Delete(project);

            _basicService.Complete();

            return Json(project);
        }


        [HttpGet]
        public IActionResult Search(string searchType)
        {
            if (string.IsNullOrWhiteSpace(searchType)) {
                return NotFound();
            }
            var options = new SearchProjectOptions()
            {
                StringToFind = searchType
            };
            
            var projects = _projectService.Search(options).ToList();

            return View(projects);
        }

        [HttpPost]
        public IActionResult Fund([FromBody] FundViewModel viewmodel)
        {
            
            if (viewmodel == null)
            {
                return BadRequest();
            }

            var fundingOptions = new FundProjectOptions()
            {
                FundingPackageId = int.Parse(viewmodel.FundingPackageId),
                ProjectId = int.Parse(viewmodel.ProjectId)
            };

            _projectService.FundProject(fundingOptions);

            var projectFunded = _projectService.GetById(int.Parse(viewmodel.ProjectId));
            var funder = _userService.GetById(User.GetUsername());

            _userBackerService.Add(new UserBacker(projectFunded.ProjectId, funder.Id));

            
            _basicService.Complete();



            return Ok();
        }

        [HttpGet]
        public IActionResult ShowByCategory(Category cat)
        {
            var projects = _projectService.Search(new SearchProjectOptions()
            {
                category = cat
            }); ;

            return View(projects);
        }
    }
}