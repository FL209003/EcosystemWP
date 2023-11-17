using DTOs;
using System.ComponentModel.DataAnnotations;

namespace EcosystemApp.Models
{
    public class VMSpecies
    {
        public SpeciesDTO Species { get; set; }        

        public IEnumerable<ThreatDTO> Threats { get; set; }
        public List<int> IdSelectedThreats { get; set; }
        
        public IEnumerable<EcosystemDTO> Ecosystems { get; set; }
        public List<int> IdSelectedEcos { get; set; }

        public IFormFile ImgSpecies { get; set; }
    }
}
