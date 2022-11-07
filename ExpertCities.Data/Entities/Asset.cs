using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCities.Data
{
    public class Asset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public AssetCatEnum Category { get; set; }
        public AssetStructEnum Structure { get; set; }
        public BuildShapeEnum Shape { get; set; }
        public string? Denomination { get; set; }
        public AssetTypeEnum Type { get; set; }
        public DateTime? Date_Acquire { get; set; }
        public DateTime? Date_Commission { get; set; }
        public DateTime? Date_Garantee { get; set; }
        public int Val_Acquire { get; set; }
        public int Val_Now { get; set; }
        public int Val_Work { get; set; }
        public int Val_Remplace { get; set; }
        public int State { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
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
        public List<AssetImage> Images { get; set; }
        public List<AssetFile> Files { get; set; }
        public List<Inspection> Inspections { get; set; }
        public List<Work> Works { get; set; }
    }
    public class AssetImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int Building_ID { get; set; }
        [ForeignKey(nameof(Building_ID))]
        public Asset Asset { get; set; }
        public byte[] Image { get; set; }
    }
}