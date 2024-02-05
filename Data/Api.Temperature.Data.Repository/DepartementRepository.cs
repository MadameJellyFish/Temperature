using Api.Temperature.Data.Context.Contract;
using Api.Temperature.Data.Entity.Model;
using Api.Temperature.Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temperature.Data.Repository
{
    public class DepartementRepository : IDepartementRepository
    {
        /// <summary>
        ///  Context de connexion à la base de données
        /// </summary>
        private readonly IMeteoFranceDBContext _dBContext;

        public DepartementRepository(IMeteoFranceDBContext meteoFranceDBContext)
        {
            _dBContext = meteoFranceDBContext;
        }

        /// <summary>
        /// Cette méthode permet de créer un département.
        /// </summary>
        /// <param name="departement">The departement.</param>
        /// <returns></returns>
        public async Task<Departement> CreateDepartementAsync(Departement departement)
        {
            var elementAdded = await _dBContext.Departements.AddAsync(departement).ConfigureAwait(false);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementAdded.Entity;
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'un departement par son nom.
        /// </summary>
        /// <param name="name">le nom du departement.</param>
        /// <returns></returns>
        public async Task<Departement> GetDepartementByNameAsync(string name)
        {
            return await _dBContext.Departements.FirstOrDefaultAsync(departement => departement.NomDepartement == name)
                .ConfigureAwait(false);
        }
    }
}
