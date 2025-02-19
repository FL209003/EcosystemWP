﻿
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }        
        public string Username { get; set; }        
        public string Password { get; set; }
        public string HashPassword { get; set; }        
        public string Role { get; set; }      
        public DateTime RegDate { get; set; }

        public UserDTO() { }

        public UserDTO(int id, string username, string pass, string role)
        {
            Id = id;
            Username = username; 
            Password = pass;
            HashPassword = pass;
            Role = role;
            RegDate = DateTime.Now;
        }
    }
}
