using System;
using System.ComponentModel.DataAnnotations;

namespace YourProject.Models
{
    // Enum for membership types
    public enum MembershipType
    {
        Basic,
        Premium,
        VIP
    }

    public class Member
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }  // Full name is required

        [Required]
        public DateTime DateOfBirth { get; set; }  // Date of Birth is required

        [Required]
        [EmailAddress]
        public string Email { get; set; }  // Email is required and must be a valid email address

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }  // Phone number is required and must be a valid phone number

        [Required]
        public string Address { get; set; }  // Address is required

        [Required]
        public MembershipType MembershipType { get; set; }  // Membership Type is required

        public DateTime RegistrationDate { get; set; }  // Registration Date will be set automatically

        public DateTime? ExpiryDate { get; set; }  // Expiry Date is optional

        [Required]
        public string Password { get; set; }  // Password is required
    }
}
