using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class WorkAction
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Work_ID { get; set; }
        [ForeignKey(nameof(Work_ID))]
        public Work Work { get; set; }
        public string Worker { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
