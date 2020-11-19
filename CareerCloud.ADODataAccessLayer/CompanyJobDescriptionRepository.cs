using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobDescriptionRepository : BaseAdo, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (CompanyJobDescriptionPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                   ([Id]
                                   ,[Job]
                                   ,[Job_Name]
                                   ,[Job_Descriptions])
                             VALUES
                                   (@Id,
                                   @Job,
                                   @JobName,
                                   @JobDescriptions)";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@JobName", item.JobName);
                cmd.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions);
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();


            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
            cmd.CommandText = @"SELECT *
                                  FROM [dbo].[Company_Jobs_Descriptions]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[5000];
                while (rdr.Read())
                {
                CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.JobName = rdr.GetString(2);
                    poco.JobDescriptions = rdr.IsDBNull(3) ? null :  rdr.GetString(3);
                // poco.TimeStamp = Encoding.ASCII.GetBytes(rdr["Time_Stamp"].ToString());
                poco.TimeStamp = rdr.IsDBNull(4) ? null : (byte[])rdr[4];

                pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(p => p != null).ToList();


            }
        

        public IList<CompanyJobDescriptionPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (CompanyJobDescriptionPoco item in items)
            {

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                                    WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (var item in items)
            {

                cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                    SET
                                   [Job] = @Job,
                                   [Job_Name] = @JobName,
                                   [Job_Descriptions] = @JobDescriptions
                                    WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@JobName", item.JobName);
                cmd.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
