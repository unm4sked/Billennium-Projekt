using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Praktyki.Models;

namespace Praktyki.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Register(InternModel i)
        {   
            // check empty fields
            if(String.IsNullOrEmpty(i.FirstName) || String.IsNullOrEmpty(i.LastName) || String.IsNullOrEmpty(i.About))
            {
                ViewData["Message"]="You must complete all fields";
                return View("Index");
            }
            // check First, Last name not number or special character
            if(!Regex.IsMatch(i.FirstName, @"^[a-zA-Z]+$") || !Regex.IsMatch(i.LastName, @"^[a-zA-Z]+$"))
            {
                ViewData["Message"]="You must use only letters to FirstName, LastName fields";
                return View("Index");
            }
            // check First, Last name in capital letters
            if(!Regex.IsMatch(i.FirstName, @"^[A-Z][a-z]+$") || !Regex.IsMatch(i.LastName, @"^[A-Z][a-z]+$"))
            {
                ViewData["Message"]="Name and surname begin with uppercase letters";
                return View("Index");
            }
            if(i.FirstName.Length <= 3)
            {
                ViewData["Message"]="Enter the correct data, First Name must be greater than 3";
                return View("Index");
            }
            return View(i);
        }
        public IActionResult StopIt()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
