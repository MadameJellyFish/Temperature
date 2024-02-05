using System;
using System.Collections.Generic;

namespace Api.Temperature.Data.Entity.Model
{
    public partial class Unite
    {
        public Unite()
        {
            Mesures = new HashSet<Mesure>();
        }

        public int IdUnite { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Mesure> Mesures { get; set; }
    }
}
