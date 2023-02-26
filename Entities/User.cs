using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMicroservice.Entities;

public partial class User
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }
}
