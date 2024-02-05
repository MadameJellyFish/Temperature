using Api.Temeprature.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Service.Contract
{
    public interface IUniteService
    {

        /// <summary>
        /// Cette méthode permet de récupérer les listes des unités de mesure.
        /// </summary>
        /// <returns></returns>
        Task<List<UniteDTO>> GetUniteMeasuresAsync();

        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="unite">L'unité à créer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà une unité de mesure du même nom !!</exception>
        Task<UniteDTO> CreateUniteMeasureAsync(UniteDTO unite);

        /// <summary>
        /// Cette méthode permet de mettre à jour une unité de mesure .
        /// </summary>
        /// <param name="idUnite">l'identifiant de unité</param>
        /// <param name="unite">l'unité modifié</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Il existe déjà une unité de mesure du même nom !!
        /// or
        /// Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}
        /// </exception>
        Task<UniteDTO> UpdateUniteMeasureAsync(int idUnite, UniteDTO unite);

        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="idUnite">L'identifiant de l'unité à supprimer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}</exception>
        Task<UniteDTO> DeleteUniteMeasureAsync(int idUnite);
    }
}
