using Api.Temeprature.Business.DTO.Departements;
using Api.Temeprature.Business.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Temperature.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementsController : ControllerBase
    {
        /// <summary>
        ///  Le service de gestion des departements
        /// </summary>
        private readonly IDepartementService _departementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartementsController"/> class.
        /// </summary>
        /// <param name="departementService">The departement service.</param>
        public DepartementsController(IDepartementService departementService)
        {
            _departementService = departementService;
        }

        // POST api/Departements

         [Authorize(Roles = "Admin, Clement")]
        [Authorize(Roles = "Axel")]

        [HttpPost]
        [ProducesResponseType(typeof(ReadDepartementDto), 200)]
        public async Task<ActionResult> CreateDepartementAsync([FromBody] CreateDepartementDto departementDto)
        {
            if (string.IsNullOrWhiteSpace(departementDto.Name))
            {
                return Problem("Echec : nous avons un nom de departement vide !!");
            }

            try
            {
                var departementAdded = await _departementService.CreateDepartementAsync(departementDto).ConfigureAwait(false);

                return Ok(departementAdded);
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