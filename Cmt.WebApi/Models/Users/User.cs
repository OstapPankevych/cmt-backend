﻿using System;
namespace Cmt.WebApi.Models.Users
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Jwt Jwt { get; set; }
    }
}
