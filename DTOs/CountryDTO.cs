
namespace DTOs
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alpha3 { get; set; }

        public CountryDTO() { }
        public CountryDTO(int id, string name, string alpha3)
        {
            Id = id;
            Name = name;
            Alpha3 = alpha3;
        }
    }
}