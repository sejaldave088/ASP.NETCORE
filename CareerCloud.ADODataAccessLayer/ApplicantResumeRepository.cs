using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository : BaseAdo, IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (ApplicantResumePoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                   ([Id]
                                   ,[Applicant]
                                   ,[Resume]
                                   ,[Last_Updated])
                             VALUES
                                   (@Id,
                                   @Applicant,
                                   @Resume,
                                   @LastUpdated)";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Resume", item.Resume);
                cmd.Parameters.AddWithValue("@LastUpdated", item.LastUpdated);
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT  *
                                FROM [dbo].[Applicant_Resumes]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                ApplicantResumePoco[] pocos = new ApplicantResumePoco[1000];
                while (rdr.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Resume = rdr.GetString(2);
                    poco.LastUpdated = rdr.IsDBNull(3) ?
                       (DateTime?)null : rdr.GetDateTime(3);
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(p => p != null).ToList();
            }

        }


        public IList<ApplicantResumePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantResumePoco item in items)
                {

                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes]
                                         WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (var item in items)
            {

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                   SET 
                                      [Applicant] = @Applicant, 
                                      [Resume] = @Resume, 
                                      [Last_Updated] = @LastUpdated 
                                 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Resume", item.Resume);
                cmd.Parameters.AddWithValue("@LastUpdated", item.LastUpdated);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}
