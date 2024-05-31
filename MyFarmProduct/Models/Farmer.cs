using Microsoft.AspNetCore.Identity;

namespace MyFarmProduct.Models
{
    public class Farmer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public double FarmSize { get; set; }
        public string AdditionalInfo { get; set; }
        public string UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
