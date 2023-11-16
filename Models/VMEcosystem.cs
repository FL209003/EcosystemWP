using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace EcosystemApp.Models
{
    public class VMEcosystem
    {
        public  Ecosystem Ecosystem { get; set; }

        public  String EcosystemNameVAL { get; set; }

        public  String EcoDescriptionVAL { get; set; }

        public  Decimal Lat { get; set; }

        public  Decimal Long { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        public List<int> IdSelectedCountry { get; set; }

        public IEnumerable<Threat> Threats { get; set; }
        public List<int>IdSelectedThreats { get; set; }

        public  IFormFile ImgEco { get; set; }
    }
}