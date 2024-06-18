using CRUD_application_2.Models;
using System;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index(string searchString)
        {
            var users = from m in userlist
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.Contains(searchString) || s.Email.Contains(searchString));
            }

            return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Find the user in the userlist by the provided id
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is not found, return a HttpNotFound result
            if (user == null)
            {
                return HttpNotFound();
            }

            // If the user is found, return the view with the user object
            return View(user);
        }
 
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
 
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // Add the new user to the list
                userlist.Add(user);

                // Redirect to the Index view after adding the user
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
 
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
 
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            try
            {
                var userToUpdate = userlist.FirstOrDefault(u => u.Id == id);
                if (userToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Update properties
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                // Update other properties as needed

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
 
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var user = userlist.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    userlist.Remove(user);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
