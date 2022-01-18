using Microsoft.AspNetCore.Mvc;
using NewTekkenApp.Data;
using TekkenApp.Models;


namespace TEKKEN_WEB.areas.Admin.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        CommandService<Command, Command_name> CommandService { get; set; } = default!;
        //[Inject]
        //protected CommandService<Command, Command_name> CommandService { get; set; } = default!;

        public CommandsController(CommandService<Command, Command_name> _CommandService)
        {
            CommandService = _CommandService;

        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await CommandService.GetCommands());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
