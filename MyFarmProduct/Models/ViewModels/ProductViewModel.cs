using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyFarmProduct.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ProductionDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public Guid FarmerId { get; set; }
        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
