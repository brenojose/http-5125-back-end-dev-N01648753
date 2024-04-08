using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using C1_SchoolProject.Models;
using System.Diagnostics;

namespace C1_SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        //<summary>
        // Displays the default view for the Teacher controller.
        //</summary>
        //<returns>The default view.</returns>
        public ActionResult Index()
        {
            return View();
        }

        //<summary>
        // Retrieves a list of all teachers and displays them.
        //</summary>
        //<returns>The view containing the list of teachers.</returns>
        public ActionResult List()
        {
            // Initializes a TeacherDataController instance to access teacher data.
            TeacherDataController controller = new TeacherDataController();
            // Retrieves the list of teachers.
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            // Returns the view with the list of teachers.
            return View(Teachers);
        }

        //<summary>
        // Retrieves and displays details of a specific teacher.
        //</summary>
        //<param name="id">The ID of the teacher to display.</param>
        //<returns>The view containing details of the specified teacher.</returns>
        public ActionResult Show(int id)
        {
            // Initializes a TeacherDataController instance to access teacher data.
            TeacherDataController controller = new TeacherDataController();
            // Retrieves details of the specified teacher.
            Teacher NewTeacher = controller.FindTeacher(id);
            // Returns the view with details of the specified teacher.
            return View(NewTeacher);
        }

        //<summary>
        // Searches for teachers based on specified criteria and displays the results.
        //</summary>
        //<param name="searchName">The name of the teacher to search for.</param>
        //<param name="searchHireDate">The hire date of the teacher to search for.</param>
        //<param name="searchSalary">The salary of the teacher to search for.</param>
        //<returns>The view containing the search results.</returns>
        public ActionResult Search(string searchName, DateTime? searchHireDate, decimal? searchSalary)
        {
            // Initializes a TeacherDataController instance to access teacher data.
            TeacherDataController controller = new TeacherDataController();
            // Searches for teachers based on specified criteria.
            IEnumerable<Teacher> Teachers = controller.SearchTeachers(searchName, searchHireDate, searchSalary);
            // Returns the view with the search results.
            return View(Teachers);
        }

        public ActionResult New()
        {
            //Navigate to Views/Teacher/New.cshtml
            return View();
        }
        [HttpPost]
        // <summary>
        // Creates a new teacher record in the database.
        // This action is triggered by submitting the teacher creation form.
        // </summary>
        // <param name="newTeacher">The teacher data collected from the form, automatically bound to a Teacher object.</param>
        // <returns>Redirects to the List action to display all teachers.</returns>
        public ActionResult Create(Teacher newTeacher) // Use model binding for cleaner code
        {
            // Logs the method invocation to the debug output for troubleshooting.
            Debug.WriteLine("Teacher Create method");
            // Logs the received teacher information to verify if data binding is correct.
            Debug.WriteLine("Received teacher info: ");
            Debug.WriteLine($"First Name: {newTeacher.TeacherFname}");
            Debug.WriteLine($"Last Name: {newTeacher.TeacherLname}");
            Debug.WriteLine($"Employee Number: {newTeacher.EmployeeNumber}");
            Debug.WriteLine($"Hire Date: {newTeacher.HireDate.ToString()}");
            Debug.WriteLine($"Salary: {newTeacher.Salary.ToString()}");

            // Initializes a new instance of TeacherDataController to interact with the database.
            TeacherDataController TeacherController = new TeacherDataController();

            // Prepares the new teacher object to be inserted into the database.
            Teacher newTeacherToInsert = new Teacher
            {
                TeacherFname = newTeacher.TeacherFname,
                TeacherLname = newTeacher.TeacherLname,
                EmployeeNumber = newTeacher.EmployeeNumber,
                HireDate = newTeacher.HireDate,
                Salary = newTeacher.Salary
            };

            // Inserts the new teacher record into the database.
            TeacherController.AddTeacher(newTeacherToInsert);

            // Redirects to the List action to show the updated list of teachers.
            return RedirectToAction("List");
        }

        // <summary>
        // Displays a confirmation page for deleting a teacher.
        // </summary>
        // <param name="id">The ID of the teacher to delete.</param>
        // <returns>The confirmation view with the selected teacher's details.</returns>
        public ActionResult DeleteConfirm(int id)
        {
            // Initializes a new instance of TeacherDataController for database operations.
            TeacherDataController TeacherController = new TeacherDataController();

            // Retrieves the details of the teacher to be deleted.
            Teacher SelectedTeacher = TeacherController.FindTeacher(id);
            // Returns the view for delete confirmation, passing in the selected teacher.
            return View(SelectedTeacher);
        }

        // <summary>
        // Deletes the specified teacher record from the database.
        // </summary>
        // <param name="id">The ID of the teacher to delete.</param>
        // <returns>Redirects to the List action to display the current list of teachers.</returns>
        public ActionResult Delete(int id)
        {
            // Initializes a new instance of TeacherDataController for database operations.
            TeacherDataController TeacherController = new TeacherDataController();

            // Performs the delete operation for the specified teacher.
            TeacherController.DeleteTeacher(id);

            // Redirects to the List action to show the updated list of teachers after deletion.
            return RedirectToAction("List");
        }
    }
}