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
    public class UniteRepository : IUniteRepository
    {
        /// <summary>
        ///  Context de connexion à la base de données
        /// </summary>
        private readonly IMeteoFranceDBContext _dBContext;

        public UniteRepository(IMeteoFranceDBContext meteoFranceDBContext) {
            _dBContext = meteoFranceDBContext;
        }

        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="unite">The unite.</param>
        /// <returns></returns>
        public async Task<Unite> CreateUniteAsync(Unite unite)
        {
           var elementAdded = await _dBContext.Unites.AddAsync(unite).ConfigureAwait(false);
           await _dBContext.SaveChangesAsync().ConfigureAwait(false);
           return elementAdded.Entity;
        }

        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="unite">The unite.</param>
        /// <returns></returns>
        public async Task<Unite> DeleteUniteAsync(Unite unite)
        {
            var elementDeleted =  _dBContext.Unites.Remove(unite);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementDeleted.Entity;
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité par identifiant.
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        public async Task<Unite> GetUniteByIdAsync(int id)
        {
            return await _dBContext.Unites.FirstOrDefaultAsync(unite => unite.IdUnite == id)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité par son nom.
        /// </summary>
        /// <param name="name">le nom de l'unité.</param>
        /// <returns></returns>
        public async Task<Unite> GetUniteByNameAsync(string name)
        {
            return await _dBContext.Unites.FirstOrDefaultAsync(unite => unite.Nom == name)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Cette méthode permet de récupérer la liste de toutes les unités.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Unite>> GetUnitesAsync()
        {
          return   await _dBContext.Unites.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Cette méthode permet de mettre une unité de mesure .
        /// </summary>
        /// <param name="unite">The unite.</param>
        /// <returns></returns>
        public async Task<Unite> UpdateUniteAsync(Unite unite)
        {
            var elementUpdated =  _dBContext.Unites.Update(unite);

            await _dBContext.SaveChangesAsync().ConfigureAwait(false);

            return elementUpdated.Entity;
        }
    }
}
