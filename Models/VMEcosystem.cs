using Domain.Entities;
using DTOs;
using System.ComponentModel.DataAnnotations;

namespace EcosystemApp.Models
{
    public class VMEcosystem
    {
        public EcosystemDTO Ecosystem { get; set; }

        public IEnumerable<CountryDTO> Countries { get; set; }
        public List<int> IdSelectedCountry { get; set; }

        public IEnumerable<ThreatDTO> Threats { get; set; }
        public List<int> IdSelectedThreats { get; set; }

        public IFormFile ImgEco { get; set; }
    }
}