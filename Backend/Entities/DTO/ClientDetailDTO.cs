﻿using System;

namespace Entities.DTO
{
    public class ClientDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
    }
}
