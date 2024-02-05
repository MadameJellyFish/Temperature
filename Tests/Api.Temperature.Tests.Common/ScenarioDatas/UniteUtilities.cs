using Api.Temperature.Data.Context.Contract;
using Api.Temperature.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temperature.Tests.Common.ScenarioDatas
{
    public static class UniteUtilities
    {
        public static void CreateUnite(this IMeteoFranceDBContext meteoFranceDBContext)
        {
            var unite = new Unite { IdUnite = 1, Nom = "CelsiusTest" };
            meteoFranceDBContext.Unites.Add(unite);
            meteoFranceDBContext.SaveChanges();
        }

        public static void CreateUnites(this IMeteoFranceDBContext meteoFranceDBContext)
        {
            var unite1 = new Unite { IdUnite = 1, Nom = "CelsiusTest" };
            var unite2 = new Unite { IdUnite = 2, Nom = "KelvinTest" };
            meteoFranceDBContext.Unites.AddRange(unite1, unite2);
            meteoFranceDBContext.SaveChanges();
        }
    }
}
