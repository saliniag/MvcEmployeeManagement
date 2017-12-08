using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        // access the data
        public IEnumerable<Employee> Employees{
            get
            {
                string connString = ConfigurationManager.ConnectionStrings["Dbcs"].ConnectionString;
                //list to store all the data
                List<Employee> employees = new List<Employee>();
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    SqlCommand cmd = new SqlCommand("spGetAllEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                   
                    while (rdr.Read())
                    {
                        Employee current = new Employee();
                        current.Id = Convert.ToInt32(rdr["Id"]);
                        current.FirstName = rdr["FirstName"].ToString();
                        current.LastName = rdr["LastName"].ToString();
                        current.City = rdr["City"].ToString();
                        employees.Add(current);
                    }
                }
                return employees;
            }
        }
//method to submit the data in the database to create a new employee
        public void AddNewEmployee(Employee emp)
        {
            //get the connectionString
            string connString = ConfigurationManager.ConnectionStrings["Dbcs"].ConnectionString;
            //spAddNewEmployee is procedure to add a new employee
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spAddNewEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //add the parameters
                //SqlParameter paraId = new SqlParameter();
                //paraId.ParameterName= "@Id";
                //paraId.Value = emp.Id;
                //cmd.Parameters.Add(paraId);
                SqlParameter paraFirst = new SqlParameter();
                paraFirst.ParameterName = "@FirstName";
                paraFirst.Value = emp.FirstName;
                cmd.Parameters.Add(paraFirst);
                SqlParameter paraLast = new SqlParameter();
                paraLast.ParameterName = "@LastName";
                paraLast.Value = emp.LastName;
                cmd.Parameters.Add(paraLast);
                SqlParameter paraCity = new SqlParameter();
                paraCity.ParameterName = "@City";
                paraCity.Value = emp.City;
                cmd.Parameters.Add(paraCity);

                //open the connection
                conn.Open();
                //execute the query
                cmd.ExecuteNonQuery();
                

                
            }
        }

        //method to update the employee data
        public void saveUpdatedEmployeeInfo(Employee emp)
        {
            //get the connectionString
            string connString = ConfigurationManager.ConnectionStrings["Dbcs"].ConnectionString;
            //spAddNewEmployee is procedure to add a new employee
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //add the parameters
                SqlParameter paraId = new SqlParameter();
                paraId.ParameterName = "@Id";
                paraId.Value = emp.Id;
                cmd.Parameters.Add(paraId);
                SqlParameter paraFirst = new SqlParameter();
                paraFirst.ParameterName = "@FirstName";
                paraFirst.Value = emp.FirstName;
                cmd.Parameters.Add(paraFirst);
                SqlParameter paraLast = new SqlParameter();
                paraLast.ParameterName = "@LastName";
                paraLast.Value = emp.LastName;
                cmd.Parameters.Add(paraLast);
                SqlParameter paraCity = new SqlParameter();
                paraCity.ParameterName = "@City";
                paraCity.Value = emp.City;
                cmd.Parameters.Add(paraCity);

                //open the connection
                conn.Open();
                //execute the query
                cmd.ExecuteNonQuery();



            }

        }

        //method to delete the record

        public void DeleteEmployee(int id)
        {
             //get the connectionString
            string connString = ConfigurationManager.ConnectionStrings["Dbcs"].ConnectionString;
            //spAddNewEmployee is procedure to add a new employee
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // add the parameters
                SqlParameter paraId = new SqlParameter();
                paraId.ParameterName = "@Id";
                paraId.Value = id;
                cmd.Parameters.Add(paraId);
                //open connection
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
