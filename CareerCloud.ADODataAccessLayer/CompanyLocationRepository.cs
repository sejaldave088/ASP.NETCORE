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
    public class CompanyLocationRepository : BaseAdo, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (CompanyLocationPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                   ([Id]
                                   ,[Company]
                                   ,[Country_Code]
                                   ,[State_Province_Code]
                                   ,[Street_Address]
                                   ,[City_Town]
                                   ,[Zip_Postal_Code])
                             VALUES
                                   (@Id, 
                                    @Company, 
                                    @Country_Code, 
                                    @Province,
                                    @Street,
                                    @City,
                                    @PostalCode)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Locations]";
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int counter = 0;
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[5000];
            while (rdr.Read())
            {
                CompanyLocationPoco poco = new CompanyLocationPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Company = rdr.GetGuid(1);
                poco.CountryCode = rdr.IsDBNull(2) ? null : rdr.GetString(2);
                poco.Province = rdr.IsDBNull(3) ? null : rdr.GetString(3);
                poco.Street = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                poco.City = rdr.IsDBNull(5) ? null : rdr.GetString(5);
                poco.PostalCode = rdr.IsDBNull(6) ? null : rdr.GetString(6);
                poco.TimeStamp = (byte[])rdr[7];
                pocos[counter] = poco;
                counter++;
            }

            conn.Close();
            return pocos.Where(p => p != null).ToList();

        }


        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (CompanyLocationPoco item in items)
            {

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Locations]
                                     WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (var item in items)
            {

                cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
                                   SET 
                                      [Company] = @Company,
                                      [Country_Code] = @Country_Code,
                                      [State_Province_Code] = @Province, 
                                      [Street_Address] = @Street, 
                                      [City_Town] = @City, 
                                      [Zip_Postal_Code] = @PostalCode
                                 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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
