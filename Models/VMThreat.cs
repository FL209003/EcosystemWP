using Domain.Entities;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace EcosystemApp.Models
{
    public class VMThreat
    {
        public Threat Threat { get; set; }

        public String ThreatNameVAL { get; set; }

        public String ThreatDescriptionVal { get; set; }
    }
}
