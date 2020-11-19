using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : BaseAdo, IDataRepository<ApplicantEducationPoco>
    {


        public void Add(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (ApplicantEducationPoco item in items)
            {

                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                   ([Id]
                                   ,[Applicant]
                                   ,[Major]
                                   ,[Certificate_Diploma]
                                   ,[Start_Date]
                                   ,[Completion_Date]
                                   ,[Completion_Percent])
                             VALUES
                                   (@Id,
                                    @Applicant,
                                    @Major,
                                    @CertificateDiploma,
                                    @StartDate, 
                                    @CompletionDate,
                                    @CompletionPercent)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                cmd.Parameters.AddWithValue("@StartDate", item.StartDate);
                cmd.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                cmd.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }

        }


        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT *
                                  FROM [dbo].[Applicant_Educations]";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    int counter = 0;
                    ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
                    while (rdr.Read())
                    {
                        ApplicantEducationPoco poco = new ApplicantEducationPoco();
                        poco.Id = rdr.GetGuid(0);
                        poco.Applicant = rdr.GetGuid(1);
                        poco.Major = rdr.GetString(2);
                    poco.CertificateDiploma = rdr.GetString(3);
                    poco.StartDate = rdr.IsDBNull(4) ? 
                        (DateTime?) null : rdr.GetDateTime(4);
                    poco.CompletionDate = (DateTime?) rdr[5];
                    poco.CompletionPercent = (byte?)rdr[6];
                    poco.TimeStamp = (byte[])rdr[7];
                    pocos[counter]= poco;
                    counter++;
                }
               
                conn.Close();
                return pocos.Where(p => p != null).ToList();
            
        }
        

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable(); 
                return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantEducationPoco item in items)
                {

                    cmd.CommandText = @"DELETE FROM[dbo].[Applicant_Educations]
                    WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (var item in items)
                {

                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                                       SET
                                          [Applicant] = @Applicant, 
                                          [Major] = @Major,
                                          [Certificate_Diploma] = @CertificateDiploma,
                                          [Completion_Date] = @CompletionDate,
                                          [Start_Date]=@Startdate,
                                          [Completion_Percent] = @CompletionPercent
                                     WHERE [Id]=@Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@StartDate", item.StartDate);
                    cmd.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);                    
                    cmd.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

    
