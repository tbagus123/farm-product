namespace MyFarmProduct.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ProductionDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Image { get; set; }
        public Guid FarmerId { get; set; }
        public Farmer Farmer { get; set; }
    }
}
