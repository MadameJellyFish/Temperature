using System;
using System.Collections.Generic;

namespace Api.Temperature.Data.Entity.Model
{
    public partial class Departement
    {
        public Departement()
        {
            Temperatures = new HashSet<Temperature>();
        }

        public int IdDepartement { get; set; }
        public string NomDepartement { get; set; } = null!;
        public string CodeDepartement { get; set; } = null!;

        public virtual ICollection<Temperature> Temperatures { get; set; }
    }
}
