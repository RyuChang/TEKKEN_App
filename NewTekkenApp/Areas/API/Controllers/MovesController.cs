using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TekkenApp.Data;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace TEKKEN_WEB.areas.Admin.Controllers
{
    //[Microsoft.AspNetCore.Mvc.Route("api/[controller]/{id?}")]
    [ApiController]
    [Area("API")]
    [Produces("application/json")]
    public class MovesController : ControllerBase
    {
        [Inject]
        IMoveService MoveService { get; set; } = default!;

        public MovesController(IMoveService _MoveService)
        {
            MoveService = _MoveService;
        }

        [HttpGet]
        [Route("api/[controller]/{id?}")]
        public async Task<ActionResult> GetEntityWithCommandsByCharacterIdAsync(int id)
        {
            try
            {
                return Ok(await MoveService.GetEntityWithCommandsByCharacterIdAsync(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
