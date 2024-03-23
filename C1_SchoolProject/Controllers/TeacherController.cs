using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using C1_SchoolProject.Models;

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
    }
}
