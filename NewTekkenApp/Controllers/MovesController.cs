using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NewTekkenApp.Data;
using TekkenApp.Models;


namespace TEKKEN_WEB.areas.Admin.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MovesController : ControllerBase
    {
        [Inject]
        MoveService<Move, Move_name> MoveService { get; set; } = default!;

        public MovesController(MoveService<Move, Move_name> _MoveService)
        {
            MoveService = _MoveService;
        }

        [HttpGet]
        public async Task<ActionResult> GetMovesByCharacterId(int character_id)
        {
            try
            {

                return Ok(await MoveService.GetEntitiesWithCharacterCode(character_id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
