using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Web;

namespace SportsProLibrary
{
    public static class DBUtl
    {
        public static string GetConnString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["dbTechSupport"].ConnectionString;
        }
        public static SqlConnection GetDbConnection()
        {
            return new SqlConnection(GetConnString());
        }
        public static DataTable SQLSelect(SqlCommand cmdSQLQuery)
        {
            using (cmdSQLQuery)
            {
                if (cmdSQLQuery.Connection == null)
                {
                    cmdSQLQuery.Connection = GetDbConnection();
                }
                DataSet dsPageInfo = new DataSet();
                SqlDataAdapter daPageInfo = new SqlDataAdapter(cmdSQLQuery);
                cmdSQLQuery.Connection.Open();
                daPageInfo.Fill(dsPageInfo);
                cmdSQLQuery.Connection.Close();
                return dsPageInfo.Tables[0];
            }
        }

        public static int ExecuteSQLCommand(SqlCommand CommandToExecute)
        {
            using (CommandToExecute)
            {
                if (CommandToExecute.Connection == null)
                {
                    CommandToExecute.Connection = GetDbConnection();
                }
                CommandToExecute.Connection.Open();
                int result = CommandToExecute.ExecuteNonQuery();
                CommandToExecute.Connection.Close();
                return result;
            }

        }
        private static bool IsSimple(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal))
              || type.Equals(typeof(DateTime));
        }

        private static string ErrorHandler(Exception ex)
        {
            //if (ex.GetType() == typeof(SqlException))
            //{
            //    return string.Format("{0}:{1}", ((SqlException)ex).Number, ex.Message);
            //}
            //else
            //{
            //    return string.Format("{0}:{1}", ex.Source, ex.Message);
            //}
            throw ex;
        }

        public static Dictionary<string, object> SqlColumns(object entity)
        {
            Dictionary<string, object> Cols = new Dictionary<string, object>();
            var objectType = entity.GetType();
            var properties = objectType.GetProperties();



            foreach (var prop in properties)
            {
                if (IsSimple(prop.PropertyType))
                {
                    if (prop.GetCustomAttributes(false).Where(y => y is ColumnAttribute).Select(x => (x as ColumnAttribute)).FirstOrDefault().IsPrimaryKey)
                    {
                        continue;
                    }
                    if (prop.PropertyType.IsPublic)
                    {
                        var dbColName = prop.GetCustomAttributes(false).Where(y => y is ColumnAttribute).Select(x => (x as ColumnAttribute).Name).FirstOrDefault();
                        Cols.Add(dbColName, prop.GetValue(entity, null));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return Cols;
        }

        public static string INSERT(object entity)
        {
            Dictionary<string, object> Cols = SqlColumns(entity);
            try
            {
                var objectType = entity.GetType();
                var properties = objectType.GetProperties();
                var tableName = objectType.GetCustomAttributes(false).Select(x => (x as TableAttribute).Name).FirstOrDefault();

                using (SqlConnection conn = GetDbConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO {0} ({1})VALUES({2})", tableName, string.Join(",", Cols.Keys), string.Join(",", Cols.Keys.Select(x => x.Insert(0, "@")).ToArray()));
                        foreach (var col in Cols)
                        {
                            cmd.Parameters.AddWithValue("@" + col.Key, col.Value ?? DBNull.Value);
                        }
                        return ExecuteSQLCommand(cmd).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ErrorHandler(ex);
            }
        }
        public static string UPDATE(object entity)
        {
            Dictionary<string, object> Cols = SqlColumns(entity);
            try
            {
                var objectType = entity.GetType();
                var properties = objectType.GetProperties();
                var tableName = objectType.GetCustomAttributes(false).Select(x => (x as TableAttribute).Name).FirstOrDefault();
                var PrimaryField = properties.FirstOrDefault(x => x.GetCustomAttributes(false).Where(y => y is ColumnAttribute).Select(z => (z as ColumnAttribute)).FirstOrDefault().IsPrimaryKey);

                using (SqlConnection conn = GetDbConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE {0} SET {1} WHERE {2} = @{2}", tableName, string.Join(",", Cols.Select(x => x.Key + " = @" + x.Key).ToArray()), PrimaryField.Name);


                        foreach (var col in Cols)
                        {
                            cmd.Parameters.AddWithValue("@" + col.Key, col.Value ?? DBNull.Value);
                        }
                        cmd.Parameters.AddWithValue("@" + PrimaryField.Name, PrimaryField.GetValue(entity) ?? DBNull.Value);
                        return ExecuteSQLCommand(cmd).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ErrorHandler(ex);
            }
        }
        public static string DELETE(object entity)
        {
            try
            {
                Dictionary<string, object> Cols = SqlColumns(entity);
                var objectType = entity.GetType();
                var properties = objectType.GetProperties();
                var tableName = objectType.GetCustomAttributes(false).Select(x => (x as TableAttribute).Name).FirstOrDefault();

                var PrimaryField = properties.FirstOrDefault(x => x.GetCustomAttributes(false).Where(y => y is ColumnAttribute).Select(z => (z as ColumnAttribute)).FirstOrDefault().IsPrimaryKey);
                using (SqlConnection conn = GetDbConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("DELETE FROM {0} WHERE {1} = {2}", tableName, PrimaryField.Name, "@" + PrimaryField.Name);
                        cmd.Parameters.AddWithValue("@" + PrimaryField.Name, PrimaryField.GetValue(entity) ?? DBNull.Value);
                        return ExecuteSQLCommand(cmd).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ErrorHandler(ex);
            }
        }


    }
}
