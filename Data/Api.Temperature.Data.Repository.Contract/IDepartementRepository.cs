using Api.Temperature.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temperature.Data.Repository.Contract
{
    public interface IDepartementRepository
    {
        /// <summary>
        /// Cette méthode permet de créer un département.
        /// </summary>
        /// <param name="departement">The departement.</param>
        /// <returns></returns>
        Task<Departement> CreateDepartementAsync(Departement departement);


        /// <summary>
        /// Cette méthode permet de récupérer les informations d'un departement par son nom.
        /// </summary>
        /// <param name="name">le nom du departement.</param>
        /// <returns></returns>
        Task<Departement> GetDepartementByNameAsync(string name);
    }
}
