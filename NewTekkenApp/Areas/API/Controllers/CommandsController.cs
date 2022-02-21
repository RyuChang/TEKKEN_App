using Microsoft.AspNetCore.Mvc;
using TekkenApp.Data;


namespace TEKKEN_WEB.areas.Admin.Controllers
{
    //[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    //[ApiController]
    [Area("API")]
    public class CommandsController : ControllerBase
    {
        ICommandService CommandService { get; set; } = default!;
        //[Inject]
        //protected CommandService<Command, Command_name> CommandService { get; set; } = default!;

        public CommandsController(ICommandService _CommandService)
        {
            CommandService = _CommandService;

        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult> GetCommands()
        {
            try
            {
                return Ok(await CommandService.GetKeyMaps());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
