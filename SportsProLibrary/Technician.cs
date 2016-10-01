using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SportsProLibrary
{
    public class Technicians
    {
        private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbTechSupport"].ConnectionString;
        public Technicians()
        {

        }
        public int GetNextUniqueID()
        {
            int _returnID = -1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = "Select Top 1 TechID from Technicians ORDER BY TechID DESC";
                    cmd.Connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _returnID = Convert.ToInt32(reader["TechID"]);
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            _returnID++;

            return _returnID;
        }
        public List<oTechnician> GetTechnicians(int _TechnicianId = -1)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();

            List<oTechnician> _Technicians = new List<oTechnician>();

            sql.Append("SELECT * FROM Technicians");
            sql.Append(" WHERE (TechID = @TechID OR @TechID = -1)");
            sql.Append(" ORDER BY Name ASC");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@TechID", _TechnicianId));
                    cmd.Connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var oTechnician = new oTechnician();
                            oTechnician.TechID = Convert.ToInt32(reader["TechID"]);
                            oTechnician.Name = reader["Name"].ToString();
                            oTechnician.Email = reader["Email"].ToString();
                            oTechnician.Phone = reader["Phone"].ToString();
                            _Technicians.Add(oTechnician);
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            return _Technicians;
        }

        public void RemoveTechnician(int _TechnicianId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = "DELETE FROM Technicians WHERE TechID = @TechnicianId";
                    cmd.Parameters.Add(new SqlParameter("@TechnicianId", _TechnicianId));

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
        }
        public void UpdateTechnician(oTechnician _oTechnician)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                System.Text.StringBuilder sql = new System.Text.StringBuilder();
                Type _type = typeof(oTechnician);
                System.Reflection.PropertyInfo[] propInfo = _type.GetProperties();

                foreach (var pi in propInfo)
                {
                    if (pi.Name != "TechID")
                    {
                        if (sql.ToString() == string.Empty)
                        {
                            sql.AppendFormat("UPDATE {0} SET {1} = {2}", "Technicians", pi.Name, "@" + pi.Name);
                        }
                        else
                        {
                            sql.AppendFormat(", {0} = {1}", pi.Name, "@" + pi.Name);
                        }
                    }

                    cmd.Parameters.Add(new SqlParameter("@" + pi.Name, pi.GetValue(_oTechnician)));

                }
                sql.Append(" WHERE TechID = @TechID");

                using (cmd.Connection)
                {
                    cmd.Connection.Open();
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }

        }
        public void AddTechnician(oTechnician _oTechnician)
        {
            _oTechnician.TechID = this.GetNextUniqueID();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                System.Text.StringBuilder sql = new System.Text.StringBuilder();
                Type _type = typeof(oTechnician);
                System.Reflection.PropertyInfo[] propInfo = _type.GetProperties();

                sql.Append("INSERT INTO Technicians (?C?)VALUES(?V?)");

                foreach (var pi in propInfo)
                {
                    if (pi.Name != "TechID")
                    {
                        if (cmd.Parameters.Count == 0)
                        {
                            sql.Replace("?C?", pi.Name + "?C?");
                            sql.Replace("?V?", "@" + pi.Name + "?V?");
                        }
                        else
                        {
                            sql.Replace("?C?", ", " + pi.Name + "?C?");
                            sql.Replace("?V?", ", @" + pi.Name + "?V?");
                        }
                        cmd.Parameters.Add(new SqlParameter("@" + pi.Name, pi.GetValue(_oTechnician)));
                    }
                }
                sql.Replace("?C?", "");
                sql.Replace("?V?", "");

                using (cmd.Connection)
                {
                    cmd.Connection.Open();
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
        }
    }
    public class oTechnician
    {
        public int TechID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public oTechnician()
        {

        }

        public void Delete()
        {
            Technicians _techs = new Technicians();
            _techs.RemoveTechnician(this.TechID);
        }
        public void Add()
        {
            Technicians _techs = new Technicians();
            _techs.AddTechnician(this);
        }

        public void Save()
        {
            Technicians _techs = new Technicians();
            _techs.UpdateTechnician(this);
        }

    }

}
