using Api.Temperature.Data.Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temperature.Data.Context.Contract
{
    public interface IMeteoFranceDBContext : IDbContext
    {
        DbSet<Departement> Departements { get; set; }
        DbSet<Mesure> Mesures { get; set; }
        DbSet<Entity.Model.Temperature> Temperatures { get; set; }
        DbSet<Unite> Unites { get; set; }
    }
}
