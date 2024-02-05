using Api.Temeprature.Business.DTO;
using Api.Temeprature.Business.DTO.Departements;
using Api.Temperature.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Mapper
{
    public static class DepartementMapper
    {
        public static Departement TransformCreateDtoToEntity(CreateDepartementDto departementDto)
        {
            return new Departement()
            {
                NomDepartement = departementDto.Name,
                CodeDepartement = departementDto.Code,
            };
        }

        public static ReadDepartementDto TransformEntityToReadDto(Departement departement)
        {
            return new ReadDepartementDto()
            {
                Id = departement.IdDepartement,
                Name = departement.NomDepartement,
                Code = departement.CodeDepartement
            };
        }
    }
}
