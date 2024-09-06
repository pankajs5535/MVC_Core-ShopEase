using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string Name { get; set; }    
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        // Foreign key for the Company
        public int? CompanyId { get; set; }

        // Navigation property to the Company
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company? Company { get; set; }

    }
}


/*
  In ASP.NET Core Identity, IdentityUser is a predefined class provided by the framework that represents a user in the identity system. 
The IdentityUser class contains the basic properties and methods required to manage user accounts, such as username, email, password, and 
security-related information.

When you inherit from IdentityUser, you create a custom user class (ApplicationUser in your example) that can extend the functionality of 
the IdentityUser class by adding additional properties or methods specific to your application.

Key Properties of IdentityUser
Here are some of the key properties available in the IdentityUser class:

Id: The unique identifier for the user (typically a GUID).
UserName: The username of the user.
NormalizedUserName: The normalized username (usually stored in uppercase for case-insensitive comparisons).
Email: The user's email address.
NormalizedEmail: The normalized email (also stored in uppercase).
EmailConfirmed: A boolean indicating whether the user's email has been confirmed.
PasswordHash: The hashed password of the user.
SecurityStamp: A random value that is changed whenever a user's credentials are changed (used to invalidate tokens).
ConcurrencyStamp: A random value used to handle concurrency.
PhoneNumber: The user's phone number.
PhoneNumberConfirmed: A boolean indicating whether the user's phone number has been confirmed.
TwoFactorEnabled: A boolean indicating whether two-factor authentication is enabled for the user.
LockoutEnd: The date and time until which the user is locked out (if lockout is enabled).
LockoutEnabled: A boolean indicating whether the user can be locked out.
AccessFailedCount: The number of failed login attempts for the user.

*/