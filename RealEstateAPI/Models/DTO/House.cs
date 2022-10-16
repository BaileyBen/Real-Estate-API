using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Models.DTO
{
    public class House
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string Suburb { get; set; }
        public double Postcode { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public int Room { get; set; }
        public int Shed { get; set; }
        public double SqMeter { get; set; }
        public string Text { get; set; }
        public Guid RegionId { get; set; }
        public Guid LandscapeId { get; set; }

        public Region Region { get; set; }
        public Landscape Landscape { get; set; }
    }
}
