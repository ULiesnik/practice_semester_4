using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using PracticeAPIAuthSem4.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using PracticeAPIAuthSem4.Validation;

namespace PracticeAPIAuthSem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public UserController(IConfiguration c)
        {
            configuration = c;
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(User user)
        {
            JsonResult errors;
            if (Confirm.userIsValid(user, out errors))
            {
                user.Password = PasswordHasher.Hash(user.Password);
                string query = @"
                insert into users(user_name,first_name,last_name,email,password)
                       values(@UserName,@FirstName,@LastName,@Email,@Password)
                ";
                string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
                {
                    myConnect.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                    {
                        myCommand.Parameters.AddWithValue("@UserName", user.UserName);
                        myCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                        myCommand.Parameters.AddWithValue("@LastName", user.LastName);
                        myCommand.Parameters.AddWithValue("@Email", user.Email);
                        myCommand.Parameters.AddWithValue("@Password", user.Password);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myConnect.Close();
                    }
                }
                Dictionary<string, string> message = new Dictionary<string, string>();
                message.Add("token", "Signed up successfully");
                return Ok(message);
            }
            return BadRequest(errors);
        }

        [Authorize]
        [HttpDelete("logout")]
        public IActionResult Logout()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();
            Tokens.AddToBlacklist(token, configuration);

            Dictionary<string, string> message = new Dictionary<string, string>();
            message.Add("message", "Logged out successfully. Token is now in blacklist");
            return Ok(message);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginData d)
        {
            User user = Auth(d);
            if (user != null)
            {
                string token = Tokens.TokenGenerate(user,configuration);

                Dictionary<string, string> message = new Dictionary<string, string>();
                message.Add("token", token);
                return Ok(message);
            }
            return NotFound("User is not found");
        }

        private User Auth(LoginData d)
        {
            string query = @"
                select user_name as ""UserName"",
                       first_name as ""FirstName"",
                       last_name as ""LastName"",
                       email as ""Email"",
                       password as ""Password""
                from users
                where user_name = @name;
            ";
            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
            {
                myConnect.Close();
                myConnect.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                {
                    myCommand.Parameters.AddWithValue("@name", d.UserName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConnect.Close();
                }
            }
            if(table.Rows.Count == 1)
            {
                var row = table.Rows[0];
                if (PasswordHasher.Verify(d.Password, Convert.ToString(row["Password"])))
                {
                    return new User
                    {
                        UserName = d.UserName,
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Email = Convert.ToString(row["Email"])
                    };
                }
            }
            return null;
        }

    }
}
