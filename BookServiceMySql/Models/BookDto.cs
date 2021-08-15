using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookServiceMySql.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
        public string Genre { get; set; }
        public string AuthorName { get; set; }
    }
}