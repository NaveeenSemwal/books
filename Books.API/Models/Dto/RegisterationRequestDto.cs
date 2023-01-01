using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Books.API.Models.Dto
{
    public class RegisterationRequestDto
    {
        [Required]
        public string Name { get; set; }

        // [Required]
        // public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Introducation { get; set; }

        [Required]
        public string LookingFor { get; set; }

        [Required]
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }


    public class RegisterationResponsetDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
