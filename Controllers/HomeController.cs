using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Praktyki.Models;

namespace Praktyki.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;

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
            if(!Regex.IsMatch(i.FirstName, @"^[a-zżźćńółęąśA-ZŻŹĆĄŚĘŁÓŃ]+$") || !Regex.IsMatch(i.LastName, @"^[a-zżźćńółęąśA-ZŻŹĆĄŚĘŁÓŃ]+$"))
            {
                ViewData["Message"]="You must use only letters to FirstName, LastName fields";
                return View("Index");
            }
            // check First, Last name in capital letters
            if(!Regex.IsMatch(i.FirstName, @"^[A-ZŻŹĆĄŚĘŁÓŃ][a-zżźćńółęąś]+$") || !Regex.IsMatch(i.LastName, @"^[A-ZŻŹĆĄŚĘŁÓŃ][a-zżźćńółęąś]+$"))
            {
                ViewData["Message"]="Name and surname begin with uppercase letters";
                return View("Index");
            }
            if(i.FirstName.Length < 3)
            {
                ViewData["Message"]="Enter the correct data, First Name must be greater than 3";
                return View("Index");
            }
            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS01;Database=master;Trusted_Connection=True;";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                //SqlCommand cmd = new SqlCommand("CREATE TABLE Interns1 ( ID int NOT NULL IDENTITY (1,1) PRIMARY KEY,LastName varchar(255) NOT NULL, FirstName varchar(255), About varchar(255)); ", connection);
                string querry = "INSERT INTO Interns1 (LastName,FirstName,About) VALUES (@LastName,@FirstName,@About)";
                SqlCommand cmd = new SqlCommand(querry, connection);
                cmd.Parameters.AddWithValue("@LastName", i.LastName);
                cmd.Parameters.AddWithValue("@FirstName", i.FirstName);
                cmd.Parameters.AddWithValue("@About",i.About);
                int x = cmd.ExecuteNonQuery();
                if (x < 0)
                {
                    ViewData["TotalData"] = "Error inserting data into Database!";
                }
                connection.Close();
                ViewData["Isok"] = true;
                ViewData["TotalData"] = "Your data has been correctly added to the database";
            }
            catch(Exception ex)
            {
                ViewData["TotalData"] = ex.Message;
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
