using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using C1_SchoolProject.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Description;

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
        public IEnumerable<Teacher> SearchTeachers(string name = null, DateTime? hireDate = null, decimal? minSalary = null, decimal? maxSalary = null)
        {
            // Establishes a connection to the school database.
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();

            // Constructs the SQL query to search for teachers based on provided criteria.
            string query = @"
            SELECT * FROM Teachers
            WHERE (@name IS NULL OR CONCAT(teacherfname, ' ', teacherlname) LIKE CONCAT('%', @name, '%'))
            AND (@hireDate IS NULL OR DATE_FORMAT(hiredate, '%Y-%m-%d') = DATE_FORMAT(@hireDate, '%Y-%m-%d'))";

            // Extends the query to include filtering by salary range if both minimum and maximum salaries are provided.
            if (minSalary.HasValue && maxSalary.HasValue)
            {
                query += " AND (salary BETWEEN @minSalary AND @maxSalary)";
            }
            else if (minSalary.HasValue)
            {
                query += " AND (salary >= @minSalary)";
            }
            else if (maxSalary.HasValue)
            {
                query += " AND (salary <= @maxSalary)";
            }

            // Prepares the command with the constructed query and binds parameter values.
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@name", name ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@hireDate", hireDate.HasValue ? hireDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@minSalary", minSalary ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@maxSalary", maxSalary ?? (object)DBNull.Value);
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> Teachers = new List<Teacher>();
            // Iterates through the result set to create Teacher objects for each row.
            while (ResultSet.Read())
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
            // Closes the database connection after the operation.
            Conn.Close();
            return Teachers;
        }

        /// <summary>
        /// Receives teacher information and enters it into the database.
        /// This action is accessed through an API call and expects teacher data in the request body.
        /// </summary>
        /// <param name="NewTeacher">The teacher object containing information to be inserted.</param>
        /// <returns>A success response upon successful insertion of teacher data.</returns>
        /// <example>
        /// POST api/TeacherData/AddTeacher with teacher data in request body.
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        public IHttpActionResult AddTeacher([FromBody] Teacher NewTeacher)
        {
            // Opens a connection to the database.
            using (MySqlConnection Conn = School.AccessDatabase())
            {
                Conn.Open();
                string query = @"
                INSERT INTO Teachers (TeacherFname, TeacherLname, EmployeeNumber, HireDate, Salary) 
                VALUES (@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";
                MySqlCommand Cmd = Conn.CreateCommand();
                // Binds the parameters with the received teacher information.
                Cmd.CommandText = query;
                Cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
                Cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
                Cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
                Cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
                Cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
                // Executes the command to insert the new teacher.
                Cmd.ExecuteNonQuery();
                // Closes the connection after completion.
                Conn.Close();
                return Ok();
            }
        }

        /// <summary>
        /// Deletes a teacher record from the database based on the provided teacher ID.
        /// This action is accessed through an API call.
        /// </summary>
        /// <param name="TeacherId">The ID of the teacher to be deleted.</param>
        /// <returns>A success response upon successful deletion of the teacher record.</returns>
        /// <example>
        /// POST api/TeacherData/DeleteTeacher/{TeacherId}
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{TeacherId}")]
        public IHttpActionResult DeleteTeacher(int TeacherId)
        {
            // Opens a connection to the database.
            using (MySqlConnection Conn = School.AccessDatabase())
            {
                Conn.Open();
                string query = "DELETE FROM Teachers WHERE teacherid = @TeacherId";
                MySqlCommand Cmd = Conn.CreateCommand();
                // Binds the teacher ID parameter to the query.
                Cmd.CommandText = query;
                Cmd.Parameters.AddWithValue("@TeacherId", TeacherId);
                // Executes the command to delete the specified teacher.
                Cmd.ExecuteNonQuery();
                // Closes the connection after completion.
                Conn.Close();
                return Ok();
            }
        }
        public IHttpActionResult UpdateTeacher(Teacher updatedTeacher)
        {

            using (MySqlConnection Conn = School.AccessDatabase())
            {
                Conn.Open();
                string query = @"
            UPDATE Teachers
            SET
                TeacherFname = @TeacherFname,
                TeacherLname = @TeacherLname,
                EmployeeNumber = @EmployeeNumber,
                HireDate = @HireDate,
                Salary = @Salary
            WHERE TeacherId = @TeacherId";

                MySqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = query;
                Cmd.Parameters.AddWithValue("@TeacherId", updatedTeacher.TeacherId);
                Cmd.Parameters.AddWithValue("@TeacherFname", updatedTeacher.TeacherFname);
                Cmd.Parameters.AddWithValue("@TeacherLname", updatedTeacher.TeacherLname);
                Cmd.Parameters.AddWithValue("@EmployeeNumber", updatedTeacher.EmployeeNumber);
                Cmd.Parameters.AddWithValue("@HireDate", updatedTeacher.HireDate);
                Cmd.Parameters.AddWithValue("@Salary", updatedTeacher.Salary);

                Cmd.ExecuteNonQuery();
                Conn.Close();
                return Ok();
            }

        }
    }
}