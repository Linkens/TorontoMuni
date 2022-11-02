using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class InspectionReport
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Inspection_ID { get; set; }
        [ForeignKey(nameof(Inspection_ID))]
        public Inspection Inspection { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int People { get; set; }
        public string? Transport { get; set; }
        public string? ToolUsed { get; set; }
        public string? MaterialUsed { get; set; }
        public InspectionStateEnum State { get; set; }
        public InspectionConditionEnum Condition { get; set; }
        public InspectionCauseEnum Cause { get; set; }
        public InspectionImpactEnum Impact { get; set; }
        public InspectionRiskEnum Risk { get; set; }
        public InspectionOperationEnum Operation { get; set; }
        public string? Recommandation { get; set; }
        public int? Approver_ID { get; set; }
        [ForeignKey(nameof(Approver_ID))]
        public Workforce? Approver { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string? Comments { get; set; }
        public bool Approved { get; set; }
    }
}
