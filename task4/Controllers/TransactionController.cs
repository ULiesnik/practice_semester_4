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
using PracticeAPISem4.Models;

namespace PracticeAPISem4.Controllers
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

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByName(string name)
        {
            string query = @"
                select payer_name as ""Name"",
                       id as ""Id"",
                       card_number as ""CardNumber"",
                       cvc as ""Cvc"",
                       month as ""Month"",
                       year as ""Year"",
                       date as ""Date"",
                       amount as ""Amount""
                from transaction
                where payer_name = @name;
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
                    myCommand.Parameters.AddWithValue("@name", name);
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string sort_by = null, string sort_type = "asc", string value = null)
        {
            string query = @"
                select payer_name as ""Name"",
                       id as ""Id"",
                       card_number as ""CardNumber"",
                       cvc as ""Cvc"",
                       month as ""Month"",
                       year as ""Year"",
                       date as ""Date"",
                       amount as ""Amount""
                from transaction
            ;";
            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
            {
                myConnect.Close();
                myConnect.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                {
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
                foreach(DataRow dr in table.Rows)
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(Transaction transaction)
        {
            JsonResult errors;
            if (Validation.isValid(transaction, out errors))
            {
                string query = @"
                insert into transaction(payer_name,id,card_number,cvc,month,year,date,amount)
                       values(@Name,@Id,@CardNumber,@Cvc,@Month,@Year,@Date,@Amount)
                ";
                string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
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
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myConnect.Close();
                    }
                }
                return new JsonResult("Added Successfully");
            }
            return BadRequest(errors);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(Transaction transaction)
        {
            JsonResult errors;
            if (Validation.isValid(transaction, out errors))
            {
                string query = @"
                 update transaction 
                 set payer_name = @Name,
                    id = @Id,
                    card_number = @CardNumber,
                    cvc = @Cvc,
                    month = @Month,
                    year = @Year,
                    date = @Date,
                    amount = @Amount
                where id=@Id;
            ";
                string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
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
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myConnect.Close();
                    }
                }
                return new JsonResult("Updated Successfully if item with such id existed before");
            }
            return BadRequest(errors);
        }

        [HttpDelete("{name}")]
        public JsonResult Delete(string name)
        {
            string query = @"
                delete from transaction 
                where payer_name=@name;
            ";
            string sqlDataSource = configuration.GetConnectionString("TransactionAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConnect = new NpgsqlConnection(sqlDataSource))
            {
                myConnect.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConnect))
                {
                    myCommand.Parameters.AddWithValue("@name", name);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myConnect.Close();
                }
            }
            return new JsonResult("Deleted Successfully if item existed before");
        }
    }
}
