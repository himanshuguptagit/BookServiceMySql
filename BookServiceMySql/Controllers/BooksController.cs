using BookServiceMySql.Lib;
using BookServiceMySql.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookServiceMySql.Controllers
{
    public class BooksController : ApiController
    {
        // GET: api/Books
        public async Task<IHttpActionResult> Get()
        {
            var sql = @"Select b.* , a.Name as AuthorName
                        From Books b
                        INNER JOIN Authors a on a.Id = b.AuthorId LIMIT 100";
            try
            {
                using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
                {
                    var result = await conn.QueryAsync<BookDto>(sql);
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        // GET: api/Books/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var sql = @"Select b.*, a.Name as AuthorName From Books b
                        INNER JOIN Authors a on a.Id = b.AuthorId WHERE b.Id=@id";

            using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
            {
                var result = await conn.QuerySingleOrDefaultAsync<BookDto>(sql, new { id = id });
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

        // POST: api/Books
        public async Task<IHttpActionResult> Post([FromBody]Book book)
        {
            if (!ModelValidator.isValid(book))
            {
                return BadRequest();
            }

            var sql = @"INSERT INTO Books (Title, Year, Price, AuthorId, Genre) VALUES (@Title, @Year, @Price, @AuthorId, @Genre)";

            try
            {
                using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
                {
                    var result = await conn.ExecuteAsync(sql, book);
                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }

        // PUT: api/Books/5
        public async Task<IHttpActionResult> Put([FromBody]Book book)
        {

            if (!ModelValidator.isValid(book))
            {
                return BadRequest();
            }

            var sql = @"UPDATE Books
                        SET Title = @Title,
                        Price = @Price,
                        Year = @Year,
                        AuthorId = @AuthorId,
                        Genre = @Genre
                        WHERE Id=@Id";
            try
            {
                using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
                {
                    var result = await conn.ExecuteAsync(sql, book);
                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }



        }

        // DELETE: api/Books/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var sql = "DELETE FROM Books WHERE Id = @Id";

            using (var conn = new MySqlConnection(Constants.DATABASE_CONNECTION_STRING))
            {
                await conn.ExecuteAsync(sql, new { Id = id });
            }

            return Ok();
        }
    }
}
