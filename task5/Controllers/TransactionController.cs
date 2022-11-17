using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using PracticeAPIAuthSem4.Models;
using PracticeAPIAuthSem4.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace PracticeAPIAuthSem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public TransactionController(IConfiguration c)
        {
            configuration = c;
        }

        [Authorize]
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();
            if (Tokens.IsNotBlocked(token, configuration))
            {
                string query = @"
                select payer_name as ""Name"",
                       id as ""Id"",
                       card_number as ""CardNumber"",
                       cvc as ""Cvc"",
                       month as ""Month"",
                       year as ""Year"",
                       to_char(date,'YYYY-MM-DD') as ""Date"",
                       amount as ""Amount""
                from transactions
                where payer_name = @name and user_name = @user;
            ";
                DataTable table = new DataTable();
                string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
                User currentUser = CurrentUser();
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
                {
                    myConnect.Close();
                    myConnect.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                    {
                        myCommand.Parameters.AddWithValue("@name", name);
                        myCommand.Parameters.AddWithValue("@user", currentUser.UserName);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myConnect.Close();
                    }
                }
                if (table.Rows.Count == 0)
                {
                    return NotFound();
                }
                return new JsonResult(table);
            }
            return Forbid();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(string sort_by = null, string sort_type = "asc", string value = null)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            if (Tokens.IsNotBlocked(token, configuration))
            {
                string query = @"
                select payer_name as ""Name"",
                       id as ""Id"",
                       card_number as ""CardNumber"",
                       cvc as ""Cvc"",
                       month as ""Month"",
                       year as ""Year"",
                       to_char(date,'YYYY-MM-DD') as ""Date"",
                       amount as ""Amount""
                from transactions
                where user_name = @user
            ;";
                DataTable table = new DataTable();
                string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
                User currentUser = CurrentUser();
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
                {
                    myConnect.Close();
                    myConnect.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                    {
                        myCommand.Parameters.AddWithValue("@user", currentUser.UserName);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myConnect.Close();
                    }
                }
                if (value != null)
                {
                    string filter = $"(Name+Id+CardNumber+Cvc+Month+Year+Date+Amount) like '%{value}%'";
                    DataRow[] rows = table.Select(filter);
                    DataTable data = table.Clone();
                    foreach (DataRow row in rows)
                    {
                        data.ImportRow(row);
                    }
                    data.AcceptChanges();
                    table = data;
                }
                if (sort_by != null)
                {
                    DataTable temp = table.Clone();
                    temp.Columns["Month"].DataType = Type.GetType("System.Int32");
                    temp.Columns["Year"].DataType = Type.GetType("System.Int32");
                    temp.Columns["Date"].DataType = Type.GetType("System.DateTime");
                    temp.Columns["Amount"].DataType = Type.GetType("System.Int32");
                    foreach (DataRow dr in table.Rows)
                    {
                        temp.ImportRow(dr);
                    }
                    temp.AcceptChanges();

                    DataView dv = temp.DefaultView;
                    dv.Sort = $"{sort_by} {sort_type.ToUpper()}";
                    table = dv.ToTable();
                }
                if (table.Rows.Count == 0)
                {
                    return NotFound();
                }
                return new JsonResult(table);
            }
            return Forbid();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(Transaction transaction)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();
            if (Tokens.IsNotBlocked(token, configuration))
            {
                JsonResult errors;
                if (Confirm.transactionIsValid(transaction, out errors))
                {
                    string query = @"
                insert into transactions(payer_name,id,card_number,cvc,month,year,date,amount,user_name)
                       values(@Name,@Id,@CardNumber,@Cvc,@Month,@Year,@Date,@Amount,@user)
                ";
                    string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
                    User currentUser = CurrentUser();
                    NpgsqlDataReader myReader;
                    using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
                    {
                        myConnect.Open();
                        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                        {
                            myCommand.Parameters.AddWithValue("@Name", transaction.Name);
                            myCommand.Parameters.AddWithValue("@Id", transaction.Id);
                            myCommand.Parameters.AddWithValue("@CardNumber", transaction.CardNumber);
                            myCommand.Parameters.AddWithValue("@Cvc", transaction.Cvc);
                            myCommand.Parameters.AddWithValue("@Month", transaction.Month);
                            myCommand.Parameters.AddWithValue("@Year", transaction.Year);
                            myCommand.Parameters.AddWithValue("@Date", transaction.Date);
                            myCommand.Parameters.AddWithValue("@Amount", transaction.Amount);
                            myCommand.Parameters.AddWithValue("@user", currentUser.UserName);
                            myReader = myCommand.ExecuteReader();
                            myReader.Close();
                            myConnect.Close();
                        }
                    }
                    Dictionary<string, string> message = new Dictionary<string, string>();
                    message.Add("message", "Added successfully");
                    return Ok(message);
                }
                return BadRequest(errors);
            }
            return Forbid();
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(Transaction transaction)
        {
            JsonResult errors;
            if (Confirm.transactionIsValid(transaction, out errors))
            {
                string token = HttpContext.Request.Headers["Authorization"].ToString();
                if (Tokens.IsNotBlocked(token, configuration))
                {
                    string query = @"
                 update transactions 
                 set payer_name = @Name,
                    id = @Id,
                    card_number = @CardNumber,
                    cvc = @Cvc,
                    month = @Month,
                    year = @Year,
                    date = @Date,
                    amount = @Amount
                where id=@Id and user_name = @user;
            ";
                    string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
                    User currentUser = CurrentUser();
                    NpgsqlDataReader myReader;
                    using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
                    {
                        myConnect.Open();
                        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                        {
                            myCommand.Parameters.AddWithValue("@Name", transaction.Name);
                            myCommand.Parameters.AddWithValue("@Id", transaction.Id);
                            myCommand.Parameters.AddWithValue("@CardNumber", transaction.CardNumber);
                            myCommand.Parameters.AddWithValue("@Cvc", transaction.Cvc);
                            myCommand.Parameters.AddWithValue("@Month", transaction.Month);
                            myCommand.Parameters.AddWithValue("@Year", transaction.Year);
                            myCommand.Parameters.AddWithValue("@Date", transaction.Date);
                            myCommand.Parameters.AddWithValue("@Amount", transaction.Amount);
                            myCommand.Parameters.AddWithValue("@user", currentUser.UserName);
                            myReader = myCommand.ExecuteReader();
                            myReader.Close();
                            myConnect.Close();
                        }
                    }
                    Dictionary<string, string> message = new Dictionary<string, string>();
                    message.Add("message", "Updated Successfully if item with such id existed before and was added by you");
                    return Ok(message);
                }
                return BadRequest(errors);
            }
            return Forbid();
        }

        [Authorize]
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();
            if (Tokens.IsNotBlocked(token, configuration))
            {
                string query = @"
                delete from transactions 
                where payer_name=@name and user_name = @user;
            ";
                string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
                User currentUser = CurrentUser();
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
                {
                    myConnect.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                    {
                        myCommand.Parameters.AddWithValue("@name", name);
                        myCommand.Parameters.AddWithValue("@user", currentUser.UserName);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myConnect.Close();
                    }
                }
                Dictionary<string, string> message = new Dictionary<string, string>();
                message.Add("message", "Deleted Successfully if item with such id existed before and was added by you");
                return Ok(message);
            }
            return Forbid();
        }

        private User CurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                User user = new User
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
                };
                return user;
            }
            return null;
        }
            
    }
}
