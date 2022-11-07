using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
namespace ExpertCities.Data
{
    public static class EnumHelp
    {
        public static string GetLocalizeName(this Enum e)
        {
            var rm = new ResourceManager("ExpertCities.Data.Tools.EnumHelp",typeof(EnumHelp).Assembly);
            string? resourceDisplayName = null;
            try
            {
                resourceDisplayName = rm.GetString(e.GetType().Name + "_" + e, CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
            }
            return string.IsNullOrWhiteSpace(resourceDisplayName) ? string.Format("{0}", e) : resourceDisplayName;
        }
    }
    public enum AssetTypeEnum
    {
        Building, Land, Equipement, Road, Piping, Energy
    }
    public enum AssetCatEnum
    {
        Residential = 1, Administrative = 2, Recreational = 4, Commercial = 8, Industrial = 16, Agricultural = 32, Hospital = 64, School = 128
    }
    public enum AssetStructEnum
    {
        Wood = 1, Metal = 2, Concrete = 4, Wood_Concrete_Mix = 5, Wood_Metal_Mix = 6, Other = 8
    }
    public enum BuildShapeEnum
    {
        Triangular = 1, Square = 2, Rectangular = 4, Trapezoidal = 5, Irregular = 6
    }
    public enum ChoiceListEnum
    {
        Denomination = 1
    }
    public enum DateTypeEnum
    {
        Acquired = 1, Commissioning = 2, Guarantee = 3, Other
    } 
    public enum WorkStateEnum
    {
        WorkOrder = 1, Completed = 2, Verify = 4
    }
    public enum FileTypeEnum
    {
        Drawing, Quotation, WorkShopDrawing, DataSheets, Other
    }
    public enum InspectionTypeEnum
    {
        Scheduled, Spontaneous
    } 
    public enum InspectionClassEnum
    {
        A_Foundation, B_Casing, C_InteriorFinishing, D_OperatingSystem
    }
    public enum InspectionAffectationEnum
    {
        Internal, External
    }
    public enum WorkforceTypeEnum
    {
        Validator = 1, InspectionAgent = 2, WorkAgent = 4
    }
    public enum InspectionStateEnum
    {
        Missing, Defective, Harmful, Useless, Unaccessible, Improper, Nonfunctional, Satisfying, ExcededLifetime,LimitationToObserve, Other
    }
    public enum InspectionConditionEnum
    {
        Excellent, Good, Acceptable, Poor, Critical
    }
    public enum InspectionCauseEnum
    {
        OriginalCondition, EvolutionOfNeed, Undetermined, Preventive, NotApplicable, NormalWear, UnsuitableWear, Other
    }
    public enum InspectionImpactEnum
    {
        HealthSecurity, WaterTightness, Integrity, Upgrading, Functioning, Unnatural, EnergeticEfficiency, RepetitiveServiceCall, LogisticsAvailability, Maintenance, Other
    }
    public enum InspectionRiskEnum
    {
        High, Medium, Low, NA
    }
    public enum InspectionOperationEnum
    {
        Add, Replace, Repare, Paint,Expertise,Specifications, Recommandation, Other
    }
}
