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
    public enum BuildCatEnum
    {
        Residential = 1, Administrative = 2, Recreational = 4, Commercial = 8, Industrial = 16, Agricultural = 32, Hospital = 64, School = 128
    }
    public enum BuildStructEnum
    {
        Wood = 1, Metal = 2, Concrete = 4, Mixte_Wood_Concrete = 5, Mixte_Wood_Metal = 6, Other = 8
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
    public enum BuildFileTypeEnum
    {
        Drawing, Quotation, WorkShopDrawing, DataSheets, Other
    }
}
