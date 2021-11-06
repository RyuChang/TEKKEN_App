using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;

namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoveTextController : BaseCharacterController
    {
        private IMoveTextRepository _moveTextRepository;

        public MoveTextController(IStringLocalizer<SharedResource> sharedLocalizer,
            ICharacterRepository characterRepository,
            IMoveTextRepository moveTextRepository,
            ITranslateNameRepository translateNameRepository,
            IBaseCharacterRepository baseCharacterRepository) : base(sharedLocalizer, translateNameRepository, moveTextRepository, characterRepository)
        {
            _moveTextRepository = moveTextRepository;
            Initialize(TableName.MoveText);
            _moveTextRepository.SetTable(tableName);
        }


        [HttpGet]
        public IActionResult SelectMoveText(string stateType, int character_code)
        {
            ViewBag.stateType = stateType;
            //ViewBag.SelectMoveTexts = _moveTextRepository.GetMoveTextsByCharacterSelectItems(character_code);
            ViewBag.SelectMoveTexts = _moveTextRepository.GetSelectListByCharacter(character_code);
            return PartialView("_SelectMoveModalPartial");
        }
    }
}
