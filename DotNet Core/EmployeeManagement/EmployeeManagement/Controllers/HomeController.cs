using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    //[Route("Home")]
    //[Route("[controller]")] - Token rounting which match with Controller name
    //[Route("[controller]/[action]") - Token routing with match controller and action
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // [Route("/")]
         //[Route("Index")]
        //[Route("[action]")]  - Token routing which match with method name
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

       
        //[Route("[action]/{id?}")]
        public ViewResult Details(int? Id)
        {

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(Id??1),
                PageTitle = "Employee Details"
            };

            //Passing data to the view by ViewData (Loosely couple data binding)
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";

            //Passing data to the view by ViewBag
            //ViewBag.PageTitle = "Employee Details";


            return View(homeDetailsViewModel);
        }
    }
}
