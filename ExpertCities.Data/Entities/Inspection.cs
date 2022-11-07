using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class Inspection
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Asset_ID { get; set; }
        [ForeignKey(nameof(Asset_ID))]
        public Asset Asset { get; set; }
        public int? Inventory_ID { get; set; }
        [ForeignKey(nameof(Inventory_ID))]
        public Inventory? Inventory { get; set; }
        public string? ExternalAffectation { get; set; }
        public int? Workforce_ID { get; set; }
        [ForeignKey(nameof(Workforce_ID))]
        public Workforce? Workforce { get; set; }
        public InspectionTypeEnum Type { get; set; }
        public InspectionClassEnum Class { get; set; }
        public InspectionAffectationEnum Affectation { get; set; }
        public string Item { get; set; }
        public string? Description { get; set; }
        public DateTime Scheduled { get; set; }
        public InspectionReport Report { get; set; }
    }
}
