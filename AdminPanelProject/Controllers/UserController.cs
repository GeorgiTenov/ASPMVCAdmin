using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanelProject.DataAccess;
using AdminPanelProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace AdminPanelProject.Controllers
{
    public class UserController : Controller
    {
        private UserContext _db;
        public UserController(UserContext db)
        {
            this._db = db;
        }
        //Index Page
        public IActionResult Index()
        {
          
            return View("Views/User/AdminPanelUsers.cshtml");
        }

        //Showing ALL USERS TO ADMIN
        public IActionResult AdminPanelUsers(User user)
        {
           
            List<User> users = _db.Users.ToList();
           
            foreach (var u in users)
            {
                if(user.Username != null && user.Password != null)
                {
                    if(u.Username != null && u.Password != null)
                    {
                        if (u.Username.Equals(user.Username) || u.EmailAddress.Equals(user.EmailAddress))
                        {
                            return View("Index");
                        }
                    }
                   
                }
               
            }
            if (user.Username != null && user.Password != null)
            {
                if (!user.Username.Equals("admin") && !user.Password.Equals("admin"))
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
            }
        
           
            List<User> endUsers = _db.Users.ToList();
            if(user.Username != null && user.Password != null)
            {
                if (user.Username.Equals("admin") && user.Password.Equals("admin"))
                {
                    return View(endUsers);
                }
            }
         
            //RETURNING USER VIEW IF NOT ADMIN
            return View("Views/User/UserPage.cshtml");

           
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        //Created Page
        public IActionResult SuccessfullCreated(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return View();
        }

        //Edit for Get Request
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            User user = _db.Users.Single(u => u.Id == id);
            return View(user);
        }

        //Edit for post request
        [HttpPost]
        public IActionResult Edit(int id,User user)
        {
            User userToChange = _db.Users.Single(u => u.Id == id);
            if (ModelState.IsValid)
            {
                if(user.Username!= null && user.Password != null)
                {
                    userToChange.Username = user.Username;
                    userToChange.Password = user.Password;
                    userToChange.EmailAddress = user.EmailAddress;
                    _db.Update(userToChange);
                    _db.SaveChangesAsync();
                    return View("Index");
                }
              
            }
          
            return View(userToChange);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            _db.Remove(_db.Users.Single(user => user.Id == id));
            _db.SaveChanges();
            return View("Index");
        }

        //Details
        public IActionResult Details(int id)
        {
            User user =_db.Users.Single(user => user.Id == id);
            if(user.Username != null && user.Password != null)
            {
                return View(user);
            }
            return View();
           
        }
    }
}