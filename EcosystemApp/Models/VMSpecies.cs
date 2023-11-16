using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace EcosystemApp.Models
{
    public class VMSpecies
    {
        public Species Species { get; set; }

        public String SpeciesNameVal { get; set; }

        public String SpeciesDescriptionVal { get; set; }

        public IEnumerable<Threat> Threats { get; set; }

        public List<int> IdSelectedThreats { get; set; }
        
        public IEnumerable<Ecosystem> Ecosystems { get; set; }
        public List<int> IdSelectedEcos { get; set; }
        public IFormFile ImgSpecies { get; set; }
    }
}
