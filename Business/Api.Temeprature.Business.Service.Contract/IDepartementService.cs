using Api.Temeprature.Business.DTO.Departements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Service.Contract
{
    public interface IDepartementService
    {
        /// <summary>
        /// Creates the departement asynchronous.
        /// </summary>
        /// <param name="departementDto">The departement dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà un département du même nom !!</exception>
        Task<ReadDepartementDto> CreateDepartementAsync(CreateDepartementDto departementDto);
    }
}
