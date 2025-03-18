using System;
using System.ComponentModel.DataAnnotations;

namespace YourProject.Models
{
    // Enum for membership types
    public enum MembershipType
    {
        Basic = 1, // Basic Member
        Premium = 2, // Premium Member
        VIP = 3 // VIP Member
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

        public DateTime ExpiryDate { get; private set; }  // Expiry Date is calculated automatically

        [Required]
        public string Password { get; set; }  // Password is required

        // Constructor to set RegistrationDate and ExpiryDate based on MembershipType
        public Member(MembershipType membershipType)
        {
            MembershipType = membershipType;  // Set the MembershipType
            RegistrationDate = DateTime.UtcNow; // Set registration date to current time
            SetExpiryDate(); // Set expiry date based on membership type
        }

        // Method to calculate ExpiryDate based on MembershipType
        private void SetExpiryDate()
        {
            switch (MembershipType)
            {
                case MembershipType.Basic:
                    ExpiryDate = RegistrationDate.AddYears(1); // Basic membership: 1 year
                    break;
                case MembershipType.Premium:
                    ExpiryDate = RegistrationDate.AddYears(3); // Premium membership: 3 years
                    break;
                case MembershipType.VIP:
                    ExpiryDate = RegistrationDate.AddYears(6); // VIP membership: 6 years
                    break;
                default:
                    ExpiryDate = RegistrationDate.AddYears(1); // Default: 1 year if invalid
                    break;
            }
        }
    }
}
