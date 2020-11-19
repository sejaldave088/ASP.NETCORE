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
    public class SecurityLoginRepository : BaseAdo, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (SecurityLoginPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                   ([Id]
                                   ,[Login]
                                   ,[Password]
                                   ,[Created_Date]
                                   ,[Password_Update_Date]
                                   ,[Agreement_Accepted_Date]
                                   ,[Is_Locked]
                                   ,[Is_Inactive]
                                   ,[Email_Address]
                                   ,[Phone_Number]
                                   ,[Full_Name]
                                   ,[Force_Change_Password]
                                   ,[Prefferred_Language])
                             VALUES
                                   (@Id,
                                   @Login,
                                   @Password,
                                   @Created,
                                   @PasswordUpdate,
                                   @AgreementAccepted,
                                   @IsLocked,
                                   @IsInactive,
                                   @EmailAddress,
                                   @PhoneNumber,
                                   @FullName,
                                   @ForceChangePassword,
                                   @PrefferredLanguage)";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Password", item.Password);
                cmd.Parameters.AddWithValue("@Created", item.Created);
                cmd.Parameters.AddWithValue("@PasswordUpdate", item.PasswordUpdate);
                cmd.Parameters.AddWithValue("@AgreementAccepted", item.AgreementAccepted);
                cmd.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                cmd.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@EmailAddress", item.EmailAddress);
                cmd.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                cmd.Parameters.AddWithValue("@FullName", item.FullName);
                cmd.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                cmd.Parameters.AddWithValue("@PrefferredLanguage", item.PrefferredLanguage);
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Security_Logins]";
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int counter = 0;
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[5000];
            while (rdr.Read())
            {
                SecurityLoginPoco poco = new SecurityLoginPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Login = rdr.GetString(1);
                poco.Password = rdr.GetString(2);
                poco.Created = (DateTime)rdr[3];
                poco.PasswordUpdate = rdr.IsDBNull(4) ?
                       (DateTime?)null : rdr.GetDateTime(4);
                poco.AgreementAccepted = rdr.IsDBNull(5) ?
                     (DateTime?)null : rdr.GetDateTime(5);
                poco.IsLocked = rdr.GetBoolean(6);
                poco.IsInactive = rdr.GetBoolean(7);
                poco.EmailAddress = rdr.GetString(8);
                poco.PhoneNumber = rdr.IsDBNull(9) ? null : rdr.GetString(9);
                poco.FullName = rdr.IsDBNull(10) ? null : rdr.GetString(10);
                poco.ForceChangePassword = rdr.GetBoolean(11);
                poco.PrefferredLanguage = rdr.IsDBNull(12) ? null : rdr.GetString(12);
                poco.TimeStamp = (byte[])rdr[13];
                pocos[counter] = poco;
                counter++;
            }

            conn.Close();
            return pocos.Where(p => p != null).ToList();

        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (SecurityLoginPoco item in items)
            {

                cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins]
                                    WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connStr);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (var item in items)
            {

                cmd.CommandText = @"UPDATE [dbo].[Security_Logins]
                                       SET 
                                          [Login] = @Login,
                                          [Password] = @Password,
                                          [Created_Date] = @Created,
                                          [Password_Update_Date] = @PasswordUpdate,
                                          [Agreement_Accepted_Date] = @AgreementAccepted,
                                          [Is_Locked] = @IsLocked,
                                          [Is_Inactive] = @IsInactive,
                                          [Email_Address] = @EmailAddress, 
                                          [Phone_Number] = @PhoneNumber,
                                          [Full_Name] = @FullName, 
                                          [Force_Change_Password] = @ForceChangePassword,
                                          [Prefferred_Language] = @PrefferredLanguage
                                     WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Password", item.Password);
                cmd.Parameters.AddWithValue("@Created", item.Created);
                cmd.Parameters.AddWithValue("@PasswordUpdate", item.PasswordUpdate);
                cmd.Parameters.AddWithValue("@AgreementAccepted", item.AgreementAccepted);
                cmd.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                cmd.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@EmailAddress", item.EmailAddress);
                cmd.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                cmd.Parameters.AddWithValue("@FullName", item.FullName);
                cmd.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                cmd.Parameters.AddWithValue("@PrefferredLanguage", item.PrefferredLanguage);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}
