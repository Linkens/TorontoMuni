using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoMuni.Data
{
    public enum BatCatEnum
    {
        Residentiel =1, Administratif = 2, Recreatif = 4, Commercial = 8, Industriel = 16, Agricole = 32, Hospitalier = 64, Scolaire = 128
    }
    public enum BatStructEnum
    {
        Bois = 1, Metal = 2, Beton = 4, MixteBB = 5, MixteBM = 6, Autre = 8
    }
    public enum BatFormEnum
    {
        Triangulaire = 1, Carré = 2, Rectangulaire = 4, Trapezoidale = 5, Irrégumière = 6
    }
    public enum ChoiceListEnum
    {
        Denomination = 1
    }
    public enum DateTypeEnum
    {
        Acquisition = 1,        MiseEnService = 2, Garantie = 3 , Autre
    }
}
