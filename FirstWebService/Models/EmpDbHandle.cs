using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FirstWebService.Models
{
    public class EmpDbHandle
    {
        private MySqlConnection con;
        private void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["EmployeeContext"].ToString();
            con = new MySqlConnection(constring);
        }
        public List<Employee> GetAll()
        {
            Connection();
            List<Employee> EmployeeList = new List<Employee>();

            MySqlCommand cmd = new MySqlCommand("GetAllEmployees", con); 
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                EmployeeList.Add(
                    new Employee
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"])
                    });
            }
            return EmployeeList;
        }
        public bool UpdateEmp(Employee smodel , int id)
        {
            Connection();
            MySqlCommand cmd = new MySqlCommand("UpdateEmp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@empId",id);
            cmd.Parameters.AddWithValue("@fname", smodel.FirstName);
            cmd.Parameters.AddWithValue("@pname", smodel.LastName);
            

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool AddEmp(Employee smodel)
        {
            Connection();
            MySqlCommand cmd = new MySqlCommand("CreateEmp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@empId", smodel.ID);
            cmd.Parameters.AddWithValue("@fname", smodel.FirstName);
            cmd.Parameters.AddWithValue("@pname", smodel.LastName);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeleteEmp(int id)
        {
            Connection();
            MySqlCommand cmd = new MySqlCommand("DELETEemp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@empId", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}