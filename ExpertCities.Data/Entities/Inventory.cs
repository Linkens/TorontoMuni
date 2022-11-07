using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class Inventory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Asset_ID { get; set; }
        [ForeignKey(nameof(Asset_ID))]
        public Asset Asset { get; set; }
        public int? Uniformat_ID { get; set; }
        [ForeignKey(nameof(Uniformat_ID))]
        public Uniformat? Uniformat { get; set; }
        public InventoryMethodEnum Method { get; set; }
        public bool Exterior { get; set; }
        public string? Room { get; set; }
        public string? Level { get; set; }
        public string? Item { get; set; }
        public string? Manufacturer { get; set; }
        public string? Brand { get; set; }
        public string? Series { get; set; }
        public int Quantity { get; set; } = 1;
        public float Length { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Perimeter { get; set; }
        public float Surface { get; set; }
        public float Volume { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
    }
}
