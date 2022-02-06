using Microsoft.AspNetCore.Mvc;
using NewTekkenApp.Data;
using TekkenApp.Models;


namespace TEKKEN_WEB.areas.Admin.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult> GetDepartments()
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
