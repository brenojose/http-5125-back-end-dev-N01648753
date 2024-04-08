using C1_SchoolProject.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C1_SchoolProject.Models
{
    //<summary>
    // Represents a teacher in the school project, encapsulating details such as
    // name, employee number, hire date, and salary. This class also includes data
    // annotations for validation purposes, ensuring that all necessary properties
    // are provided and correctly formatted before a teacher object can be successfully
    // created or modified.
    //</summary>
    public class Teacher
    {
        //<summary>
        // Gets or sets the ID of the teacher. This property serves as the unique identifier
        // for teachers within the system.
        //</summary>
        public int TeacherId { get; set; }

        //<summary>
        // Gets or sets the first name of the teacher. This property is required to ensure
        // that each teacher has a first name specified. The [Required] attribute enforces
        // this validation rule, and provides a custom error message if the first name is not provided.
        // I used the same method for the others, the explanation of the C# validation is the same from bellow.
        //</summary>
        [Required(ErrorMessage = "First name is required")]
        public string TeacherFname { get; set; }

        //<summary>
        // Gets or sets the last name of the teacher.
        //</summary>
        [Required(ErrorMessage = "Last name is required")]
        public string TeacherLname { get; set; }

        //<summary>
        // Gets or sets the employee number of the teacher.
        //</summary>
        [Required(ErrorMessage = "Employee number is required")]
        public string EmployeeNumber { get; set; }

        //<summary>
        // Gets or sets the hire date of the teacher.
        //</summary>
        [Required(ErrorMessage = "Hire date is required")]
        public DateTime HireDate { get; set; }

        //<summary>
        // Gets or sets the salary of the teacher.
        //</summary>
        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }

    }
}
