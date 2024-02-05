using Api.Temeprature.Business.DTO;
using Api.Temeprature.Business.Service.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Temperature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitesController : ControllerBase
    {
        /// <summary>
        ///  Le service de gestion des unités de mesure
        /// </summary>
        private readonly IUniteService _uniteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitesController"/> class.
        /// </summary>
        /// <param name="uniteService">The unite service.</param>
        public UnitesController(IUniteService uniteService)
        {
            _uniteService = uniteService;
        }

        // GET api/Unites
        /// <summary>
        /// Ressource pour récupérer la liste des unités de mesure.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UniteDTO>), 200)]
        public async Task<ActionResult> GetUnitesAsync()
        {
            var unites = await _uniteService.GetUniteMeasuresAsync().ConfigureAwait(false);

            return Ok(unites);
        }

        // POST api/Unites
        /// <summary>
        /// Ressource pour créer une nouvelle unité de mesure.
        /// </summary>
        /// <param name="unite">les données de l'unité à créer</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UniteDTO), 200)]
        public async Task<ActionResult> CreateUniteAsync([FromBody] UniteDTO unite)
        {
            if(string.IsNullOrWhiteSpace(unite.MeasureUnite))
            {
                return Problem("Echec : nous avons un nom d'unité de mesure vide !!");
            }

            try
            {
                var uniteAdded = await _uniteService.CreateUniteMeasureAsync(unite).ConfigureAwait(false);

                return Ok(uniteAdded);
            }
            catch(Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        // PUT api/Unites/1
        /// <summary>
        /// Ressource pour mettre à jour une unité de mesure.
        /// </summary>
        /// <param name="id">L'identifiant de l'unité.</param>
        /// <param name="unite">les données modifiées.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UniteDTO), 200)]
        public async Task<ActionResult> UpdateUniteAsync(int id, [FromBody] UniteDTO unite)
        {
            if (string.IsNullOrWhiteSpace(unite.MeasureUnite))
            {
                return Problem("Echec : nous avons un nom d'unité de mesure vide !!");
            }

            try
            {
                var uniteUpdated = await _uniteService.UpdateUniteMeasureAsync(id,unite).ConfigureAwait(false);

                return Ok(uniteUpdated);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        // DELETE api/Unites/1
        /// <summary>
        /// Ressource pour supprimer une unité de mesure.
        /// </summary>
        /// <param name="id">L'identifiant de l'unité.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UniteDTO), 200)]
        public async Task<ActionResult> DeleteUniteAsync(int id)
        {
            try
            {
                var uniteDeleted = await _uniteService.DeleteUniteMeasureAsync(id).ConfigureAwait(false);

                return Ok(uniteDeleted);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

    }
}
