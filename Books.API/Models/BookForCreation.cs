using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Models
{
    public class BookForCreation
    {
        public Guid AuthorId { get; set; }

        public string Title { get; set; } 

        public string Description { get; set; }

        public IFormFile FormFile { get; set; }
    }

}
