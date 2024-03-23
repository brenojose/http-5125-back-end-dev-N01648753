using C1_SchoolProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C1_SchoolProject.Models
{
    //<summary>
    // Represents a teacher in the school project.
    //</summary>
    public class Teacher
    {
        //<summary>
        // Gets or sets the ID of the teacher.
        //</summary>
        public int TeacherId;

        //<summary>
        // Gets or sets the first name of the teacher.
        //</summary>
        public string TeacherFname;

        //<summary>
        // Gets or sets the last name of the teacher.
        //</summary>
        public string TeacherLname;

        //<summary>
        // Gets or sets the employee number of the teacher.
        //</summary>
        public string EmployeeNumber;

        //<summary>
        // Gets or sets the hire date of the teacher.
        //</summary>
        public DateTime HireDate;

        //<summary>
        // Gets or sets the salary of the teacher.
        //</summary>
        public decimal Salary;

    }
}
