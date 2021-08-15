using BookServiceMySql.Lib;
using BookServiceMySql.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MySql.Data.MySqlClient;
using NLog;

namespace BookServiceMySql.Controllers
{
    public class AuthorsController : ApiController
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: api/Authors
        public async Task<IHttpActionResult> Get()
        {
            var sql = "Select * From Authors LIMIT 100";

            try
            {

                using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
                {

                    var result = await conn.QueryAsync<Author>(sql);
                    logger.Info("Authors fetched: " + result.Count());

                    return Ok(result);
                }
            }catch(Exception e)
            {
                logger.Debug(e.Message);

                return InternalServerError(e);
            }

        }

        // GET: api/Authors/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var sql = "Select * From Authors WHERE Id=@id";

            using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
            {

                var result = await conn.QuerySingleOrDefaultAsync<Author>(sql, new { id = id});
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST: api/Authors
        public async Task<IHttpActionResult> Post([FromBody]Author author)
        {
            if (!ModelValidator.isValid(author))
            {
                return BadRequest();
            }

            var sql = @"INSERT INTO Authors (Name) VALUES (@Name)";

            try
            {
                using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
                {
                    var result = await conn.ExecuteAsync(sql, new { Name = author.Name });
                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }catch(Exception e)
            {
                logger.Debug(e.Message);
                return InternalServerError(e);
            }

  
        }

        // PUT: api/Authors/5
        public async Task<IHttpActionResult> Put([FromBody]Author author)
        {

            if (!ModelValidator.isValid(author))
            {
                return BadRequest();
            }

            var sql = @"UPDATE Authors
                        SET Name = @Name
                        WHERE Id=@Id";

            try
            {
                using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
                {
                    var result = await conn.ExecuteAsync(sql, author);
                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            } catch(Exception e)
            {

                logger.Debug(e.Message);
                return InternalServerError(e);
            }
 


        }

        // DELETE: api/Authors/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var sql = "DELETE FROM Authors WHERE Id = @Id";

            using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
            {
                await conn.ExecuteAsync(sql, new { Id = id });
            }

            return Ok();
        }
    }
}
