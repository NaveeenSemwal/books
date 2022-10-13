﻿using Books.API.Entities;

namespace Books.API.Models.Dto
{
    public class LoginResponseDto
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
    }
}
