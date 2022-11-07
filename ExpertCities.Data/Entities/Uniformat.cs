using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class Uniformat : ILocalize
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? Parent_ID { get; set; }
        [ForeignKey(nameof(Parent_ID))]
        public Uniformat? Parent { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string ValueCA { get; set; }
        public List<Uniformat> Children { get; set; }
    }
}
