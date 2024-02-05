using Api.Temperature.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temperature.Data.Repository.Contract
{
    public interface IUniteRepository
    {
        /// <summary>
        /// Cette méthode permet de récupérer la liste de toutes les unités.
        /// </summary>
        /// <returns></returns>
        Task<List<Unite>> GetUnitesAsync();

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité par identifiant.
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        Task<Unite> GetUniteByIdAsync(int id);

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité par son nom.
        /// </summary>
        /// <param name="name">le nom de l'unité.</param>
        /// <returns></returns>
        Task<Unite> GetUniteByNameAsync(string name);

        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="unite">The unite.</param>
        /// <returns></returns>
        Task<Unite> CreateUniteAsync(Unite unite);

        /// <summary>
        /// Cette méthode permet de mettre une unité de mesure .
        /// </summary>
        /// <param name="unite">The unite.</param>
        /// <returns></returns>
        Task<Unite> UpdateUniteAsync(Unite unite);


        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="unite">The unite.</param>
        /// <returns></returns>
        Task<Unite> DeleteUniteAsync(Unite unite);
    }
}
