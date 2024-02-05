using System;
using System.Collections.Generic;

namespace Api.Temperature.Data.Entity.Model
{
    public partial class Temperature
    {
        public Temperature()
        {
            Mesures = new HashSet<Mesure>();
        }

        public int IdTemperature { get; set; }
        public DateTime DatePriseTemperature { get; set; }
        public int IdDepartement { get; set; }

        public virtual Departement IdDepartementNavigation { get; set; } = null!;
        public virtual ICollection<Mesure> Mesures { get; set; }
    }
}
