using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace DTOs
{
    public class ThreatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Danger { get; set; }
        public List<EcosystemDTO>? Ecosystems { get; set; }
        public List<SpeciesDTO>? Species { get; set; }
        public ThreatDTO() { }
        public ThreatDTO(int id, string name, string desc, int danger, List<EcosystemDTO> ecosDto, List<SpeciesDTO> specsDto)
        {
            Id = id;
            Name = name;
            Description = desc;
            Danger = danger;
            Ecosystems = ecosDto;
            Species = specsDto;            
        }
    }
}