using Api.Temeprature.Business.DTO;
using Api.Temeprature.Business.Mapper;
using Api.Temeprature.Business.Service.Contract;
using Api.Temperature.Data.Entity.Model;
using Api.Temperature.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Service
{
    public class UniteService : IUniteService
    {
        /// <summary>
        /// Le repository de gestion des unités de mesures
        /// </summary>
        private readonly IUniteRepository _uniteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniteService"/> class.
        /// </summary>
        /// <param name="uniteRepository">The unite repository.</param>
        public UniteService(IUniteRepository uniteRepository)
        {
            _uniteRepository = uniteRepository;
        }

        /// <summary>
        /// Cette méthode permet de récupérer les listes des unités de mesure.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UniteDTO>> GetUniteMeasuresAsync()
        {
            var unites = await _uniteRepository.GetUnitesAsync().ConfigureAwait(false);
            List<UniteDTO> uniteDTOs = new (unites.Count);

            foreach (var unite in unites)
            {
                uniteDTOs.Add(UniteMapper.TransformEntityToDTO(unite));
            }

            return uniteDTOs;
        }

        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="unite">L'unité à créer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà une unité de mesure du même nom !!</exception>
        public async Task<UniteDTO> CreateUniteMeasureAsync(UniteDTO unite)
        {
            var isExiste = await CheckUniteNameExisteAsync(unite.MeasureUnite).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà une unité de mesure du même nom !!");

           var uniteToAdd =  UniteMapper.TransformDTOToEntity(unite);

           var uniteAdded = await _uniteRepository.CreateUniteAsync(uniteToAdd).ConfigureAwait(false);

           return UniteMapper.TransformEntityToDTO(uniteAdded);
        }

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
        public async Task<UniteDTO> UpdateUniteMeasureAsync(int idUnite, UniteDTO unite)
        {
            var isExiste = await CheckUniteNameExisteAsync(unite.MeasureUnite).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà une unité de mesure du même nom !!");

            var uniteGet = await _uniteRepository.GetUniteByIdAsync(idUnite).ConfigureAwait(false);
            if (uniteGet == null)
                throw new Exception($"Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}");

            uniteGet.Nom = unite.MeasureUnite;

            var uniteUpdated = await _uniteRepository.UpdateUniteAsync(uniteGet).ConfigureAwait(false);

            return UniteMapper.TransformEntityToDTO(uniteUpdated);
        }

        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="idUnite">L'identifiant de l'unité à supprimer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}</exception>
        public async Task<UniteDTO> DeleteUniteMeasureAsync(int idUnite)
        {
            var uniteGet = await _uniteRepository.GetUniteByIdAsync(idUnite).ConfigureAwait(false);
            if (uniteGet == null)
                throw new Exception($"Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}");

            var uniteDeleted = await _uniteRepository.DeleteUniteAsync(uniteGet).ConfigureAwait(false);

            return UniteMapper.TransformEntityToDTO(uniteDeleted);
        }

        /// <summary>
        /// Cette méthode permet de vérifier si une unité existe déjà avec le même nom.
        /// </summary>
        /// <param name="uniteName">le nom de l'unité.</param>
        private async Task<bool> CheckUniteNameExisteAsync(string uniteName)
        {
            var uniteGet = await _uniteRepository.GetUniteByNameAsync(uniteName).ConfigureAwait(false);

            return uniteGet != null;
        }
    }
}
