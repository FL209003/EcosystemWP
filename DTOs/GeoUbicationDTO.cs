
namespace DTOs
{
    public class GeoUbicationDTO
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public GeoUbicationDTO() { }

        public GeoUbicationDTO(decimal lat, decimal lon)
        {
            Latitude = lat;
            Longitude = lon;
        }        
    }
}