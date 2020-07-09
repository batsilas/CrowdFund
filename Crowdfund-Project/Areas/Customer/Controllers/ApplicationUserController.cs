using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowFund.Core.Data;
using CrowFund.Core.Models;
using CrowFund.Core.Services.Basic;
using CrowFund.Core.Services.IServices;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.ApplicationUserOptions;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_Project.Areas.Customer.Controllers
{
    public class ApplicationUserController : Controller
    {
        private IUserService _userService;
        private IBasicService _basicService;

        public ApplicationUserController(IUserService userService, IBasicService basicService)
        {
            _userService = userService;
            _basicService = basicService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = _userService.GetAll();

            return Json(projects);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var project = _userService.GetById(id);

            if (project == null)
            {
                return BadRequest();
            }

            return Json(project);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var project = _userService.GetById(id);

            if (project == null)
            {
                return BadRequest();
            }

            _userService.Delete(project);

            _basicService.Complete();

            return Json(project);
        }

        [HttpPost]
        public IActionResult Create(CreateUserOptions options)//options
        {
            var project = _userService.Create(options);

            if (project == null)
            {
                return BadRequest();
            }

            _userService.Add(project);

            _basicService.Complete();

            return Json(project);
        }

        [HttpPatch]
        public IActionResult Update(UpdateUserOptions options) //options
        {
            if (options == null)
            {
                return BadRequest();
            }

            _userService.Update(options);

            //update
            _basicService.Complete();

            return Ok();
        }

        [HttpPost]
        public IActionResult Search(SearchUserOptions options)
        {
            var project = _userService.Search(options).ToList();

            if (!project.Any())
            {
                return NotFound();
            }

            return Json(project);
        }
    }
}
