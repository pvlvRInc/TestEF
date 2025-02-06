﻿using Library.DTOs.Abstraction;

namespace Library.DTOs;

public class UserModel : ModelBase
{
    public string FirstName    { get; set; }
    public string MiddleName   { get; set; }
    public string LastName     { get; set; }
    public string Email        { get; set; }
    public string HashPassword { get; set; }
}