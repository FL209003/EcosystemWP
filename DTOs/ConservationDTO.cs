
namespace DTOs
{
    public class ConservationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinSecurityRange { get; set; }
        public int MaxSecurityRange { get; set; }
        public List<EcosystemDTO>? ConservationEcosystems { get; set; }
        public List<SpeciesDTO>? ConservationSpecies { get; set; }

        public ConservationDTO() { }

        public ConservationDTO(int id, string name, int min, int max, List<EcosystemDTO> ecosDto, List<SpeciesDTO> specsDto)
        {
            Id = id;
            Name = name;
            MinSecurityRange = min;
            MaxSecurityRange = max;
            ConservationEcosystems = ecosDto;
            ConservationSpecies = specsDto;
        }        
    }
}