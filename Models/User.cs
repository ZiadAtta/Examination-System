﻿namespace Examination_System.Models
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRole? Role { get; set; } 
    }

    public enum UserRole
    {
        Instructor,
        Student
    }
}
