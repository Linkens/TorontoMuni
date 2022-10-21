using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public enum BuildCatEnum
    {
        Residential =1, Administrative = 2, Recreational = 4, Commercial = 8, Industrial = 16, Agricultural = 32, Hospital = 64, School = 128
    }
    public enum BuildStructEnum
    {
        Wood = 1, Metal = 2, Concrete = 4, Mixte_Wood_Concrete = 5, Mixte_Wood_Metal = 6, Autre = 8
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
        Acquired = 1, Commissioning = 2, Guarantee = 3 , Other
    }
}
