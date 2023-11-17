
namespace DTOs
{
    public class EcosystemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GeoUbicationDTO GeoDetails { get; set; }
        public decimal Area { get; set; }
        public string Description { get; set; }
        public ConservationDTO Conservation { get; set; }
        public string ImgRoute { get; set; }
        public int Security { get; set; }
        public List<SpeciesDTO>? Species { get; set; }
        public List<ThreatDTO>? Threats { get; set; }
        public List<CountryDTO> Countries { get; set; }

        public EcosystemDTO() { }

        public EcosystemDTO(int id, string name, GeoUbicationDTO geo, decimal area, string desc, ConservationDTO cons,
            string imgRoute, int security, List<SpeciesDTO> specsDto, List<ThreatDTO> threatsDto, List<CountryDTO> countriesDto)
        {
            Id = id;
            Name = name;
            GeoDetails = geo;
            Area = area;
            Description = desc;
            Conservation = cons;
            ImgRoute = imgRoute;
            Security = security;
            Species = specsDto;
            Threats = threatsDto;
            Countries = countriesDto;
        }
    }
}
