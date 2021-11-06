using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using User.Models;


namespace TEKKEN_WEB.Areas.User.Controllers
{
    [Area("User")]
    public class MoveListController : Controller
    {
        private IMoveListRepository _moveListRepository;

        public MoveListController(
            IStringLocalizer<SharedResource> sharedLocalizer,
                                IMoveListRepository moveListRepository)
        {
            _moveListRepository = moveListRepository;
            //_moveRepository = moveRepository;
            //Initialize(TableName.move);

        }


        [HttpGet]
        public IActionResult Index()
        {
            //ViewBag.tableName = tableName;
            //ViewBag.TitleDescription = TitleDescription;
            //ViewBag.AllList = _baseRepository.GetAllList(baseType, 0);
            //ViewBag.baseType = BaseType.Default.ToString();
            //ViewBag.AllList = _moveTypeRepository.GetAllMoveTypes();
            return View();
        }
        public JsonResult GetMoveList(int character_code=1)
        {
            return Json(_moveListRepository.GetAllMoveList(character_code));
        }
    }
}
