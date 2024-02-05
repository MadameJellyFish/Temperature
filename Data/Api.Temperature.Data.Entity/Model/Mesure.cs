using System;
using System.Collections.Generic;

namespace Api.Temperature.Data.Entity.Model
{
    public partial class Mesure
    {
        public int IdMesure { get; set; }
        public double Valeur { get; set; }
        public int IdUnite { get; set; }
        public int IdTemperature { get; set; }

        public virtual Temperature IdTemperatureNavigation { get; set; } = null!;
        public virtual Unite IdUniteNavigation { get; set; } = null!;
    }
}
