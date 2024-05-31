using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyFarmProduct.Models.ViewModels
{
    public class FarmerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string Contact { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Farm Size")]
        public double FarmSize { get; set; }
        [Display(Name = "Additional Info")]
        public string AdditionalInfo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Password must be minimum length of 6 character long.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
