using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantSkillRepository : BaseAdo, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (ApplicantSkillPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                   ([Id]
                                   ,[Applicant]
                                   ,[Skill]
                                   ,[Skill_Level]
                                   ,[Start_Month]
                                   ,[Start_Year]
                                   ,[End_Month]
                                   ,[End_Year])
                             VALUES
                                   (@Id,
                                   @Applicant, 
                                   @Skill,
                                   @SkillLevel, 
                                   @StartMonth, 
                                   @StartYear,
                                   @EndMonth, 
                                   @EndYear)";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Skill", item.Skill);
                cmd.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                cmd.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                cmd.Parameters.AddWithValue("@StartYear", item.StartYear);
                cmd.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                cmd.Parameters.AddWithValue("@EndYear", item.EndYear);
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantSkillPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT *
                                FROM [dbo].[Applicant_Skills]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1000];
                while (rdr.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Skill = rdr.GetString(2);
                    poco.SkillLevel = rdr.GetString(3);
                    poco.StartMonth = (byte)rdr[4];
                    poco.StartYear = (Int32)rdr[5];
                    poco.EndMonth = (byte)rdr[6];
                    poco.EndYear = (Int32)rdr[7];
                    poco.TimeStamp = (byte[])rdr[8];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(p => p != null).ToList();
            }
        }
        public IList<ApplicantSkillPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (ApplicantSkillPoco item in items)
            {

                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Skills]
                                    WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (var item in items)
            {

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                   SET 
                                      [Applicant] = @Applicant,
                                      [Skill] = @Skill,
                                      [Skill_Level] = @SkillLevel,
                                      [Start_Month] = @StartMonth, 
                                      [Start_Year] = @StartYear, 
                                      [End_Month] = @EndMonth,
                                      [End_Year] = @EndYear
                                 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Skill", item.Skill);
                cmd.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                cmd.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                cmd.Parameters.AddWithValue("@StartYear", item.StartYear);
                cmd.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                cmd.Parameters.AddWithValue("@EndYear", item.EndYear);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
