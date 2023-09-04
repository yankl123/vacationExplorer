using Dache.DP;
using Microsoft.Data.SqlClient;
using System; 
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Dache.DAL
{
    public class SqueryDB
    {

        
        private static string ConStr = @"workstation id=VacationDB.mssql.somee.com;packet size=4096;user id=BEILABOIM_SQLLogin_1;pwd=hwamyro7cr;data source=VacationDB.mssql.somee.com;persist security info=False;initial catalog=VacationDB";

        public static void Insert<T>(T RegisterData, string Table )
        {
                 
            using (SqlConnection con = new SqlConnection(ConStr))
            {    
                StringBuilder Colums = new StringBuilder();
                StringBuilder Values = new StringBuilder();

                var Props = RegisterData.GetType().GetProperties();
                var test = Props[0].Name;
                int len = Props.Length;
             
                for (int i = 2; i < len ; i++)
                {
                    var c = Props[i].Name;
                    var v = Props[i].GetValue(RegisterData, null);

                    if (v != null) 
                    {
                        Colums.Append(c + ",");
                        Values.Append($"@{c},");
                    }
                }

                string query = string.Empty;

                        Colums = Colums.Remove(Colums.Length - 1, 1);
                        Values = Values.Remove(Values.Length - 1, 1);
                        query = $"INSERT INTO {Table} ({Colums}) VALUES ({Values})";  
               
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    for (int i = 2; i < len ; i++)
                    {
                        var c = Props[i].Name;
                        var v = Props[i].GetValue(RegisterData, null);
                        if (v != null) { cmd.Parameters.AddWithValue($"@{c}", v); }
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public static void Update<T>(T Canges , string Table ,string filter)

        {
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                StringBuilder UpdateString = new StringBuilder();
    

                var Props = Canges.GetType().GetProperties();
                var test = Props[0].Name;
                int len = Props.Length;

                for (int i = 2; i < len -2; i++)
                {
                    var c = Props[i].Name;
                    var v = Props[i].GetValue(Canges, null);
                    if ((string)v != "null")
                    {
                        UpdateString.Append($"{c} = @{c},");
                    }
                }

                string query = string.Empty;

                UpdateString = UpdateString.Remove(UpdateString.Length - 1, 1);
              
                query = $"UPDATE {Table} SET {UpdateString} {filter}";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    for (int i = 2; i < len -2; i++)
                    {
                        var c = Props[i].Name;
                        var v = Props[i].GetValue(Canges, null);
                        if ((string)v != "null") { cmd.Parameters.AddWithValue($"@{c}", v); }
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

        }

        public static bool CheckUsername(Login credentials)
        {
            bool exist;
            
            using (SqlConnection con = new SqlConnection(ConStr))
            { 
                string query = "SELECT UserName FROM Supplier WHERE UserName = @UserName " + ((!String.IsNullOrEmpty(credentials.Password)) ? "AND Password = @Password" : "");
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@UserName", credentials.UserName);
                    if (!String.IsNullOrEmpty(credentials.Password)) { cmd.Parameters.AddWithValue("@Password", credentials.Password) ;};
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                            exist = true;
                        else
                            exist = false;
                    }
                   
                }
                con.Close();
            }
            return exist;
        }

        public static UserHeader FindUser(string UserName ,string Password)
        {
            UserHeader userHeader = new UserHeader();
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                string query = "SELECT Id , Name , LogoUrl ,lastAccsesTime FROM Supplier WHERE UserName=@UserName AND Password=@Password";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userHeader.Id = Convert.ToInt32(reader["Id"]);
                            userHeader.Name = reader["Name"].ToString();
                            userHeader.LogoUrl = reader["LogoUrl"].ToString();
                            userHeader.lastAccsesTime = Convert.ToDateTime(reader["lastAccsesTime"]).ToString("HH:mm dd-MM-yyyy");
                        }
                    }
                    DateTime Dt = DateTime.Now;
                    cmd.Parameters.AddWithValue("@Dt", Dt);
                    string UPdate = $"UPDATE Supplier SET lastAccsesTime = @Dt WHERE Id = {userHeader.Id}";
                    cmd.CommandText = UPdate;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return userHeader;
        }

       

        public static List<Service> GetInfo(string filter)
        {
            List<Service> result = new List<Service>();

            using (SqlConnection con = new SqlConnection(ConStr))
            {
                string query = "SELECT * FROM Service "+filter;
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Service
                            {
                                Id = Convert.ToInt32(reader[0]),
                                Name = reader[1].ToString(),
                                Catrgory = reader[2].ToString(),
                                Head = reader[3].ToString(),
                                Description = reader[4].ToString(),
                                Area = reader[5].ToString(),
                                Addrese = reader[6].ToString(),
                                Phone = reader[7].ToString(),
                                MorDetiles = reader[8].ToString(),
                                ImgUrl = reader[9].ToString(),
                                SupplierID = Convert.ToInt32(reader[10].ToString()),
                                SupplierLogoUrl = reader[11].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return result;
        }
        public static string Delete(string Table ,int id )
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                try
                {
                    string query;
                    query = $"DELETE FROM {Table} WHERE Id =@id";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue($"@Id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    return "OK";
                }
                catch (Exception)
                {
                    return "NOTOK";
                    throw;
                }      
            }
        }

    }
}
