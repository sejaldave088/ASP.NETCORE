using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : BaseAdo, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (ApplicantProfilePoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                   ([Id]
                                   ,[Login]
                                   ,[Current_Salary]
                                   ,[Current_Rate]
                                   ,[Currency]
                                   ,[Country_Code]
                                   ,[State_Province_Code]
                                   ,[Street_Address]
                                   ,[City_Town]
                                   ,[Zip_Postal_Code])
                             VALUES
                                   (@Id, 
                                   @Login, 
                                   @CurrentSalary, 
                                   @CurrentRate,
                                   @Currency, 
                                   @Country, 
                                   @Province, 
                                   @Street,
                                   @City,
                                   @PostalCode)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                cmd.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                cmd.Parameters.AddWithValue("@Currency", item.Currency);
                cmd.Parameters.AddWithValue("@Country", item.Country);
                cmd.Parameters.AddWithValue("@Province", item.Province);
                cmd.Parameters.AddWithValue("@Street", item.Street);
                cmd.Parameters.AddWithValue("@City", item.City);
                cmd.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT *
                                  FROM [dbo].[Applicant_Profiles]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];
                while (rdr.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.CurrentSalary = (Decimal?)rdr[2];
                    poco.CurrentRate = (Decimal?)rdr[3];
                    poco.Currency = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                    poco.Country = rdr.IsDBNull(5) ? null : rdr.GetString(5);
                    poco.Province = rdr.IsDBNull(6) ? null : rdr.GetString(6);
                    poco.Street = rdr.IsDBNull(7) ? null : rdr.GetString(7);
                    poco.City = rdr.IsDBNull(8) ? null : rdr.GetString(8);
                    poco.PostalCode = rdr.IsDBNull(9) ? null : rdr.GetString(9);
                    poco.TimeStamp = (byte[])rdr[10];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(p => p != null).ToList();
            }
        }


        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {

                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles]
                                        WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (var item in items)
            {

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                   SET
                                       [Login] = @Login,
                                       [Current_Salary] = @CurrentSalary,
                                       [Current_Rate] = @CurrentRate, 
                                       [Currency] = @Currency, 
                                       [Country_Code] = @Country, 
                                       [State_Province_Code] = @Province, 
                                       [Street_Address] = @Street, 
                                       [City_Town] = @City, 
                                       [Zip_Postal_Code] = @PostalCode
                                 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                cmd.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                cmd.Parameters.AddWithValue("@Currency", item.Currency);
                cmd.Parameters.AddWithValue("@Country", item.Country);
                cmd.Parameters.AddWithValue("@Province", item.Province);
                cmd.Parameters.AddWithValue("@Street", item.Street);
                cmd.Parameters.AddWithValue("@City", item.City);
                cmd.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
