using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorontoMuni.Data
{
    public class Batiment
    {
    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public BatCatEnum Categorie { get; set; }
        public BatStructEnum Structure { get; set; }
        public BatFormEnum Forme { get; set; }
        public string? Denomination { get; set; }
        public List<BatDates> Dates { get; set; }
        public int ValAcqu { get; set; }
        public int ValNow { get; set; }
        public int ValTravaux { get; set; }
        public int ValRemplacement { get; set; }
        public int Vetucité { get; set; }

        public int Longueur { get; set; }
        public int Largeur { get; set; }
        public int Profondeur { get; set; }
        public int Surface { get; set; }
        public int Volume { get; set; }
        public string? Pays { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }
        public string? NumeroCivique { get; set; }
        public string? Rue { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public string? Description { get; set; }
        public string? Observations { get; set; }
    }
    public class BatDates
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Batiment_ID { get; set; }
        [ForeignKey(nameof(Batiment_ID))]
        public Batiment Batiment { get; set; }
        public DateTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
    }
}