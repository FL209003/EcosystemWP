
namespace DTOs
{
    public class SpeciesDTO
    {

        public int Id { get; set; }
        public string CientificName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal WeightRangeMin { get; set; }
        public decimal WeightRangeMax { get; set; }
        public decimal LongRangeAdultMin { get; set; }
        public decimal LongRangeAdultMax { get; set; }
        public ConservationDTO Conservation { get; set; }
        public string ImgRoute { get; set; }
        public int Security { get; set; }
        public List<EcosystemDTO>? Ecosystems { get; set; }
        public List<ThreatDTO>? Threats { get; set; }

        public SpeciesDTO() { }

        public SpeciesDTO(int id, string cientificName, string name, string desc, decimal weightMin, decimal weightMax,
            decimal longMin, decimal longMax, ConservationDTO cons, string imgRoute, int security,
            List<EcosystemDTO> ecosDto, List<ThreatDTO> threatsDto)
        {
            Id = id;
            CientificName = cientificName;
            Name = name;
            Description = desc;
            WeightRangeMin = weightMin;
            WeightRangeMax = weightMax;
            LongRangeAdultMin = longMin;
            LongRangeAdultMax = longMax;
            Conservation = cons;
            ImgRoute = imgRoute;
            Security = security;
            Ecosystems = ecosDto;
            Threats = threatsDto;
        }
    }
}