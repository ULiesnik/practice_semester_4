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

namespace PracticeAPIAuthSem4.Validation
{
    class Tokens
    {
        public static string TokenGenerate(User u, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, u.UserName),
                new Claim(ClaimTypes.Name, u.FirstName),
                new Claim(ClaimTypes.Surname, u.LastName),
                new Claim(ClaimTypes.Email, u.Email),
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static void AddToBlacklist(string token, IConfiguration configuration)
        {
            string query = @"
                insert into blacklist(token,date)
                       values(@token,@date);
                ";
            string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
            {
                myConnect.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                {
                    myCommand.Parameters.AddWithValue("@token", token);
                    myCommand.Parameters.AddWithValue("@date", DateTime.Now.Date);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myConnect.Close();
                }
            }
        }
        public static bool IsNotBlocked(string token, IConfiguration configuration)
        {
            string query = @"
                select date from blacklist where token=@token;
                ";
            string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
            DataTable table = new DataTable();
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
            {
                myConnect.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                {
                    myCommand.Parameters.AddWithValue("@token", token);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConnect.Close();
                }
            }
            if (table.Rows.Count == 0) 
            {
                return true;
            }
            return false;
        }
    }
}
