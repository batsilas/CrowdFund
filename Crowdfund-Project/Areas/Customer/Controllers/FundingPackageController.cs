using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CrowFund.Core.ExtraMethods;
using CrowFund.Core.Models;
using CrowFund.Core.Services.Basic;
using CrowFund.Core.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Crowdfund_Project.Areas.Customer.Controllers
{

    [Area("Customer")]

    [Authorize]
    public class FundingPackageController : Controller
    {
        private IProjectService _projectService;
        private IBasicService _basicService;
        private IUserService _userService;
        private IFundingPackageService _fundingPackage;


        public FundingPackageController(IProjectService projectService, IBasicService basicService, IUserService userService,IFundingPackageService fundingPackage)
        {
            _projectService = projectService;
            _basicService = basicService;
            _fundingPackage = fundingPackage;
            _userService = userService;

        }

        public IActionResult Create(int id)
        {

            var project = _projectService.GetById(id);

            if (project.UserCreator.Id == User.GetUsername())
            {

                var fundingpackage = new FundingPackage()
                {
                    Project = project,
                    ProjectId = project.ProjectId
                };

                return View(fundingpackage);

            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FundingPackage fundingPackage)
        {
            if (ModelState.IsValid)
            {
                _fundingPackage.Add(fundingPackage);

                return RedirectToAction("ShowMine", "Project");
            }

            return RedirectToAction("Index", "Home");

        }
    }
}