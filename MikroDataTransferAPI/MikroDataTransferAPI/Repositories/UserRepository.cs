using Dapper;
using MikroDataTransferAPI.Contracts;
using MikroDataTransferAPI.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MikroDataTransferAPI.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private string _connString;
        public AuthRepository(string connString)
        {
            _connString = connString;
        }
        public User Login(string userName, string password)
        {
            var users = GetAllUsers();

            var user = users.Where(x=>x.UserName==userName).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private List<User> GetAllUsers()
        {
            string query = "select * from [dbo].[Users]";
            using (var conn=new SqlConnection(_connString))
            {
                var data = conn.Query<User>(query, 
                    commandType: System.Data.CommandType.Text, 
                    commandTimeout: 0);

                if (data == null)
                    return new List<User>();

                return data.ToList();
            }
        }

        private bool VerifyPasswordHash(string password, 
            byte[] userPasswordHash, 
            byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public  Task<User> Register(User user, string password)
        {
           return  Task.Run(() =>
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                AddUser(user);

                return user;
            });

        }

        private void AddUser(User user)
        {
           using(var conn=new SqlConnection(_connString))
            {
                conn.Open();
                string query= @" insert into Users (UserName,UserLongName,RoleName,PasswordHash,PasswordSalt)
 values(@UserName, @UserLongName, @RoleName, @PasswordHash, @PasswordSalt)";

                conn.Execute(query, 
                    new
                    {
                        UserName = user.UserName,
                        UserLongName = user.UserLongName,
                        RoleName = user.RoleName,
                        PasswordHash = user.PasswordHash,
                        PasswordSalt = user.PasswordSalt
                    }, commandTimeout: 0, commandType: System.Data.CommandType.Text);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Task<bool> UserExists(string userName)
        {
            return Task.Run(() => {
                using (var conn = new SqlConnection(_connString))
                {
                    conn.Open();
                    string query = "select count(*) from Users where UserName=@UserName";
                    var data = conn.ExecuteScalar(query,
                        new { UserName = userName },
                        commandTimeout: 0,
                        commandType: System.Data.CommandType.Text);

                    if (data == null)
                    {
                        return false;
                    }

                    int.TryParse(data.ToString(), out int rowCount);
                   
                    if(rowCount>0)
                    {
                        return true;
                    }

                    return false;

                }
            });
           
        }
    }
}
