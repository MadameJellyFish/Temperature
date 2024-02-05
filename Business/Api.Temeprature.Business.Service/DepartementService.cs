using Api.Temeprature.Business.DTO;
using Api.Temeprature.Business.DTO.Departements;
using Api.Temeprature.Business.Mapper;
using Api.Temeprature.Business.Service.Contract;
using Api.Temperature.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Service
{
    public class DepartementService : IDepartementService
    {
        /// <summary>
        /// Le repository de gestion des departements
        /// </summary>
        private readonly IDepartementRepository _departementRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartementService"/> class.
        /// </summary>
        /// <param name="departementRepository">The departement repository.</param>
        public DepartementService(IDepartementRepository departementRepository)
        {
            _departementRepository = departementRepository;
        }

        /// <summary>
        /// Creates the departement asynchronous.
        /// </summary>
        /// <param name="departementDto">The departement dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà un département du même nom !!</exception>
        public async Task<ReadDepartementDto> CreateDepartementAsync(CreateDepartementDto departementDto)
        {
            var departement = await _departementRepository.GetDepartementByNameAsync(departementDto.Name)
                .ConfigureAwait(false);

            if (departement is not null)
                throw new Exception("Il existe déjà un département du même nom !!");

            var departementToAdd = DepartementMapper.TransformCreateDtoToEntity(departementDto);

            var departementAdded = await _departementRepository.CreateDepartementAsync(departementToAdd).ConfigureAwait(false);

            return DepartementMapper.TransformEntityToReadDto(departementAdded);
        }
    }
}
