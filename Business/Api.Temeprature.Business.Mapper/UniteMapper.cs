using Api.Temeprature.Business.DTO;
using Api.Temperature.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Mapper
{
    public static class UniteMapper
    {
        public static  Unite TransformDTOToEntity(UniteDTO uniteDTO)
        {
            return new Unite()
            {
                Nom = uniteDTO.MeasureUnite
            };
        }

        public static UniteDTO TransformEntityToDTO(Unite uniteEntity)
        {
            return new UniteDTO()
            {
                Id = uniteEntity.IdUnite,
                MeasureUnite = uniteEntity.Nom,
            };
        }
    }
}
