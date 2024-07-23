namespace API.Models
{
    public class OrderVM
    {
        public int Quantity { get; set; }
     
        public string NameOrder { get; set; }
        public string ImageOrder { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
    }
}
