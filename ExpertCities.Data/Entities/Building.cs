using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpertCities.Data.Tools;

namespace ExpertCities.Data.Entities
{
    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public BuildCatEnum Category { get; set; }
        public BuildStructEnum Structure { get; set; }
        public BuildShapeEnum Shape { get; set; }
        public string? Denomination { get; set; }

        public DateTime? Date_Acquire { get; set; }
        public DateTime? Date_Commission { get; set; }
        public DateTime? Date_Garantee { get; set; }
        public DateTime? Date_Other { get; set; }
        public int Val_Acquire { get; set; }
        public int Val_Now { get; set; }
        public int Val_Work { get; set; }
        public int Val_Remplace { get; set; }
        public int State { get; set; }

        public int Length { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int Surface { get; set; }
        public int Volume { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? CivicNumber { get; set; }
        public string? Street { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public string? Description { get; set; }
        public string? Observation { get; set; }
        public List<BuildingImage> Images { get; set; }
    }
    public class BuildingImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int Building_ID { get; set; }
        [ForeignKey(nameof(Building_ID))]
        public Building Building { get; set; }
        public byte[] Image { get; set; }
    }
}