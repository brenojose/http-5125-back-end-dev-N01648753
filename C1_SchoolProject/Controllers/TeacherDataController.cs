using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using C1_SchoolProject.Models;
using MySql.Data.MySqlClient;

namespace C1_SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        //<summary>
        // Establishes a connection to the school database.
        //</summary>
        private SchoolDbContext School = new SchoolDbContext();

        //<summary>
        // Retrieves a list of all teachers from the database.
        //</summary>
        //<returns>
        // An IEnumerable collection of Teacher objects.
        //</returns>
        //<example>
        // GET api/TeacherData/ListTeachers
        //</example>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            // Opens a connection to the database.
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> Teachers = new List<Teacher>();
            while (ResultSet.Read()) // Iterates through the result set.
            {
                int teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherFname = ResultSet["teacherfname"].ToString();
                string teacherLname = ResultSet["teacherlname"].ToString();
                string employeeNumber = ResultSet["employeenumber"].ToString();
                DateTime hireDate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];
                Teacher newTeacher = new Teacher
                {
                    TeacherId = teacherId,
                    TeacherFname = teacherFname,
                    TeacherLname = teacherLname,
                    EmployeeNumber = employeeNumber,
                    HireDate = hireDate,
                    Salary = salary
                };
                Teachers.Add(newTeacher);
            }
            // Closes the database connection.
            Conn.Close();
            return Teachers;
        }

        //<summary>
        // Retrieves a specific teacher based on their ID.
        //</summary>
        //<param name="TeacherId">The ID of the teacher to retrieve.</param>
        //<returns>
        // A Teacher object representing the retrieved teacher.
        //</returns>
        //<example>
        // GET api/TeacherData/FindTeacher/1
        //</example>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Teachers WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", TeacherId);
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            Teacher teacher = new Teacher();
            if (ResultSet.Read()) // Checks if the result set contains data.
            {
                teacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                teacher.TeacherFname = ResultSet["teacherfname"].ToString();
                teacher.TeacherLname = ResultSet["teacherlname"].ToString();
                teacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                teacher.HireDate = (DateTime)ResultSet["hiredate"];
                teacher.Salary = (decimal)ResultSet["salary"];
            }
            Conn.Close(); // Closes the database connection.
            return teacher;
        }

        //<summary>
        // Searches for teachers based on optional parameters such as name, hire date, and salary.
        //</summary>
        //<param name="name">The name of the teacher to search for.</param>
        //<param name="hireDate">The hire date of the teacher to search for.</param>
        //<param name="salary">The salary of the teacher to search for.</param>
        //<returns>
        // An IEnumerable collection of Teacher objects matching the search criteria.
        //</returns>
        //<example>
        // GET api/TeacherData/SearchTeachers?name=John&hireDate=2023-01-01&salary=50000
        //</example>
        public IEnumerable<Teacher> SearchTeachers(string name = null, DateTime? hireDate = null, decimal? salary = null)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL query to search for teachers based on provided criteria.
            string query = @"
                SELECT * FROM Teachers
                WHERE (@name IS NULL OR CONCAT(teacherfname, ' ', teacherlname) LIKE CONCAT('%', @name, '%'))
                AND (@hireDate IS NULL OR DATE_FORMAT(hiredate, '%Y-%m-%d') LIKE DATE_FORMAT(@hireDate, '%Y-%m-%d'))
                AND (@salary IS NULL OR CAST(salary AS CHAR) LIKE CONCAT('%', CAST(@salary AS CHAR), '%'))";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@name", name ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@hireDate", hireDate.HasValue ? hireDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@salary", salary.HasValue ? salary.ToString() : (object)DBNull.Value);
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> Teachers = new List<Teacher>();
            while (ResultSet.Read()) // Iterates through the result set.
            {
                int teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherFname = ResultSet["teacherfname"].ToString();
                string teacherLname = ResultSet["teacherlname"].ToString();
                string employeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                Teacher newTeacher = new Teacher
                {
                    TeacherId = teacherId,
                    TeacherFname = teacherFname,
                    TeacherLname = teacherLname,
                    EmployeeNumber = employeeNumber,
                    HireDate = HireDate,
                    Salary = Salary
                };
                Teachers.Add(newTeacher);
            }
            Conn.Close(); // Closes the database connection.
            return Teachers;
        }
    }
}
