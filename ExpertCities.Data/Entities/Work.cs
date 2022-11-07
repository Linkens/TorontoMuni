using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public  class Work
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? Asset_ID { get; set; }
        [ForeignKey(nameof(Asset_ID))]
        public Asset? Asset { get; set; }
        public bool IsInternal { get; set; }
        public WorkStateEnum State { get; set; }
        public string Summary { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public DateTime? WorkCompleted { get; set; }
        public DateTime? Verified { get; set; }
        public List<WorkAction> Actions { get; set; }
    }
}
