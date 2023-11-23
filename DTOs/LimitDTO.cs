namespace EcosystemApp.DTOs
{
    public class LimitDTO
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public LimitDTO() { }

        public LimitDTO(int min, int max) { Min = min; Max = max; }
    }
}
