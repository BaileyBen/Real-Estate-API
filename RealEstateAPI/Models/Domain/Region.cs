namespace RealEstateAPI.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public double Population { get; set; }

        public IEnumerable<House> Houses { get; set; }
    }
}
